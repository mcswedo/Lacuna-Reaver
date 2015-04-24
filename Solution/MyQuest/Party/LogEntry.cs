
namespace MyQuest
{
    /// <summary>
    /// Represents an entry in the Party's conversation log
    /// </summary>
    public class LogEntry
    {
        string location;

        /// <summary>
        /// The map this dialog was delivered in
        /// </summary>
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        string speaker;

        /// <summary>
        /// The character who delivered the dialog
        /// </summary>
        public string Speaker
        {
            get { return speaker; }
            set { speaker = value; }
        }

        string dialog;

        public string Dialog
        {
            get { return dialog; }
            set { dialog = value; }
        }

        public LogEntry()
        {
        }

        public LogEntry(string location, string speaker, params string[] dialog)
        {
            this.location = location;
            this.speaker = speaker;
            foreach (string s in dialog)
            {
                this.dialog += s;
            }
        }
    }
}
