using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Author: Nicholas Sixbury
 * File: CellButton.cs
 * Purpose: To provide a type of button which can store a reference to
 * a Row object
 */

namespace ImageJImporter
{
    /// <summary>
    /// class extends Button and holds a row so that we can store the row
    /// inside the button
    /// </summary>
    public class CellButton : Button
    {
        private Cell cell;
        /// <summary>
        /// the row that this button represents. Not reference-safe
        /// </summary>
        public Cell Cell
        {
            get { return cell; }
            set { cell = value; }
        }//end constructor

        private LevelInformation levels = LevelInformation.DefaultLevels;

        private string cellPropertyNameToCompareToLevel;

        /// <summary>
        /// constructor for this class. Must initialize with a Cell object.
        /// </summary>
        /// <param name="cell">the cell that this button represents</param>
        public CellButton(Cell cell) : base()
        {
            this.cell = cell;
            cellPropertyNameToCompareToLevel = nameof(cell.Chalk);
        }//end CellButton no-arg constructor

        /// <summary>
        /// the the levelInformation for this object
        /// </summary>
        /// <param name="levels">the level information for this object</param>
        /// <param name="cellParamName">The name of the property in our cell object
        /// that we should compare with the level information. If set to null, will
        /// use whatever we find in levels</param>
        public void SetLevelInformation(LevelInformation levels, string cellParamName)
        {
            this.levels = levels;
            if(cellParamName != null)
            {
                cellPropertyNameToCompareToLevel = cellParamName;
            }//end if the user wants to specify a specific cellParamName
            else
            {
                cellPropertyNameToCompareToLevel = levels.PropertyToTest;
            }//end else we'll use the name from the level info
        }//end SetLevelInformation(levels)

        /// <summary>
        /// Formats this CellButton based on the flags of its
        /// Cell. Tooltip assigning is optional. To not have a
        /// tooltip, just set tip to null.
        /// </summary>
        /// <param name="tip">the tooltip used to set the button's tooltip.
        /// Set to null if you don't want a tooltip</param>
        public void FormatCellButton(ToolTip tip)
        {
            //format the text of the button
            Text = Cell.RowSpan.ToString();

            //set the font size
            Font = new Font(FontFamily.GenericSansSerif, 10);

            //basically we're figuring out what the levels are based off of here
            Tuple<string, int> correctLevel = levels
                    .FindLevel((decimal)Cell
                    .GetType()
                    .GetProperty(cellPropertyNameToCompareToLevel)
                    .GetValue(cell));


            if (correctLevel.Item1 != null)
            {
                BackColor = levels.Levels[correctLevel.Item2].BackColor;
                ForeColor = levels.Levels[correctLevel.Item2].ForeColor;
            }//end if we have a level to set this to
            else
            {
                BackColor = Color.Transparent;
                ForeColor = Color.Black;
            }//end else the level wasn't found

            //set germ thing
            if (Cell.isGerm)
            {
                FlatStyle = FlatStyle.Standard;
            }//end if cell has a germ
            else
            {
                FlatStyle = FlatStyle.Popup;
            }//end else the cell doesn't have a germ

            //format color and tooltip
            if (Cell.IsNewRowFlag)
            {
                //set tooltip
                tip?.SetToolTip(this, "This cell" +
                    " simply represents a new grid row and " +
                    "doesn't contain any novel information.");
            }//end if the button represents a flag for a new row
            else if (Cell.IsEmptyCell)
            {
                //set tooltip
                tip?.SetToolTip(this, "This cell is" +
                    "correctly formatted, but it doesn't have any" +
                    " information stored in it.");
            }//end if the cell is properly formatted but empty
            else if (Cell.RowSpan == 2 && Cell.IsFullCell)
            {
                //set tooltip
                tip?.SetToolTip(this, "This cell is " +
                    "correctly formatted and has normal data, making " +
                    "it easy to calculate chaulkiness for.");
            }//end if we have a normal cell
            else if (Cell.IsFullCell)
            {
                //set tooltip
                tip?.SetToolTip(this, "This cell is " +
                    "correctly formatted, but because of its data, " +
                    "it\'s difficult to use.");
            }//end else we have a properly formatted by abnormal cell
            else
            {
                //set tooltip
                tip?.SetToolTip(this, "This cell is formatted " +
                    "incorrectly. This could either be a problem with processing" +
                    " this cell\'s row information, or it could mean your input data " +
                    "was corrupted somehow.");
            }//end else this cell is formatted wrong
        }//end FormatCellButton(tip)
    }//end class
}//end namespace