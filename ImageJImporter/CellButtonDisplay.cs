using BrightIdeasSoftware;
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
        private Grid grid;
        private LevelInformation allLevelInformation;

        /// <summary>
        /// the typed version of our OLV
        /// </summary>
        private TypedObjectListView<Row> tlist;

        public CellButtonDisplay(CallMethod closeDisplays, Cell cell, LevelInformation levels)
        {
            //initialize visual controls
            InitializeComponent();
            //initialize fields
            this.closeAllDisplays = closeDisplays;
            List<Row> rowsInCell = cell.Rows;
            this.allLevelInformation = levels;
            grid = new Grid(rowsInCell);
            //initialize typedObjectListView
            tlist = new TypedObjectListView<Row>(this.uxCellView);

            //set up object list view
            tlist.GetColumn(0).GroupKeyGetter = delegate (Row rowObject)
            {
                Row row = rowObject;
                return row.CurrentCellOwner;
            };
            this.rowName.GroupKeyToTitleConverter = delegate (object groupKey)
            {
                Cell groupCell = (Cell)groupKey;
                string cellTypePlusChalkPlusLevel = "";
                string germMessage = cell.isGerm ? " - IsGerm = true" : "";
                //determine header based on cell state
                if (groupCell.IsNewRowFlag) cellTypePlusChalkPlusLevel = $"New Row Flag";
                else if (!groupCell.IsFullCell) cellTypePlusChalkPlusLevel = $"Incomplete Cell";
                else if (groupCell.IsEmptyCell) cellTypePlusChalkPlusLevel = $"Empty Cell";
                else if (groupCell.RowSpan == -2) cellTypePlusChalkPlusLevel = $"Abnormal Cell";
                else cellTypePlusChalkPlusLevel = $"Normal Cell - Chalk = {groupCell.Chalk:N1}% - Level = {allLevelInformation.FindLevel(groupCell.Chalk).Item1}" +
                    $"{germMessage}";
                return $"{cellTypePlusChalkPlusLevel}";
            };
            this.rowName.GroupFormatter = (OLVGroup group,
                GroupingParameters parms) =>
            {
                Cell groupCell = (Cell)group.Key;
                int index = 0;
                if(groupCell.OwningGridObject != null) index = groupCell.OwningGridObject.Cells.IndexOf(groupCell);
                group.Id = index;
                parms.GroupComparer = Comparer<OLVGroup>.Create((x, y) => (x.Id.CompareTo(y.Id)));
            };

            //actually give the cell to the OLV
            uxCellView.SetObjects(grid.Rows);

            //set up germ report
            FormatGermReport();
        }//end constructor

        /// <summary>
        /// Sets up the text in the germ report textbox
        /// </summary>
        private void FormatGermReport()
        {
            //initialize stringbuilder
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Generating Germ Report...");
            if(grid.Cells.Count != 1 || grid.Cells[0] == null)
            {
                sb.AppendLine("No Valid Seed Was Found for this Cell");                
            }//end if we have a cell to work with
            else if (grid.Cells[0].IsNewRowFlag)
            {
                sb.AppendLine("This cell is a flag for a new row.");
            }//end else if we have a new row flag
            else if (grid.Cells[0].IsEmptyCell)
            {
                sb.AppendLine("This cell has no data in it.");
            }//end else if we have an empty cell
            else if (grid.Cells[0].IsFullCell)
            {
                sb.AppendLine("This cell appears to have data," +
                    " and it's correctly formatted.");
                sb.AppendLine($"dx:\t\t{grid.Cells[0].dx.ToString("N3")}");
                sb.AppendLine($"dy:\t\t{grid.Cells[0].dy.ToString("N3")}");
                sb.AppendLine($"z:\t\t{grid.Cells[0].z.ToString("N3")}");
                sb.AppendLine($"halfMajor:\t{grid.Cells[0].halfMajor.ToString("N3")}");
                sb.AppendLine($"ratio:\t\t{grid.Cells[0].ratio.ToString("N3")}");
                sb.AppendLine($"threshold:\t{grid.Cells[0].GermThreshold.ToString("N3")}");
                sb.AppendLine($"isGerm:\t\t{grid.Cells[0].isGerm}");
                sb.AppendLine($"twoSpots:\t{grid.Cells[0].twoSpots}");
                sb.AppendLine($"Chalk1:\t\t{grid.Cells[0].Chalk1.ToString("N3")}%");
                sb.AppendLine($"Chalk2:\t\t{grid.Cells[0].Chalk.ToString("N3")}%");
            }//end else if the cell is good to go
            else
            {
                sb.AppendLine("This cell has extemely broken" +
                    " formatting and can't be read from.");
            }//end else we don't have a valid cell

            sb.AppendLine("Ending Report");

            //update text of textbox
            uxGermReportDisplay.Text = sb.ToString();
        }//end UpdateCellView()

        private void uxRowListView_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (e.Model is Row currentRow)
            {
                Cell owningCell = currentRow.CurrentCellOwner;
                Tuple<string, int> level = allLevelInformation.FindLevel(owningCell.Chalk);
                LevelInformation.Level thisLevel = allLevelInformation[level.Item2];
                e.Item.BackColor = thisLevel.BackColor;
                e.Item.ForeColor = thisLevel.ForeColor;
            }//end if our model is a row
        }//end event handler for formatting each row in OLV

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