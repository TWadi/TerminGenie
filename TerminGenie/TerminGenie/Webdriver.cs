using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TerminGenie
{
    public class CustomWebDriver : IDisposable
    {
        private IWebDriver _driver;
        private readonly TimeSpan _implicitWaitTime = TimeSpan.FromSeconds(20);

        public CustomWebDriver()
        {
            InitializeDriver();
        }

        private void InitializeDriver()
        {
            var options = new ChromeOptions();
            // Mimic browser behavior to avoid detection
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            // Optionally, specify the ChromeDriver directory if it's not in your PATH
            // var driverService = ChromeDriverService.CreateDefaultService("path/to/your/chromedriver");
            // _driver = new ChromeDriver(driverService, options);

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = _implicitWaitTime;

            // Use JavaScript to set the navigator.webdriver flag to undefined
            ((IJavaScriptExecutor)_driver).ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");
        }

        public IWebDriver Driver => _driver;

        public void Dispose()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}
