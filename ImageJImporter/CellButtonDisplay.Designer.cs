
namespace ImageJImporter
{
    partial class CellButtonDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellButtonDisplay));
            this.uxCloseAll = new System.Windows.Forms.Button();
            this.uxCloseThis = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // uxCloseAll
            // 
            this.uxCloseAll.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.uxCloseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCloseAll.Location = new System.Drawing.Point(12, 265);
            this.uxCloseAll.Name = "uxCloseAll";
            this.uxCloseAll.Size = new System.Drawing.Size(602, 37);
            this.uxCloseAll.TabIndex = 1;
            this.uxCloseAll.Text = "Close All Cell Display WIndows";
            this.uxCloseAll.UseVisualStyleBackColor = true;
            // 
            // uxCloseThis
            // 
            this.uxCloseThis.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uxCloseThis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCloseThis.Location = new System.Drawing.Point(12, 308);
            this.uxCloseThis.Name = "uxCloseThis";
            this.uxCloseThis.Size = new System.Drawing.Size(602, 37);
            this.uxCloseThis.TabIndex = 0;
            this.uxCloseThis.Text = "Close This Window";
            this.uxCloseThis.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(13, 13);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(601, 246);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // CellButtonDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 357);
            this.ControlBox = false;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.uxCloseThis);
            this.Controls.Add(this.uxCloseAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CellButtonDisplay";
            this.Text = "Cell Display";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uxCloseAll;
        private System.Windows.Forms.Button uxCloseThis;
        private System.Windows.Forms.ListView listView1;
    }
}