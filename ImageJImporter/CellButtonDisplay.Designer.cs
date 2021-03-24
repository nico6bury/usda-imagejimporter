
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
            this.uxCellView = new System.Windows.Forms.ListView();
            this.uxRowHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxAreaHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxXHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxYHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxPerimHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxMajorHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxMinorHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxAngleHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxCircHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxARHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxRoundHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxSolidityHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.uxCloseAll.Text = "Close All Cell Display Windows";
            this.uxCloseAll.UseVisualStyleBackColor = true;
            this.uxCloseAll.Click += new System.EventHandler(this.uxCloseAll_Click);
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
            this.uxCloseThis.Click += new System.EventHandler(this.uxCloseThis_Click);
            // 
            // uxCellView
            // 
            this.uxCellView.AllowColumnReorder = true;
            this.uxCellView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.uxRowHeader,
            this.uxAreaHeader,
            this.uxXHeader,
            this.uxYHeader,
            this.uxPerimHeader,
            this.uxMajorHeader,
            this.uxMinorHeader,
            this.uxAngleHeader,
            this.uxCircHeader,
            this.uxARHeader,
            this.uxRoundHeader,
            this.uxSolidityHeader});
            this.uxCellView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCellView.FullRowSelect = true;
            this.uxCellView.GridLines = true;
            this.uxCellView.HideSelection = false;
            this.uxCellView.Location = new System.Drawing.Point(13, 13);
            this.uxCellView.Name = "uxCellView";
            this.uxCellView.Size = new System.Drawing.Size(601, 246);
            this.uxCellView.TabIndex = 2;
            this.uxCellView.UseCompatibleStateImageBehavior = false;
            this.uxCellView.View = System.Windows.Forms.View.Details;
            // 
            // uxRowHeader
            // 
            this.uxRowHeader.Text = "Row";
            this.uxRowHeader.Width = 72;
            // 
            // uxAreaHeader
            // 
            this.uxAreaHeader.Text = "Area";
            this.uxAreaHeader.Width = 120;
            // 
            // uxXHeader
            // 
            this.uxXHeader.Text = "X";
            this.uxXHeader.Width = 51;
            // 
            // uxYHeader
            // 
            this.uxYHeader.Text = "Y";
            this.uxYHeader.Width = 104;
            // 
            // uxPerimHeader
            // 
            this.uxPerimHeader.Text = "Perim";
            this.uxPerimHeader.Width = 71;
            // 
            // uxMajorHeader
            // 
            this.uxMajorHeader.Text = "Major";
            this.uxMajorHeader.Width = 64;
            // 
            // uxMinorHeader
            // 
            this.uxMinorHeader.Text = "Minor";
            // 
            // uxAngleHeader
            // 
            this.uxAngleHeader.Text = "Angle";
            this.uxAngleHeader.Width = 0;
            // 
            // uxCircHeader
            // 
            this.uxCircHeader.Text = "Circ";
            this.uxCircHeader.Width = 0;
            // 
            // uxARHeader
            // 
            this.uxARHeader.Text = "AR";
            this.uxARHeader.Width = 0;
            // 
            // uxRoundHeader
            // 
            this.uxRoundHeader.Text = "Round";
            this.uxRoundHeader.Width = 0;
            // 
            // uxSolidityHeader
            // 
            this.uxSolidityHeader.Text = "Solidity";
            this.uxSolidityHeader.Width = 0;
            // 
            // CellButtonDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 357);
            this.ControlBox = false;
            this.Controls.Add(this.uxCellView);
            this.Controls.Add(this.uxCloseThis);
            this.Controls.Add(this.uxCloseAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CellButtonDisplay";
            this.Text = "Cell Display";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uxCloseAll;
        private System.Windows.Forms.Button uxCloseThis;
        private System.Windows.Forms.ListView uxCellView;
        private System.Windows.Forms.ColumnHeader uxRowHeader;
        private System.Windows.Forms.ColumnHeader uxAreaHeader;
        private System.Windows.Forms.ColumnHeader uxXHeader;
        private System.Windows.Forms.ColumnHeader uxYHeader;
        private System.Windows.Forms.ColumnHeader uxPerimHeader;
        private System.Windows.Forms.ColumnHeader uxMajorHeader;
        private System.Windows.Forms.ColumnHeader uxMinorHeader;
        private System.Windows.Forms.ColumnHeader uxAngleHeader;
        private System.Windows.Forms.ColumnHeader uxCircHeader;
        private System.Windows.Forms.ColumnHeader uxARHeader;
        private System.Windows.Forms.ColumnHeader uxRoundHeader;
        private System.Windows.Forms.ColumnHeader uxSolidityHeader;
    }
}