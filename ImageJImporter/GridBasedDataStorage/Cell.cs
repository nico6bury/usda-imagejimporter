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
    public class Cell : ICollection<Row>, IEnumerable<Row>
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
                        Row newRow = new Row(row);
                        newRow.CurrentCellOwner = this;
                        tempRowList.Add(newRow);
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
        /// Whether or not germ detection will affect chalk
        /// </summary>
        public static bool UseGermDetection { get; set; } = true;

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
        /// whether or not this cell is just a SeedStartFlag and SeedEndFlag
        /// </summary>
        public bool IsEmptyCell
        {
            get
            {
                //if there aren't the right amount of rows
                if (rows.Count != 2) return false;
                //if the first row isn't a SeedStartFlag
                if (!rows[0].IsSeedStartFlag) return false;
                //if the last row isn't a SeedEndFlag
                if (!rows[1].IsSeedEndFlag) return false;
                //if we got here, it must be true
                return true;
            }//end getter
        }//end IsEmptyCell

        /// <summary>
        /// The chalkniess of this cell. If this is a new row flag, 
        /// an empty cell, or incorrectly formatted, then this will
        /// return -1. Otherwise, it will return percent chalkiness
        /// of this cell as a percent. If the seed is a germ, 
        /// returns 1%
        /// </summary>
        public decimal Chalk
        {
            get
            {
                if (IsNewRowFlag)
                {
                    return -2;
                }//end if this is a flag for a new row in grid
                else if (!IsFullCell)
                {
                    return -10;
                }//end else if this isn't a complete cell
                else if (IsEmptyCell)
                {
                    return -0.1M;
                }//end else if this cell is empty
                else if(UseGermDetection == false)
                {
                    return Chalk1;
                }//end else if we should just use normal germ detection
                else
                {
                    if (isGerm && !twoSpots)
                    {
                        return (decimal)0.01 * 100;
                    }//end if this seed has a germ on it
                    else if(rows.Count == 4)
                    {
                        //just normal chalk area divided by kernel area
                        return rows[2].Area / rows[1].Area*100;
                    }//end if we have the normal amount of rows
                    else if (rows.Count == 5)
                    {
                        if (isGerm && twoSpots)
                        {
                            //divide third data row area (chalk area) by first data row area (kernel area)
                            return rows[3].Area / rows[1].Area * 100;
                        }//end if we should exclude area of the germ
                        else
                        {
                            //add both chalkiness areas together and divide that by kernel area
                            decimal totalChalkiness = rows[2].Area + rows[3].Area;
                            return totalChalkiness / rows[1].Area * 100;
                        }//end else we have a normal instance of weird chalkiness
                    }//end else if we have two rows of chalkiness
                    else
                    {
                        return (decimal)0.01 * 100;
                    }//end else we have a seed with very little chalkiness
                }//end else this must be a complete cell
            }//end getter
        }//end Chalk

        /// <summary>
        /// The chalkniess of this cell. If this is a new row flag, 
        /// an empty cell, or incorrectly formatted, then this will
        /// return -1. Otherwise, it will return percent chalkiness
        /// of this cell as a percent. This property doesn't take
        /// germ potential into account
        /// </summary>
        public decimal Chalk1
        {
            get
            {
                if (IsNewRowFlag)
                {
                    return -2;
                }//end if this is a flag for a new row in grid
                else if (!IsFullCell)
                {
                    return -10;
                }//end else if this isn't a complete cell
                else if (IsEmptyCell)
                {
                    return -0.1M;
                }//end else if this cell is empty
                else
                {
                    if (rows.Count == 4)
                    {
                        return rows[2].Area / rows[1].Area * 100;
                    }//end if we have the normal amount of rows
                    else if (rows.Count == 5)
                    {
                        decimal totalChalkiness = rows[2].Area + rows[3].Area;
                        return totalChalkiness / rows[1].Area * 100;
                    }//end else if we have two rows of chalkiness
                    else
                    {
                        return (decimal)0.01 * 100;
                    }//end else we have a seed with very little chalkiness
                }//end else this must be a complete cell
            }//end getter
        }//end Chalk1

        /// <summary>
        /// A placeholder method to get the area of the kernel.
        /// In cases where applicable, just returns area of first
        /// data row. Otherwise, returns -0.10
        /// </summary>
        public decimal KernelArea
        {
            get
            {
                if(IsFullCell && !IsEmptyCell)
                {
                    return this.Rows[1].Area;
                }//end if this is a data cell with at least one row
                else
                {
                    return -0.10M;
                }//end else this cell has no kernel information
            }//end getter
        }//end KernelArea

        private decimal germThreshold = 0.5M;
        public decimal GermThreshold
        {
            get { return germThreshold; }
            set
            {
                //sets to 0 if less than 0, or value otherwise
                germThreshold = value < 0 ? 0 : value;
            }//end setter
        }//end GermThreshold

        /// <summary>
        /// The number of rows in this cell, minus 2 to account for the 
        /// cell start and cell end flags
        /// </summary>
        public int RowSpan
        {
            get
            {
                return rows.Count - 2;
            }//end getter
        }//end RowSpan

        /// <summary>
        /// this is the X of the spot on the seed that
        /// represents Chalk minus the X of the kernel.
        /// Doesn't take any potential third data row
        /// into account if it exists.
        /// </summary>
        public decimal dx
        {
            get
            {
                if (RowSpan < 2)
                {
                    //just return arbitrary small value
                    return 0;
                }//end if data rows are less than 2
                else
                {
                    //get kernel X
                    decimal kernel = rows[1].X;
                    //add first spot
                    decimal spots = rows[2].X;
                    ////add second spot if it's there
                    //if (RowSpan == 3)
                    //    spots += rows[3].X;
                    return spots - kernel;
                }//end else we can calculate stuff
            }//end getter
        }//end dx
        /// <summary>
        /// same as dx for for the Y
        /// </summary>
        public decimal dy
        {
            get
            {
                if (RowSpan < 2)
                {
                    //just return arbitrary small value
                    return 0;
                }//end if data rows are less than 2
                else
                {
                    //get kernel X
                    decimal kernel = rows[1].Y;
                    //add first spot
                    decimal spots = rows[2].Y;
                    ////add second spot if it's there
                    //if (RowSpan == 3)
                    //    spots += rows[3].Y;
                    return spots - kernel;
                }//end else we can calculate stuff
            }//end getter
        }//end dy
        /// <summary>
        /// ((dx^2)+(dy^2))^(0.5), top of ratio
        /// </summary>
        public decimal z
        {
            get
            {
                decimal dx2 = dx * dx;
                decimal dy2 = dy * dy;
                double result = Math.Sqrt((double)dx2 + (double)dy2);
                return (decimal)result;
            }//end getter
        }//end z
        /// <summary>
        /// the major of the kernel row (first row), divided
        /// by 2, bottom of ratio
        /// </summary>
        public decimal halfMajor
        {
            get
            {
                if (RowSpan < 1)
                {
                    return 0.0001M;
                }//end if there are no data rows
                else
                {
                    return rows[1].Major / (decimal)2.0;
                }//end else we can calculate major
            }//end getter
        }//end halfMajor
        /// <summary>
        /// the number of rows between the cell start and end
        /// flags that we have detected for the specified cell
        /// </summary>
        public int detectedDataRows
        {
            get
            {
                //whether we should increment counter
                bool flag = false;
                //the number of rows we're looking for
                int counter = 0;
                foreach (Row row in rows)
                {
                    if (row.IsSeedEndFlag) flag = false;

                    if (flag)
                    {
                        counter++;
                    }//end if flag true
                    else
                    {
                        if (row.IsSeedStartFlag) flag = true;
                    }//end else flag false
                }//end looping

                return counter;
            }//end get
        }//end detectedDataRows
        /// <summary>
        /// the fraction of z / halfMajor as a decimal
        /// </summary>
        public decimal ratio
        {
            get
            {
                return z / halfMajor;
            }//end getter
        }//end ratio
        /// <summary>
        /// whether or not this cell seems to contain a germ. Doesn't account
        /// for twoSpots
        /// </summary>
        public bool isGerm
        {
            get
            {
                return ratio > germThreshold;
            }//end getter
        }//end isGerm
        /// <summary>
        /// whether or not there are two spots in this seed
        /// </summary>
        public bool twoSpots
        {
            get
            {
                //check to make sure stuff isn't borken
                int dataRows = detectedDataRows;
                //if (dataRows != RowSpan && RowSpan >= 0) throw new InvalidOperationException("detectedDataRows and RowSpan don't match");
                return dataRows > 2;
            }//end getter
        }//end twoSpots

        /// <summary>
        /// the Grid object that contains this cell. If this
        /// cell is not in a grid object, then this will be
        /// equal to Grid.BlankGrid
        /// </summary>
        public Grid OwningGridObject { get; set; }

        /// <summary>
        /// A static definition for a cell with nothing in it at all.
        /// Used primarily to check if a row has been assigned a Cell.
        /// </summary>
        public static Cell BlankCell
        {
            get
            {
                Cell blankCell = new Cell();
                return blankCell;
            }//end getter
        }//end BlankCell

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
                rows[index].CurrentCellOwner = this;
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
        /// cell object. Sets OwningGridObject to BlankGrid
        /// </summary>
        /// <param name="cell">The Cell you wish to copy</param>
        public Cell(Cell cell)
        {
            //creates deep copy
            this.Rows = cell.Rows;
            this.OwningGridObject = Grid.BlankGrid;
        }//end 1-arg copy constructor

        /// <summary>
        /// initializes this object with a single row
        /// </summary>
        /// <param name="row">the row which will be initialized into Rows</param>
        public Cell(Row row)
        {
            rows.Add(new Row(row));
            row.CurrentCellOwner = this;
        }//end 1-arg row constructor

        /// <summary>
        /// initializes this object with its rows initializes as the 
        /// specified list of rows.
        /// </summary>
        /// <param name="rows">The rows you wish to initialize the object
        /// with</param>
        public Cell(List<Row> rows)
        {
            this.Rows = rows;
            foreach(Row row in Rows)
            {
                row.CurrentCellOwner = this;
            }//end setting cellowner to each row
        }//end 1-arg list constructor

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
            item.CurrentCellOwner = this;
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
            List<Row> tempRowList = Rows;
            foreach (Row row in tempRowList)
            {
                yield return row;
            }//end yield returning each row
        }//end GetEnumerator()

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }//end IEnumerable.GetEnumerator()
    }//end class
}//end namespace