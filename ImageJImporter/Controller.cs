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
        /// function pointer for UpdateGrids in view. set up in program.cs
        /// </summary>
        public SendGrids updateGrids;

        /// <summary>
        /// function pointer for the CloseRowList in view. set up in program.cs
        /// </summary>
        public CallMethod closeSeedList;

        /// <summary>
        /// function pointer to request a new filename from the view
        /// </summary>
        public RequestSpecificStrings getNewFilename;

        /// <summary>
        /// function pointer to tell the view to update it's level information
        /// </summary>
        public SendLevelInformation updateLevelInformation;

        /// <summary>
        /// Tell the view to show something to the user
        /// </summary>
        public SendStrings sendExcelExport;

        /// <summary>
        /// the grid that represents all of this object's cells and the rows
        /// that comprise them
        /// </summary>
        private List<Grid> internalGrids = new List<Grid>();

        /// <summary>
        /// stores whether or not there is currently a file loaded in the program
        /// </summary>
        private bool IsFileCurrentlyLoaded = false;

        /// <summary>
        /// all the information for the levels of seed stuff. This should be updated
        /// directly whenever the view gets it updated
        /// </summary>
        private LevelInformation allLevelInformation = new LevelInformation();

        private LogManager generalLog;
        private LogManager sumLog;
        private LogManager gridLog;
        private LogManager excelLog;
        private LogManager chalkLog;
        private LogManager chalkBlockLog;
        private LogManager germLog;

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
            this.generalLog = new LogManager(fileIO.GenerateLogDirectory(), $"HVAC - Log - {DateTime.Now:MMMM}.txt");
            this.sumLog = new LogManager(fileIO.GenerateLogDirectory(), $"HVAC - Sum - {DateTime.Now:MMMM}.txt");
            this.gridLog = new LogManager(fileIO.GenerateLogDirectory(), $"HVAC - Grids - {DateTime.Now:MMMM}.txt");
            this.excelLog = new LogManager(fileIO.GenerateLogDirectory(), $"HVAC - Grids Excel Format - {DateTime.Now:MMMM}.txt");
            this.chalkLog = new LogManager(fileIO.GenerateLogDirectory(), $"HVAC - Chalk - {DateTime.Now:MMMM}.txt");
            this.chalkBlockLog = new LogManager(fileIO.GenerateLogDirectory(), $"HVAC - Chalk Block - {DateTime.Now:MMMM}.txt");
            this.germLog = new LogManager(fileIO.GenerateLogDirectory(), $"HVAC - Germ Detect - {DateTime.Now:MMMM}.txt");
        }//end constructor

        public void GetLevelInfoFromView(LevelInformation levels)
        {
            this.allLevelInformation = levels;
            updateLevelInformation(allLevelInformation);
            updateGrids(internalGrids);
            //for(int i = 0; i < internalGrids.Count; i++)
            //{
            //    AppendShortSummaryToLog(internalGrids[i].Filename, internalGrids[i], this.allLevelInformation);
            //}//end looping over all the grids
        }//end GetLevelInfoFromView(levels)

        /// <summary>
        /// handle event of user wanting to open a file
        /// </summary>
        public void OpenDataFile()
        {
            //get filename from the view
            string[] filenames = getNewFilename(Request.OpenFile);
            if (filenames != null)
            {
                //get the row lists from the file
                List<List<Row>> curLists = new List<List<Row>>();
                List<string> curFilenames = new List<string>();
                foreach(string filename in filenames)
                {
                    List<Row> curList = fileIO.LoadFile(filename);
                    curLists.Add(curList);
                    curFilenames.Add(filename);
                }//end looping over all the files

                //create our grids
                List<Grid> grids = new List<Grid>();
                for(int i = 0; i < curLists.Count; i++)
                {
                    Grid grid = new Grid(curLists[i]);
                    grid.Filename = curFilenames[i];
                    grids.Add(grid);
                }//end looping foreach row list in curlists

                //reset the view before we update it
                closeSeedList();

                //tries updating view with all grids, then gets whether we were successful
                List<bool> successes = updateGrids(grids);

                //update boolean
                IsFileCurrentlyLoaded = true;

                if (successes.Contains(false))
                {
                    //display error message
                    showMessage("Some or all of the grids we tried to display have failed.",
                        "Grid Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //append short error message to log
                    AppendToHeaderLog("An attempt to display multiple grids was made, but some of" +
                        "them failed to be properly displayed.\n");
                }//end if we had an error
                else
                {
                    //reset and update internal grid
                    internalGrids = new List<Grid>();
                    internalGrids = grids;

                    //update log
                    string[] tempFilenames = new string[grids.Count];
                    for (int i = 0; i < grids.Count; i++)
                    {
                        AppendToHeaderLog($"Loaded {BuildFileMessage(grids[i].Filename, grids[i].Rows)}");
                        AppendShortSummaryToLog(grids[i].Filename, grids[i], this.allLevelInformation);
                        tempFilenames[i] = grids[i].Filename;
                    }//end looping over all the grids
                    AppendToHeaderLog("\n");
                    //generate an array of filenames
                    if(tempFilenames.Length > 0)
                        AppendLongSummaryToInternalLog(tempFilenames, grids, allLevelInformation);
                }//end else we're all clear
            }//end if the filename isn't empty
            //if the filename was empty, don't do anything
        }//end OpenDataFile()

        /// <summary>
        /// Saves the file that is currently loaded
        /// </summary>
        public void SaveCurrentFile(int index)
        {
            if (IsFileCurrentlyLoaded)
            {
                //tell fileIO to save our current file with current data
                fileIO.SaveFile(internalGrids[index].Filename, internalGrids[index].Rows);

                //update log
                AppendToHeaderLog($"Successfully saved {internalGrids[index].Count}" +
                    $" rows to \"{fileIO.mostRecentAccessedFile}\"\n");
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
        public void SaveCurrentListAsNewFile(int index)
        {
            if (IsFileCurrentlyLoaded)
            {
                //get the filenames from the view
                string[] newFileNames = getNewFilename(Request.SaveFileAs);
                if(newFileNames != null)
                {
                    //save the row information as the specified filename
                    fileIO.SaveFile(newFileNames[0], internalGrids[index].Rows);

                    //update log
                    AppendToHeaderLog($"Successfully saved {internalGrids[index].Count}" +
                        $" rows to \"{newFileNames[0]}\"\n");
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
            internalGrids.Clear();
            IsFileCurrentlyLoaded = false;

            StringBuilder tempFileNameRef = new StringBuilder(fileIO.mostRecentAccessedFile);

            //removes last file reference from fileIO
            fileIO.mostRecentAccessedFile = "";

            //updates seed list in view
            closeSeedList();

            //update log
            AppendToHeaderLog($"Successfully closed \"{tempFileNameRef}\"\n");
        }//end CloseCurrentFile()

        /// <summary>
        /// Verifies that every index in the supplied enumberable collection
        /// of indices is valid for CurrentRowList
        /// </summary>
        /// <param name="indices">the enumerable collection of indices</param>
        /// <exception cref="ArgumentOutOfRangeException">Exception thrown when
        /// an index supplied is outside the bounds of CurrentRowList</exception>
        private void CheckIndices(IEnumerable<int> indices, Grid grid)
        {
            foreach (int index in indices)
            {
                if (index < 0 || index >= grid.Count)
                {
                    throw new ArgumentOutOfRangeException($"The supplied index" +
                        $" {index} was outside the acceptable range from 0 " +
                        $"to {grid.Count - 1}.");
                }//end if the index is outside range of list
            }//end checking each index is valid
        }//end CheckIndices(indices)

        ///// <summary>
        ///// Builds a list of all the rows at the specified indices with
        ///// CurrentRowList. Doesn't do any sort of verification that indices are
        ///// valid.
        ///// </summary>
        ///// <param name="indices">indices at which to grab rows</param>
        ///// <returns>list of rows at specified indices</returns>
        //private List<Row> GetRowsAtIndices(List<int> indices)
        //{
        //    //initialize list of rows
        //    List<Row> rowRegister = new List<Row>();

        //    foreach (int index in indices)
        //    {
        //        rowRegister.Add(internalGrid[index]);
        //    }//end adding each requested row to rowRegister

        //    return rowRegister;
        //}//end GetRowsAtIndices(indices)

        /// <summary>
        /// Saves the row data of the specified rows at the specified indices
        /// </summary>
        /// <param name="rowIndexPairs">a dictionary which contains KeyValuePairs
        /// for each row. The Key is the string which represents the row, and 
        /// the value is the index of that row within the larger list</param>
        public void SaveRowData(Dictionary<string, int> rowIndexPairs, int gridIndex)
        {
            //verify all indices are valid
            CheckIndices(rowIndexPairs.Values, internalGrids[gridIndex]);

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
                Grid tempGrid = new Grid(internalGrids[gridIndex]);
                foreach(Row newRow in processedRowIndexPairs.Keys)
                {
                    int index = processedRowIndexPairs[newRow];
                    tempGrid[index] = newRow;
                }//end updating internal list for each row

                //try to update the grid in the view
                if (updateGrid(tempGrid))
                {
                    internalGrids[gridIndex] = new Grid(tempGrid);
                    //update log so user knows save operation was successful
                    AppendToHeaderLog($"Successfully saved {processedRowIndexPairs.Count}" +
                        $" rows to internal reference list. To update file, select" +
                        $" option to save data to a file.\n");
                }//end if we succeeded
                else
                {
                    //tell the user that something went wrong
                    showMessage("The operation to update the displayed grid has failed."
                        , "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AppendToHeaderLog("Failed to update grid display after successfully processing all rows.\n");
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
                    $" Save Operation Failed.\n");
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
            //AppendShortSummaryToLog(internalGrids, this.allLevelInformation);
        }//end UpdateLevelInformation(levelInformation)

        /// <summary>
        /// loads some information from the config file and sends
        /// relevant information to the view
        /// </summary>
        public void OpenView()
        {
            //Make a level information object to pass as out param
            LevelInformation tempLevelsRef;
            string[] defaultFileNames;
            Row.RowFlagProps rowFlagProps;
            bool useGermDetection;

            //get data file to load plus level information
            bool success = fileIO.LoadConfigFile(out tempLevelsRef, out defaultFileNames, out rowFlagProps, out useGermDetection);

            //send stuff to the view based on what we found
            if (success)
            {
                //update our level information
                allLevelInformation = tempLevelsRef;

                //update whether or not we're using germ detection
                Cell.UseGermDetection = useGermDetection;

                //set level information as default if it's not set up
                if (allLevelInformation.Count == 0 || String.IsNullOrEmpty(allLevelInformation.PropertyToTest))
                    allLevelInformation = LevelInformation.DefaultLevels; allLevelInformation.PropertyToTest = nameof(Cell.Chalk);

                //tell the view to update its level information
                updateLevelInformation(allLevelInformation);

                //update row flags based on what the config file told us
                Row.SetFlagsAndTolerances(rowFlagProps);

                internalGrids.Clear();
                for (int i = 0; i < defaultFileNames.Length; i++)
                {
                    if (!String.IsNullOrEmpty(defaultFileNames[i]) && File.Exists(defaultFileNames[i]))
                    {
                        //load row information from this file
                        List<Row> rows = fileIO.LoadFile(defaultFileNames[i]);

                        //save our row list to our internal grid
                        Grid tempGrid = new Grid(rows);
                        tempGrid.Filename = defaultFileNames[i];
                        internalGrids.Add(tempGrid);

                        //update boolean flag
                        IsFileCurrentlyLoaded = true;

                        //update log with recent file name and row count
                        AppendToHeaderLog("Found configuration file. Loaded" +
                            $" {BuildFileMessage(defaultFileNames[i], tempGrid.Rows)}");
                        //AppendShortSummaryToLog(defaultFileNames[i], tempGrid, tempLevelsRef);
                    }//end if the specified name is actually valid
                    //pass grid back to the view
                    if (internalGrids.Count > 0) updateGrids(internalGrids);
                    else
                        AppendToHeaderLog("A configuration file was found, but none of the files " +
                            "were valid");
                }//end looping over all the filenames
                AppendToHeaderLog("\n");
                if(defaultFileNames.Length > 0) { }
                    //AppendLongSummaryToInternalLog(defaultFileNames, internalGrids, tempLevelsRef);
            }//end if we successfully found a config file
            else
            {
                //generate level information and send it to the view
                allLevelInformation = LevelInformation.DefaultLevels;
                allLevelInformation.PropertyToTest = "Chalk";
                updateLevelInformation(allLevelInformation);
                AppendToHeaderLog("No Configuration File Found. Loading Defaults.\n");
            }//end else we didn't find a config file
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
            //get an array of filenames
            string[] filenamesFromGrids = new string[internalGrids.Count];
            for(int i = 0; i < internalGrids.Count; i++)
            {
                filenamesFromGrids[i] = internalGrids[i].Filename;
            }//end getting filename from each grid in internalGrids

            //saves configuration info to config file
            fileIO.SaveConfigFile(allLevelInformation, filenamesFromGrids, Row.CurrentRowFlagProperties, Cell.UseGermDetection);
        }//end CloseView()

        /// <summary>
        /// saves the specified lines to the monthly log file
        /// </summary>
        /// <param name="lines">the lines to save to the log</param>
        public void SaveLogToFile(string[] lines)
        {
            //save general log, then have all three write to files
            generalLog.AppendLog(lines);

            generalLog.WriteToLog();
            gridLog.WriteToLog();
            excelLog.WriteToLog();
            sumLog.WriteToLog();
            chalkLog.WriteToLog();
            chalkBlockLog.WriteToLog();
            germLog.WriteToLog();
        }//end SaveLogToFile(lines)

        public SendString appendTextLog;
        public void AppendToHeaderLog(string text)
        {
            appendTextLog(text);
        }//end AppendToHeaderLog(text)

        /// <summary>
        /// Appends the short format summary information for a particular file
        /// to the log. 
        /// </summary>
        /// <param name="filename">the file the data came from</param>
        /// <param name="grid">the data from the file</param>
        /// <param name="levels">the object which tells us how to categorize data</param>
        private void AppendShortSummaryToLog(string filename, Grid grid, LevelInformation levels)
        {
            AppendToHeaderLog(FileIO.FileLevelsProcessedToOneLine(filename, grid, levels));
        }//end AppendShortSummaryToLog(filename, grid, levels)

        /// <summary>
        /// Tells the view about some excel stuff.
        /// </summary>
        internal void GetExcelStuff()
        {
            // generate formatting for excel and send it to the view
            string[] excelFormat = GenerateExcelFormat();
            sendExcelExport(excelFormat);
        }//end GetExcelStuff()

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string[] GenerateExcelFormat()
        {
            // initialize counters for levels
            int[] levelCounters = new int[allLevelInformation.Levels.Count];

            // count up the levels
            string[] export = new string[levelCounters.Length + 4];
            foreach (Grid grid in internalGrids)
            {
                // initialize counters
                int germCount = 0;
                int twoSpot = 0;
                int sum = 0;

                // reset level counters
                for (int i = 0; i < levelCounters.Length; i++)
                {
                    levelCounters[i] = 0;
                }//end resetting levelCounters

                // build all the stuff
                foreach (Cell cell in grid.Cells)
                {
                    int levelIndex = allLevelInformation
                        .FindLevel(cell.Chalk).Item2;
                    try
                    {
                        levelCounters[levelIndex]++;
                    }//end trying to ignore bad indexes
                    catch (IndexOutOfRangeException) { }
                    catch (ArgumentOutOfRangeException) { }

                    if (cell.isGerm) germCount++;
                    if (cell.twoSpots) twoSpot++;
                    sum++;
                }//end looping foreach cell in this grid

                // format results for this grid
                int j = 0;
                for (j = 0; j < levelCounters.Length; j++)
                {
                    export[j] += $"{levelCounters[j]}\t";
                }//end looping for each level counter
                export[j] += $"{sum}\t"; j++;
                export[j] += ""; j++;
                export[j] += $"{germCount}\t"; j++;
                export[j] += $"{twoSpot}\t"; j++;
            }//end looping foreach grid in the program

            // remove extra tabs
            for (int i = 0; i < export.Length; i++)
            {
                string thisLine = export[i];
                if (thisLine == null || thisLine.Length < 1) continue;
                if (thisLine[thisLine.Length - 1] == '\t')
                {
                    export[i] = thisLine.Substring(0, thisLine.Length - 1);
                }//end if export ends with a tab
            }//end looping over export lines

            if (allLevelInformation.Levels[0].LevelStart < 0)
            {
                export[0] = "Don't copy this line";
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < internalGrids.Count; i++)
                {
                    sb.Append($"Grid{i + 1}\t");
                }//end looping for each grid in internal grids
                if (sb.Length > 0) export[0] = sb.ToString();
            }//end if we can do level headers
            return export;
        }//end GenerateExcelFormat()

        ///// <summary>
        ///// Appends the short format summary information for a particular file
        ///// to the log. 
        ///// </summary>
        ///// <param name="filenames">the file the data came from</param>
        ///// <param name="grids">the data from the file</param>
        ///// <param name="levels">the object which tells us how to categorize data</param>
        //private void AppendShortSummaryToLog(List<Grid> grids, LevelInformation levels)
        //{
        //    for (int i = 0; i < grids.Count; i++)
        //    {
        //        AppendToHeaderLog(FileIO.FileLevelsProcessedToOneLine(grids[i].Filename, grids[i], levels));
        //    }//end looping over all the grids and files
        //}//end AppendShortSummaryToLog(filename, grid, levels)

        /// <summary>
        /// Appends a summary to the log managers second line set.
        /// An ArgumentOutOfRangeException can be thrown if there are no filenames.
        /// </summary>
        /// <param name="dataGrids">the grids of data</param>
        /// <param name="levels">the level configuration settings</param>
        /// <param name="filenames">The names of the files we're getting the grids from</param>
        /// <exception cref="ArgumentOutOfRangeException">Can be thrown if there are no files to process</exception>
        private void AppendLongSummaryToInternalLog(string[] filenames, List<Grid> dataGrids, LevelInformation levels)
        {
            //initialize the strinbuilder for building the log message
            StringBuilder gridLogBuilder = new StringBuilder();
            StringBuilder sumLogBuilder = new StringBuilder();

            //get a list of the filenames
            StringBuilder fileLister = new StringBuilder("\n");
            foreach(string filename in filenames)
            {
                fileLister.Append($"{TrimFileName(Path.GetFileName(filename))}\n");
            }
            fileLister.Length -= 1;

            //add filenames, date, and grid number
            string setHeader = $"{DateTime.Now:F}\t{dataGrids.Count}-Grids";
            gridLogBuilder.Append($"{setHeader}\n");
            sumLogBuilder.Append($"{setHeader}\t{fileLister}\n");

            //each index should be 0 by default
            int[] allGridCounters = new int[levels.Count];
            int totalNonFlags = 0;
            for(int i = 0; i < dataGrids.Count; i++)
            {
                //figure out the number of each of the levels for this grid
                int[] perGridCounters = new int[levels.Count];
                 //count up all the levels
                int gridNonFlags = 0;
                foreach (Cell cell in dataGrids[i].Cells)
                {
                    //find out what level the cell is in
                    Tuple<string, int> levelresult = levels.FindLevel((decimal)cell.GetType().GetProperty(levels.PropertyToTest).GetValue(cell));
                    try
                    {
                        //increment the corresponding counter
                        perGridCounters[levelresult.Item2]++;
                        allGridCounters[levelresult.Item2]++;
                    }//end trying to ignore bad index ranges
                    catch (IndexOutOfRangeException) { }
                    catch (ArgumentOutOfRangeException) { }
                    //increment nonFLagCount
                    if (cell.IsFullCell && !cell.IsEmptyCell)
                    {
                        gridNonFlags++;
                        totalNonFlags++;
                    }//end if cell 
                }//end looping over cells in grid to add levels

                //print the label for this grid
                gridLogBuilder.Append($"{TrimFileName(Path.GetFileName(filenames[i]))}\n");

                //print out raw numbers for each level to log
                for (int j = 0; j < perGridCounters.Length; j++)
                {
                    // only print data levels
                    if(levels[j].LevelStart > 0)
                        gridLogBuilder.Append($"{levels.Levels[j].LevelName} = {perGridCounters[j]}\t");
                }//end looping for each level in the levels info
                 //add total number of levelled cells
                gridLogBuilder.Append($"Total = {gridNonFlags}\n");

                //print out percentages for each level
                decimal totalPercentage = 0;
                for (int k = 0; k < perGridCounters.Length; k++)
                {
                    decimal percentForThisLevel;
                    try
                    {
                        percentForThisLevel = (decimal)perGridCounters[k] / (decimal)gridNonFlags * 100;
                    }//end trying to perform division
                    catch (DivideByZeroException)
                    {
                        percentForThisLevel = 0;
                    }//end catching divide by zero errors
                    
                    
                    //make sure not to add non-data levels to percent total
                    if(levels[k].LevelStart > 0)
                        totalPercentage += percentForThisLevel;

                    //honestly don't even print non-data levels
                    if(levels[k].LevelStart > 0)
                        gridLogBuilder.Append($"{levels.Levels[k].LevelName} = {percentForThisLevel:N1}%\t");
                }//end looping for each level in the levels info
                gridLogBuilder.Append($"Total = {totalPercentage:0}%\n");
            }//end looping over each grid to get stats and populate the gridLogBuilder

            //go ahead and populate the sumLogBuilder
            //initialize two string builders for separate lines
            StringBuilder lineTotalBuilder = new StringBuilder();
            StringBuilder linePercentBuilder = new StringBuilder();
            //counter for total percentages
            decimal totalPercents = 0;
            for(int i = 0; i < levels.Count; i++)
            {
                //we'll just skip levels starting at 0 (that weeds out Lvl0)
                if (levels[i].LevelStart <= 0) continue;

                //get the total for this level
                int countForThisLevel = allGridCounters[i];
                lineTotalBuilder.Append($"{levels[i].LevelName} = {countForThisLevel}\t");

                //get the percent for this level
                try
                {
                    decimal percentForThisLevel = (decimal)countForThisLevel / (decimal)totalNonFlags * 100;
                    totalPercents += percentForThisLevel;
                    linePercentBuilder.Append($"{levels[i].LevelName} = {percentForThisLevel:N1}%\t");
                }//end trying to get the percent for this level
                catch (DivideByZeroException)
                {
                    linePercentBuilder.Append("No Non-Flags\t");
                }//end catching divide by zero exception
            }//end populating sumLogBuilder with info for each level
            //get total number and total percent to add to end
            lineTotalBuilder.Append($"Total = {totalNonFlags} Seeds\n");
            linePercentBuilder.Append($"Total = {totalPercents:N0}%\n");

            // go ahead and fill up the chalk log
            StringBuilder chalkBuilder = new StringBuilder();
            chalkBuilder.Append($"{setHeader}\n");
            foreach (Grid grid in dataGrids)
            {
                chalkBuilder.Append($"{Path.GetFileName(grid.Filename)}:\n");
                foreach(Cell cell in grid.Cells)
                {
                    if (!cell.IsEmptyCell && !cell.IsNewRowFlag && cell.IsFullCell)
                        chalkBuilder.Append($"{cell.Chalk:N2}\n");
                }//end looping over cells in grid
                chalkBuilder.Append('\n');
            }//end looping over each grid

            // go ahead and fill up the chalk block log
            StringBuilder chalkBlockBuilder = new StringBuilder();
            chalkBlockBuilder.Append($"{setHeader}\n");
            foreach(Grid grid in dataGrids)
            {
                chalkBlockBuilder.Append($"{Path.GetFileName(grid.Filename)}:\n");
                List<List<Cell>> jaggedGrid = grid.FormatCellsAs2DList();
                for(int i = 0; i < jaggedGrid.Count; i++)
                {
                    for(int j = 0; j < jaggedGrid[i].Count; j++)
                    {
                        if (!jaggedGrid[i][j].IsEmptyCell && !jaggedGrid[i][j].IsNewRowFlag && jaggedGrid[i][j].IsFullCell)
                            chalkBlockBuilder.Append($"{jaggedGrid[i][j].Chalk:N2}\t");
                    }//end looping over second dimension
                    chalkBlockBuilder.Length--;
                    chalkBlockBuilder.Append('\n');
                }//end looping over first dimension
                chalkBlockBuilder.Append('\n');
            }//end looping over each grid

            // finally go ahead and fill up the germ log
            StringBuilder germBuilder = new StringBuilder();
            germBuilder.Append($"{setHeader}\n");
            foreach(Grid grid in dataGrids)
            {
                germBuilder.Append($"{Path.GetFileName(grid.Filename)}:\n");
                foreach(Cell cell in grid.Cells)
                {
                    if(!cell.IsEmptyCell && !cell.IsNewRowFlag && cell.IsFullCell)
                    {
                        if (cell.isGerm) germBuilder.Append("1\n");
                        else germBuilder.Append("0\n");
                    }//end if this isn't a flag
                }//end getting germ detect from each cell
            }//end looping over each grid

            //go ahead and add our stuff to the log manager
            germLog.AppendLog($"{germBuilder}");
            chalkLog.AppendLog($"{chalkBuilder}\n");
            chalkBlockLog.AppendLog($"{chalkBlockBuilder}");
            gridLog.AppendLog($"{gridLogBuilder}\n");
            excelLog.AppendLog(GenerateExcelFormat());
            excelLog.AppendLog("\n\n");
            sumLog.AppendLog($"{sumLogBuilder}{lineTotalBuilder}{linePercentBuilder}\n");
        }//end AppendLongSummaryToInternalLog()

        /// <summary>
        /// Trims away the ImageJ and file-extension from a filename with the
        /// ImageJ-FGIS22-C-11-1-1.txt format
        /// </summary>
        /// <param name="filename">the name of the file you want to trim</param>
        /// <returns>a trimmed filename, great for being displayed as text</returns>
        private string TrimFileName(string filename)
        {
            StringBuilder outputBuilder = new StringBuilder();

            if (!filename.Contains("ImageJ-") || !filename.Contains(".txt"))
                return filename;

            bool foundFirstHyphen = false;
            foreach(char character in filename)
            {
                if (!foundFirstHyphen)
                {
                    if (character == '-')
                        foundFirstHyphen = true;
                }//end if we're still cutting ImageJ- out
                else if (character == '.') break;
                else outputBuilder.Append(character);
            }//end looping over each character in the filename

            return $"{outputBuilder}";
        }//end TrimFileName(filename)
    }//end class
}//end namespace