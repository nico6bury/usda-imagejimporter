using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Nicholas Sixbury
 * File: Grid.cs
 * Purpose: To serve as a sort of wrapper around a list of Cells
 * in order to at least partially treat a list of lists as one
 * dimensional.
 */

namespace ImageJImporter
{
    public class Grid : ICollection<Cell>, ICollection<Row>
    {
        private List<Cell> cells = new List<Cell>();
        public List<Cell> Cells
        {
            get
            {
                List<Cell> tempCellList = new List<Cell>();
                foreach(Cell cell in cells)
                {
                    //creates deep copy
                    tempCellList.Add(new Cell(cell));
                }//end iterating over all cells in cells
                return tempCellList;
            }//end getter
            set
            {
                if(value != null)
                {
                    List<Cell> tempCellList = new List<Cell>();
                    foreach(Cell cell in value)
                    {
                        tempCellList.Add(new Cell(cell));
                    }//end foreach
                    cells = tempCellList;
                }//end if the value isn't null
                else
                {
                    throw new ArgumentNullException("You cannot set a Grid\'s " +
                        "internal cell list to null.");
                }//end else the value is null
            }//end setter
        }//end Cells

        /// <summary>
        /// read-only property for returning list of rows in this object
        /// </summary>
        public List<Row> Rows
        {
            get
            {
                List<Row> tempRowList = new List<Row>();
                foreach(Cell cell in cells)
                {
                    foreach(Row row in cell)
                    {
                        tempRowList.Add(row);
                    }//end looping over rows
                }//end looping over cells
                return tempRowList;
            }//end getter
        }//end Rows

        /// <summary>
        /// returns the total number of rows contained in this object
        /// </summary>
        public int Count
        {
            get
            {
                int counter = 0;
                
                for(int i = 0; i < cells.Count; i++)
                {
                    for(int j = 0; j < cells[i].Count; j++)
                    {
                        counter++;
                    }//end looping over rows
                }//end looping over cells

                return counter;
            }//end getter
        }//end Count

        public bool IsReadOnly => false;

