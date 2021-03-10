
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
            this.uxSeedList = new System.Windows.Forms.ListBox();
            this.uxEditSeed = new System.Windows.Forms.Button();
            this.uxViewSeed = new System.Windows.Forms.Button();
            this.uxTextViewer = new System.Windows.Forms.TextBox();
            this.uxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.uxSaveSeed = new System.Windows.Forms.Button();
            this.uxHeaderLog = new System.Windows.Forms.TextBox();
            this.uxSeedDisplayGroup = new System.Windows.Forms.GroupBox();
            this.uxGridDisplay = new System.Windows.Forms.GroupBox();
            this.uxStartReference = new System.Windows.Forms.Button();
            this.ViewFormMenuStrip.SuspendLayout();
            this.uxSeedDisplayGroup.SuspendLayout();
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
            this.ViewFormMenuStrip.Size = new System.Drawing.Size(1016, 28);
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
            // uxSeedList
            // 
            this.uxSeedList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSeedList.FormattingEnabled = true;
            this.uxSeedList.HorizontalScrollbar = true;
            this.uxSeedList.ItemHeight = 20;
            this.uxSeedList.Location = new System.Drawing.Point(12, 271);
            this.uxSeedList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxSeedList.Name = "uxSeedList";
            this.uxSeedList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.uxSeedList.Size = new System.Drawing.Size(791, 244);
            this.uxSeedList.TabIndex = 1;
            this.uxToolTip.SetToolTip(this.uxSeedList, "This is a list of all the seeds loaded into the program. You have to select a see" +
        "d here before you can edit or view its information.");
            // 
            // uxEditSeed
            // 
            this.uxEditSeed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uxEditSeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxEditSeed.Location = new System.Drawing.Point(7, 220);
            this.uxEditSeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxEditSeed.Name = "uxEditSeed";
            this.uxEditSeed.Size = new System.Drawing.Size(182, 43);
            this.uxEditSeed.TabIndex = 3;
            this.uxEditSeed.Text = "Edit Row Data";
            this.uxToolTip.SetToolTip(this.uxEditSeed, "Allows you to start editing the seed you have selected in the list to the left.");
            this.uxEditSeed.UseVisualStyleBackColor = true;
            this.uxEditSeed.Click += new System.EventHandler(this.EditSeedData);
            // 
            // uxViewSeed
            // 
            this.uxViewSeed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uxViewSeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxViewSeed.Location = new System.Drawing.Point(621, 220);
            this.uxViewSeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxViewSeed.Name = "uxViewSeed";
            this.uxViewSeed.Size = new System.Drawing.Size(182, 43);
            this.uxViewSeed.TabIndex = 2;
            this.uxViewSeed.Text = "View Row Data";
            this.uxToolTip.SetToolTip(this.uxViewSeed, "Allows you to view the data for the seed you have selected without the worry of a" +
        "ccidentally editing it.");
            this.uxViewSeed.UseVisualStyleBackColor = true;
            this.uxViewSeed.Click += new System.EventHandler(this.ViewSeedData);
            // 
            // uxTextViewer
            // 
            this.uxTextViewer.AcceptsReturn = true;
            this.uxTextViewer.AcceptsTab = true;
            this.uxTextViewer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxTextViewer.Location = new System.Drawing.Point(7, 30);
            this.uxTextViewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxTextViewer.Multiline = true;
            this.uxTextViewer.Name = "uxTextViewer";
            this.uxTextViewer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.uxTextViewer.Size = new System.Drawing.Size(796, 182);
            this.uxTextViewer.TabIndex = 5;
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
            this.uxSaveSeed.Location = new System.Drawing.Point(323, 220);
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
            this.uxHeaderLog.Location = new System.Drawing.Point(19, 32);
            this.uxHeaderLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxHeaderLog.Multiline = true;
            this.uxHeaderLog.Name = "uxHeaderLog";
            this.uxHeaderLog.ReadOnly = true;
            this.uxHeaderLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uxHeaderLog.Size = new System.Drawing.Size(812, 188);
            this.uxHeaderLog.TabIndex = 7;
            this.uxHeaderLog.Text = "Hello and welcome to v2 of ImageJ Data Importation Program";
            this.uxToolTip.SetToolTip(this.uxHeaderLog, "This is the header of the program");
            // 
            // uxSeedDisplayGroup
            // 
            this.uxSeedDisplayGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxSeedDisplayGroup.Controls.Add(this.uxTextViewer);
            this.uxSeedDisplayGroup.Controls.Add(this.uxSaveSeed);
            this.uxSeedDisplayGroup.Controls.Add(this.uxViewSeed);
            this.uxSeedDisplayGroup.Controls.Add(this.uxSeedList);
            this.uxSeedDisplayGroup.Controls.Add(this.uxEditSeed);
            this.uxSeedDisplayGroup.Enabled = false;
            this.uxSeedDisplayGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSeedDisplayGroup.Location = new System.Drawing.Point(12, 228);
            this.uxSeedDisplayGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.uxSeedDisplayGroup.Name = "uxSeedDisplayGroup";
            this.uxSeedDisplayGroup.Padding = new System.Windows.Forms.Padding(4, 4, 6, 0);
            this.uxSeedDisplayGroup.Size = new System.Drawing.Size(819, 534);
            this.uxSeedDisplayGroup.TabIndex = 6;
            this.uxSeedDisplayGroup.TabStop = false;
            this.uxSeedDisplayGroup.Text = "Seed Display";
            // 
            // uxGridDisplay
            // 
            this.uxGridDisplay.AutoSize = true;
            this.uxGridDisplay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxGridDisplay.Controls.Add(this.uxStartReference);
            this.uxGridDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxGridDisplay.Location = new System.Drawing.Point(901, 32);
            this.uxGridDisplay.Name = "uxGridDisplay";
            this.uxGridDisplay.Size = new System.Drawing.Size(115, 122);
            this.uxGridDisplay.TabIndex = 8;
            this.uxGridDisplay.TabStop = false;
            this.uxGridDisplay.Text = "Grid Display";
            // 
            // uxStartReference
            // 
            this.uxStartReference.Location = new System.Drawing.Point(34, 56);
            this.uxStartReference.Name = "uxStartReference";
            this.uxStartReference.Size = new System.Drawing.Size(75, 41);
            this.uxStartReference.TabIndex = 0;
            this.uxStartReference.Text = "button1";
            this.uxStartReference.UseVisualStyleBackColor = true;
            this.uxStartReference.Visible = false;
            // 
            // View
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(855, 481);
            this.Controls.Add(this.uxGridDisplay);
            this.Controls.Add(this.uxHeaderLog);
            this.Controls.Add(this.uxSeedDisplayGroup);
            this.Controls.Add(this.ViewFormMenuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ViewFormMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "View";
            this.Text = "NS ImageJ Data v2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseForm);
            this.Load += new System.EventHandler(this.OpenForm);
            this.ViewFormMenuStrip.ResumeLayout(false);
            this.ViewFormMenuStrip.PerformLayout();
            this.uxSeedDisplayGroup.ResumeLayout(false);
            this.uxSeedDisplayGroup.PerformLayout();
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
        private System.Windows.Forms.ListBox uxSeedList;
        private System.Windows.Forms.ToolTip uxToolTip;
        private System.Windows.Forms.Button uxEditSeed;
        private System.Windows.Forms.Button uxViewSeed;
        private System.Windows.Forms.TextBox uxTextViewer;
        private System.Windows.Forms.Button uxSaveSeed;
        private System.Windows.Forms.GroupBox uxSeedDisplayGroup;
        private System.Windows.Forms.ToolStripMenuItem uxMenuCloseFile;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleWordWrappingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxCurrentFilenameRequest;
        private System.Windows.Forms.TextBox uxHeaderLog;
        private System.Windows.Forms.GroupBox uxGridDisplay;
        private System.Windows.Forms.Button uxStartReference;
    }
}

