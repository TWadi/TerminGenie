import logging
from genie import AppointmentGenie

# Configure logging settings
logging.basicConfig(
    format='%(asctime)s\t%(levelname)s\t%(message)s',
    level=logging.INFO,
)

# Main execution block
if __name__ == "__main__":
    scheduler = AppointmentGenie()
    scheduler.continuously_run()