        /// <summary>
        /// access an index of this object as if it were a 1-d
        /// array or list.
        /// </summary>
        /// <param name="index">the index of the row you want</param>
        /// <returns>the specified row</returns>
        public Row this[int index]
        {
            get
            {
                if(index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException($"index {index} is out of range.");
                }//end if we have an error
                else
                {
                    int rowsPassed = 0;
                    for (int i = 0; i < cells.Count; i++)
                    {
                        for (int j = 0; j < cells[i].Count; j++)
                        {
                            if (rowsPassed == index)
                            {
                                return new Row(cells[i][j]);
                            }//end if we found the right row
                            rowsPassed++;
                        }//end looping over rows
                    }//end looping over cells
                }//end else we are in bounds

                //we shouldn't ever get here, but to make the compiler happy:
                throw new ArgumentException("The specified index does not exist.");
            }//end getter
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException($"index {index} is out of range.");
                }//end if we have an error
                else
                {
                    int rowsPassed = 0;
                    for (int i = 0; i < cells.Count; i++)
                    {
                        for (int j = 0; j < cells[i].Count; j++)
                        {
                            if (rowsPassed == index)
                            {
                                cells[i][j] = new Row(value);
                            }//end if we found the right row
                            rowsPassed++;
                        }//end looping over rows
                    }//end looping over cells
                }//end else we are within the bounds
            }//end setter
        }//end 1-dimensional indexer

        /// <summary>
        /// access an index of this object as if it were a 2-d
        /// array or list.
        /// </summary>
        /// <param name="cellIndex">The index of the cell you want</param>
        /// <param name="rowIndex">The index within the cell of the
        /// row that you want</param>
        /// <returns>returns the specified Row</returns>
        public Row this[int cellIndex, int rowIndex]
        {
            get
            {
                return new Row(cells[cellIndex][rowIndex]);
            }//end getter
            set
            {
                cells[cellIndex][rowIndex] = new Row(value);
            }//end setter
        }//end 2-dimensional indexer

        /// <summary>
        /// initializes the object and its cell list, nothing else
        /// </summary>
        public Grid()
        {
            //I guess don't do anything ¯\_(ツ)_/¯
        }//end no-arg constructor

        /// <summary>
        /// initializes this object as an exact copy of the specified
        /// grid
        /// </summary>
        /// <param name="grid">the grid you wish to copy</param>
        public Grid(Grid grid)
        {
            foreach(Cell cell in grid.cells)
            {
                //add a deep copy to our internal cell list
                this.cells.Add(new Cell(cell));
            }//end looping over cells
        }//end 1-arg copy constructor

        /// <summary>
        /// initializes this object with a specified list of cells
        /// </summary>
        /// <param name="cells">the list of cells to associate with this
        /// object. Reference safe.</param>
        public Grid(List<Cell> cells)
        {
            Cells = cells;
        }//end 1-arg cell list constructor

        /// <summary>
        /// Attempts to initialize this object by adding all the specified rows
        /// to proper cells through the use of this class's Add(row) method. In
        /// the case that some rows cause exceptions caused by the Add(row) method,
        /// the indices of said rows will be printed out in an exception, but
        /// they'll still be added, and this object will probably still be initialized
        /// </summary>
        /// <param name="rows">the rows you want to initialize this object with</param>
        /// <exception cref="ArgumentException"></exception>
        public Grid(List<Row> rows)
        {
            //initialize some helpful variables
            bool exceptionEncountered = false;
            StringBuilder eb = new StringBuilder();
            eb.AppendLine("\tThe following exceptions were encountered: ");

            //start looping through everything
            for(int i = 0; i < rows.Count; i++)
            {
                try
                {
                    Add(rows[i]);
                }//end trying to add a new row
                catch (DoubleNewRowException dnre)
                {
                    exceptionEncountered = true;
                    eb.AppendLine($"DoubleNewRowException Enountered at rows[{i}]. " +
                        $"The message was \"{dnre.Message}\"");
                }//end catching DoubleNewRowExceptions
                catch (PriorCellIncompleteException pcie)
                {
                    exceptionEncountered = true;
                    eb.AppendLine($"PriorCellIncompleteException Encountered at rows[{i}]. " +
                        $"The message was \"{pcie.Message}\"");
                }//end catching PriorCellIncompleteExceptions
            }//end looping over all the rows

            //figure out if we need to print an exception
            if (exceptionEncountered)
            {
                throw new ArgumentException(eb.ToString());
            }//end if we encountered an exception
        }//end 1-arg row list constructor

        /// <summary>
        /// Adds a new Cell item
        /// </summary>
        /// <param name="cell">the Cell you wish to add</param>
        public void Add(Cell cell)
        {
            cells.Add(new Cell(cell));
        }//end Add(item)

        /// <summary>
        /// Adds a new row item to the most appropriate cell or if necessary
        /// creates a new cell to add the row to. If adding to a new cell, make
        /// sure it's a CellStartFlag or RowStartFlag. If adding to an existing
        /// cell, please make sure it's either a normal row or a CellEndFlag.
        /// </summary>
        /// <param name="row">the Row you wish to add</param>
        /// <exception cref="DoubleNewRowException"></exception>
        /// <exception cref="PriorCellIncompleteException"></exception>
        public void Add(Row row)
        {
            //check to make sure we actually have cells already
            if(cells.Count == 0)
            {
                cells.Add(new Cell(new Row(row)));
                return;
            }//end if we should just add it and stop

            //initialize some reference variables
            Cell lastCell = cells[cells.Count - 1];
            
            //start figuring out where and what to add
            if (row.IsNewRowFlag)
            {
                //adds the row as a new cell with a single row
                cells.Add(new Cell(new Row(row)));

                if (!lastCell.IsFullCell && !lastCell.IsNewRowFlag)
                {
                    throw new DoubleNewRowException("You have added two new grid " +
                        "rows without anything in between them. This likely indicates " +
                        "corrupted input data. I added them anyways though.");
                }//end if the last cell was not ended properly
            }//end if row is NewRowFlag
            else if (row.IsSeedStartFlag)
            {
                //adds the row as a new cell with a signle row
                cells.Add(new Cell(new Row(row)));

                if(!lastCell.IsFullCell && !lastCell.IsNewRowFlag)
                {
                    throw new PriorCellIncompleteException("The previous cell in this" +
                        " grid does not seem to have been ended properly. This likely " +
                        "indicates input data has been corrupted somehow. The new row " +
                        "was added anyways though.");
                }//end if the last cell was not ended properly
            }//end if row is SeedStartFlag
            else
            {
                lastCell.Add(new Row(row));
            }//end else we should just append this row to the end of the last cell
        }//end Add(row)

        /// <summary>
        /// clears the list of cells.
        /// </summary>
        public void Clear()
        {
            cells.Clear();
        }//end Clear()

        /// <summary>
        /// returns true if this grid contains the specified cell, 
        /// based on Cell.Equals
        /// </summary>
        public bool Contains(Cell cell)
        {
            foreach(Cell cellItem in cells)
            {
                if (cellItem.Equals(cell)) return true;
            }//end looping over each cell
            return false;
        }//end Contains(cell)

        /// <summary>
        /// returns true if this grid contains the specified row
        /// in any of its cells. Based on Row.Equals
        /// </summary>
        public bool Contains(Row row)
        {
            foreach(Cell cellItem in cells)
            {
                foreach(Row rowItem in cellItem)
                {
                    if (rowItem.Equals(row)) return true;
                }//end looping over rows
            }//end looping over cells
            return false;
        }//end Contains(row)

        /// <summary>
        /// Copies the cells from Cells into the specified array, with the first copy
        /// being put into cellArray[arrayIndex]. Continues until either this object runs
        /// out of cells or the specified array runs out of indices
        /// </summary>
        /// <param name="cellArray">The array of cells you wish to copy to</param>
        /// <param name="arrayIndex">the index in the cellArray you wish to start at</param>
        public void CopyTo(Cell[] cellArray, int arrayIndex)
        {
            for(int i = 0; i < cells.Count && arrayIndex < cellArray.Length; i++)
            {
                cellArray[arrayIndex] = new Cell(cells[i]);
                arrayIndex++;
            }//end looping over cells
        }//end CopyTo(cellArray, arrayIndex)

        /// <summary>
        /// Copies the rows from all this object's cells into the specified array, with
        /// the first copy being put into rowArray[arrayIndex]. Continues until either this
        /// object runs out of rows or the specified array runs out of indices.
        /// </summary>
        /// <param name="rowArray">The array of rows you wish to copy to</param>
        /// <param name="arrayIndex">the index in the rowArray you wish to start at</param>
        public void CopyTo(Row[] rowArray, int arrayIndex)
        {
            for(int i = 0; i < cells.Count; i++)
            {
                for(int j = 0; j < cells[i].Count; j++)
                {
                    rowArray[arrayIndex] = new Row(cells[i][j]);
                    arrayIndex++;
                }//end looping over rows
            }//end looping over cells
        }//end CopyTo(rowArray, arrayIndex)

        /// <summary>
        /// removes the specified cell from this object, including all its rows
        /// </summary>
        /// <param name="cell">the cell to remove</param>
        /// <returns>returns true if the cell was successfully removed, or false if
        /// it was not removed</returns>
        public bool Remove(Cell cell)
        {
            return cells.Remove(cell);
        }//end Remove(cell)

        /// <summary>
        /// removes the lowest index instance of the cell in this object that contains the
        /// specified row
        /// </summary>
        /// <param name="row">the row to remove</param>
        /// <returns>returns true if the cell was successfully removed, or false if 
        /// it was not removed</returns>
        public bool Remove(Row row)
        {
            foreach(Cell cellItem in cells)
            {
                if (cellItem.Contains(row))
                {
                    return cells.Remove(cellItem);
                }//end if this cell contains the specified row
            }//end looping through cells
            //we must not have found it...
            return false;
        }//end Remove(row)

        /// <summary>
        /// Enumerator for returning rows
        /// </summary>
        /// <returns>returns enumeration of row</returns>
        public IEnumerator<Row> GetEnumerator()
        {
            foreach (Cell cell in cells)
            {
                List<Row> tempRowList = cell.Rows;
                foreach (Row row in tempRowList)
                {
                    yield return row;
                }//end looping over rows
            }//end looping over cells
        }//end Cell Enumerator

        /// <summary>
        /// not sure how this is even called
        /// </summary>
        /// <returns>returns Enumerator of Cells</returns>
        IEnumerator<Cell> IEnumerable<Cell>.GetEnumerator()
        {
            foreach (Cell cell in cells)
            {
                yield return new Cell(cell);
            }//end looping over each cell
        }//end whatever this is

        /// <summary>
        /// Enumerator for returning rows
        /// </summary>
        /// <returns>returns enumeration of rows</returns>
        IEnumerator<Row> IEnumerable<Row>.GetEnumerator()
        {
            foreach(Cell cell in cells)
            {
                List<Row> tempRowList = cell.Rows;
                foreach(Row row in tempRowList)
                {
                    yield return row;
                }//end looping over rows
            }//end looping over cells
        }//end Row Enumerator

        /// <summary>
        /// returns enumeration of rows
        /// </summary>
        /// <returns>returns rows in this object</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }//end IEnumerable.GetEnumerator()
    }//end class

    /// <summary>
    /// exception called when you try to do two new grid rows
    /// right after another without any actual data cells in between
    /// </summary>
    public class DoubleNewRowException : Exception
    {
        public DoubleNewRowException()
        {
        }

        public DoubleNewRowException(string message)
            : base(message)
        {
        }

        public DoubleNewRowException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }//end class DoubleNewRowException

    /// <summary>
    /// exception called when you try to start a new cell without
    /// ending the previous one
    /// </summary>
    public class PriorCellIncompleteException: Exception
    {
        public PriorCellIncompleteException()
        {
        }

        public PriorCellIncompleteException(string message)
            : base(message)
        {
        }

        public PriorCellIncompleteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }//end class PriorCellIncompleteException
}//end namespace