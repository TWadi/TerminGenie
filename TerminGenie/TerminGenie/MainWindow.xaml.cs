using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Threading.Tasks; // Make sure to include this for async programming

namespace TerminGenie
{
    public partial class MainWindow : Window
    {
        private SchedulerConfig schedulerConfig;

        public MainWindow()
        {
            InitializeComponent();
            schedulerConfig = new SchedulerConfig
            {
                StartPageUrl = "https://otv.verwalt-berlin.de/ams/TerminBuchen",
                StartButtonXPath = "//*[@id='mainForm']/div/div/div/div/div/div/div/div/div/div[1]/div[1]/div[2]/a",
                AgreementCheckboxXPath = "//*[@id='xi-div-1']/div[4]/label[2]/p", // Updated to match the XPath used in AgreeTermsAndConditions method
                ProceedButtonId = "applicationForm:managedForm:proceed", // Correct, matches the ID used in multiple methods
                CountrySelectId = "xi-sel-400", // Correct, used in FillAppointmentForm method for country selection
                CountryName = "",
                PersonSelectId = "xi-sel-422", // Updated to match the ID used in FillAppointmentForm method for number of persons selection
                NumberOfPersons = "eine Person", // Updated to match the option used in FillAppointmentForm method
                FamilySelectId = "xi-sel-427", // Updated to match the ID used in FillAppointmentForm method for family selection
                FamilyOption = "nein", // Updated to match the option used in FillAppointmentForm method
                ExtendStayXPath = "//*[@id='xi-div-30']/div[2]/label/p", // Updated based on the XPath used in FillAppointmentForm method for extend stay option
                StudyGroupXPath = "", // Updated to match the XPath used in FillAppointmentForm method for study group option
                StudyReasonXPath = "/html/body/div[2]/div[2]/div[4]/div[2]/form/div[2]/div/div[2]/div[8]/div[2]/div[2]/div[1]/fieldset/div[8]/div[1]/div[1]/div[1]/div[8]/div/div[2]/div/div[5]/label", // Updated to match the XPath used in FillAppointmentForm method for study reason option
                SubmitButtonId = "applicationForm:managedForm:proceed" // Correct, matches the ID used in multiple methods
            };


        }

        private async void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            // Check if the CountryName is not set or empty
            if (string.IsNullOrEmpty(schedulerConfig.CountryName))
            {
                MessageBox.Show("Please select a country before starting.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Stop further execution if no country is selected
            }

            var scheduler = new AppointmentScheduler(schedulerConfig);
            await Task.Run(() => scheduler.ContinuouslyRun());
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null || schedulerConfig == null) return;

            // Extracting the Content property of the selected ComboBoxItem
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                var countryName = selectedItem.Content.ToString();

                switch (countryName)
                {
                    case "Tunesien":
                        schedulerConfig.CountryName = "Tunesien";
                        schedulerConfig.StudyGroupXPath = "/html/body/div[2]/div[2]/div[4]/div[2]/form/div[2]/div/div[2]/div[8]/div[2]/div[2]/div[1]/fieldset/div[8]/div[1]/div[1]/div[1]/div[8]/div/div[1]/label";
                        schedulerConfig.StudyReasonXPath = "//*[@id=\"SERVICEWAHL_DE285-0-2-3-305244\"]";
                        break;

                    case "Indien":
                        schedulerConfig.CountryName = "Indien";
                        schedulerConfig.StudyGroupXPath = "/html/body/div[2]/div[2]/div[4]/div[2]/form/div[2]/div/div[2]/div[8]/div[2]/div[2]/div[1]/fieldset/div[8]/div[1]/div[1]/div[1]/div[9]/div/div[1]/label";
                        schedulerConfig.StudyReasonXPath = "//*[@id=\"SERVICEWAHL_DE436-0-2-3-305244\"]"; 
                        break;

                    case "Soudan":
                        schedulerConfig.CountryName = "Sudan";
                        schedulerConfig.StudyGroupXPath = "/html/body/div[2]/div[2]/div[4]/div[2]/form/div[2]/div/div[2]/div[8]/div[2]/div[2]/div[1]/fieldset/div[8]/div[1]/div[1]/div[1]/div[8]/div/div[1]/label";
                        schedulerConfig.StudyReasonXPath = "//*[@id=\"SERVICEWAHL_DE276-0-2-3-305244\"]";
                        break;

                    // Add more cases as needed

                    default:
                        // Optional: Handle unknown selection or reset to default values
                        break;
                }
            }
        }


    }

}
