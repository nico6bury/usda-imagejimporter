using System;
using System.Collections.Generic;
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

        /// <summary>
        /// constructor for this class. Must initialize with a Cell object.
        /// </summary>
        /// <param name="cell">the cell that this button represents</param>
        public CellButton(Cell cell) : base()
        {
            this.cell = new Cell(cell);
        }//end CellButton no-arg constructor
    }//end class
}//end namespace