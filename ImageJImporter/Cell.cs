using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Nicholas Sixbury
 * File: Cell.cs
 * Purpose: To represent and hold all the information for a single
 * cell from the grid we use to scan seeds with. Holds several row
 * objects and considates their inforamtion into one usable chunk.
 */

namespace ImageJImporter
{
    /// <summary>
    /// Represents a single cell (or new grid-line flag) in the grid
    /// used for seed scanning. Contains a few rows and considates that
    /// information.
    /// </summary>
    class Cell : ICollection<Row>, IEnumerable<Row>
    {
        private List<Row> rows = new List<Row>();
        /// <summary>
        /// The rows of data that make up this cell. For this cell
        /// to be complete, this list will either be composed of
        /// just a single NewRowFlag or start with a SeedStartFlag
        /// and end with a SeedEndFlag. If you want to set an index
        /// of this list to a particular value, use this class's
        /// indexer method instead.
        /// </summary>
        public List<Row> Rows
        {
            get
            {
                List<Row> tempRowList = new List<Row>();
                foreach(Row row in rows)
                {
                    tempRowList.Add(new Row(row));
                }//end adding all rows in rows to tempRowList
                return tempRowList;
            }//end getter
            set
            {
                if(value != null)
                {
                    List<Row> tempRowList = new List<Row>();
                    foreach (Row row in value)
                    {
                        tempRowList.Add(new Row(row));
                    }//end adding all rows in givenRowList to tempRowList
                    rows = tempRowList;
                }//end if the value isn't null
                else
                {
                    throw new ArgumentNullException("You cannot set a Cell\'s " +
                        "internal row list to null.");
                }//end else the value is null, and we should spew an error
            }//end setter
        }//end Rows Property

        /// <summary>
        /// the number of rows contained in this object
        /// </summary>
        public int Count => rows.Count;

        /// <summary>
        /// always false
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// whether or not this cell consists of a single row which
        /// is a NewRowFlag
        /// </summary>
        public bool IsNewRowFlag
        {
            get
            {
                //if we hav the wrong number of rows
                if (rows.Count != 1) return false;
                //if the flag is wrong
                if (!rows[0].IsNewRowFlag) return false;
                //if we got here, we must be true
                return true;
            }//end getter
        }//end IsNewRowFlag

        /// <summary>
        /// whether or not this cell starts with a SeedStartFlag and
        /// ends with a SeedEndFlag
        /// </summary>
        public bool IsFullCell
        {
            get
            {
                //if there aren't enough rows
                if (rows.Count < 2) return false;
                //if the first row isn't a SeedStartFlag
                if (!rows[0].IsSeedStartFlag) return false;
                //if the last row isn't a SeedEndFlag
                if (!rows[rows.Count - 1].IsSeedEndFlag) return false;
                //if we got here, it must be true
                return true;
            }//end getter
        }//end IsFullCell

        /// <summary>
        /// gets or sets the specified index of the rows this cell
        /// contains
        /// </summary>
        /// <param name="index">the index you wish to access</param>
        public Row this[int index]
        {
            get
            {
                if (index < 0 || index >= rows.Count)
                    throw new IndexOutOfRangeException($"{index} is out of range");
                return new Row(rows[index]);
            }//end getter
            set
            {
                if (index < 0 || index >= rows.Count)
                    throw new IndexOutOfRangeException($"{index} is out of range");
                rows[index] = new Row(value);
            }//end setter
        }//end Row indexer

        /// <summary>
        /// initializes this object with no default data
        /// </summary>
        public Cell()
        {
            rows = new List<Row>();
        }//end no-arg constructor

        /// <summary>
        /// initializes this object as a copy of the specified
        /// cell object
        /// </summary>
        /// <param name="cell">The Cell you wish to copy</param>
        public Cell(Cell cell)
        {
            this.Rows = cell.Rows;
        }//end 1-arg copy constructor

        /// <summary>
        /// initializes this object with its rows initializes as the 
        /// specified list of rows.
        /// </summary>
        /// <param name="rows">The rows you wish to initialize the object
        /// with</param>
        public Cell(List<Row> rows)
        {
            this.Rows = rows;
        }//end 1-arg constructor

        /// <summary>
        /// returns a string representation of this object, in this case
        /// a string containing the ToStrings of all its Rows, with the
        /// \n character in between.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Row row in rows)
            {
                sb.Append($"{row}\n");
            }//end getting string data from each row in rows
            return sb.ToString();
        }//end ToString()

        /// <summary>
        /// Two Cell objects are equal if all rows
        /// are equal
        /// </summary>
        public bool Equals(Cell other)
        {
            //check for right length
            if (other.Count != this.Count) return false;

            //check for individual row equality and order
            for(int i = 0; i < other.Count; i++)
            {
                if (!other[i].Equals(this[i])) return false;
            }//end looping through each row in the other cell

            //if we managed to get here, they must be equal
            return true;
        }//end Equals(obj)

        /// <summary>
        /// adds a new row to the end of Rows
        /// </summary>
        public void Add(Row item)
        {
            rows.Add(item);
        }//end Add(item)

        /// <summary>
        /// clears the Rows list
        /// </summary>
        public void Clear()
        {
            rows.Clear();
        }//end Clear()

        /// <summary>
        /// returns whether or not a specified item
        /// is contained within this cell
        /// </summary>
        public bool Contains(Row item)
        {
            foreach(Row row in rows)
            {
                if (row.Equals(item)) return true;
            }//end checking each row
            
            //if we got here, we must not have found it
            return false;
        }//end Contains(item)

        /// <summary>
        /// Copies the rows of this cell into an array, starting at the
        /// specified index. Continues until either there are no more
        /// elements to copy from Rows list or until we've reached the
        /// end of the specified array.
        /// </summary>
        /// <param name="array">the array to copy to</param>
        /// <param name="arrayIndex">the index to start at</param>
        public void CopyTo(Row[] array, int arrayIndex)
        {
            List<Row> tempRowList = Rows;
            //keeps track of what index of tempRowList we're copying from
            int secondIndexCounter = 0;
            for(int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = tempRowList[secondIndexCounter];
                secondIndexCounter++;
            }//end for loop
        }//end CopyTo(array, arrayIndex)

        /// <summary>
        /// Method currently not supported
        /// </summary>
        public bool Remove(Row item)
        {
            throw new InvalidOperationException("This operation is not valid" +
                " for this type. If necessary, please just make a new Cell.");
        }//end Remove(item)

        public IEnumerator<Row> GetEnumerator()
        {
            return GetEnumerator();
        }//end GetEnumerator()

        IEnumerator IEnumerable.GetEnumerator()
        {
            List<Row> tempRowList = Rows;
            foreach(Row row in tempRowList)
            {
                yield return row;
            }//end yield returning each row
        }//end IEnumerable.GetEnumerator()
    }//end class
}//end namespace