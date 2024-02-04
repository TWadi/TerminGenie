using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Media;

namespace TerminGenie
{
    public class AppointmentScheduler
    {
        private int waitTime = 20000; // Milliseconds
        private string errorMessage = "Für die gewählte Dienstleistung sind aktuell keine Termine frei! Bitte";
        private string alarmSound = @"Alarm.wav"; 

        public void VisitStartPage(IWebDriver driver)
        {
            Console.WriteLine("Visiting start page");
            driver.Navigate().GoToUrl("https://otv.verwalt-berlin.de/ams/TerminBuchen");
            var startButtonXPath = "//*[@id='mainForm']/div/div/div/div/div/div/div/div/div/div[1]/div[1]/div[2]/a";
            driver.FindElement(By.XPath(startButtonXPath)).Click();
            Thread.Sleep(5000);
        }

        public void AgreeTermsAndConditions(IWebDriver driver)
        {
            Console.WriteLine("Agreeing to terms and conditions");
            var agreementCheckboxXPath = "//*[@id='xi-div-1']/div[4]/label[2]/p";
            driver.FindElement(By.XPath(agreementCheckboxXPath)).Click();
            Thread.Sleep(1000);
            var proceedButtonId = "applicationForm:managedForm:proceed";
            driver.FindElement(By.Id(proceedButtonId)).Click();
            Thread.Sleep(5000);
        }

        public void FillAppointmentForm(IWebDriver driver)
        {
            Console.WriteLine("Filling out appointment form");

            // Select country (example: Tunisia)
            var countrySelectId = "xi-sel-400";
            var countrySelect = new SelectElement(driver.FindElement(By.Id(countrySelectId)));
            countrySelect.SelectByText("Tunesien");
            Thread.Sleep(5000); 

            // Confirm country selection
            var selectedCountry = countrySelect.SelectedOption.Text;
            if (selectedCountry == "Tunesien")
            {
                // Number of persons
                var personSelectId = "xi-sel-422";
                var personSelect = new SelectElement(driver.FindElement(By.Id(personSelectId)));
                personSelect.SelectByText("eine Person");
                Thread.Sleep(2000); 

                // Family option
                var familySelectId = "xi-sel-427";
                var familySelect = new SelectElement(driver.FindElement(By.Id(familySelectId)));
                familySelect.SelectByText("nein");
                Thread.Sleep(2000);
            }

            // Extend stay
            var extendStayXPath = "//*[@id='xi-div-30']/div[2]/label/p";
            driver.FindElement(By.XPath(extendStayXPath)).Click();
            Thread.Sleep(2000); 

            // Click on study group
            var studyGroupXPath = "/html/body/div[2]/div[2]/div[4]/div[2]/form/div[2]/div/div[2]/div[8]/div[2]/div[2]/div[1]/fieldset/div[8]/div[1]/div[1]/div[1]/div[8]/div/div[1]/label";
            driver.FindElement(By.XPath(studyGroupXPath)).Click();
            Thread.Sleep(2000);

            // b/c of study
            var studyReasonXPath = "/html/body/div[2]/div[2]/div[4]/div[2]/form/div[2]/div/div[2]/div[8]/div[2]/div[2]/div[1]/fieldset/div[8]/div[1]/div[1]/div[1]/div[8]/div/div[2]/div/div[5]/label";
            driver.FindElement(By.XPath(studyReasonXPath)).Click();
            Thread.Sleep(4000); 

            // Submit form
            var submitButtonId = "applicationForm:managedForm:proceed";
            driver.FindElement(By.Id(submitButtonId)).Click();
            Thread.Sleep(10000); 
        }

        public void CheckSuccessAndRetry(IWebDriver driver)
        {
            for (int i = 0; i < 10; i++)
            {
                if (!driver.PageSource.Contains(errorMessage))
                {
                    Console.WriteLine("!!!SUCCESS - do not close the window!!!");
                    PlaySound(alarmSound);
                    Thread.Sleep(15000);
                    break;
                }
                Console.WriteLine("Retrying form submission");
                var submitButtonId = "applicationForm:managedForm:proceed";
                driver.FindElement(By.Id(submitButtonId)).Click();
                Thread.Sleep(waitTime);
            }
        }

        private void PlaySound(string filePath)
        {
            using (var player = new SoundPlayer(filePath))
            {
                player.PlaySync(); 
            }
        }

        public void RunOnce()
        {
            using (var driverWrapper = new TerminGenie.CustomWebDriver())
            {
                var driver = driverWrapper.Driver;
                VisitStartPage(driver);
                AgreeTermsAndConditions(driver);
                FillAppointmentForm(driver); 
                CheckSuccessAndRetry(driver);
            }
        }

        public void ContinuouslyRun()
        {
            while (true)
            {
                Console.WriteLine("Starting a new attempt");
                RunOnce();
                Thread.Sleep(waitTime);
            }
        }
    }

}
