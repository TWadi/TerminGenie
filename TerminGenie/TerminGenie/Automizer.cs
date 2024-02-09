using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Media;
using System.Diagnostics;

namespace TerminGenie
{
    public class AppointmentScheduler
    {
        private SchedulerConfig config;
        private int waitTime = 20000; // Milliseconds
        private string errorMessage = "Dieses Feld ist ein Pflichtfeld";
        private string alarmSound = @"Alarm.wav";

        public AppointmentScheduler(SchedulerConfig schedulerConfig)
        {
            config = schedulerConfig;
        }

        public void VisitStartPage(IWebDriver driver)
        {
            Console.WriteLine("Visiting start page");
            driver.Navigate().GoToUrl(config.StartPageUrl);
            driver.FindElement(By.XPath(config.StartButtonXPath)).Click();
            Thread.Sleep(5000);
        }

        public void AgreeTermsAndConditions(IWebDriver driver)
        {
            Console.WriteLine("Agreeing to terms and conditions");
            driver.FindElement(By.XPath(config.AgreementCheckboxXPath)).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"applicationForm:managedForm:proceed\"]")).Click();
            Thread.Sleep(5000);
        }

        public void FillAppointmentForm(IWebDriver driver)
        {
            Console.WriteLine("Filling out appointment form");

            // Select country
            var countrySelect = new SelectElement(driver.FindElement(By.Id(config.CountrySelectId)));
            countrySelect.SelectByText(config.CountryName);
            // Confirm country selection
            var selectedCountry = countrySelect.SelectedOption.Text;
            if (selectedCountry == config.CountryName)
            {
                // Number of persons
                var personSelect = new SelectElement(driver.FindElement(By.Id(config.PersonSelectId)));
                personSelect.SelectByText(config.NumberOfPersons);
                // Family option
                var familySelect = new SelectElement(driver.FindElement(By.Id(config.FamilySelectId)));
                familySelect.SelectByText(config.FamilyOption);
                Thread.Sleep(5000);
            }

            // Extend stay
            driver.FindElement(By.XPath(config.ExtendStayXPath)).Click();
            Thread.Sleep(2000);

            // Click on study group
            driver.FindElement(By.XPath(config.StudyGroupXPath)).Click();
            Thread.Sleep(2000);

            // b/c of study
            driver.FindElement(By.XPath(config.StudyReasonXPath)).Click();
            Thread.Sleep(5000);

            // Submit form
            driver.FindElement(By.Id("applicationForm:managedForm:proceed")).Click();
            Thread.Sleep(5000);
        }

        public void CheckSuccessAndRetry(IWebDriver driver)
{
    bool success = false;
    for (int i = 0; i < 10; i++)
    {
        if (driver.PageSource.Contains("Bitte wählen Sie einen Tag"))
        {
            Console.WriteLine("!!!SUCCESS - do not close the window!!!");
            success = true;
            break; // Break out of the loop if success is detected
        }
        Console.WriteLine("Retrying form submission");
                driver.FindElement(By.Id("applicationForm:managedForm:proceed")).Click();
                Thread.Sleep(waitTime);
    }

    if (success)
    {
        // Continue to play the sound every 15 seconds to alert the user.
        // This loop will run indefinitely until the user manually closes the program.
        while (true)
        {
            PlaySound(alarmSound);
            Thread.Sleep(15000); // Wait for 15 seconds before playing the sound again.
        }
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
            using (var driverWrapper = new CustomWebDriver())
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



