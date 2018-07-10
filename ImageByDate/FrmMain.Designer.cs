namespace ImageByDate
{
    partial class frmImagesByDate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("fsf 1");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("23214 2");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("dfdfb 3");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("sfsfs d 4");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("fsf3 f3qf 5");
            this.fbdSourceDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.txtSourceDirectory = new System.Windows.Forms.TextBox();
            this.lblSourceDirectory = new System.Windows.Forms.Label();
            this.btnSourceDirectory = new System.Windows.Forms.Button();
            this.pgrTotalProgress = new System.Windows.Forms.ProgressBar();
            this.lblTotalProgress = new System.Windows.Forms.Label();
            this.lstProcessedMoves = new System.Windows.Forms.ListView();
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTotalProgressStats = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblTargetDirectory = new System.Windows.Forms.Label();
            this.txtTargetDirectory = new System.Windows.Forms.TextBox();
            this.btnTargetDirectory = new System.Windows.Forms.Button();
            this.fbdTargetDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.rdbMoveFiles = new System.Windows.Forms.RadioButton();
            this.rdbCopyFiles = new System.Windows.Forms.RadioButton();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.pnlDirectoryChoices = new System.Windows.Forms.Panel();
            this.chkCreateYearFolder = new System.Windows.Forms.CheckBox();
            this.chkCreateMonthFolder = new System.Windows.Forms.CheckBox();
            this.chkCreateDayFolder = new System.Windows.Forms.CheckBox();
            this.chkUseMonthName = new System.Windows.Forms.CheckBox();
            this.chkIncludeFullDateInDayFolderName = new System.Windows.Forms.CheckBox();
            this.pnlMoveOrCopy = new System.Windows.Forms.Panel();
            this.lblCopyOrMove = new System.Windows.Forms.Label();
            this.pnlDirectoryCreationBase = new System.Windows.Forms.Panel();
            this.txtBasenameRegexp = new System.Windows.Forms.TextBox();
            this.lblBasenameRegexp = new System.Windows.Forms.Label();
            this.rbnNamebaseModifiedDate = new System.Windows.Forms.RadioButton();
            this.lblDirectoryCreationBase = new System.Windows.Forms.Label();
            this.rbnNamebaseFilename = new System.Windows.Forms.RadioButton();
            this.grpProcessdMoves = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpSettings.SuspendLayout();
            this.pnlDirectoryChoices.SuspendLayout();
            this.pnlMoveOrCopy.SuspendLayout();
            this.pnlDirectoryCreationBase.SuspendLayout();
            this.grpProcessdMoves.SuspendLayout();
            this.SuspendLayout();
            // 
            // fbdSourceDirectory
            // 
            this.fbdSourceDirectory.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fbdSourceDirectory.ShowNewFolderButton = false;
            // 
            // txtSourceDirectory
            // 
            this.txtSourceDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceDirectory.Location = new System.Drawing.Point(9, 32);
            this.txtSourceDirectory.Name = "txtSourceDirectory";
            this.txtSourceDirectory.Size = new System.Drawing.Size(416, 20);
            this.txtSourceDirectory.TabIndex = 0;
            // 
            // lblSourceDirectory
            // 
            this.lblSourceDirectory.AutoSize = true;
            this.lblSourceDirectory.Location = new System.Drawing.Point(6, 17);
            this.lblSourceDirectory.Name = "lblSourceDirectory";
            this.lblSourceDirectory.Size = new System.Drawing.Size(87, 13);
            this.lblSourceDirectory.TabIndex = 1;
            this.lblSourceDirectory.Text = "Source directory:";
            // 
            // btnSourceDirectory
            // 
            this.btnSourceDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSourceDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSourceDirectory.Location = new System.Drawing.Point(431, 30);
            this.btnSourceDirectory.Name = "btnSourceDirectory";
            this.btnSourceDirectory.Size = new System.Drawing.Size(28, 23);
            this.btnSourceDirectory.TabIndex = 2;
            this.btnSourceDirectory.Text = "...";
            this.btnSourceDirectory.UseVisualStyleBackColor = true;
            this.btnSourceDirectory.Click += new System.EventHandler(this.btnSourceDirectory_Click);
            // 
            // pgrTotalProgress
            // 
            this.pgrTotalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgrTotalProgress.Location = new System.Drawing.Point(13, 399);
            this.pgrTotalProgress.Name = "pgrTotalProgress";
            this.pgrTotalProgress.Size = new System.Drawing.Size(463, 23);
            this.pgrTotalProgress.TabIndex = 3;
            // 
            // lblTotalProgress
            // 
            this.lblTotalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalProgress.AutoSize = true;
            this.lblTotalProgress.Location = new System.Drawing.Point(13, 380);
            this.lblTotalProgress.Name = "lblTotalProgress";
            this.lblTotalProgress.Size = new System.Drawing.Size(77, 13);
            this.lblTotalProgress.TabIndex = 4;
            this.lblTotalProgress.Text = "Total progress:";
            // 
            // lstProcessedMoves
            // 
            this.lstProcessedMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProcessedMoves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMessage});
            this.lstProcessedMoves.FullRowSelect = true;
            this.lstProcessedMoves.GridLines = true;
            this.lstProcessedMoves.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstProcessedMoves.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.lstProcessedMoves.Location = new System.Drawing.Point(6, 19);
            this.lstProcessedMoves.MultiSelect = false;
            this.lstProcessedMoves.Name = "lstProcessedMoves";
            this.lstProcessedMoves.Size = new System.Drawing.Size(453, 89);
            this.lstProcessedMoves.TabIndex = 5;
            this.lstProcessedMoves.UseCompatibleStateImageBehavior = false;
            this.lstProcessedMoves.View = System.Windows.Forms.View.Details;
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 429;
            // 
            // lblTotalProgressStats
            // 
            this.lblTotalProgressStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalProgressStats.Location = new System.Drawing.Point(271, 380);
            this.lblTotalProgressStats.Name = "lblTotalProgressStats";
            this.lblTotalProgressStats.Size = new System.Drawing.Size(206, 13);
            this.lblTotalProgressStats.TabIndex = 7;
            this.lblTotalProgressStats.Text = "23/145 items";
            this.lblTotalProgressStats.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(402, 428);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(321, 429);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Go!";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblTargetDirectory
            // 
            this.lblTargetDirectory.AutoSize = true;
            this.lblTargetDirectory.Location = new System.Drawing.Point(6, 59);
            this.lblTargetDirectory.Name = "lblTargetDirectory";
            this.lblTargetDirectory.Size = new System.Drawing.Size(84, 13);
            this.lblTargetDirectory.TabIndex = 11;
            this.lblTargetDirectory.Text = "Target directory:";
            // 
            // txtTargetDirectory
            // 
            this.txtTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetDirectory.Location = new System.Drawing.Point(9, 74);
            this.txtTargetDirectory.Name = "txtTargetDirectory";
            this.txtTargetDirectory.Size = new System.Drawing.Size(416, 20);
            this.txtTargetDirectory.TabIndex = 10;
            // 
            // btnTargetDirectory
            // 
            this.btnTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTargetDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTargetDirectory.Location = new System.Drawing.Point(431, 72);
            this.btnTargetDirectory.Name = "btnTargetDirectory";
            this.btnTargetDirectory.Size = new System.Drawing.Size(28, 23);
            this.btnTargetDirectory.TabIndex = 12;
            this.btnTargetDirectory.Text = "...";
            this.btnTargetDirectory.UseVisualStyleBackColor = true;
            this.btnTargetDirectory.Click += new System.EventHandler(this.btnTargetDirectory_Click);
            // 
            // fbdTargetDirectory
            // 
            this.fbdTargetDirectory.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // rdbMoveFiles
            // 
            this.rdbMoveFiles.AutoSize = true;
            this.rdbMoveFiles.Checked = true;
            this.rdbMoveFiles.Location = new System.Drawing.Point(3, 19);
            this.rdbMoveFiles.Name = "rdbMoveFiles";
            this.rdbMoveFiles.Size = new System.Drawing.Size(73, 17);
            this.rdbMoveFiles.TabIndex = 13;
            this.rdbMoveFiles.TabStop = true;
            this.rdbMoveFiles.Text = "Move files";
            this.rdbMoveFiles.UseVisualStyleBackColor = true;
            this.rdbMoveFiles.CheckedChanged += new System.EventHandler(this.rdbMoveFiles_CheckedChanged);
            // 
            // rdbCopyFiles
            // 
            this.rdbCopyFiles.AutoSize = true;
            this.rdbCopyFiles.Location = new System.Drawing.Point(82, 19);
            this.rdbCopyFiles.Name = "rdbCopyFiles";
            this.rdbCopyFiles.Size = new System.Drawing.Size(70, 17);
            this.rdbCopyFiles.TabIndex = 14;
            this.rdbCopyFiles.Text = "Copy files";
            this.rdbCopyFiles.UseVisualStyleBackColor = true;
            this.rdbCopyFiles.CheckedChanged += new System.EventHandler(this.rdbCopyFiles_CheckedChanged);
            // 
            // grpSettings
            // 
            this.grpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSettings.Controls.Add(this.pnlDirectoryChoices);
            this.grpSettings.Controls.Add(this.pnlMoveOrCopy);
            this.grpSettings.Controls.Add(this.pnlDirectoryCreationBase);
            this.grpSettings.Controls.Add(this.lblSourceDirectory);
            this.grpSettings.Controls.Add(this.txtSourceDirectory);
            this.grpSettings.Controls.Add(this.btnSourceDirectory);
            this.grpSettings.Controls.Add(this.btnTargetDirectory);
            this.grpSettings.Controls.Add(this.lblTargetDirectory);
            this.grpSettings.Controls.Add(this.txtTargetDirectory);
            this.grpSettings.Location = new System.Drawing.Point(12, 12);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(465, 245);
            this.grpSettings.TabIndex = 15;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Settings";
            // 
            // pnlDirectoryChoices
            // 
            this.pnlDirectoryChoices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDirectoryChoices.Controls.Add(this.chkCreateYearFolder);
            this.pnlDirectoryChoices.Controls.Add(this.chkCreateMonthFolder);
            this.pnlDirectoryChoices.Controls.Add(this.chkCreateDayFolder);
            this.pnlDirectoryChoices.Controls.Add(this.chkUseMonthName);
            this.pnlDirectoryChoices.Controls.Add(this.chkIncludeFullDateInDayFolderName);
            this.pnlDirectoryChoices.Location = new System.Drawing.Point(9, 191);
            this.pnlDirectoryChoices.Name = "pnlDirectoryChoices";
            this.pnlDirectoryChoices.Size = new System.Drawing.Size(450, 46);
            this.pnlDirectoryChoices.TabIndex = 27;
            // 
            // chkCreateYearFolder
            // 
            this.chkCreateYearFolder.AutoSize = true;
            this.chkCreateYearFolder.Checked = true;
            this.chkCreateYearFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateYearFolder.Location = new System.Drawing.Point(6, 3);
            this.chkCreateYearFolder.Name = "chkCreateYearFolder";
            this.chkCreateYearFolder.Size = new System.Drawing.Size(115, 17);
            this.chkCreateYearFolder.TabIndex = 15;
            this.chkCreateYearFolder.Text = "Create year folder?";
            this.chkCreateYearFolder.UseVisualStyleBackColor = true;
            this.chkCreateYearFolder.CheckedChanged += new System.EventHandler(this.chkCreateYearFolder_CheckedChanged);
            // 
            // chkCreateMonthFolder
            // 
            this.chkCreateMonthFolder.AutoSize = true;
            this.chkCreateMonthFolder.Location = new System.Drawing.Point(127, 3);
            this.chkCreateMonthFolder.Name = "chkCreateMonthFolder";
            this.chkCreateMonthFolder.Size = new System.Drawing.Size(124, 17);
            this.chkCreateMonthFolder.TabIndex = 16;
            this.chkCreateMonthFolder.Text = "Create month folder?";
            this.chkCreateMonthFolder.UseVisualStyleBackColor = true;
            this.chkCreateMonthFolder.CheckedChanged += new System.EventHandler(this.chkCreateMonthFolder_CheckedChanged);
            // 
            // chkCreateDayFolder
            // 
            this.chkCreateDayFolder.AutoSize = true;
            this.chkCreateDayFolder.Checked = true;
            this.chkCreateDayFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateDayFolder.Location = new System.Drawing.Point(257, 3);
            this.chkCreateDayFolder.Name = "chkCreateDayFolder";
            this.chkCreateDayFolder.Size = new System.Drawing.Size(112, 17);
            this.chkCreateDayFolder.TabIndex = 17;
            this.chkCreateDayFolder.Text = "Create day folder?";
            this.chkCreateDayFolder.UseVisualStyleBackColor = true;
            this.chkCreateDayFolder.CheckedChanged += new System.EventHandler(this.chkCreateDayFolder_CheckedChanged);
            // 
            // chkUseMonthName
            // 
            this.chkUseMonthName.AutoSize = true;
            this.chkUseMonthName.Checked = true;
            this.chkUseMonthName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseMonthName.Enabled = false;
            this.chkUseMonthName.Location = new System.Drawing.Point(127, 26);
            this.chkUseMonthName.Name = "chkUseMonthName";
            this.chkUseMonthName.Size = new System.Drawing.Size(112, 17);
            this.chkUseMonthName.TabIndex = 19;
            this.chkUseMonthName.Text = "Use month name?";
            this.chkUseMonthName.UseVisualStyleBackColor = true;
            // 
            // chkIncludeFullDateInDayFolderName
            // 
            this.chkIncludeFullDateInDayFolderName.AutoSize = true;
            this.chkIncludeFullDateInDayFolderName.Checked = true;
            this.chkIncludeFullDateInDayFolderName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeFullDateInDayFolderName.Location = new System.Drawing.Point(257, 26);
            this.chkIncludeFullDateInDayFolderName.Name = "chkIncludeFullDateInDayFolderName";
            this.chkIncludeFullDateInDayFolderName.Size = new System.Drawing.Size(190, 17);
            this.chkIncludeFullDateInDayFolderName.TabIndex = 18;
            this.chkIncludeFullDateInDayFolderName.Text = "Include full date in day folder name";
            this.chkIncludeFullDateInDayFolderName.UseVisualStyleBackColor = true;
            // 
            // pnlMoveOrCopy
            // 
            this.pnlMoveOrCopy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMoveOrCopy.Controls.Add(this.lblCopyOrMove);
            this.pnlMoveOrCopy.Controls.Add(this.rdbMoveFiles);
            this.pnlMoveOrCopy.Controls.Add(this.rdbCopyFiles);
            this.pnlMoveOrCopy.Location = new System.Drawing.Point(9, 100);
            this.pnlMoveOrCopy.Name = "pnlMoveOrCopy";
            this.pnlMoveOrCopy.Size = new System.Drawing.Size(450, 38);
            this.pnlMoveOrCopy.TabIndex = 26;
            // 
            // lblCopyOrMove
            // 
            this.lblCopyOrMove.AutoSize = true;
            this.lblCopyOrMove.Location = new System.Drawing.Point(3, 3);
            this.lblCopyOrMove.Name = "lblCopyOrMove";
            this.lblCopyOrMove.Size = new System.Drawing.Size(96, 13);
            this.lblCopyOrMove.TabIndex = 14;
            this.lblCopyOrMove.Text = "Copy or move files:";
            // 
            // pnlDirectoryCreationBase
            // 
            this.pnlDirectoryCreationBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDirectoryCreationBase.Controls.Add(this.txtBasenameRegexp);
            this.pnlDirectoryCreationBase.Controls.Add(this.lblBasenameRegexp);
            this.pnlDirectoryCreationBase.Controls.Add(this.rbnNamebaseModifiedDate);
            this.pnlDirectoryCreationBase.Controls.Add(this.lblDirectoryCreationBase);
            this.pnlDirectoryCreationBase.Controls.Add(this.rbnNamebaseFilename);
            this.pnlDirectoryCreationBase.Location = new System.Drawing.Point(9, 142);
            this.pnlDirectoryCreationBase.Name = "pnlDirectoryCreationBase";
            this.pnlDirectoryCreationBase.Size = new System.Drawing.Size(450, 43);
            this.pnlDirectoryCreationBase.TabIndex = 25;
            // 
            // txtBasenameRegexp
            // 
            this.txtBasenameRegexp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBasenameRegexp.Enabled = false;
            this.txtBasenameRegexp.Location = new System.Drawing.Point(266, 18);
            this.txtBasenameRegexp.Name = "txtBasenameRegexp";
            this.txtBasenameRegexp.Size = new System.Drawing.Size(181, 20);
            this.txtBasenameRegexp.TabIndex = 23;
            this.txtBasenameRegexp.Text = "([\\w\\s]+_)(?<year>[\\d]{4})(?<month>[\\d]{2})(?<day>[\\d]{2})([\\w\\d\\s]+)";
            // 
            // lblBasenameRegexp
            // 
            this.lblBasenameRegexp.AutoSize = true;
            this.lblBasenameRegexp.Enabled = false;
            this.lblBasenameRegexp.Location = new System.Drawing.Point(213, 21);
            this.lblBasenameRegexp.Name = "lblBasenameRegexp";
            this.lblBasenameRegexp.Size = new System.Drawing.Size(47, 13);
            this.lblBasenameRegexp.TabIndex = 24;
            this.lblBasenameRegexp.Text = "Regexp:";
            // 
            // rbnNamebaseModifiedDate
            // 
            this.rbnNamebaseModifiedDate.AutoSize = true;
            this.rbnNamebaseModifiedDate.Checked = true;
            this.rbnNamebaseModifiedDate.Location = new System.Drawing.Point(6, 19);
            this.rbnNamebaseModifiedDate.Name = "rbnNamebaseModifiedDate";
            this.rbnNamebaseModifiedDate.Size = new System.Drawing.Size(91, 17);
            this.rbnNamebaseModifiedDate.TabIndex = 20;
            this.rbnNamebaseModifiedDate.TabStop = true;
            this.rbnNamebaseModifiedDate.Text = "Modified Date";
            this.rbnNamebaseModifiedDate.UseVisualStyleBackColor = true;
            this.rbnNamebaseModifiedDate.CheckedChanged += new System.EventHandler(this.rbnNamebaseModifiedDate_CheckedChanged);
            // 
            // lblDirectoryCreationBase
            // 
            this.lblDirectoryCreationBase.AutoSize = true;
            this.lblDirectoryCreationBase.Location = new System.Drawing.Point(3, 3);
            this.lblDirectoryCreationBase.Name = "lblDirectoryCreationBase";
            this.lblDirectoryCreationBase.Size = new System.Drawing.Size(133, 13);
            this.lblDirectoryCreationBase.TabIndex = 21;
            this.lblDirectoryCreationBase.Text = "Base directory creation on:";
            // 
            // rbnNamebaseFilename
            // 
            this.rbnNamebaseFilename.AutoSize = true;
            this.rbnNamebaseFilename.Enabled = false;
            this.rbnNamebaseFilename.Location = new System.Drawing.Point(103, 19);
            this.rbnNamebaseFilename.Name = "rbnNamebaseFilename";
            this.rbnNamebaseFilename.Size = new System.Drawing.Size(104, 17);
            this.rbnNamebaseFilename.TabIndex = 22;
            this.rbnNamebaseFilename.Text = "Date in Filename";
            this.rbnNamebaseFilename.UseVisualStyleBackColor = true;
            this.rbnNamebaseFilename.CheckedChanged += new System.EventHandler(this.rbnNamebaseFilename_CheckedChanged);
            // 
            // grpProcessdMoves
            // 
            this.grpProcessdMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProcessdMoves.Controls.Add(this.lstProcessedMoves);
            this.grpProcessdMoves.Location = new System.Drawing.Point(12, 263);
            this.grpProcessdMoves.Name = "grpProcessdMoves";
            this.grpProcessdMoves.Size = new System.Drawing.Size(465, 114);
            this.grpProcessdMoves.TabIndex = 16;
            this.grpProcessdMoves.TabStop = false;
            this.grpProcessdMoves.Text = "Processed moves:";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(240, 429);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // frmImagesByDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 462);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.grpProcessdMoves);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTotalProgressStats);
            this.Controls.Add(this.lblTotalProgress);
            this.Controls.Add(this.pgrTotalProgress);
            this.MinimumSize = new System.Drawing.Size(505, 501);
            this.Name = "frmImagesByDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Images By Date v0.8";
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            this.pnlDirectoryChoices.ResumeLayout(false);
            this.pnlDirectoryChoices.PerformLayout();
            this.pnlMoveOrCopy.ResumeLayout(false);
            this.pnlMoveOrCopy.PerformLayout();
            this.pnlDirectoryCreationBase.ResumeLayout(false);
            this.pnlDirectoryCreationBase.PerformLayout();
            this.grpProcessdMoves.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbdSourceDirectory;
        private System.Windows.Forms.TextBox txtSourceDirectory;
        private System.Windows.Forms.Label lblSourceDirectory;
        private System.Windows.Forms.Button btnSourceDirectory;
        private System.Windows.Forms.ProgressBar pgrTotalProgress;
        private System.Windows.Forms.Label lblTotalProgress;
        private System.Windows.Forms.ListView lstProcessedMoves;
        private System.Windows.Forms.Label lblTotalProgressStats;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblTargetDirectory;
        private System.Windows.Forms.TextBox txtTargetDirectory;
        private System.Windows.Forms.Button btnTargetDirectory;
        private System.Windows.Forms.FolderBrowserDialog fbdTargetDirectory;
        private System.Windows.Forms.RadioButton rdbMoveFiles;
        private System.Windows.Forms.RadioButton rdbCopyFiles;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Label lblCopyOrMove;
        private System.Windows.Forms.CheckBox chkCreateMonthFolder;
        private System.Windows.Forms.CheckBox chkCreateYearFolder;
        private System.Windows.Forms.CheckBox chkCreateDayFolder;
        private System.Windows.Forms.CheckBox chkIncludeFullDateInDayFolderName;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.CheckBox chkUseMonthName;
        private System.Windows.Forms.Label lblBasenameRegexp;
        private System.Windows.Forms.TextBox txtBasenameRegexp;
        private System.Windows.Forms.RadioButton rbnNamebaseFilename;
        private System.Windows.Forms.Label lblDirectoryCreationBase;
        private System.Windows.Forms.RadioButton rbnNamebaseModifiedDate;
        private System.Windows.Forms.Panel pnlDirectoryChoices;
        private System.Windows.Forms.Panel pnlMoveOrCopy;
        private System.Windows.Forms.Panel pnlDirectoryCreationBase;
        private System.Windows.Forms.GroupBox grpProcessdMoves;
        private System.Windows.Forms.Button btnReset;
    }
}

