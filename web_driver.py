import logging
from selenium import webdriver
from selenium.webdriver.chrome.options import Options

class CustomWebDriver:
    def __init__(self):
        self._driver = None
        self._implicit_wait_time = 20

    def __enter__(self):
        logging.info("Open browser")
        options = Options()
        options.add_argument('--disable-blink-features=AutomationControlled')
        self._driver = webdriver.Chrome(options=options)
        self._driver.implicitly_wait(self._implicit_wait_time)
        self._driver.execute_script("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})")
        self._driver.execute_cdp_cmd('Network.setUserAgentOverride', {"userAgent": 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.53 Safari/537.36'})
        return self._driver

    def __exit__(self, exc_type, exc_value, exc_tb):
        logging.info("Close browser")
        if self._driver:
            self._driver.quit()
