// ***********************************************************************
// Assembly         : FolderSync
// Component        : Main.cs
// Created          : 05-31-2016
// 
// Version          : 1.0.0
// Last Modified On : 05-31-2016
// ***********************************************************************
// <copyright file="Main.cs" company="">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the Main class to run the FolderSync program
// </summary>
//
// Changelog: 
//            - 1.0.2 (05-31-2016) - Various fixes
//            - 1.0.1 (05-31-2016) - Adjusted timer to only restart after the directory copy is complete.
//            - 1.0.0 (05-31-2016) - Initial version created.
// ***********************************************************************
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FolderSync
{
    /// <summary>
    /// Class Main.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Main : Form
    {
        #region Private Fields

        /// <summary>
        /// The number of settings in a settings file
        /// </summary>
        private const int NeededSettings = 4;
        /// <summary>
        /// Flag to stop timers
        /// </summary>
        private bool stopTimers = false;
        /// <summary>
        /// The stop timers message
        /// </summary>
        private string stopTimersMessage = "";
        /// <summary>
        /// The time left text timer
        /// </summary>
        private EnhancedTimer timeLeftTimer;
        /// <summary>
        /// The directory copy timer
        /// </summary>
        private EnhancedTimer timer;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            InitializeComponent();

            if (LoadSettings())
                sync_btn_Click(null, null);
            else
                SetFormTitle($"{ExecutableName} - {Version} - {SaveFileName}");
        }

        #endregion Public Constructors

        #region Private Delegates

        /// <summary>
        /// Delegate GetOverrideTextCallback
        /// </summary>
        /// <returns>System.String.</returns>
        private delegate string GetOverrideTextCallback();

        /// <summary>
        /// Delegate SetTextCallback
        /// </summary>
        /// <param name="text">The text.</param>
        private delegate void SetTextCallback(string text);

        #endregion Private Delegates

        #region Private Properties

        /// <summary>
        /// Gets or sets the destination text.
        /// </summary>
        /// <value>The destination text.</value>
        private string DestinationText
        {
            get { return destination_txt.Text; }
            set { destination_txt.Text = value; }
        }

        /// <summary>
        /// Gets the name of the executable.
        /// </summary>
        /// <value>The name of the executable.</value>
        private string ExecutableName
        {
            get { return System.Diagnostics.Process.GetCurrentProcess().ProcessName; }
        }

        /// <summary>
        /// Gets or sets the interval in seconds.
        /// </summary>
        /// <value>The interval seconds.</value>
        private int IntervalSeconds
        {
            get
            {
                int output;
                if (int.TryParse(IntervalText, out output))
                    return output;

                IntervalText = "60";
                return 60;
            }
            set { IntervalText = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the interval text.
        /// </summary>
        /// <value>The interval text.</value>
        private string IntervalText
        {
            get { return interval_txt.Text; }
            set { interval_txt.Text = value; }
        }

        /// <summary>
        /// Gets or sets the override mode text.
        /// </summary>
        /// <value>The override mode text.</value>
        private string OverrideModeText
        {
            get { return overrideModes_cmb.Text; }
            set { overrideModes_cmb.Text = value; }
        }

        /// <summary>
        /// Gets the name of the save file.
        /// </summary>
        /// <value>The name of the save file.</value>
        private string SaveFileName
        {
            get { return $"{ExecutableName}.xml"; }
        }

        /// <summary>
        /// Gets or sets the source text.
        /// </summary>
        /// <value>The source text.</value>
        private string SourceText
        {
            get { return source_txt.Text; }
            set { source_txt.Text = value; }
        }

        /// <summary>
        /// Gets or sets the time left text.
        /// </summary>
        /// <value>The time left text.</value>
        private string TimeLeftText
        {
            get { return timeLeft_txt.Text; }
            set { timeLeft_txt.Text = value; }
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        private string Version
        {
            get { return Application.ProductVersion; }
        }

        #endregion Private Properties

        #region Private Methods

        /// <summary>
        /// Copies a source folder to the destination.
        /// </summary>
        /// <param name="sourcePath">Name of the source folder.</param>
        /// <param name="destinationPath">Name of the destination folder.</param>
        ///  Changelog:
        ///             - 1.0.0 (05-31-2016) - Initial version.
        private void CopyDirectory(string sourcePath, string destinationPath)
        {
            // verify source exists
            if (!Directory.Exists(sourcePath))
            {
                stopTimers = true;
                stopTimersMessage = "Could not find source folder!";
                return;
            }

            // create the destination folder (if necessary)
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);

            // copy the files in the current folder
            foreach (var file in Directory.EnumerateFiles(sourcePath))
            {
                string destination = Path.Combine(destinationPath, Path.GetFileName(file));

                // determine if we are allowed to override files
                bool canOverride = false;
                switch (GetOverrideText())
                {
                    case "Always":
                        canOverride = true;
                        break;

                    case "SourceNewer":
                        if (!File.Exists(destination))
                            canOverride = false;
                        else if (File.GetLastWriteTimeUtc(file) > File.GetLastWriteTimeUtc(destination))
                            canOverride = true;
                        break;

                    case "Never":
                    default:
                        canOverride = false;
                        break;
                }

                if (canOverride && File.Exists(destination))
                    return;
                File.Copy(file, destination, canOverride);
            }

            // copy the sub-dirs
            foreach (var directory in Directory.EnumerateDirectories(sourcePath))
            {
                string destination = Path.Combine(destinationPath, Path.GetFileName(directory));
                CopyDirectory(sourcePath, destination);
            }
        }

        /// <summary>
        /// Gets the override text.
        /// </summary>
        /// <returns>System.String.</returns>
        ///  Changelog:
        ///             - 1.0.0 (05-31-2016) - Initial version.
        private string GetOverrideText()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.timeLeft_txt.InvokeRequired)
            {
                try
                {
                    GetOverrideTextCallback d = new GetOverrideTextCallback(GetOverrideText);
                    return Invoke(d).ToString();
                }
                catch { }

                return "";
            }
            else
            {
                return overrideModes_cmb.Text;
            }
        }

        /// <summary>
        /// Loads the settings file (if it exists).
        /// </summary>
        /// <returns><c>true</c> if load was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (05-31-2016) - Initial version.
        private bool LoadSettings()
        {
            // if we have a settings file...
            if (File.Exists(SaveFileName))
            {
                try
                {
                    // load the settings file...
                    var doc = XDocument.Load(SaveFileName);
                    var settingCount = 0;

                    // traverse each node...
                    foreach (var setting in doc.Root.Elements())
                    {
                        // if the value is empty, skip this node...
                        if (string.IsNullOrEmpty(setting.Value))
                            continue;

                        // depending on the name of the node, assign the value to a different setting...
                        switch (setting.Name.ToString())
                        {
                            case "source":
                                SourceText = setting.Value;
                                settingCount++;
                                break;

                            case "destination":
                                DestinationText = setting.Value;
                                settingCount++;
                                break;

                            case "interval":
                                IntervalText = setting.Value;
                                settingCount++;
                                break;

                            case "override":
                                OverrideModeText = setting.Value;
                                settingCount++;
                                break;
                        }
                    }

                    // return if we have enough settings in the file or not
                    return settingCount == NeededSettings;
                }
                catch { }
            }

            return false;
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        ///  Changelog:
        ///             - 1.0.0 (05-31-2016) - Initial version.
        private void SaveSettings()
        {
            // create the settings save data
            StringBuilder saveData = new StringBuilder();

            saveData.AppendLine("<settings>");
            saveData.AppendLine($"<source>{SourceText}</source>");
            saveData.AppendLine($"<destination>{DestinationText}</destination>");
            saveData.AppendLine($"<interval>{IntervalText}</interval>");
            saveData.AppendLine($"<override>{OverrideModeText}</override>");
            saveData.AppendLine("</settings>");

            // save it
            var doc = XDocument.Parse(saveData.ToString());
            doc.Save(SaveFileName);

            // adjust the title of the form
            SetFormTitle($"{ExecutableName} - {Version} - {SaveFileName}");
        }

        /// <summary>
        /// Sets the form title.
        /// </summary>
        /// <param name="title">The title.</param>
        ///  Changelog:
        ///             - 1.0.0 (05-31-2016) - Initial version.
        private void SetFormTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Sets the time left text.
        /// </summary>
        /// <param name="text">The text.</param>
        ///  Changelog:
        ///             - 1.0.0 (05-31-2016) - Initial version.
        private void SetTimeLeftText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.timeLeft_txt.InvokeRequired)
            {
                try
                {
                    SetTextCallback d = new SetTextCallback(SetTimeLeftText);
                    Invoke(d, new object[] { text });
                }
                catch { }
            }
            else
            {
                timeLeft_txt.Text = text;
            }
        }

        /// <summary>
        /// Synchronizes the folders.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        ///  Changelog:
        ///             - 1.0.1 (05-31-2016) - Added timer restart code.
        ///             - 1.0.0 (05-31-2016) - Initial version.
        private void Sync(object sender, System.Timers.ElapsedEventArgs e)
        {
            CopyDirectory(SourceText, DestinationText);

            timer.Start();
        }

        /// <summary>
        /// Handles the Click event of the sync_btn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        ///  Changelog:
        ///             - 1.0.2 (05-31-2016) - Various fixes.
        ///             - 1.0.1 (05-31-2016) - Made the timer no longer auto-reset.
        ///             - 1.0.0 (05-31-2016) - Initial version.
        private void sync_btn_Click(object sender, EventArgs e)
        {
            // dispose of old timers
            if (timer != null)
            {
                try
                {
                    timer.Stop();
                    timer.Dispose();
                }
                catch { }
            }
            if (timeLeftTimer != null)
            {
                try
                {
                    timeLeftTimer.Stop();
                    timeLeftTimer.Dispose();
                }
                catch { }
            }

            // save the settings
            SaveSettings();

            // create new timers
            timer = new EnhancedTimer();
            timer.AutoReset = false;
            timer.Interval = IntervalSeconds * 1000;
            timer.Elapsed += Sync;

            timeLeftTimer = new EnhancedTimer();
            timeLeftTimer.AutoReset = true;
            timeLeftTimer.Interval = 5;
            timeLeftTimer.Elapsed += TimeLeft;

            // start the new timers
            timer.Start();
            timeLeftTimer.Start();
        }

        /// <summary>
        /// Times the left.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        ///  Changelog:
        ///             - 1.0.0 (05-31-2016) - Initial version.
        private void TimeLeft(object sender, System.Timers.ElapsedEventArgs e)
        {
            SetTimeLeftText($"Time Left: {Math.Round(timer.TimeLeft / 1000, 3)}");
        }

        #endregion Private Methods
    }
}