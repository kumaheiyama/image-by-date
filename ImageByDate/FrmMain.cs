using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace ImageByDate
{
    public partial class frmImagesByDate : Form
    {
        private string sourceDirectory = String.Empty;
        private string targetDirectory = String.Empty;
        private MoveBgWorker totalBgWorker = new MoveBgWorker();
        private String[] sourceFiles;

        public frmImagesByDate()
        {
            InitializeComponent();

            totalBgWorker.WorkerReportsProgress = true;
            totalBgWorker.WorkerSupportsCancellation = true;
            totalBgWorker.DoWork += new DoWorkEventHandler(totalBgWorker_DoWork);
            totalBgWorker.ProgressChanged += new ProgressChangedEventHandler(totalBgWorker_ProgressChanged);
            totalBgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(totalBgWorker_RunWorkerCompleted);

            this.FormClosing += new FormClosingEventHandler(frmImagesByDate_FormClosing);

            initProgressMeters(0);
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
                if (sourceFiles == null || sourceFiles.Length == 0)
                {
                    return;
                }
                lblTotalProgressStats.Text = "0 / " + sourceFiles.Length + " items";
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
            
            if (rbnNamebaseFilename.Checked && txtBasenameRegexp.TextLength > 0 && !string.IsNullOrWhiteSpace(txtBasenameRegexp.Text)) {
                GetFileListInSourceDirectory();
                var firstFile = sourceFiles != null || sourceFiles.Length > 0 ? sourceFiles.First() : string.Empty;
                if (!string.IsNullOrEmpty(firstFile))
                {
                    message += "\n\n";
                    message += "Date will be based on the following format, is this correct?\n\n";

                    var regex = new Regex(txtBasenameRegexp.Text);
                    var groupNames = regex.GetGroupNames();
                    var fileName = Path.GetFileNameWithoutExtension(firstFile);
                    var fileNameWithExtension = Path.GetFileName(firstFile);

                    string year = string.Empty, month = string.Empty, day = string.Empty;
                    if (DateTime.TryParse(fileName.Replace('.', ':'), out DateTime date))
                    {
                        year = date.Year.ToString();
                        month = date.Month.ToString().PadLeft(2, '0');
                        day = date.Day.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        var regexMatch = regex.Match(fileName);
                        year = regexMatch.Groups["year"].Value;
                        month = regexMatch.Groups["month"].Value;
                        day = regexMatch.Groups["day"].Value;
                    }
                    message += sourceDirectory + "\\" + fileNameWithExtension + " -->\n";
                    message += targetDirectory + "\\" + year + "\\" + year + month + day + "\\" + fileNameWithExtension;
                }
            }

            var result = MessageBox.Show(message, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                StartProcessing();

                initProgressMeters(0);
                totalBgWorker.Report = new ProgressReport();
                totalBgWorker.RunWorkerAsync();
            }
        }

        void totalBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CancelProcessing();

            var result = totalBgWorker.Report;
            updateTotalProgressMeter(result);

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
                addListItem((String)e.UserState);
            }
            else if (e.UserState is ProgressReport)
            {
                var result = totalBgWorker.Report;
                updateTotalProgressMeter(result);
            }
        }

        void totalBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GetFileListInSourceDirectory();
            if (sourceFiles == null || sourceFiles.Length == 0)
            {
                return;
            }
            totalBgWorker.Report.Maximum = sourceFiles.Length;
            totalBgWorker.ReportProgress();

            if (!CheckTargetPath()) return;

            for (int i = 0; i < sourceFiles.Length; i++)
            {
                //Get each file's modified date
                var modDate = File.GetLastWriteTime(sourceFiles[i]);

                string targetPath = targetDirectory;

                #region Create year directory
                if (chkCreateYearFolder.Checked)
                {
                    //Check if date year folder exists
                    //If not, create
                    //If too, use

                    string dirMessage = String.Empty;
                    try
                    {
                        targetPath = Path.Combine(targetPath, "" + modDate.Year);
                        if (!Directory.Exists(targetPath))
                        {
                            var dirInfo = Directory.CreateDirectory(targetPath);

                            totalBgWorker.ReportProgress("Added year directory \"" + targetPath + "\"");
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
                        return;
                    }
                }
                #endregion

                #region Create month directory
                if (chkCreateMonthFolder.Checked)
                {
                    //Check if date month folder exists
                    //If not, create
                    //If too, use

                    string dirMessage = String.Empty;
                    try
                    {
                        if (chkUseMonthName.Checked)
                        {
                            targetPath = Path.Combine(targetPath, GetMonthName(modDate.Month));
                        }
                        else
                        {
                            targetPath = Path.Combine(targetPath, modDate.Month.ToString().PadLeft(2, '0'));
                        }
                        
                        if (!Directory.Exists(targetPath))
                        {
                            var dirInfo = Directory.CreateDirectory(targetPath);

                            totalBgWorker.ReportProgress("Added month directory \"" + targetPath + "\"");
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
                        return;
                    }
                }
                #endregion

                #region Create day directory
                if (chkCreateDayFolder.Checked)
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
                                modDate.Year.ToString().PadLeft(4, '0')
                                + modDate.Month.ToString().PadLeft(2, '0')
                                + modDate.Day.ToString().PadLeft(2, '0'));
                        }
                        else
                        {
                            targetPath = Path.Combine(targetPath, "" + modDate.Day);
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
                        return;
                    }
                }
                #endregion

                #region Move/copy file
                //Check if file already exists
                //If not, copy/Move each file to target folder
                //If too, check with user
                string fileName = Path.GetFileName(sourceFiles[i]);

                targetPath = Path.Combine(targetPath, fileName);

                totalBgWorker.Report.Value = (i + 1);
                if (File.Exists(targetPath))
                {
                    totalBgWorker.ReportProgress("Target path \"" + targetPath + "\" already exists.");
                    totalBgWorker.Report.Exists++;
                }
                else
                {
                    string fileMessage = String.Empty;
                    try
                    {
                        if (rdbMoveFiles.Checked)
                        {
                            File.Move(sourceFiles[i], targetPath);
                        }
                        else if (rdbCopyFiles.Checked)
                        {
                            File.Copy(sourceFiles[i], targetPath);
                        }

                        totalBgWorker.ReportProgress(
                            (rdbMoveFiles.Checked ? "Moved " : "")
                            + (rdbCopyFiles.Checked ? "Copied " : "")
                            + "\"" + sourceFiles[i] + "\" to \"" + targetPath + "\""); 
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
                        fileMessage = "Source file name \"" + sourceFiles[i] + "\" is not found.";
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
                    if (!String.IsNullOrEmpty(fileMessage))
                    {
                        totalBgWorker.ReportProgress(fileMessage);
                    }
                }
                totalBgWorker.ReportProgress();
                #endregion
            }
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
            sourceFiles = null;
            try
            {
                sourceFiles = Directory.GetFiles(sourceDirectory);
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
            else if (sourceFiles == null || sourceFiles.Length == 0)
            {
                MessageBox.Show("No files in source path.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
        }
        private void initProgressMeters(int fileCount)
        {
            lstProcessedMoves.Items.Clear();

            pgrTotalProgress.Maximum = fileCount;
            pgrTotalProgress.Minimum = 0;
            pgrTotalProgress.Step = 1;
            pgrTotalProgress.Value = 0;

            lblTotalProgressStats.Text = pgrTotalProgress.Value + " / " + pgrTotalProgress.Maximum + " items";
        }
        private void updateTotalProgressMeter(ProgressReport report)
        {
            pgrTotalProgress.Maximum = report.Maximum;
            pgrTotalProgress.Value = report.Value;
            lblTotalProgressStats.Text = pgrTotalProgress.Value + " / " + pgrTotalProgress.Maximum + " items";
        }
        private void addListItem(String message)
        {
            lstProcessedMoves.Items.Add(message);
            lstProcessedMoves.EnsureVisible(lstProcessedMoves.Items.Count - 1);
        }

        private void chkCreateYearFolder_CheckedChanged(object sender, EventArgs e)
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
            lblTotalProgressStats.Text = pgrTotalProgress.Value + " / " + pgrTotalProgress.Maximum + " items";
            btnReset.Enabled = false;
        }
    }
}
