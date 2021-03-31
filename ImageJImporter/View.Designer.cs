
namespace ImageJImporter
{
    partial class View
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View));
            this.ViewFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenuOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenuSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenuSaveFileAs = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenuCloseFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleWordWrappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxCurrentFilenameRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.uxEditRow = new System.Windows.Forms.Button();
            this.uxViewRow = new System.Windows.Forms.Button();
            this.uxTextViewer = new System.Windows.Forms.TextBox();
            this.uxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.uxSaveSeed = new System.Windows.Forms.Button();
            this.uxHeaderLog = new System.Windows.Forms.TextBox();
            this.uxCurrentDateTime = new System.Windows.Forms.TextBox();
            this.uxLockListSelection = new System.Windows.Forms.Button();
            this.uxRowDisplayGroup = new System.Windows.Forms.GroupBox();
            this.uxRowListView = new BrightIdeasSoftware.ObjectListView();
            this.rowName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowArea = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowX = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowY = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowPerim = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowMajor = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowMinor = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowAngle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowCirc = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowAR = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowRound = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rowSolidity = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.uxGridDisplay = new System.Windows.Forms.GroupBox();
            this.uxStartReference = new System.Windows.Forms.Button();
            this.ViewFormMenuStrip.SuspendLayout();
            this.uxRowDisplayGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxRowListView)).BeginInit();
            this.uxGridDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewFormMenuStrip
            // 
            this.ViewFormMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ViewFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.ViewFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.ViewFormMenuStrip.Name = "ViewFormMenuStrip";
            this.ViewFormMenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.ViewFormMenuStrip.Size = new System.Drawing.Size(1020, 28);
            this.ViewFormMenuStrip.TabIndex = 0;
            this.ViewFormMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxMenuOpenFile,
            this.uxMenuSaveFile,
            this.uxMenuSaveFileAs,
            this.uxMenuCloseFile});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // uxMenuOpenFile
            // 
            this.uxMenuOpenFile.Name = "uxMenuOpenFile";
            this.uxMenuOpenFile.Size = new System.Drawing.Size(129, 24);
            this.uxMenuOpenFile.Text = "Open";
            this.uxMenuOpenFile.ToolTipText = "Allows you to load a file into the program. It\'s meant to use .txt files, so I\'m " +
    "not sure what would happen if you imported other file types.";
            this.uxMenuOpenFile.Click += new System.EventHandler(this.OpenFile);
            // 
            // uxMenuSaveFile
            // 
            this.uxMenuSaveFile.Name = "uxMenuSaveFile";
            this.uxMenuSaveFile.Size = new System.Drawing.Size(129, 24);
            this.uxMenuSaveFile.Text = "Save";
            this.uxMenuSaveFile.ToolTipText = "Allows you to save the current data back into the original file you opened.";
            this.uxMenuSaveFile.Click += new System.EventHandler(this.SaveFile);
            // 
            // uxMenuSaveFileAs
            // 
            this.uxMenuSaveFileAs.Name = "uxMenuSaveFileAs";
            this.uxMenuSaveFileAs.Size = new System.Drawing.Size(129, 24);
            this.uxMenuSaveFileAs.Text = "Save As";
            this.uxMenuSaveFileAs.ToolTipText = "Allows you to save the seed data you\'ve edited as a new .txt file with the same s" +
    "tructure as the original file.";
            this.uxMenuSaveFileAs.Click += new System.EventHandler(this.SaveFileAs);
            // 
            // uxMenuCloseFile
            // 
            this.uxMenuCloseFile.Name = "uxMenuCloseFile";
            this.uxMenuCloseFile.Size = new System.Drawing.Size(129, 24);
            this.uxMenuCloseFile.Text = "Close";
            this.uxMenuCloseFile.ToolTipText = "Closes the currently loaded file without saving changes or exiting the program.";
            this.uxMenuCloseFile.Click += new System.EventHandler(this.CloseFile);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleWordWrappingToolStripMenuItem,
            this.uxCurrentFilenameRequest});
            this.toolsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // toggleWordWrappingToolStripMenuItem
            // 
            this.toggleWordWrappingToolStripMenuItem.Name = "toggleWordWrappingToolStripMenuItem";
            this.toggleWordWrappingToolStripMenuItem.Size = new System.Drawing.Size(242, 24);
            this.toggleWordWrappingToolStripMenuItem.Text = "Toggle Word Wrapping";
            this.toggleWordWrappingToolStripMenuItem.ToolTipText = "Toggles whether or not the text in the seed display will wrap across lines";
            this.toggleWordWrappingToolStripMenuItem.Click += new System.EventHandler(this.ToggleWordWrap);
            // 
            // uxCurrentFilenameRequest
            // 
            this.uxCurrentFilenameRequest.Name = "uxCurrentFilenameRequest";
            this.uxCurrentFilenameRequest.Size = new System.Drawing.Size(242, 24);
            this.uxCurrentFilenameRequest.Text = "Tell Me Current Filename";
            this.uxCurrentFilenameRequest.Click += new System.EventHandler(this.AskForFilename);
            // 
            // uxEditRow
            // 
            this.uxEditRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uxEditRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxEditRow.Location = new System.Drawing.Point(231, 220);
            this.uxEditRow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxEditRow.Name = "uxEditRow";
            this.uxEditRow.Size = new System.Drawing.Size(182, 43);
            this.uxEditRow.TabIndex = 3;
            this.uxEditRow.Text = "Edit Row Data";
            this.uxToolTip.SetToolTip(this.uxEditRow, "Allows you to start editing the seed you have selected in the list to the left.");
            this.uxEditRow.UseVisualStyleBackColor = true;
            this.uxEditRow.Click += new System.EventHandler(this.EditSeedData);
            // 
            // uxViewRow
            // 
            this.uxViewRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uxViewRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxViewRow.Location = new System.Drawing.Point(676, 220);
            this.uxViewRow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxViewRow.Name = "uxViewRow";
            this.uxViewRow.Size = new System.Drawing.Size(182, 43);
            this.uxViewRow.TabIndex = 2;
            this.uxViewRow.Text = "View Row Data";
            this.uxToolTip.SetToolTip(this.uxViewRow, "Allows you to view the data for the seed you have selected without the worry of a" +
        "ccidentally editing it.");
            this.uxViewRow.UseVisualStyleBackColor = true;
            this.uxViewRow.Click += new System.EventHandler(this.ViewRowData);
            // 
            // uxTextViewer
            // 
            this.uxTextViewer.AcceptsReturn = true;
            this.uxTextViewer.AcceptsTab = true;
            this.uxTextViewer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxTextViewer.Location = new System.Drawing.Point(7, 30);
            this.uxTextViewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxTextViewer.Multiline = true;
            this.uxTextViewer.Name = "uxTextViewer";
            this.uxTextViewer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.uxTextViewer.Size = new System.Drawing.Size(851, 182);
            this.uxTextViewer.TabIndex = 1;
            this.uxTextViewer.TabStop = false;
            this.uxToolTip.SetToolTip(this.uxTextViewer, "This allows you to view the data for the seed you are editing. You can also save " +
        "the data for this seed by clicking a button.");
            // 
            // uxToolTip
            // 
            this.uxToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.uxToolTip.ToolTipTitle = "What This Does:";
            // 
            // uxSaveSeed
            // 
            this.uxSaveSeed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uxSaveSeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSaveSeed.Location = new System.Drawing.Point(7, 220);
            this.uxSaveSeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxSaveSeed.Name = "uxSaveSeed";
            this.uxSaveSeed.Size = new System.Drawing.Size(182, 43);
            this.uxSaveSeed.TabIndex = 4;
            this.uxSaveSeed.Text = "Save Row Data";
            this.uxToolTip.SetToolTip(this.uxSaveSeed, "Allows you to save the data for this seed. Won\'t affect anything unless you also " +
        "save the file after doing this.");
            this.uxSaveSeed.UseVisualStyleBackColor = true;
            this.uxSaveSeed.Click += new System.EventHandler(this.SaveSeedData);
            // 
            // uxHeaderLog
            // 
            this.uxHeaderLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxHeaderLog.Location = new System.Drawing.Point(12, 67);
            this.uxHeaderLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxHeaderLog.Multiline = true;
            this.uxHeaderLog.Name = "uxHeaderLog";
            this.uxHeaderLog.ReadOnly = true;
            this.uxHeaderLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uxHeaderLog.Size = new System.Drawing.Size(858, 153);
            this.uxHeaderLog.TabIndex = 0;
            this.uxHeaderLog.Text = "Hello and welcome to 24 Mar 21 v2.4 of ImageJ Data Importation Program";
            this.uxToolTip.SetToolTip(this.uxHeaderLog, "This is the header of the program");
            // 
            // uxCurrentDateTime
            // 
            this.uxCurrentDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCurrentDateTime.Location = new System.Drawing.Point(12, 31);
            this.uxCurrentDateTime.Name = "uxCurrentDateTime";
            this.uxCurrentDateTime.ReadOnly = true;
            this.uxCurrentDateTime.Size = new System.Drawing.Size(858, 29);
            this.uxCurrentDateTime.TabIndex = 9;
            this.uxCurrentDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uxToolTip.SetToolTip(this.uxCurrentDateTime, "Shows the current date and time");
            // 
            // uxLockListSelection
            // 
            this.uxLockListSelection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uxLockListSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxLockListSelection.Location = new System.Drawing.Point(458, 220);
            this.uxLockListSelection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxLockListSelection.Name = "uxLockListSelection";
            this.uxLockListSelection.Size = new System.Drawing.Size(182, 43);
            this.uxLockListSelection.TabIndex = 6;
            this.uxLockListSelection.Text = "Lock List Selection";
            this.uxToolTip.SetToolTip(this.uxLockListSelection, "Allows you to lock the list of rows so that you won\'t accidentally change the sel" +
        "ection.");
            this.uxLockListSelection.UseVisualStyleBackColor = true;
            this.uxLockListSelection.Click += new System.EventHandler(this.uxLockListSelectionClick);
            // 
            // uxRowDisplayGroup
            // 
            this.uxRowDisplayGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxRowDisplayGroup.Controls.Add(this.uxRowListView);
            this.uxRowDisplayGroup.Controls.Add(this.uxLockListSelection);
            this.uxRowDisplayGroup.Controls.Add(this.uxTextViewer);
            this.uxRowDisplayGroup.Controls.Add(this.uxSaveSeed);
            this.uxRowDisplayGroup.Controls.Add(this.uxViewRow);
            this.uxRowDisplayGroup.Controls.Add(this.uxEditRow);
            this.uxRowDisplayGroup.Enabled = false;
            this.uxRowDisplayGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxRowDisplayGroup.Location = new System.Drawing.Point(12, 228);
            this.uxRowDisplayGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.uxRowDisplayGroup.Name = "uxRowDisplayGroup";
            this.uxRowDisplayGroup.Padding = new System.Windows.Forms.Padding(4, 4, 6, 0);
            this.uxRowDisplayGroup.Size = new System.Drawing.Size(867, 643);
            this.uxRowDisplayGroup.TabIndex = 6;
            this.uxRowDisplayGroup.TabStop = false;
            this.uxRowDisplayGroup.Text = "Row Display";
            // 
            // uxRowListView
            // 
            this.uxRowListView.AllColumns.Add(this.rowName);
            this.uxRowListView.AllColumns.Add(this.rowArea);
            this.uxRowListView.AllColumns.Add(this.rowX);
            this.uxRowListView.AllColumns.Add(this.rowY);
            this.uxRowListView.AllColumns.Add(this.rowPerim);
            this.uxRowListView.AllColumns.Add(this.rowMajor);
            this.uxRowListView.AllColumns.Add(this.rowMinor);
            this.uxRowListView.AllColumns.Add(this.rowAngle);
            this.uxRowListView.AllColumns.Add(this.rowCirc);
            this.uxRowListView.AllColumns.Add(this.rowAR);
            this.uxRowListView.AllColumns.Add(this.rowRound);
            this.uxRowListView.AllColumns.Add(this.rowSolidity);
            this.uxRowListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.uxRowListView.CellEditUseWholeCell = false;
            this.uxRowListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.rowName,
            this.rowArea,
            this.rowX,
            this.rowY,
            this.rowPerim,
            this.rowMajor,
            this.rowMinor});
            this.uxRowListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.uxRowListView.FullRowSelect = true;
            this.uxRowListView.GridLines = true;
            this.uxRowListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.uxRowListView.HeaderUsesThemes = true;
            this.uxRowListView.HideSelection = false;
            this.uxRowListView.Location = new System.Drawing.Point(7, 270);
            this.uxRowListView.Name = "uxRowListView";
            this.uxRowListView.ShowCommandMenuOnRightClick = true;
            this.uxRowListView.ShowSortIndicators = false;
            this.uxRowListView.Size = new System.Drawing.Size(851, 361);
            this.uxRowListView.SortGroupItemsByPrimaryColumn = false;
            this.uxRowListView.TabIndex = 10;
            this.uxRowListView.UseCompatibleStateImageBehavior = false;
            this.uxRowListView.View = System.Windows.Forms.View.Details;
            this.uxRowListView.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.uxRowListView_CellEditFinished);
            // 
            // rowName
            // 
            this.rowName.AspectName = "RowNum";
            this.rowName.Text = "Row";
            this.rowName.Width = 82;
            // 
            // rowArea
            // 
            this.rowArea.AspectName = "Area";
            this.rowArea.Text = "Area";
            this.rowArea.Width = 112;
            // 
            // rowX
            // 
            this.rowX.AspectName = "X";
            this.rowX.Text = "X";
            this.rowX.Width = 57;
            // 
            // rowY
            // 
            this.rowY.AspectName = "Y";
            this.rowY.Text = "Y";
            this.rowY.Width = 91;
            // 
            // rowPerim
            // 
            this.rowPerim.AspectName = "Perim";
            this.rowPerim.Text = "Perim";
            this.rowPerim.Width = 77;
            // 
            // rowMajor
            // 
            this.rowMajor.AspectName = "Major";
            this.rowMajor.Text = "Major";
            this.rowMajor.Width = 71;
            // 
            // rowMinor
            // 
            this.rowMinor.AspectName = "Minor";
            this.rowMinor.Text = "Minor";
            this.rowMinor.Width = 82;
            // 
            // rowAngle
            // 
            this.rowAngle.AspectName = "Angle";
            this.rowAngle.DisplayIndex = 7;
            this.rowAngle.IsVisible = false;
            this.rowAngle.Text = "Angle";
            // 
            // rowCirc
            // 
            this.rowCirc.AspectName = "Circ";
            this.rowCirc.DisplayIndex = 8;
            this.rowCirc.IsVisible = false;
            this.rowCirc.Text = "Circ";
            // 
            // rowAR
            // 
            this.rowAR.AspectName = "AR";
            this.rowAR.DisplayIndex = 9;
            this.rowAR.IsVisible = false;
            this.rowAR.Text = "AR";
            // 
            // rowRound
            // 
            this.rowRound.AspectName = "Round";
            this.rowRound.DisplayIndex = 10;
            this.rowRound.IsVisible = false;
            this.rowRound.Text = "Round";
            // 
            // rowSolidity
            // 
            this.rowSolidity.AspectName = "Solidity";
            this.rowSolidity.DisplayIndex = 11;
            this.rowSolidity.IsVisible = false;
            this.rowSolidity.Text = "Solidity";
            // 
            // uxGridDisplay
            // 
            this.uxGridDisplay.AutoSize = true;
            this.uxGridDisplay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxGridDisplay.Controls.Add(this.uxStartReference);
            this.uxGridDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxGridDisplay.Location = new System.Drawing.Point(915, 31);
            this.uxGridDisplay.Name = "uxGridDisplay";
            this.uxGridDisplay.Size = new System.Drawing.Size(105, 118);
            this.uxGridDisplay.TabIndex = 8;
            this.uxGridDisplay.TabStop = false;
            this.uxGridDisplay.Text = "Grid Display";
            // 
            // uxStartReference
            // 
            this.uxStartReference.Location = new System.Drawing.Point(24, 52);
            this.uxStartReference.Name = "uxStartReference";
            this.uxStartReference.Size = new System.Drawing.Size(75, 41);
            this.uxStartReference.TabIndex = 0;
            this.uxStartReference.Text = "Reference Button";
            this.uxStartReference.UseVisualStyleBackColor = true;
            this.uxStartReference.Visible = false;
            // 
            // View
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(901, 519);
            this.Controls.Add(this.uxCurrentDateTime);
            this.Controls.Add(this.uxGridDisplay);
            this.Controls.Add(this.uxHeaderLog);
            this.Controls.Add(this.uxRowDisplayGroup);
            this.Controls.Add(this.ViewFormMenuStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ViewFormMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "View";
            this.Text = "NS ImageJ Data 24 Mar 21 v2.4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseForm);
            this.Load += new System.EventHandler(this.OpenForm);
            this.ViewFormMenuStrip.ResumeLayout(false);
            this.ViewFormMenuStrip.PerformLayout();
            this.uxRowDisplayGroup.ResumeLayout(false);
            this.uxRowDisplayGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxRowListView)).EndInit();
            this.uxGridDisplay.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ViewFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxMenuOpenFile;
        private System.Windows.Forms.ToolStripMenuItem uxMenuSaveFile;
        private System.Windows.Forms.ToolStripMenuItem uxMenuSaveFileAs;
        private System.Windows.Forms.ToolTip uxToolTip;
        private System.Windows.Forms.Button uxEditRow;
        private System.Windows.Forms.Button uxViewRow;
        private System.Windows.Forms.TextBox uxTextViewer;
        private System.Windows.Forms.Button uxSaveSeed;
        private System.Windows.Forms.GroupBox uxRowDisplayGroup;
        private System.Windows.Forms.ToolStripMenuItem uxMenuCloseFile;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleWordWrappingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxCurrentFilenameRequest;
        private System.Windows.Forms.TextBox uxHeaderLog;
        private System.Windows.Forms.GroupBox uxGridDisplay;
        private System.Windows.Forms.Button uxStartReference;
        private System.Windows.Forms.TextBox uxCurrentDateTime;
        private System.Windows.Forms.Button uxLockListSelection;
        private BrightIdeasSoftware.ObjectListView uxRowListView;
        private BrightIdeasSoftware.OLVColumn rowName;
        private BrightIdeasSoftware.OLVColumn rowArea;
        private BrightIdeasSoftware.OLVColumn rowX;
        private BrightIdeasSoftware.OLVColumn rowY;
        private BrightIdeasSoftware.OLVColumn rowPerim;
        private BrightIdeasSoftware.OLVColumn rowMajor;
        private BrightIdeasSoftware.OLVColumn rowMinor;
        private BrightIdeasSoftware.OLVColumn rowAngle;
        private BrightIdeasSoftware.OLVColumn rowCirc;
        private BrightIdeasSoftware.OLVColumn rowAR;
        private BrightIdeasSoftware.OLVColumn rowRound;
        private BrightIdeasSoftware.OLVColumn rowSolidity;
    }
}

