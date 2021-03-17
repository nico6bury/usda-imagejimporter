using System;
using System.Collections.Generic;
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
        /// function pointer for SendRowList in view. set up in program.cs
        /// </summary>
        public SendRowList updateSeedList;

        /// <summary>
        /// function pointer for DoWordsWrap in view. set up in program.cs
        /// </summary>
        public ReturnBool wordsWrap;

        /// <summary>
        /// function pointer for the SetWordWrap in view. set up in program.cs
        /// </summary>
        public SetBool setWordWrap;

        /// <summary>
        /// function pointer for the CloseRowList in view. set up in program.cs
        /// </summary>
        public CallMethod closeSeedList;

        /// <summary>
        /// function pointer to request a new filename from the view
        /// </summary>
        public RequestString getNewFilename;

        /// <summary>
        /// the list of rows that is currently being displayed or used
        /// </summary>
        private List<Row> currentRowList = new List<Row>();

        /// <summary>
        /// stores whether or not there is currently a file loaded in the program
        /// </summary>
        private bool IsFileCurrentlyLoaded = false;

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

        public string GiveCurrentFilename()
        {
            return fileIO.file;
        }//end GiveCurrentFileName()

        /// <summary>
        /// handle event of user wanting to open a file
        /// </summary>
        public void OpenDataFile()
        {
            //get filename from the view
            string filename = getNewFilename();
            if (filename != "")
            {
                //get the row list from the file
                List<Row> curList = fileIO.LoadFile(filename);

                //reset the view before we update it
                closeSeedList();

                //update list of seeds in the view
                if (updateSeedList(curList))
                {
                    //reset and update currentRowList
                    currentRowList.Clear();
                    foreach(Row row in curList)
                    {
                        currentRowList.Add(row);
                    }//end adding each row to currentRowList

                    //updates boolean
                    IsFileCurrentlyLoaded = true;

                    //update log
                    AppendToHeaderLog($"\nLoaded \"{filename}\" with {currentRowList.Count} rows.");
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
                fileIO.SaveFile(fileIO.file, currentRowList);

                //update log
                AppendToHeaderLog($"Successfully saved {currentRowList.Count}" +
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
                string newFileName = getNewFilename();
                if(newFileName != "")
                {
                    //save the row information as the specified filename
                    fileIO.SaveFile(newFileName, currentRowList);

                    //update log
                    AppendToHeaderLog($"Successfully saved {currentRowList.Count}" +
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
            currentRowList.Clear();
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
        /// of indices is valid for currentRowList
        /// </summary>
        /// <param name="indices">the enumerable collection of indices</param>
        /// <exception cref="ArgumentOutOfRangeException">Exception thrown when
        /// an index supplied is outside the bounds of currentRowList</exception>
        private void CheckIndices(IEnumerable<int> indices)
        {
            foreach (int index in indices)
            {
                if (index < 0 || index >= currentRowList.Count)
                {
                    throw new ArgumentOutOfRangeException($"The supplied index" +
                        $" {index} was outside the acceptable range from 0 " +
                        $"to {currentRowList.Count - 1}.");
                }//end if the index is outside range of list
            }//end checking each index is valid
        }//end CheckIndices(indices)

        /// <summary>
        /// Builds a list of all the rows at the specified indices with
        /// currentRowList. Doesn't do any sort of verification that indices are
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
                rowRegister.Add(currentRowList[index]);
            }//end adding each requested row to rowRegister

            return rowRegister;
        }//end GetRowsAtIndices(indices)

        public SendRowList viewRows;
        /// <summary>
        /// assembles list of rows at specified indices and tells view to
        /// display them to the user
        /// </summary>
        /// <param name="indices">the indices of currentRowList which you'd like
        /// to view</param>
        public void ViewRowData(List<int> indices)
        {
            //verify all indices are valid
            CheckIndices(indices);

            //get rows at specified indices
            List<Row> rowRegister = GetRowsAtIndices(indices);

            //tell view to display specified rows
            viewRows(rowRegister);
        }//end ViewRowData(indices)

        public SendRowList editRows;
        /// <summary>
        /// assembles list of rows at specified indices and tells view to allow
        /// the user to edit them
        /// </summary>
        /// <param name="indices">the indices of currentRowList which you'd like
        /// to edit</param>
        public void EditRowData(List<int> indices)
        {
            //verify all indices are valid
            CheckIndices(indices);

            //get rows at specified indices
            List<Row> rowRegister = GetRowsAtIndices(indices);

            //tell view to allow user to edit specified rows
            editRows(rowRegister);
        }//end EditRowData(indices)

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
                foreach(Row newRow in processedRowIndexPairs.Keys)
                {
                    currentRowList[processedRowIndexPairs[newRow]] = newRow;
                }//end updating internal list for each row

                //update the row list in the view
                updateSeedList(currentRowList);

                //update log so user knows save operation was successful
                AppendToHeaderLog($"Successfully saved {processedRowIndexPairs.Count}" +
                    $" rows to internal reference list. To update file, select" +
                    $" option to save data to a file.");
            }//end trying to process all row strings
            catch
            {
                //display error message to user
                showMessage("One of the edited rows cannot be read. Please" +
                    " fix formatting before trying to save it.", "Row Format" +
                    " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //update log so user has record of failing to save seed data
                AppendToHeaderLog($"{DateTime.Now:s} Row Data" +
                    $" Save Operation Failed.");
            }//end catching row string processing errors
        }//end SaveRowData(rowIndexPairs)

        /// <summary>
        /// loads some information from the config file and sends
        /// relevant information to the view
        /// </summary>
        public void OpenView()
        {
            List<string> data = fileIO.LoadConfigFile();

            if(data != null)
            {
                try
                {
                    //load all the row information from config file
                    List<Row> rows = fileIO.LoadFile(data[0]);

                    //pass row info back to the view
                    updateSeedList(rows);

                    //update internal row listing
                    currentRowList = rows;

                    //update boolean flag
                    IsFileCurrentlyLoaded = true;

                    //update fileIO's most recently used file property
                    fileIO.file = data[0];

                    //try to determine whether we should wrap text
                    bool wrapText = Convert.ToBoolean(data[1]);

                    //tell the view whether we should wrap text
                    setWordWrap(wrapText);

                    //update log with recent file name and row count
                    AppendToHeaderLog($"\nLoaded \"{fileIO.file}\"" +
                        $" from config file with" +
                        $" {currentRowList.Count} rows.");
                }//end trying to get input from the config
                catch
                {
                    //don't do anything
                }//end catching any errors from reading the config
            }//end if the data isn't null
        }//end OpenView()

        /// <summary>
        /// saves current configuration information to the config file
        /// </summary>
        public void CloseView()
        {
            //saves configuration info to config file
            fileIO.SaveConfigFile(wordsWrap());
        }//end CloseView()

        public SendString appendTextLog;
        public void AppendToHeaderLog(string text)
        {
            appendTextLog(text);
        }//end AppendToHeaderLog(text)
    }//end class
}//end namespace