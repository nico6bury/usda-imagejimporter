using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Author: Nicholas Sixbury
 * File: Controller.cs
 * Purpose: To keep track of and control other parts of the application. All "business logic" should be handled here.
 */

namespace ImageJImporter
{
    /// <summary>
    /// this class coordinates things between the model and the view
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// This object contains all the methods and things for handling File IO
        /// </summary>
        private FileIO fileIO;

        /// <summary>
        /// function pointer for ShowMessage in view. set up in program.cs
        /// </summary>
        public ShowFormMessage showMessage;

        /// <summary>
        /// function pointer for UpdateGrid in view. set up in program.cs
        /// </summary>
        public SendGrid updateGrid;

        /// <summary>
        /// function pointer for the CloseRowList in view. set up in program.cs
        /// </summary>
        public CallMethod closeSeedList;

        /// <summary>
        /// function pointer to request a new filename from the view
        /// </summary>
        public RequestSpecificString getNewFilename;

        /// <summary>
        /// function pointer to tell the view to update it's level information
        /// </summary>
        public SendLevelInformation updateLevelInformation;

        /// <summary>
        /// the grid that represents all of this object's cells and the rows
        /// that comprise them
        /// </summary>
        private Grid internalGrid = new Grid();

        /// <summary>
        /// stores whether or not there is currently a file loaded in the program
        /// </summary>
        private bool IsFileCurrentlyLoaded = false;

        /// <summary>
        /// all the information for the levels of seed stuff. This should be updated
        /// directly whenever the view gets it updated
        /// </summary>
        private LevelInformation allLevelInformation = new LevelInformation();

        /// <summary>
        /// this is the standard constructor for this class. It requires a FileIO
        /// object, which it calls File IO functions from. In order to avoid accidental
        /// reference passing, it creates a copy of the FileIO object provided
        /// </summary>
        /// <param name="fileIO">The fileIO object that will be used here. It can
        /// be a new object or one which has already been used.</param>
        public Controller(FileIO fileIO)
        {
            this.fileIO = new FileIO(fileIO);
        }//end constructor

        public void GetLevelInfoFromView(LevelInformation levels)
        {
            this.allLevelInformation = levels;
            updateLevelInformation(allLevelInformation);
            updateGrid(internalGrid);
        }//end GetLevelInfoFromView(levels)

