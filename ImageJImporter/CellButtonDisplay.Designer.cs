
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
            this.uxCellView = new BrightIdeasSoftware.ObjectListView();
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
            this.uxGermReportDisplay = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uxCellView)).BeginInit();
            this.SuspendLayout();
            // 
            // uxCloseAll
            // 
            this.uxCloseAll.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.uxCloseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.uxCloseAll.Location = new System.Drawing.Point(316, 402);
            this.uxCloseAll.Name = "uxCloseAll";
            this.uxCloseAll.Size = new System.Drawing.Size(298, 30);
            this.uxCloseAll.TabIndex = 1;
            this.uxCloseAll.Text = "Close All Cell Display Windows";
            this.uxCloseAll.UseVisualStyleBackColor = true;
            this.uxCloseAll.Click += new System.EventHandler(this.uxCloseAll_Click);
            // 
            // uxCloseThis
            // 
            this.uxCloseThis.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uxCloseThis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.uxCloseThis.Location = new System.Drawing.Point(12, 402);
            this.uxCloseThis.Name = "uxCloseThis";
            this.uxCloseThis.Size = new System.Drawing.Size(298, 30);
            this.uxCloseThis.TabIndex = 0;
            this.uxCloseThis.Text = "Close This Window";
            this.uxCloseThis.UseVisualStyleBackColor = true;
            this.uxCloseThis.Click += new System.EventHandler(this.uxCloseThis_Click);
            // 
            // uxCellView
            // 
            this.uxCellView.AllColumns.Add(this.rowName);
            this.uxCellView.AllColumns.Add(this.rowArea);
            this.uxCellView.AllColumns.Add(this.rowX);
            this.uxCellView.AllColumns.Add(this.rowY);
            this.uxCellView.AllColumns.Add(this.rowPerim);
            this.uxCellView.AllColumns.Add(this.rowMajor);
            this.uxCellView.AllColumns.Add(this.rowMinor);
            this.uxCellView.AllColumns.Add(this.rowAngle);
            this.uxCellView.AllColumns.Add(this.rowCirc);
            this.uxCellView.AllColumns.Add(this.rowAR);
            this.uxCellView.AllColumns.Add(this.rowRound);
            this.uxCellView.AllColumns.Add(this.rowSolidity);
            this.uxCellView.CellEditUseWholeCell = false;
            this.uxCellView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.rowName,
            this.rowArea,
            this.rowX,
            this.rowY,
            this.rowPerim,
            this.rowMajor,
            this.rowMinor});
            this.uxCellView.Cursor = System.Windows.Forms.Cursors.Default;
            this.uxCellView.EmptyListMsg = "It seems there weren\'t any rows in that Cell. That really shouldn\'t happen.";
            this.uxCellView.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCellView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCellView.FullRowSelect = true;
            this.uxCellView.GridLines = true;
            this.uxCellView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.uxCellView.HideSelection = false;
            this.uxCellView.Location = new System.Drawing.Point(12, 12);
            this.uxCellView.Name = "uxCellView";
            this.uxCellView.Size = new System.Drawing.Size(602, 232);
            this.uxCellView.TabIndex = 2;
            this.uxCellView.UseCompatibleStateImageBehavior = false;
            this.uxCellView.View = System.Windows.Forms.View.Details;
            this.uxCellView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.uxRowListView_FormatRow);
            // 
            // rowName
            // 
            this.rowName.AspectName = "RowNum";
            this.rowName.Text = "Row";
            // 
            // rowArea
            // 
            this.rowArea.AspectName = "Area";
            this.rowArea.Text = "Area";
            // 
            // rowX
            // 
            this.rowX.AspectName = "X";
            this.rowX.Text = "X";
            // 
            // rowY
            // 
            this.rowY.AspectName = "Y";
            this.rowY.Text = "Y";
            // 
            // rowPerim
            // 
            this.rowPerim.AspectName = "Perim";
            this.rowPerim.Text = "Perim";
            // 
            // rowMajor
            // 
            this.rowMajor.AspectName = "Major";
            this.rowMajor.Text = "Major";
            // 
            // rowMinor
            // 
            this.rowMinor.AspectName = "Minor";
            this.rowMinor.Text = "Minor";
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
            // uxGermReportDisplay
            // 
            this.uxGermReportDisplay.AcceptsTab = true;
            this.uxGermReportDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxGermReportDisplay.Location = new System.Drawing.Point(12, 251);
            this.uxGermReportDisplay.Name = "uxGermReportDisplay";
            this.uxGermReportDisplay.ReadOnly = true;
            this.uxGermReportDisplay.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.uxGermReportDisplay.Size = new System.Drawing.Size(602, 145);
            this.uxGermReportDisplay.TabIndex = 3;
            this.uxGermReportDisplay.Text = "";
            // 
            // CellButtonDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 444);
            this.ControlBox = false;
            this.Controls.Add(this.uxGermReportDisplay);
            this.Controls.Add(this.uxCellView);
            this.Controls.Add(this.uxCloseThis);
            this.Controls.Add(this.uxCloseAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CellButtonDisplay";
            this.Text = "Cell Display";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.uxCellView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uxCloseAll;
        private System.Windows.Forms.Button uxCloseThis;
        private BrightIdeasSoftware.ObjectListView uxCellView;
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
        private System.Windows.Forms.RichTextBox uxGermReportDisplay;
    }
}