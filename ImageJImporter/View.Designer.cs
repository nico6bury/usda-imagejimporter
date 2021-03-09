
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.uxSeedDisplayGroup = new System.Windows.Forms.GroupBox();
            this.ViewFormMenuStrip.SuspendLayout();
            this.uxSeedDisplayGroup.SuspendLayout();
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
            this.ViewFormMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.ViewFormMenuStrip.Size = new System.Drawing.Size(1093, 25);
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
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // uxMenuOpenFile
            // 
            this.uxMenuOpenFile.Name = "uxMenuOpenFile";
            this.uxMenuOpenFile.Size = new System.Drawing.Size(121, 22);
            this.uxMenuOpenFile.Text = "Open";
            this.uxMenuOpenFile.ToolTipText = "Allows you to load a file into the program. It\'s meant to use .txt files, so I\'m " +
    "not sure what would happen if you imported other file types.";
            this.uxMenuOpenFile.Click += new System.EventHandler(this.OpenFile);
            // 
            // uxMenuSaveFile
            // 
            this.uxMenuSaveFile.Name = "uxMenuSaveFile";
            this.uxMenuSaveFile.Size = new System.Drawing.Size(121, 22);
            this.uxMenuSaveFile.Text = "Save";
            this.uxMenuSaveFile.ToolTipText = "Allows you to save the current data back into the original file you opened.";
            this.uxMenuSaveFile.Click += new System.EventHandler(this.SaveFile);
            // 
            // uxMenuSaveFileAs
            // 
            this.uxMenuSaveFileAs.Name = "uxMenuSaveFileAs";
            this.uxMenuSaveFileAs.Size = new System.Drawing.Size(121, 22);
            this.uxMenuSaveFileAs.Text = "Save As";
            this.uxMenuSaveFileAs.ToolTipText = "Allows you to save the seed data you\'ve edited as a new .txt file with the same s" +
    "tructure as the original file.";
            this.uxMenuSaveFileAs.Click += new System.EventHandler(this.SaveFileAs);
            // 
            // uxMenuCloseFile
            // 
            this.uxMenuCloseFile.Name = "uxMenuCloseFile";
            this.uxMenuCloseFile.Size = new System.Drawing.Size(121, 22);
            this.uxMenuCloseFile.Text = "Close";
            this.uxMenuCloseFile.ToolTipText = "Closes the currently loaded file without saving changes or exiting the program.";
            this.uxMenuCloseFile.Click += new System.EventHandler(this.CloseFile);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleWordWrappingToolStripMenuItem,
            this.uxCurrentFilenameRequest});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 21);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // toggleWordWrappingToolStripMenuItem
            // 
            this.toggleWordWrappingToolStripMenuItem.Name = "toggleWordWrappingToolStripMenuItem";
            this.toggleWordWrappingToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.toggleWordWrappingToolStripMenuItem.Text = "Toggle Word Wrapping";
            this.toggleWordWrappingToolStripMenuItem.ToolTipText = "Toggles whether or not the text in the seed display will wrap across lines";
            this.toggleWordWrappingToolStripMenuItem.Click += new System.EventHandler(this.ToggleWordWrap);
            // 
            // uxCurrentFilenameRequest
            // 
            this.uxCurrentFilenameRequest.Name = "uxCurrentFilenameRequest";
            this.uxCurrentFilenameRequest.Size = new System.Drawing.Size(205, 22);
            this.uxCurrentFilenameRequest.Text = "Tell Me Current Filename";
            this.uxCurrentFilenameRequest.Click += new System.EventHandler(this.AskForFilename);
            // 
            // uxSeedList
            // 
            this.uxSeedList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSeedList.FormattingEnabled = true;
            this.uxSeedList.ItemHeight = 20;
            this.uxSeedList.Location = new System.Drawing.Point(6, 297);
            this.uxSeedList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxSeedList.Name = "uxSeedList";
            this.uxSeedList.ScrollAlwaysVisible = true;
            this.uxSeedList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.uxSeedList.Size = new System.Drawing.Size(1047, 324);
            this.uxSeedList.TabIndex = 1;
            this.uxToolTip.SetToolTip(this.uxSeedList, "This is a list of all the seeds loaded into the program. You have to select a see" +
        "d here before you can edit or view its information.");
            // 
            // uxEditSeed
            // 
            this.uxEditSeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxEditSeed.Location = new System.Drawing.Point(426, 243);
            this.uxEditSeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxEditSeed.Name = "uxEditSeed";
            this.uxEditSeed.Size = new System.Drawing.Size(211, 46);
            this.uxEditSeed.TabIndex = 3;
            this.uxEditSeed.Text = "Edit Row Data";
            this.uxToolTip.SetToolTip(this.uxEditSeed, "Allows you to start editing the seed you have selected in the list to the left.");
            this.uxEditSeed.UseVisualStyleBackColor = true;
            this.uxEditSeed.Click += new System.EventHandler(this.EditSeedData);
            // 
            // uxViewSeed
            // 
            this.uxViewSeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxViewSeed.Location = new System.Drawing.Point(6, 243);
            this.uxViewSeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxViewSeed.Name = "uxViewSeed";
            this.uxViewSeed.Size = new System.Drawing.Size(211, 46);
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
            this.uxTextViewer.Location = new System.Drawing.Point(6, 41);
            this.uxTextViewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxTextViewer.Multiline = true;
            this.uxTextViewer.Name = "uxTextViewer";
            this.uxTextViewer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.uxTextViewer.Size = new System.Drawing.Size(1047, 194);
            this.uxTextViewer.TabIndex = 5;
            this.uxTextViewer.TabStop = false;
            this.uxToolTip.SetToolTip(this.uxTextViewer, "This allows you to view the data for the seed you are editing. You can also save " +
        "the data for this seed by clicking a button.");
            this.uxTextViewer.WordWrap = false;
            // 
            // uxToolTip
            // 
            this.uxToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.uxToolTip.ToolTipTitle = "What This Does:";
            // 
            // uxSaveSeed
            // 
            this.uxSaveSeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSaveSeed.Location = new System.Drawing.Point(842, 243);
            this.uxSaveSeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxSaveSeed.Name = "uxSaveSeed";
            this.uxSaveSeed.Size = new System.Drawing.Size(211, 46);
            this.uxSaveSeed.TabIndex = 4;
            this.uxSaveSeed.Text = "Save Row Data";
            this.uxToolTip.SetToolTip(this.uxSaveSeed, "Allows you to save the data for this seed. Won\'t affect anything unless you also " +
        "save the file after doing this.");
            this.uxSaveSeed.UseVisualStyleBackColor = true;
            this.uxSaveSeed.Click += new System.EventHandler(this.SaveSeedData);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 34);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1064, 107);
            this.textBox1.TabIndex = 7;
            this.uxToolTip.SetToolTip(this.textBox1, "This is the header of the program");
            // 
            // uxSeedDisplayGroup
            // 
            this.uxSeedDisplayGroup.AutoSize = true;
            this.uxSeedDisplayGroup.Controls.Add(this.uxTextViewer);
            this.uxSeedDisplayGroup.Controls.Add(this.uxSaveSeed);
            this.uxSeedDisplayGroup.Controls.Add(this.uxSeedList);
            this.uxSeedDisplayGroup.Controls.Add(this.uxViewSeed);
            this.uxSeedDisplayGroup.Controls.Add(this.uxEditSeed);
            this.uxSeedDisplayGroup.Enabled = false;
            this.uxSeedDisplayGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSeedDisplayGroup.Location = new System.Drawing.Point(16, 149);
            this.uxSeedDisplayGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxSeedDisplayGroup.Name = "uxSeedDisplayGroup";
            this.uxSeedDisplayGroup.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uxSeedDisplayGroup.Size = new System.Drawing.Size(1068, 651);
            this.uxSeedDisplayGroup.TabIndex = 6;
            this.uxSeedDisplayGroup.TabStop = false;
            this.uxSeedDisplayGroup.Text = "Seed Display";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 829);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.uxSeedDisplayGroup);
            this.Controls.Add(this.ViewFormMenuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
        private System.Windows.Forms.TextBox textBox1;
    }
}

