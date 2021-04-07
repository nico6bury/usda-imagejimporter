
namespace ImageJImporter
{
    partial class ColorLevelDialog
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
            this.uxSubPanelContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uxRemoveLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMainPanelContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uxAddNewLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSubPanelContextMenuStrip.SuspendLayout();
            this.uxMainPanelContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxSubPanelContextMenuStrip
            // 
            this.uxSubPanelContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxRemoveLevel});
            this.uxSubPanelContextMenuStrip.Name = "uxSubPanelContextMenuStrip";
            this.uxSubPanelContextMenuStrip.Size = new System.Drawing.Size(148, 26);
            this.uxSubPanelContextMenuStrip.Text = "Level Options";
            // 
            // uxRemoveLevel
            // 
            this.uxRemoveLevel.Name = "uxRemoveLevel";
            this.uxRemoveLevel.Size = new System.Drawing.Size(180, 22);
            this.uxRemoveLevel.Text = "Remove Level";
            this.uxRemoveLevel.Click += new System.EventHandler(this.uxRemoveLevel_Click);
            // 
            // uxMainPanelContextMenuStrip
            // 
            this.uxMainPanelContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxAddNewLevel});
            this.uxMainPanelContextMenuStrip.Name = "uxMainPanelContextMenuStrip";
            this.uxMainPanelContextMenuStrip.Size = new System.Drawing.Size(127, 26);
            this.uxMainPanelContextMenuStrip.Text = "Level Options";
            // 
            // uxAddNewLevel
            // 
            this.uxAddNewLevel.Name = "uxAddNewLevel";
            this.uxAddNewLevel.Size = new System.Drawing.Size(180, 22);
            this.uxAddNewLevel.Text = "Add Level";
            this.uxAddNewLevel.Click += new System.EventHandler(this.uxAddNewLevel_Click);
            // 
            // ColorLevelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(614, 434);
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "ColorLevelDialog";
            this.ShowIcon = false;
            this.Text = "Color Level Dialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorLevelDialog_FormClosing);
            this.Resize += new System.EventHandler(this.ResizeMainPanelAndExitButton);
            this.uxSubPanelContextMenuStrip.ResumeLayout(false);
            this.uxMainPanelContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip uxSubPanelContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem uxRemoveLevel;
        private System.Windows.Forms.ContextMenuStrip uxMainPanelContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem uxAddNewLevel;
    }
}