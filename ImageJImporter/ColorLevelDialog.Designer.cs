
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
            this.SuspendLayout();
            // 
            // ColorLevelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(446, 287);
            this.ForeColor = System.Drawing.Color.Navy;
            this.Name = "ColorLevelDialog";
            this.Text = "Color Level Dialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorLevelDialog_FormClosing);
            this.Resize += new System.EventHandler(this.ResizeMainPanelAndExitButton);
            this.ResumeLayout(false);

        }

        #endregion
    }
}