using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using ImageByDate.Entities;
using System.Collections.Generic;

namespace ImageByDate
{
    public partial class frmImagesByDate : Form
    {
        private string sourceDirectory = String.Empty;
        private string targetDirectory = String.Empty;
        private MoveBgWorker totalBgWorker = new MoveBgWorker();
        private ICollection<SourceFile> sourceFiles;

        public frmImagesByDate()
        {
            InitializeComponent();

            totalBgWorker.DoWork += new DoWorkEventHandler(totalBgWorker_DoWork);
            totalBgWorker.ProgressChanged += new ProgressChangedEventHandler(totalBgWorker_ProgressChanged);
            totalBgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(totalBgWorker_RunWorkerCompleted);

            this.FormClosing += new FormClosingEventHandler(frmImagesByDate_FormClosing);

            InitProgressMeters(0);
        }

        void frmImagesByDate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (totalBgWorker.IsBusy)
            {
                var result = MessageBox.Show("Are you sure you want to cancel processing?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.OK)
                {
                    totalBgWorker.CancelAsync();
                    CancelProcessing();
                    e.Cancel = false;
                    return;
                }
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to quit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    e.Cancel = false;
                    return;
                }
            }
            e.Cancel = true;
        }

        private void StartProcessing()
        {
            btnClose.Text = "Cancel";
            btnStart.Enabled = false;
            grpSettings.Enabled = false;
            btnReset.Enabled = false;
        }
        private void CancelProcessing()
        {
            grpSettings.Enabled = true;
            btnStart.Enabled = true;
            btnClose.Text = "Close";
        }

