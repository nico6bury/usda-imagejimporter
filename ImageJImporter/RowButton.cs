using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Author: Nicholas Sixbury
 * File: RowButton.cs
 * Purpose: To provide a type of button which can store a reference to
 * a Row object
 */

namespace ImageJImporter
{
    /// <summary>
    /// class extends Button and holds a row so that we can store the row
    /// inside the button
    /// </summary>
    public class RowButton : Button
    {
        private Row row;
        /// <summary>
        /// the row that this button represents
        /// </summary>
        public Row Row
        {
            get { return new Row(row); }
            set { row = new Row(value); }
        }

        /// <summary>
        /// constructor for this class. Must initialize with a Row object.
        /// Initializes text field to be row number
        /// </summary>
        /// <param name="row">the row that this button represents</param>
        public RowButton(Row row) : base()
        {
            Row = new Row(row);
            this.Text = Row.RowNum.ToString();
        }//end RowButton no-arg constructor
    }//end class
}//end namespace