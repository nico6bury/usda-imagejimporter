
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
            this.uxMainMenuToolStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenuOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenuSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenuSaveFileAs = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenuCloseFile = new System.Windows.Forms.ToolStripMenuItem();
            this.excelEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenuItemToggleListDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.uxToggleGroupsCollapsed = new System.Windows.Forms.ToolStripMenuItem();
            this.uxConfigureColorLevelsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxAskWhereStart = new System.Windows.Forms.ToolStripMenuItem();
            this.uxAskSelectFile = new System.Windows.Forms.ToolStripMenuItem();
            this.uxAskConfigFileStorage = new System.Windows.Forms.ToolStripMenuItem();
            this.uxAskAvailableOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.uxAskLogFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.uxAskListFunctions = new System.Windows.Forms.ToolStripMenuItem();
            this.uxAskGridFunctions = new System.Windows.Forms.ToolStripMenuItem();
            this.uxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.uxCurrentDateTime = new System.Windows.Forms.TextBox();
            this.uxHeaderLog = new System.Windows.Forms.RichTextBox();
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
            this.uxProcessingPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uxGridPanel = new System.Windows.Forms.Panel();
            this.uxStartReference = new System.Windows.Forms.Button();
            this.uxGridListView = new BrightIdeasSoftware.ObjectListView();
            this.uxMainMenuToolStrip.SuspendLayout();
            this.uxRowDisplayGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxRowListView)).BeginInit();
            this.uxGridDisplay.SuspendLayout();
            this.uxGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxGridListView)).BeginInit();
            this.SuspendLayout();
            // 
            // uxMainMenuToolStrip
            // 
            this.uxMainMenuToolStrip.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxMainMenuToolStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.uxMainMenuToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.uxMainMenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.uxMainMenuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.uxMainMenuToolStrip.Name = "uxMainMenuToolStrip";
            this.uxMainMenuToolStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.uxMainMenuToolStrip.Size = new System.Drawing.Size(1048, 36);
            this.uxMainMenuToolStrip.TabIndex = 0;
            this.uxMainMenuToolStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxMenuOpenFile,
            this.uxMenuSaveFile,
            this.uxMenuSaveFileAs,
            this.uxMenuCloseFile,
            this.excelEToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(58, 32);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // uxMenuOpenFile
            // 
            this.uxMenuOpenFile.Name = "uxMenuOpenFile";
            this.uxMenuOpenFile.Size = new System.Drawing.Size(292, 36);
            this.uxMenuOpenFile.Text = "Open";
            this.uxMenuOpenFile.ToolTipText = "Allows you to load a file into the program. It\'s meant to use .txt files, so I\'m " +
    "not sure what would happen if you imported other file types.";
            this.uxMenuOpenFile.Click += new System.EventHandler(this.OpenFile);
            // 
            // uxMenuSaveFile
            // 
            this.uxMenuSaveFile.Enabled = false;
            this.uxMenuSaveFile.Name = "uxMenuSaveFile";
            this.uxMenuSaveFile.Size = new System.Drawing.Size(292, 36);
            this.uxMenuSaveFile.Text = "Save";
            this.uxMenuSaveFile.ToolTipText = "Allows you to save the current data back into the original file you opened.\r\nCurr" +
    "ently Unavailable.";
            this.uxMenuSaveFile.Click += new System.EventHandler(this.SaveFile);
            // 
            // uxMenuSaveFileAs
            // 
            this.uxMenuSaveFileAs.Enabled = false;
            this.uxMenuSaveFileAs.Name = "uxMenuSaveFileAs";
            this.uxMenuSaveFileAs.Size = new System.Drawing.Size(292, 36);
            this.uxMenuSaveFileAs.Text = "Save As";
            this.uxMenuSaveFileAs.ToolTipText = "Allows you to save the seed data you\'ve edited as a new .txt file with the same s" +
    "tructure as the original file.\r\nCurrently Unavailable.";
            this.uxMenuSaveFileAs.Click += new System.EventHandler(this.SaveFileAs);
            // 
            // uxMenuCloseFile
            // 
            this.uxMenuCloseFile.Name = "uxMenuCloseFile";
            this.uxMenuCloseFile.Size = new System.Drawing.Size(292, 36);
            this.uxMenuCloseFile.Text = "Close";
            this.uxMenuCloseFile.ToolTipText = "Closes the currently loaded file without saving changes or exiting the program.";
            this.uxMenuCloseFile.Click += new System.EventHandler(this.CloseFile);
            // 
            // excelEToolStripMenuItem
            // 
            this.excelEToolStripMenuItem.Name = "excelEToolStripMenuItem";
            this.excelEToolStripMenuItem.Size = new System.Drawing.Size(292, 36);
            this.excelEToolStripMenuItem.Text = "Excel Column Export";
            this.excelEToolStripMenuItem.Click += new System.EventHandler(this.excelEToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxMenuItemToggleListDisplay,
            this.uxToggleGroupsCollapsed,
            this.uxConfigureColorLevelsMenuItem});
            this.optionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(98, 32);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // uxMenuItemToggleListDisplay
            // 
            this.uxMenuItemToggleListDisplay.Name = "uxMenuItemToggleListDisplay";
            this.uxMenuItemToggleListDisplay.Size = new System.Drawing.Size(404, 36);
            this.uxMenuItemToggleListDisplay.Text = "Toggle List Display";
            this.uxMenuItemToggleListDisplay.Click += new System.EventHandler(this.uxMenuItemToggleListDisplay_Click);
            // 
            // uxToggleGroupsCollapsed
            // 
            this.uxToggleGroupsCollapsed.Name = "uxToggleGroupsCollapsed";
            this.uxToggleGroupsCollapsed.Size = new System.Drawing.Size(404, 36);
            this.uxToggleGroupsCollapsed.Text = "Toggle Collapsed-ness Of Groups";
            this.uxToggleGroupsCollapsed.Click += new System.EventHandler(this.uxToggleGroupsCollapsed_Click);
            // 
            // uxConfigureColorLevelsMenuItem
            // 
            this.uxConfigureColorLevelsMenuItem.Name = "uxConfigureColorLevelsMenuItem";
            this.uxConfigureColorLevelsMenuItem.Size = new System.Drawing.Size(404, 36);
            this.uxConfigureColorLevelsMenuItem.Text = "Configure Color Levels";
            this.uxConfigureColorLevelsMenuItem.Click += new System.EventHandler(this.uxConfigureColorLevelsMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxAskWhereStart,
            this.uxAskSelectFile,
            this.uxAskConfigFileStorage,
            this.uxAskAvailableOptions,
            this.uxAskLogFunction,
            this.uxAskListFunctions,
            this.uxAskGridFunctions});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(69, 32);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // uxAskWhereStart
            // 
            this.uxAskWhereStart.Name = "uxAskWhereStart";
            this.uxAskWhereStart.Size = new System.Drawing.Size(502, 36);
            this.uxAskWhereStart.Text = "Where to Start?";
            this.uxAskWhereStart.Click += new System.EventHandler(this.uxAskWhereStart_Click);
            // 
            // uxAskSelectFile
            // 
            this.uxAskSelectFile.Name = "uxAskSelectFile";
            this.uxAskSelectFile.Size = new System.Drawing.Size(502, 36);
            this.uxAskSelectFile.Text = "Selecting A File/Grid";
            this.uxAskSelectFile.Click += new System.EventHandler(this.uxAskSelectFile_Click);
            // 
            // uxAskConfigFileStorage
            // 
            this.uxAskConfigFileStorage.Name = "uxAskConfigFileStorage";
            this.uxAskConfigFileStorage.Size = new System.Drawing.Size(502, 36);
            this.uxAskConfigFileStorage.Text = "What\'s in Config File?";
            this.uxAskConfigFileStorage.Click += new System.EventHandler(this.uxAskConfigFileStorage_Click);
            // 
            // uxAskAvailableOptions
            // 
            this.uxAskAvailableOptions.Name = "uxAskAvailableOptions";
            this.uxAskAvailableOptions.Size = new System.Drawing.Size(502, 36);
            this.uxAskAvailableOptions.Text = "What Options Are Available?";
            this.uxAskAvailableOptions.Click += new System.EventHandler(this.uxAskAvailableOptions_Click);
            // 
            // uxAskLogFunction
            // 
            this.uxAskLogFunction.Name = "uxAskLogFunction";
            this.uxAskLogFunction.Size = new System.Drawing.Size(502, 36);
            this.uxAskLogFunction.Text = "What\'s the Log do?";
            this.uxAskLogFunction.Click += new System.EventHandler(this.uxAskLogFunction_Click);
            // 
            // uxAskListFunctions
            // 
            this.uxAskListFunctions.Name = "uxAskListFunctions";
            this.uxAskListFunctions.Size = new System.Drawing.Size(502, 36);
            this.uxAskListFunctions.Text = "What Functions Does the List Display Have?";
            this.uxAskListFunctions.Click += new System.EventHandler(this.uxAskListFunctions_Click);
            // 
            // uxAskGridFunctions
            // 
            this.uxAskGridFunctions.Name = "uxAskGridFunctions";
            this.uxAskGridFunctions.Size = new System.Drawing.Size(502, 36);
            this.uxAskGridFunctions.Text = "What Functions Does the Grid Display Have?";
            this.uxAskGridFunctions.Click += new System.EventHandler(this.uxAskGridFunctions_Click);
            // 
            // uxToolTip
            // 
            this.uxToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.uxToolTip.ToolTipTitle = "What This Does:";
            // 
            // uxCurrentDateTime
            // 
            this.uxCurrentDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCurrentDateTime.Location = new System.Drawing.Point(12, 31);
            this.uxCurrentDateTime.Name = "uxCurrentDateTime";
            this.uxCurrentDateTime.ReadOnly = true;
            this.uxCurrentDateTime.Size = new System.Drawing.Size(605, 40);
            this.uxCurrentDateTime.TabIndex = 9;
            this.uxCurrentDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uxToolTip.SetToolTip(this.uxCurrentDateTime, "Shows the current date and time");
            // 
            // uxHeaderLog
            // 
            this.uxHeaderLog.AcceptsTab = true;
            this.uxHeaderLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.uxHeaderLog.Location = new System.Drawing.Point(12, 66);
            this.uxHeaderLog.Name = "uxHeaderLog";
            this.uxHeaderLog.Size = new System.Drawing.Size(605, 155);
            this.uxHeaderLog.TabIndex = 11;
            this.uxHeaderLog.Text = "Hello and welcome to ImageJ Data Processing\nProject Name: ImageJImporter\tv2.9.7B " +
    "NS 5 November 2021\nby Nicholas Sixbury / Brabec\tUSDA-ARS Manhattan, KS\n";
            this.uxToolTip.SetToolTip(this.uxHeaderLog, "This acts as both the log and header to the program. As you take actions in the p" +
        "rogram, this will be updated, and when you exit the program, any text here will " +
        "be exported to an external file.");
            // 
            // uxRowDisplayGroup
            // 
            this.uxRowDisplayGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxRowDisplayGroup.Controls.Add(this.uxRowListView);
            this.uxRowDisplayGroup.Enabled = false;
            this.uxRowDisplayGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxRowDisplayGroup.Location = new System.Drawing.Point(12, 367);
            this.uxRowDisplayGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.uxRowDisplayGroup.Name = "uxRowDisplayGroup";
            this.uxRowDisplayGroup.Padding = new System.Windows.Forms.Padding(4, 4, 6, 0);
            this.uxRowDisplayGroup.Size = new System.Drawing.Size(605, 273);
            this.uxRowDisplayGroup.TabIndex = 6;
            this.uxRowDisplayGroup.TabStop = false;
            this.uxRowDisplayGroup.Text = "List Display";
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
            this.uxRowListView.EmptyListMsg = "No Grid Selected";
            this.uxRowListView.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxRowListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxRowListView.FullRowSelect = true;
            this.uxRowListView.GridLines = true;
            this.uxRowListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.uxRowListView.HeaderUsesThemes = true;
            this.uxRowListView.HideSelection = false;
            this.uxRowListView.IncludeColumnHeadersInCopy = true;
            this.uxRowListView.IncludeHiddenColumnsInDataTransfer = true;
            this.uxRowListView.Location = new System.Drawing.Point(7, 26);
            this.uxRowListView.Name = "uxRowListView";
            this.uxRowListView.ShowCommandMenuOnRightClick = true;
            this.uxRowListView.ShowSortIndicators = false;
            this.uxRowListView.Size = new System.Drawing.Size(589, 235);
            this.uxRowListView.SortGroupItemsByPrimaryColumn = false;
            this.uxRowListView.TabIndex = 10;
            this.uxRowListView.UseCompatibleStateImageBehavior = false;
            this.uxRowListView.View = System.Windows.Forms.View.Details;
            this.uxRowListView.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.uxRowListView_CellEditFinished);
            this.uxRowListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.uxRowListView_FormatRow);
            // 
            // rowName
            // 
            this.rowName.AspectName = "RowNum";
            this.rowName.Text = "Row";
            this.rowName.Width = 73;
            // 
            // rowArea
            // 
            this.rowArea.AspectName = "Area";
            this.rowArea.Text = "Area";
            this.rowArea.Width = 95;
            // 
            // rowX
            // 
            this.rowX.AspectName = "X";
            this.rowX.Text = "X";
            this.rowX.Width = 40;
            // 
            // rowY
            // 
            this.rowY.AspectName = "Y";
            this.rowY.Text = "Y";
            this.rowY.Width = 76;
            // 
            // rowPerim
            // 
            this.rowPerim.AspectName = "Perim";
            this.rowPerim.Text = "Perim";
            this.rowPerim.Width = 72;
            // 
            // rowMajor
            // 
            this.rowMajor.AspectName = "Major";
            this.rowMajor.Text = "Major";
            this.rowMajor.Width = 61;
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
            this.uxGridDisplay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxGridDisplay.Controls.Add(this.uxProcessingPanel);
            this.uxGridDisplay.Controls.Add(this.uxGridPanel);
            this.uxGridDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxGridDisplay.Location = new System.Drawing.Point(623, 31);
            this.uxGridDisplay.Name = "uxGridDisplay";
            this.uxGridDisplay.Size = new System.Drawing.Size(243, 154);
            this.uxGridDisplay.TabIndex = 8;
            this.uxGridDisplay.TabStop = false;
            this.uxGridDisplay.Text = "Grid Display";
            this.uxGridDisplay.Visible = false;
            // 
            // uxProcessingPanel
            // 
            this.uxProcessingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxProcessingPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.uxProcessingPanel.Location = new System.Drawing.Point(7, 71);
            this.uxProcessingPanel.Name = "uxProcessingPanel";
            this.uxProcessingPanel.Size = new System.Drawing.Size(200, 57);
            this.uxProcessingPanel.TabIndex = 11;
            // 
            // uxGridPanel
            // 
            this.uxGridPanel.AutoSize = true;
            this.uxGridPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxGridPanel.BackColor = System.Drawing.Color.Moccasin;
            this.uxGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.uxGridPanel.Controls.Add(this.uxStartReference);
            this.uxGridPanel.Location = new System.Drawing.Point(6, 25);
            this.uxGridPanel.Name = "uxGridPanel";
            this.uxGridPanel.Size = new System.Drawing.Size(39, 34);
            this.uxGridPanel.TabIndex = 10;
            this.uxGridPanel.SizeChanged += new System.EventHandler(this.uxGridPanel_SizeChanged);
            // 
            // uxStartReference
            // 
            this.uxStartReference.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.uxStartReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.uxStartReference.Location = new System.Drawing.Point(3, 3);
            this.uxStartReference.Name = "uxStartReference";
            this.uxStartReference.Size = new System.Drawing.Size(29, 24);
            this.uxStartReference.TabIndex = 0;
            this.uxStartReference.Text = "Reference Button";
            this.uxStartReference.UseVisualStyleBackColor = true;
            this.uxStartReference.Visible = false;
            // 
            // uxGridListView
            // 
            this.uxGridListView.CellEditUseWholeCell = false;
            this.uxGridListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.uxGridListView.EmptyListMsg = "No Files Loaded";
            this.uxGridListView.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxGridListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxGridListView.FullRowSelect = true;
            this.uxGridListView.GridLines = true;
            this.uxGridListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.uxGridListView.HideSelection = false;
            this.uxGridListView.IncludeColumnHeadersInCopy = true;
            this.uxGridListView.Location = new System.Drawing.Point(12, 227);
            this.uxGridListView.Name = "uxGridListView";
            this.uxGridListView.ShowGroups = false;
            this.uxGridListView.Size = new System.Drawing.Size(605, 133);
            this.uxGridListView.TabIndex = 10;
            this.uxGridListView.UseCompatibleStateImageBehavior = false;
            this.uxGridListView.View = System.Windows.Forms.View.Details;
            this.uxGridListView.DoubleClick += new System.EventHandler(this.uxGridListView_DoubleClick);
            // 
            // View
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(1074, 562);
            this.Controls.Add(this.uxHeaderLog);
            this.Controls.Add(this.uxGridListView);
            this.Controls.Add(this.uxGridDisplay);
            this.Controls.Add(this.uxCurrentDateTime);
            this.Controls.Add(this.uxRowDisplayGroup);
            this.Controls.Add(this.uxMainMenuToolStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.uxMainMenuToolStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "View";
            this.Text = "NS ImageJ Data 5 November v2.9.7B";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseForm);
            this.Load += new System.EventHandler(this.OpenForm);
            this.uxMainMenuToolStrip.ResumeLayout(false);
            this.uxMainMenuToolStrip.PerformLayout();
            this.uxRowDisplayGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxRowListView)).EndInit();
            this.uxGridDisplay.ResumeLayout(false);
            this.uxGridDisplay.PerformLayout();
            this.uxGridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxGridListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip uxMainMenuToolStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxMenuOpenFile;
        private System.Windows.Forms.ToolStripMenuItem uxMenuSaveFile;
        private System.Windows.Forms.ToolStripMenuItem uxMenuSaveFileAs;
        private System.Windows.Forms.ToolTip uxToolTip;
        private System.Windows.Forms.GroupBox uxRowDisplayGroup;
        private System.Windows.Forms.ToolStripMenuItem uxMenuCloseFile;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.GroupBox uxGridDisplay;
        private System.Windows.Forms.Button uxStartReference;
        private System.Windows.Forms.TextBox uxCurrentDateTime;
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
        private System.Windows.Forms.Panel uxGridPanel;
        private System.Windows.Forms.ToolStripMenuItem uxMenuItemToggleListDisplay;
        private System.Windows.Forms.ToolStripMenuItem uxToggleGroupsCollapsed;
        private System.Windows.Forms.ToolStripMenuItem uxConfigureColorLevelsMenuItem;
        private System.Windows.Forms.FlowLayoutPanel uxProcessingPanel;
        private BrightIdeasSoftware.ObjectListView uxGridListView;
        private System.Windows.Forms.RichTextBox uxHeaderLog;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxAskWhereStart;
        private System.Windows.Forms.ToolStripMenuItem uxAskSelectFile;
        private System.Windows.Forms.ToolStripMenuItem uxAskConfigFileStorage;
        private System.Windows.Forms.ToolStripMenuItem uxAskAvailableOptions;
        private System.Windows.Forms.ToolStripMenuItem uxAskLogFunction;
        private System.Windows.Forms.ToolStripMenuItem uxAskListFunctions;
        private System.Windows.Forms.ToolStripMenuItem uxAskGridFunctions;
        private System.Windows.Forms.ToolStripMenuItem excelEToolStripMenuItem;
    }
}

