import time
import logging
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import Select
from web_driver import CustomWebDriver

class AppointmentScheduler:
    """
    A class for automating the process of scheduling appointments on a specified website.
    """

    def __init__(self):
        self.wait_time = 20
        self.error_message = "Für die gewählte Dienstleistung sind aktuell keine Termine frei! Bitte"

    def visit_start_page(self, driver):
        """
        Navigates to the start page and initiates the appointment booking process.
        """
        logging.info("Visiting start page")
        driver.get("https://otv.verwalt-berlin.de/ams/TerminBuchen")
        start_button_xpath = '//*[@id="mainForm"]/div/div/div/div/div/div/div/div/div/div[1]/div[1]/div[2]/a'
        driver.find_element(By.XPATH, start_button_xpath).click()
        time.sleep(5)

    def agree_terms_and_conditions(self, driver):
        """
        Ticks off agreement checkboxes as part of the booking process.
        """
        logging.info("Agreeing to terms and conditions")
        agreement_checkbox_xpath = '//*[@id="xi-div-1"]/div[4]/label[2]/p'
        driver.find_element(By.XPATH, agreement_checkbox_xpath).click()
        time.sleep(1)
        proceed_button_id = 'applicationForm:managedForm:proceed'
        driver.find_element(By.ID, proceed_button_id).click()
        time.sleep(5)

    def fill_appointment_form(self, driver):
        """
        Fills out the appointment form with necessary details.
        """
        logging.info("Filling out appointment form")

        # Select country (example: Tunisia)
        country_select_id = 'xi-sel-400'
        country_select = Select(driver.find_element(By.ID, country_select_id))
        country_select.select_by_visible_text("Tunesien")
        time.sleep(5)

        # Confirm country selection
        selected_country = country_select.first_selected_option.text
        if selected_country == "Tunesien":
            # Number of persons
            person_select_id = 'xi-sel-422'
            person_select = Select(driver.find_element(By.ID, person_select_id))
            person_select.select_by_visible_text("eine Person")
            # Family option
            family_select_id = 'xi-sel-427'
            family_select = Select(driver.find_element(By.ID, family_select_id))
            family_select.select_by_visible_text("nein")
        time.sleep(2)

        # Extend stay
        extend_stay_xpath = '//*[@id="xi-div-30"]/div[2]/label/p'
        driver.find_element(By.XPATH, extend_stay_xpath).click()
        time.sleep(2)

        # Click on study group
        study_group_xpath = '/html/body/div[2]/div[2]/div[4]/div[2]/form/div[2]/div/div[2]/div[8]/div[2]/div[2]/div[1]/fieldset/div[8]/div[1]/div[1]/div[1]/div[8]/div/div[1]/label'
        driver.find_element(By.XPATH, study_group_xpath).click()
        time.sleep(2)

        # b/c of study
        study_reason_xpath = '/html/body/div[2]/div[2]/div[4]/div[2]/form/div[2]/div/div[2]/div[8]/div[2]/div[2]/div[1]/fieldset/div[8]/div[1]/div[1]/div[1]/div[8]/div/div[2]/div/div[5]/label'
        driver.find_element(By.XPATH, study_reason_xpath).click()
        time.sleep(4)

        # Submit form
        submit_button_id = 'applicationForm:managedForm:proceed'
        driver.find_element(By.ID, submit_button_id).click()
        time.sleep(10)

    def check_success_and_retry(self, driver):
        """
        Checks for successful form submission and retries if necessary.
        """
        for _ in range(10):
            if self.error_message not in driver.page_source:
                logging.info("!!!SUCCESS - do not close the window!!!")
                return
            logging.info("Retrying form submission")
            submit_button_id = 'applicationForm:managedForm:proceed'
            driver.find_element(By.ID, submit_button_id).click()
            time.sleep(self.wait_time)

    def run_once(self):
        """
        Executes the entire booking process once.
        """
        with CustomWebDriver() as driver:
            self.visit_start_page(driver)
            self.agree_terms_and_conditions(driver)
            self.fill_appointment_form(driver)
            self.check_success_and_retry(driver)

    def continuously_run(self):
        """
        Continuously runs the booking process until successful.
        """
        while True:
            logging.info("Starting a new attempt")
            self.run_once()
            time.sleep(self.wait_time)

# Example Usage
scheduler = AppointmentScheduler()
scheduler.continuously_run()