        private void btnSourceDirectory_Click(object sender, EventArgs e)
        {
            var result = fbdSourceDirectory.ShowDialog(this);

            if (result == DialogResult.Cancel)
            {
                sourceDirectory = String.Empty;
            }
            else if (result == DialogResult.OK)
            {
                sourceDirectory = fbdSourceDirectory.SelectedPath;
                txtSourceDirectory.Text = sourceDirectory;
                fbdSourceDirectory.SelectedPath = sourceDirectory;
                txtTargetDirectory.Text = sourceDirectory;
                fbdTargetDirectory.SelectedPath = sourceDirectory;

                if (String.IsNullOrEmpty(sourceDirectory))
                {
                    MessageBox.Show("Error while selecting directory", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }

                GetFileListInSourceDirectory();
                InitProgressMeters(sourceFiles.Count);
            }
            else
            {
                MessageBox.Show("Error while selecting directory", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnTargetDirectory_Click(object sender, EventArgs e)
        {
            var result = fbdTargetDirectory.ShowDialog(this);

            if (result == DialogResult.Cancel)
            {
                targetDirectory = String.Empty;
            }
            else if (result == DialogResult.OK)
            {
                targetDirectory = fbdTargetDirectory.SelectedPath;
                txtTargetDirectory.Text = targetDirectory;

                if (String.IsNullOrEmpty(targetDirectory))
                {
                    MessageBox.Show("Error while selecting directory", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("Error while selecting directory", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (totalBgWorker.IsBusy)
            {
                var result = MessageBox.Show("Are you sure you want to cancel processing?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.OK)
                {
                    CancelProcessing();
                }
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to quit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!CheckSourcePath()) return;
            if (!CheckTargetPath()) return;

            var message = "Are you sure you want to " + (rdbMoveFiles.Checked ? "move" : "copy") + " all files from " + sourceDirectory + " to a new folder structure in " + targetDirectory + "?";

            message = AddNamebaseMessage(message);

            var result = MessageBox.Show(message, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                StartProcessing();

                InitProgressMeters(0);
                totalBgWorker.Reset();
                totalBgWorker.RunWorkerAsync();
            }
        }

        private string AddNamebaseMessage(string message)
        {
            var newMessage = message;
            if (rbnNamebaseFilename.Checked
                && txtBasenameRegexp.TextLength > 0
                && !string.IsNullOrWhiteSpace(txtBasenameRegexp.Text))
            {
                GetFileListInSourceDirectory();
                var firstFile = sourceFiles != null || sourceFiles.Count > 0
                    ? sourceFiles.First()
                    : null;
                if (firstFile != null && !string.IsNullOrEmpty(firstFile.FilenameWithoutExtension))
                {
                    newMessage += "\n\n";
                    newMessage += "Date will be based on the following format, is this correct?\n\n";

                    var regex = new Regex(txtBasenameRegexp.Text);
                    var groupNames = regex.GetGroupNames();

                    string year = string.Empty, month = string.Empty, day = string.Empty;
                    if (DateTime.TryParse(firstFile.FilenameWithoutExtension.Replace('.', ':'), out DateTime date))
                    {
                        year = date.Year.ToString();
                        month = date.Month.ToString().PadLeft(2, '0');
                        day = date.Day.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        var regexMatch = regex.Match(firstFile.FilenameWithoutExtension);
                        year = regexMatch.Groups["year"].Value;
                        month = regexMatch.Groups["month"].Value;
                        day = regexMatch.Groups["day"].Value;
                    }
                    newMessage += sourceDirectory + "\\" + firstFile.Filename + " -->\n";
                    newMessage += targetDirectory + "\\" + year + "\\" + year + month + day + "\\" + firstFile.Filename;
                }
            }
            return newMessage;
        }

        void totalBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CancelProcessing();

            var result = totalBgWorker.Report;
            UpdateTotalProgressMeter(result);

            MessageBox.Show("Process completed:\n\n"
                + result.Maximum + " items processed\n"
                + result.Succeded + " items succeded\n"
                + result.Failed + " items failed\n"
                + "(" + result.Exists + " already exist)"
                , this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            btnReset.Enabled = true;
        }

        void totalBgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is String)
            {
                AddListItem((String)e.UserState);
            }
            else if (e.UserState is ProgressReport)
            {
                var result = totalBgWorker.Report;
                UpdateTotalProgressMeter(result);
            }
        }

        void totalBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GetFileListInSourceDirectory();
            if (sourceFiles.Count == 0)
            {
                return;
            }
            totalBgWorker.Report.Maximum = sourceFiles.Count;
            totalBgWorker.ReportProgress();

            if (!CheckTargetPath()) return;

            for (int i = 0; i < sourceFiles.Count; i++)
            {
                var currentSourceFile = sourceFiles.ElementAt(i);

                int year = 0;
                int month = 0;
                int day = 0;
                if (rbnNamebaseFilename.Checked)
                {
                    var filenameDate = GetFilenameDate(currentSourceFile.FilenameWithoutExtension);
                    year = filenameDate.Year;
                    month = filenameDate.Month;
                    day = filenameDate.Day;
                }
                else
                {
                    //Get each file's modified date
                    var modDate = File.GetLastWriteTime(currentSourceFile.FullPath);
                    year = modDate.Year;
                    month = modDate.Month;
                    day = modDate.Day;
                }

                string targetPath = targetDirectory;

                #region Create year directory
                if (chkCreateYearFolder.Checked)
                {
                    targetPath = CreateYearDirectory(year, targetPath);
                    if (string.IsNullOrEmpty(targetPath)) return;
                }
                #endregion

                #region Create month directory
                if (chkCreateMonthFolder.Checked)
                {
                    targetPath = CreateMonthDirectory(month, targetPath);
                    if (string.IsNullOrEmpty(targetPath)) return;
                }
                #endregion

                #region Create day directory
                if (chkCreateDayFolder.Checked)
                {
                    targetPath = CreateDayDirectory(year, month, day, targetPath);
                    if (string.IsNullOrEmpty(targetPath)) return;
                }
                #endregion

                #region Move/copy file
                //Check if file already exists
                //If not, copy/Move each file to target folder
                //If too, check with user
                targetPath = Path.Combine(targetPath, currentSourceFile.Filename);

                totalBgWorker.Report.Value = (i + 1);
                if (File.Exists(targetPath))
                {
                    totalBgWorker.ReportProgress($"Target path '{targetPath}' already exists.");
                    totalBgWorker.Report.Exists++;
                }
                else
                {
                    MoveOrCopyFile(currentSourceFile.FullPath, targetPath);
                }
                totalBgWorker.ReportProgress();
                #endregion
            }
        }

        private DateTime GetFilenameDate(string filenameWithoutExtension)
        {
            throw new NotImplementedException();
        }

        private void MoveOrCopyFile(string sourcePath, string targetPath)
        {
            string fileMessage = String.Empty;
            try
            {
                if (rdbMoveFiles.Checked)
                {
                    File.Move(sourcePath, targetPath);
                }
                else if (rdbCopyFiles.Checked)
                {
                    File.Copy(sourcePath, targetPath);
                }

                totalBgWorker.ReportProgress(
                    (rdbMoveFiles.Checked ? "Moved " : string.Empty)
                    + (rdbCopyFiles.Checked ? "Copied " : string.Empty)
                    + $"'{sourcePath}' to '{targetPath}'");
                totalBgWorker.Report.Succeded++;
            }
            catch (UnauthorizedAccessException ex)
            {
                //     The caller does not have the required permission.
                fileMessage = "Unauthorized to access target directory.";
                totalBgWorker.Report.Failed++;
            }
            catch (ArgumentNullException ex)
            {
                //     sourceFileName or destFileName is null.
                fileMessage = "Source or target paths cannot be empty.";
                totalBgWorker.Report.Failed++;
            }
            catch (ArgumentException ex)
            {
                //     sourceFileName or destFileName is a zero-length string, contains only white
                //     space, or contains invalid characters as defined in System.IO.Path.InvalidPathChars.
                fileMessage = "Source or target paths cannot be empty.";
                totalBgWorker.Report.Failed++;
            }
            catch (PathTooLongException ex)
            {
                //     The specified path, file name, or both exceed the system-defined maximum
                //     length. For example, on Windows-based platforms, paths must be less than
                //     248 characters and file names must be less than 260 characters.
                fileMessage = "Target path is too long.";
                totalBgWorker.Report.Failed++;
            }
            catch (DirectoryNotFoundException ex)
            {
                //     The path specified in sourceFileName or destFileName is invalid, (for example,
                //     it is on an unmapped drive).
                fileMessage = "Source of target paths are not found. Are they on an unmapped drive?";
                totalBgWorker.Report.Failed++;
            }
            catch (FileNotFoundException ex)
            {
                //     sourceFileName was not found.
                fileMessage = "Source file name \"" + sourcePath + "\" is not found.";
                totalBgWorker.Report.Failed++;
            }
            catch (NotSupportedException ex)
            {
                //     sourceFileName or destFileName is in an invalid format.
                fileMessage = "Source or target paths contains illegal characters.";
                totalBgWorker.Report.Failed++;
            }
            catch (IOException ex)
            {
                //     The destination file already exists.
                fileMessage = "Target path already exists.";
                totalBgWorker.Report.Exists++;
            }
            catch (Exception ex)
            {
                fileMessage = "An unknown error occured. " + ex.Message;
                totalBgWorker.Report.Failed++;
            }
            if (!string.IsNullOrEmpty(fileMessage))
            {
                totalBgWorker.ReportProgress(fileMessage);
            }
        }

        private string CreateDayDirectory(int year, int month, int day, string targetPath)
        {
            //Check if date month folder exists
            //If not, create
            //If too, use

            string dirMessage = String.Empty;
            try
            {
                if (chkIncludeFullDateInDayFolderName.Checked)
                {
                    targetPath = Path.Combine(targetPath,
                        year.ToString().PadLeft(4, '0')
                        + month.ToString().PadLeft(2, '0')
                        + day.ToString().PadLeft(2, '0'));
                }
                else
                {
                    targetPath = Path.Combine(targetPath, "" + day);
                }

                if (!Directory.Exists(targetPath))
                {
                    var dirInfo = Directory.CreateDirectory(targetPath);

                    totalBgWorker.ReportProgress("Added day directory \"" + targetPath + "\"");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                //     The caller does not have the required permission.
                dirMessage = "Unauthorized to access target directory.";
            }
            catch (ArgumentNullException ex)
            {
                //     path is null.
                dirMessage = "Target path cannot be empty.";
            }
            catch (ArgumentException ex)
            {
                //     path is a zero-length string, contains only white space, or contains one
                //     or more invalid characters as defined by System.IO.Path.InvalidPathChars.-or-path
                //     is prefixed with, or contains only a colon character (:).
                dirMessage = "Target path cannot be empty.";
            }
            catch (PathTooLongException ex)
            {
                //     The specified path, file name, or both exceed the system-defined maximum
                //     length. For example, on Windows-based platforms, paths must be less than
                //     248 characters and file names must be less than 260 characters.
                dirMessage = "Target path is too long.";
            }
            catch (DirectoryNotFoundException ex)
            {
                //     The specified path is invalid (for example, it is on an unmapped drive).
                dirMessage = "Target path is not found. Is it on an unmapped drive?";
            }
            catch (NotSupportedException ex)
            {
                //     path contains a colon character (:) that is not part of a drive label ("C:\").
                dirMessage = "Target path contains illegal characters.";
            }
            catch (IOException ex)
            {
                //     The directory specified by path is read-only.
                dirMessage = "Target path is read-only.";
            }
            catch (Exception ex)
            {
                dirMessage = "An unknown error occured. " + ex.Message;
            }
            if (!String.IsNullOrEmpty(dirMessage))
            {
                MessageBox.Show(dirMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return string.Empty;
            }

            return targetPath;
        }

        private string CreateMonthDirectory(int month, string targetPath)
        {
            //Check if date month folder exists
            //If not, create
            //If too, use

            string dirMessage = String.Empty;
            try
            {
                if (chkUseMonthName.Checked)
                {
                    targetPath = Path.Combine(targetPath, GetMonthName(month));
                }
                else
                {
                    targetPath = Path.Combine(targetPath, month.ToString().PadLeft(2, '0'));
                }

                if (!Directory.Exists(targetPath))
                {
                    var dirInfo = Directory.CreateDirectory(targetPath);

                    totalBgWorker.ReportProgress($"Added month directory '{targetPath}'");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                //     The caller does not have the required permission.
                dirMessage = "Unauthorized to access target directory.";
            }
            catch (ArgumentNullException ex)
            {
                //     path is null.
                dirMessage = "Target path cannot be empty.";
            }
            catch (ArgumentException ex)
            {
                //     path is a zero-length string, contains only white space, or contains one
                //     or more invalid characters as defined by System.IO.Path.InvalidPathChars.-or-path
                //     is prefixed with, or contains only a colon character (:).
                dirMessage = "Target path cannot be empty.";
            }
            catch (PathTooLongException ex)
            {
                //     The specified path, file name, or both exceed the system-defined maximum
                //     length. For example, on Windows-based platforms, paths must be less than
                //     248 characters and file names must be less than 260 characters.
                dirMessage = "Target path is too long.";
            }
            catch (DirectoryNotFoundException ex)
            {
                //     The specified path is invalid (for example, it is on an unmapped drive).
                dirMessage = "Target path is not found. Is it on an unmapped drive?";
            }
            catch (NotSupportedException ex)
            {
                //     path contains a colon character (:) that is not part of a drive label ("C:\").
                dirMessage = "Target path contains illegal characters.";
            }
            catch (IOException ex)
            {
                //     The directory specified by path is read-only.
                dirMessage = "Target path is read-only.";
            }
            catch (Exception ex)
            {
                dirMessage = "An unknown error occured. " + ex.Message;
            }
            if (!String.IsNullOrEmpty(dirMessage))
            {
                MessageBox.Show(dirMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return string.Empty;
            }

            return targetPath;
        }

        private string CreateYearDirectory(int year, string targetPath)
        {
            //Check if date year folder exists
            //If not, create
            //If too, use

            string dirMessage = String.Empty;
            try
            {
                targetPath = Path.Combine(targetPath, year.ToString());
                if (!Directory.Exists(targetPath))
                {
                    var dirInfo = Directory.CreateDirectory(targetPath);

                    totalBgWorker.ReportProgress($"Added year directory '{targetPath}'");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                //     The caller does not have the required permission.
                dirMessage = "Unauthorized to access target directory.";
            }
            catch (ArgumentNullException ex)
            {
                //     path is null.
                dirMessage = "Target path cannot be empty.";
            }
            catch (ArgumentException ex)
            {
                //     path is a zero-length string, contains only white space, or contains one
                //     or more invalid characters as defined by System.IO.Path.InvalidPathChars.-or-path
                //     is prefixed with, or contains only a colon character (:).
                dirMessage = "Target path cannot be empty.";
            }
            catch (PathTooLongException ex)
            {
                //     The specified path, file name, or both exceed the system-defined maximum
                //     length. For example, on Windows-based platforms, paths must be less than
                //     248 characters and file names must be less than 260 characters.
                dirMessage = "Target path is too long.";
            }
            catch (DirectoryNotFoundException ex)
            {
                //     The specified path is invalid (for example, it is on an unmapped drive).
                dirMessage = "Target path is not found. Is it on an unmapped drive?";
            }
            catch (NotSupportedException ex)
            {
                //     path contains a colon character (:) that is not part of a drive label ("C:\").
                dirMessage = "Target path contains illegal characters.";
            }
            catch (IOException ex)
            {
                //     The directory specified by path is read-only.
                dirMessage = "Target path is read-only.";
            }
            catch (Exception ex)
            {
                dirMessage = "An unknown error occured. " + ex.Message;
            }
            if (!String.IsNullOrEmpty(dirMessage))
            {
                MessageBox.Show(dirMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return string.Empty;
            }

            return targetPath;
        }

        private string GetMonthName(int month)
        {
            string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            return monthName.Substring(0, 1).ToUpper() + monthName.Substring(1).ToLower();
        }

        private bool CheckSourcePath()
        {
            sourceDirectory = txtSourceDirectory.Text.Trim();
            if (String.IsNullOrEmpty(sourceDirectory))
            {
                MessageBox.Show("Source path is empty.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            else if (!Directory.Exists(sourceDirectory))
            {
                MessageBox.Show("Source path doesn't exist.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }
        private bool CheckTargetPath()
        {
            targetDirectory = txtTargetDirectory.Text.Trim();
            if (String.IsNullOrEmpty(targetDirectory))
            {
                MessageBox.Show("Target path is empty.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            else if (!Directory.Exists(targetDirectory))
            {
                var result = MessageBox.Show("Target path doesn't exist. Would you like to create it?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    Directory.CreateDirectory(targetDirectory);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        private void GetFileListInSourceDirectory()
        {
            if (!CheckSourcePath()) return;

            string filesMessage = String.Empty;
            sourceFiles = new SourceFile[0];
            try
            {
                var directoryPaths = Directory.GetFiles(sourceDirectory);
                foreach (var path in directoryPaths)
                {
                    sourceFiles.Add(new SourceFile(path));
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                //     The caller does not have the required permission.
                filesMessage = "Unauthorized to access target directory.";
            }
            catch (ArgumentNullException ex)
            {
                //     path is null.
                filesMessage = "Source path cannot be empty.";
            }
            catch (ArgumentException ex)
            {
                //     path is a zero-length string, contains only white space, or contains one
                //     or more invalid characters as defined by System.IO.Path.InvalidPathChars.
                filesMessage = "Source path cannot be empty.";
            }
            catch (PathTooLongException ex)
            {
                //     The specified path, file name, or both exceed the system-defined maximum
                //     length. For example, on Windows-based platforms, paths must be less than
                //     248 characters and file names must be less than 260 characters.
                filesMessage = "Source path is too long.";
            }
            catch (DirectoryNotFoundException ex)
            {
                //     The specified path is invalid (for example, it is on an unmapped drive).
                filesMessage = "Source path is not found. Is it on an unmapped drive?";
            }
            catch (NotSupportedException ex)
            {
                //     path contains a colon character (:) that is not part of a drive label ("C:\").
                filesMessage = "Source path contains illegal characters.";
            }
            catch (IOException ex)
            {
                //     path is a file name.-or-A network error has occurred.
                filesMessage = "Source path is a filename, or an network error has occurred.";
            }
            catch (Exception ex)
            {
                filesMessage = "An unknown error occurred. " + ex.Message;
            }

            if (!String.IsNullOrEmpty(filesMessage))
            {
                MessageBox.Show(filesMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (sourceFiles == null || sourceFiles.Count == 0)
            {
                MessageBox.Show("No files in source path.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
        }
        private void InitProgressMeters(int fileCount)
        {
            lstProcessedMoves.Items.Clear();

            pgrTotalProgress.Maximum = fileCount;
            pgrTotalProgress.Minimum = 0;
            pgrTotalProgress.Step = 1;
            pgrTotalProgress.Value = 0;

            lblTotalProgressStats.Text = GetProgressText();
        }

        private string GetProgressText()
        {
            return $"{pgrTotalProgress.Value} / {pgrTotalProgress.Maximum} items";
        }

        private void UpdateTotalProgressMeter(ProgressReport report)
        {
            pgrTotalProgress.Maximum = report.Maximum;
            pgrTotalProgress.Value = report.Value;
            lblTotalProgressStats.Text = GetProgressText();
        }
        private void AddListItem(String message)
        {
            lstProcessedMoves.Items.Add(message);
            lstProcessedMoves.EnsureVisible(lstProcessedMoves.Items.Count - 1);
        }

        private void chkCreateYearFolder_CheckedChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            chkCreateMonthFolder.Enabled = chkCreateYearFolder.Checked;
        }

        private void rdbMoveFiles_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdbCopyFiles_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkCreateDayFolder_CheckedChanged(object sender, EventArgs e)
        {
            chkIncludeFullDateInDayFolderName.Enabled = chkCreateDayFolder.Checked;
        }

        private void chkCreateMonthFolder_CheckedChanged(object sender, EventArgs e)
        {
            chkUseMonthName.Enabled = chkCreateMonthFolder.Checked;
        }

        private void rbnNamebaseModifiedDate_CheckedChanged(object sender, EventArgs e)
        {
            //rbnNamebaseFilename.Checked = false;
            lblBasenameRegexp.Enabled = false;
            txtBasenameRegexp.Enabled = false;
        }

        private void rbnNamebaseFilename_CheckedChanged(object sender, EventArgs e)
        {
            //rbnNamebaseModifiedDate.Checked = false;
            lblBasenameRegexp.Enabled = true;
            txtBasenameRegexp.Enabled = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lstProcessedMoves.Items.Clear();
            pgrTotalProgress.Value = 0;
            pgrTotalProgress.Maximum = 0;
            lblTotalProgressStats.Text = GetProgressText();
            btnReset.Enabled = false;
        }
    }
}
