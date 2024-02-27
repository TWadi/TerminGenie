using System;

namespace TerminGenie
{
    public class SchedulerConfig
    {
        public int delay { get; set; }
        public string StartPageUrl { get; set; }
        public string StartButtonXPath { get; set; }
        public string AgreementCheckboxXPath { get; set; }
        public string ProceedButtonId { get; set; }

        public string CountrySelectId { get; set; }
        public string CountryName { get; set; }
        public string PersonSelectId { get; set; }
        public string NumberOfPersons { get; set; }
        public string FamilySelectId { get; set; }
        public string FamilyOption { get; set; }
        public string ExtendStayXPath { get; set; }
        public string StudyGroupXPath { get; set; }
        public string StudyReasonXPath { get; set; }
        public string SubmitButtonId { get; set; }
        public bool test_mode  { get; set; }

        // Additional properties for error handling, messages, and sounds
        public string ErrorMessage { get; set; }
        public string AlarmSoundPath { get; set; }
        public int WaitTime { get; set; } = 20000; // Default wait time in milliseconds

        // Constructor
        public SchedulerConfig()
        {
            // Initialize default values if needed
            StartPageUrl = "https://otv.verwalt-berlin.de/ams/TerminBuchen";
            ErrorMessage = "Für die gewählte Dienstleistung sind aktuell keine Termine frei! Bitte";
            AlarmSoundPath = @"Alarm.wav";
        }
        // You might want to add methods here for loading configurations from a file or database
        // This way, you can change the behavior of your automation without recompiling the application
    }
}
