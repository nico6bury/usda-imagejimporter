using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageJImporter
{
    public partial class CellButtonDisplay : Form
    {
        private CallMethod closeAllDisplays;
        private Cell Cell;

        public CellButtonDisplay(CallMethod closeDisplays, Cell cell)
        {
            InitializeComponent();
            this.closeAllDisplays = closeDisplays;
            this.Cell = cell;
            UpdateCellView();
        }//end constructor

        private void UpdateCellView()
        {
            //clear previous data
            uxCellView.Groups.Clear();
            uxCellView.Items.Clear();

            //initialize stringbuilder to build cell group header
            StringBuilder sb = new StringBuilder();

            //determine header based on cell state
            if (Cell.IsNewRowFlag) sb.Append("New Row Flag");
            else if (!Cell.IsFullCell) sb.Append("Incomplete Cell");
            else if (Cell.IsEmptyCell) sb.Append("Empty Cell");
            else if (Cell.RowSpan != 2) sb.Append("Abnormal Cell");
            else sb.Append($"Normal Cell with {Cell.Chalk.ToString("N3")} Chalk");

            //get the group ready
            ListViewGroup group = new ListViewGroup(sb.ToString());
            uxCellView.Groups.Add(group);

            //add all the times
            foreach (Row row in Cell.Rows)
            {
                ListViewItem item = new ListViewItem(row.GetRowPropertyArray());
                group.Items.Add(item);
                uxCellView.Items.Add(item);
            }//end displaying all the rows
        }//end UpdateCellView()

        private void uxCloseThis_Click(object sender, EventArgs e)
        {
            this.Close();
        }//end uxCloseThis click event handler

        private void uxCloseAll_Click(object sender, EventArgs e)
        {
            closeAllDisplays();
        }//end uxCloseAll click event handler
    }//end class
}//end namespace