        /// <summary>
        /// handle event of user wanting to open a file
        /// </summary>
        public void OpenDataFile()
        {
            //get filename from the view
            string filename = getNewFilename(Request.OpenFile);
            if (filename != null)
            {
                //get the row list from the file
                List<Row> curList = fileIO.LoadFile(filename);

                //create our grid
                Grid grid = new Grid(curList);

                //reset the view before we update it
                closeSeedList();

                //update list of seeds in the view
                if (updateGrid(grid))
                {
                    //reset and update CurrentRowList
                    internalGrid = new Grid(curList);

                    //updates boolean
                    IsFileCurrentlyLoaded = true;

                    //update log
                    AppendToHeaderLog($"Loaded {BuildFileMessage(filename, internalGrid.Rows)}");
                }//end if operation was successful
                else
                {
                    showMessage("An error has occured while updating the row list.",
                        "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }//end else the operation failed
            }//end if the filename isn't empty
            //if the filename was empty, don't do anything
        }//end OpenDataFile()

        /// <summary>
        /// Saves the file that is currently loaded
        /// </summary>
        public void SaveCurrentFile()
        {
            if (IsFileCurrentlyLoaded)
            {
                //tell fileIO to save our current file with current data
                fileIO.SaveFile(fileIO.file, internalGrid.Rows);

                //update log
                AppendToHeaderLog($"Successfully saved {internalGrid.Count}" +
                    $" rows to \"{fileIO.file}\"");
            }//end if a file is currently loaded
            else
            {
                showMessage("You don't have a file loaded.", "Invalid Operation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }//end else there is not file loaded
        }//end SaveCurrentFile()

        /// <summary>
        /// saves the current row information in the specified file
        /// </summary>
        public void SaveCurrentListAsNewFile()
        {
            if (IsFileCurrentlyLoaded)
            {
                //get the filename from the 
                string newFileName = getNewFilename(Request.SaveFileAs);
                if(newFileName != null)
                {
                    //save the row information as the specified filename
                    fileIO.SaveFile(newFileName, internalGrid.Rows);

                    //update log
                    AppendToHeaderLog($"Successfully saved {internalGrid.Count}" +
                        $" rows to \"{newFileName}\"");
                }//end if we have a file
                //if the filename was empty, we don't want to do anything
            }//end if there's currently a file loaded
            else
            {
                showMessage("You don't have a file loaded.", "Invalid Operation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }//end else there's no file loaded
        }//end SaveCurrentListAsNewFile()

        /// <summary>
        /// Closes the current file and updates any necessary displays or flags
        /// </summary>
        public void CloseCurrentFile()
        {
            //updates reference variables in this class
            internalGrid.Clear();
            IsFileCurrentlyLoaded = false;

            StringBuilder tempFileNameRef = new StringBuilder(fileIO.file);

            //removes last file reference from fileIO
            fileIO.file = "";

            //updates seed list in view
            closeSeedList();

            //update log
            AppendToHeaderLog($"Successfully closed \"{tempFileNameRef}\"");
        }//end CloseCurrentFile()

        /// <summary>
        /// Verifies that every index in the supplied enumberable collection
        /// of indices is valid for CurrentRowList
        /// </summary>
        /// <param name="indices">the enumerable collection of indices</param>
        /// <exception cref="ArgumentOutOfRangeException">Exception thrown when
        /// an index supplied is outside the bounds of CurrentRowList</exception>
        private void CheckIndices(IEnumerable<int> indices)
        {
            foreach (int index in indices)
            {
                if (index < 0 || index >= internalGrid.Count)
                {
                    throw new ArgumentOutOfRangeException($"The supplied index" +
                        $" {index} was outside the acceptable range from 0 " +
                        $"to {internalGrid.Count - 1}.");
                }//end if the index is outside range of list
            }//end checking each index is valid
        }//end CheckIndices(indices)

        /// <summary>
        /// Builds a list of all the rows at the specified indices with
        /// CurrentRowList. Doesn't do any sort of verification that indices are
        /// valid.
        /// </summary>
        /// <param name="indices">indices at which to grab rows</param>
        /// <returns>list of rows at specified indices</returns>
        private List<Row> GetRowsAtIndices(List<int> indices)
        {
            //initialize list of rows
            List<Row> rowRegister = new List<Row>();

            foreach (int index in indices)
            {
                rowRegister.Add(internalGrid[index]);
            }//end adding each requested row to rowRegister

            return rowRegister;
        }//end GetRowsAtIndices(indices)

        /// <summary>
        /// Saves the row data of the specified rows at the specified indices
        /// </summary>
        /// <param name="rowIndexPairs">a dictionary which contains KeyValuePairs
        /// for each row. The Key is the string which represents the row, and 
        /// the value is the index of that row within the larger list</param>
        public void SaveRowData(Dictionary<string, int> rowIndexPairs)
        {
            //verify all indices are valid
            CheckIndices(rowIndexPairs.Values);

            //start trying to get all the row data
            try
            {
                //initialize dictionary to hold processed row data
                Dictionary<Row, int> processedRowIndexPairs = new Dictionary<Row, int>();
                
                //make Row object from each row string and add it to our dictionary
                foreach (string rowAsString in rowIndexPairs.Keys)
                {
                    //get row from string data
                    Row newRow = new Row(rowAsString);

                    //add that row to our new dictionary
                    processedRowIndexPairs.Add(newRow, rowIndexPairs[rowAsString]);
                }//end processing data for each row

                //update our internal list of row information
                Grid tempGrid = new Grid(internalGrid);
                foreach(Row newRow in processedRowIndexPairs.Keys)
                {
                    int index = processedRowIndexPairs[newRow];
                    tempGrid[index] = newRow;
                }//end updating internal list for each row

                //try to update the grid in the view
                if (updateGrid(tempGrid))
                {
                    internalGrid = new Grid(tempGrid);
                    //update log so user knows save operation was successful
                    AppendToHeaderLog($"Successfully saved {processedRowIndexPairs.Count}" +
                        $" rows to internal reference list. To update file, select" +
                        $" option to save data to a file.");
                }//end if we succeeded
                else
                {
                    //tell the user that something went wrong
                    showMessage("The operation to update the displayed grid has failed."
                        , "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AppendToHeaderLog("Failed to update grid display after successfully processing all rows.");
                }//end else we failed somehow
            }//end trying to process all row strings
            catch
            {
                //display error message to user
                showMessage("One of the edited rows cannot be read. Please" +
                    " fix formatting before trying to save it.", "Row Format" +
                    " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //update log so user has record of failing to save seed data
                AppendToHeaderLog($"Row Data" +
                    $" Save Operation Failed.");
            }//end catching row string processing errors
        }//end SaveRowData(rowIndexPairs)

        /// <summary>
        /// Updates the level information saved by the controller
        /// </summary>
        /// <param name="levelInformation">the level information we
        /// got from the user</param>
        public void UpdateLevelInformation(LevelInformation levelInformation)
        {
            this.allLevelInformation = levelInformation;
        }//end UpdateLevelInformation(levelInformation)

        /// <summary>
        /// loads some information from the config file and sends
        /// relevant information to the view
        /// </summary>
        public void OpenView()
        {
            //Make a level information object to pass as out param
            LevelInformation tempLevelsRef;

            //get data file to load plus level information
            string defaultFileName = fileIO.LoadConfigFile(out tempLevelsRef);

            //set our level information up correctly, we hope
            allLevelInformation = tempLevelsRef;

            //we should always send level information to the view
            updateLevelInformation(allLevelInformation);

            //load data if there was a file to load
            if(!String.IsNullOrEmpty(defaultFileName))
            {
                //load all the row information from config file
                List<Row> rows = fileIO.LoadFile(defaultFileName);

                //save our row list to our internal grid
                internalGrid = new Grid(rows);

                //pass grid back to the view
                updateGrid(internalGrid);

                //update boolean flag
                IsFileCurrentlyLoaded = true;

                //update fileIO's most recently used file property
                fileIO.file = defaultFileName;

                //update log with recent file name and row count
                AppendToHeaderLog("Found configuration file. Loaded" +
                    $" {BuildFileMessage(fileIO.file, internalGrid.Rows)}");
            }//end if the data isn't null
        }//end OpenView()

        /// <summary>
        /// builds a message for doing something with a file.
        /// </summary>
        /// <param name="filename">the full path of the filename you
        /// wish to create a message from.</param>
        /// <param name="rows">the list of rows you got from the file</param>
        /// <returns>Returns a message with the name of the file, its
        /// current directory, and the number of rows and row flags</returns>
        private string BuildFileMessage(string filename, List<Row> rows)
        {
            StringBuilder sb = new StringBuilder();

            //add file path information to message
            sb.Append($"\"{Path.GetFileName(filename)}\" at " +
                $"\"{Path.GetDirectoryName(filename)}\".");
            
            //start getting row information
            if(rows != null)
            {
                //initialize counter variables
                int gridRows = 0;
                int cellStarts = 0;
                int cellEnds = 0;
                foreach(Row row in rows)
                {
                    if (row.IsNewRowFlag) gridRows++;
                    if (row.IsSeedStartFlag) cellStarts++;
                    if (row.IsSeedEndFlag) cellEnds++;
                }//end looping over every row in rows

                //add the row information to our message
                sb.Append($" File contains {gridRows} flags for " +
                    $"a new row, {cellStarts} cell start flags," +
                    $" and {cellEnds} cell end flags.");
            }//end if the rows list is valid
            
            //return our result
            return sb.ToString();
        }//end BuildFileMessage(filename)

        /// <summary>
        /// saves current configuration information to the config file
        /// </summary>
        public void CloseView()
        {
            //saves configuration info to config file
            fileIO.SaveConfigFile(allLevelInformation);
        }//end CloseView()

        /// <summary>
        /// saves the specified lines to the monthly log file
        /// </summary>
        /// <param name="lines">the lines to save to the log</param>
        public void SaveLogToFile(string[] lines)
        {
            //tell the fileIO to save the lines to the monthly log file
            fileIO.SaveLinesToLog(lines);
        }//end SaveLogToFile(lines)

        public SendString appendTextLog;
        public void AppendToHeaderLog(string text)
        {
            appendTextLog(text);
        }//end AppendToHeaderLog(text)
    }//end class
}//end namespace