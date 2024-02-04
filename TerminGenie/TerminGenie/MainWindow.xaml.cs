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
        public MainWindow()
        {
            InitializeComponent();
        }

        private string TunesienXpath;

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedValue = selectedItem.Content.ToString();
                if (selectedValue == "Tunesien")
                {
                    TunesienXpath = "/html/body/div[2]/div[2]/div[4]/div[2]/form/div[2]/div/div[2]/div[8]/div[2]/div[2]/div[1]/fieldset/div[8]/div[1]/div[1]/div[1]/div[8]/div/div[2]/div/div[5]/label";
                }
            }
        }

        // Mark the method as async and keep the return type as void
        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            string xpathValue = TunesienXpath;

            if (!string.IsNullOrEmpty(xpathValue))
            {
                string filePath = "output.xml";
                using (XmlWriter writer = XmlWriter.Create(filePath))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("root");
                    writer.WriteElementString("xpathValue", xpathValue);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                MessageBox.Show($"XPath saved to {filePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No XPath value available to save.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var scheduler = new AppointmentScheduler();
            await Task.Run(() => scheduler.ContinuouslyRun());
            // Note: The AppointmentScheduler's Run method should be compatible with async operations,
            // and if it's not designed to be awaited, you might not need 'await Task.Run()' but just 'scheduler.Run();' depending on its implementation.
        }
    }
}
