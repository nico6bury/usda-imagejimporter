﻿using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Author: Nicholas Sixbury
 * File: View.cs
 * Purpose: To provide an interface for the user to interact with and enter
 * information so the program can do what the user wants it to do.
 */

namespace ImageJImporter
{
    /// <summary>
    /// this class displays information and can be interacted with by the user
    /// </summary>
    public partial class View : Form
    {
        /// <summary>
        /// function pointer for GiveCurrentFilename in controller. set up in
        /// program.cs
        /// </summary>
        public RequestString requestFileName;

        private System.Drawing.Size defaultListBoxSize;

        private System.Timers.Timer timer;

        private List<CellButtonDisplay> displays;

        private string imageJFileHeader = "Row\tArea\tX\tY\tPerim.\tMajor\tMinor\t" +
            "Angle\tCirc.\tAR\tRound\tSolidity";

        private TypedObjectListView<Row> tlist;
        
        /// <summary>
        /// constructor for this class. Just initializes things
        /// </summary>
        public View()
        {
            //this basically makes all the controls visible (buttons, textbox, etc)
            InitializeComponent();

            //initialize typedObjectListView
            tlist = new TypedObjectListView<Row>(this.uxRowListView);

            //set local variables to initial values
            defaultListBoxSize = uxRowListView.Size;
            uxRowListView.SelectedIndexChanged += SelectedRowInListChanged;
            timer = new System.Timers.Timer(1000)
            {
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += UpdateCurrentDateTime;

            sendDateTime = ChangeDateTimeText;
            sendLogAppendation = AppendTextLogHelperMethod;
            displays = new List<CellButtonDisplay>();
            uxRowListView.OLVGroups = new List<OLVGroup>();
            uxRowListView.HasCollapsibleGroups = true;
            uxRowListView.ShowGroups = true;

            tlist.GetColumn(0).GroupKeyGetter = delegate (Row rowObject)
            {
                Row row = rowObject;
                return row.CurrentCellOwner;
            };
            this.rowName.GroupKeyToTitleConverter = delegate (object groupKey)
            {
                Cell cell = (Cell)groupKey;
                //determine header based on cell state
                if (cell.IsNewRowFlag) return ("New Row Flag");
                else if (!cell.IsFullCell) return ("Incomplete Cell");
                else if (cell.IsEmptyCell) return ("Empty Cell");
                else if (cell.RowSpan == -2) return ("Abnormal Cell");
                else return ($"Normal Cell with {cell.Chalk.ToString("N3")} Chaulkiness");
            };
            this.rowName.GroupFormatter = (OLVGroup group,
                GroupingParameters parms) =>
            {
                Cell groupCell = (Cell)group.Key;
                int index = groupCell.OwningGridObject.Cells.IndexOf(groupCell);
                group.Id = groupCell[0].RowNum;
                parms.GroupComparer = Comparer<OLVGroup>.Create((x, y) => (x.Id.CompareTo(y.Id)));
            };
            uxRowListView.BeforeCreatingGroups += uxRowListViewGroupSorting;
        }//end constructor

        private void uxRowListViewGroupSorting(object sender, CreateGroupsEventArgs e)
        {
            e.Parameters.GroupComparer = Comparer<OLVGroup>.Create((x, y) => (x.GroupId.CompareTo(y.GroupId)));
        }

        private SendString sendDateTime;
        private void UpdateCurrentDateTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            object[] args = new object[1];
            args[0] = DateTime.Now.ToString();
            try
            {
                this.BeginInvoke(sendDateTime, args);
            }//end trying to Begin an Invokation
            catch(InvalidOperationException)
            {
                //I guess do nothing? ¯\_(ツ)_/¯
            }//end catching InvalidOperationExceptions from BeginInvoke()
        }//end timer elapsed event args

        private void ChangeDateTimeText(string text)
        {
            uxCurrentDateTime.Text = text;
        }//end ChangeDateTimeText(text)

        /// <summary>
        /// Allows outside classes to cause this class to show a message box
        /// </summary>
        /// <param name="text">the text to display in the message box</param>
        /// <param name="caption">the text to display in the title of the message box</param>
        /// <param name="buttons">which buttons to display</param>
        /// <param name="icon">which icon to display</param>
        public void ShowMessage(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            //displays a message box to the user with the variables set from the parameters
            MessageBox.Show(text, caption, buttons, icon);
        }//end ShowMessage

        public void RequestCellDisplayFormClose(CellButtonDisplay sender)
        {
            sender.Close();
        }//end RequestCellDisplayFormClose

        public void CloseAllCellButtonDisplays()
        {
            foreach(CellButtonDisplay display in displays)
            {
                display.Close();
            }//end looping over all the Cell button displays
        }//end ClseAllCellButtonDisplays

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void GridButtonClickEvent(object sender, EventArgs e)
        {
            //get our button from sender and don't do anything if not right type
            CellButton button = sender as CellButton;
            if (button == null) return;

            //display the button's cell
            CellButtonDisplay display = new CellButtonDisplay(CloseAllCellButtonDisplays, button.Cell);
            displays.Add(display);
            display.Show();
        }//end GridButton Click Event

        /// <summary>
        /// updates the GUI with information from the supplied grid
        /// </summary>
        /// <param name="grid">The grid to display</param>
        /// <returns>returns true if the operation was successful, false otherwise</returns>
        public bool UpdateGrid(Grid grid)
        {
            List<Row> tempRows = grid.Rows;

            //makes the group for displaying rows interactable
            uxRowDisplayGroup.Enabled = true;

            //builds the button grid for this data
            BuildButtonGrid(grid);

            //reset list
            uxRowListView.ClearObjects();
            //update list
            uxRowListView.SetObjects(tempRows);

            //tell whoever called us that we were successful
            return true;
        }//end UpdateGrid(grid)

        /// <summary>
        /// This event handler is called whenever the selected indices of
        /// uxRowList change. It simply updates which rows are displayed in
        /// the uxTextViewer
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void SelectedRowInListChanged(object sender, EventArgs e)
        {
            //make sure sender is a listbox and has at least one index selected
            ListView list = sender as ListView;
            if (list == null || list.SelectedIndices.Count < 0) return;

            //make sure at least one button is pressed, and the whole group box is enabled
            if((!uxViewRow.Enabled || !uxEditRow.Enabled) && uxRowDisplayGroup.Enabled)
            {
                //get the list of currently selected indices
                List<int> indices = GetSelectedIndexList(list);

                if (uxViewRow.Enabled == false)
                {
                    //tell controller we want to view rows
                    viewRows(indices);
                }//end if we're viewing rows
                else if (uxEditRow.Enabled == false)
                {
                    //tell controller we want to edit rows
                    editRows(indices);
                }//end else if we're editing rows
            }//end if we should do anything
        }//end SelectedRowInListChanged event handler

        /// <summary>
        /// Event for uxLockListSelection being clicked
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void uxLockListSelectionClick(object sender, EventArgs e)
        {
            //toggles uxRowList enabled property
            uxRowListView.Enabled = !uxRowListView.Enabled;
        }//end uxLockListSelectionClick event handler

        /// <summary>
        /// builds the grid of buttons that represents the current grid. Very WIP
        /// </summary>
        /// <param name="grid">the grid that you want to build</param>
        private void BuildButtonGrid(Grid grid)
        {
            //create our jagged 2d list of CellButtons
            List<List<CellButton>> cellButtons = new List<List<CellButton>>();
            
            //we basically increment this before new row flags
            int firstDimensionIndex = -1;

            //just fix a potential formatting issue for incorrectly formatted data
            if (grid.Count > 0 && !grid.Cells[0].IsNewRowFlag) firstDimensionIndex++;

            //actually start adding those buttons in the right spot
            for(int i = 0; i < grid.Cells.Count; i++)
            {
                //make our new button to do stuff with
                CellButton tempButton;

                //format the look of our CellButton
                FormatCellButton(out tempButton, grid.Cells[i], uxToolTip);
                tempButton.Size = uxStartReference.Size;

                //do weird jagged 2-d list things
                if (tempButton.Cell.IsNewRowFlag)
                {
                    firstDimensionIndex++;
                    cellButtons.Add(new List<CellButton>());
                }//end if we have a flag for a new row

                //add click event handler
                tempButton.Click += GridButtonClickEvent;

                //actually add this cell to cellButtons
                cellButtons[firstDimensionIndex].Add(tempButton);
            }//end adding all cells to cellButtons

            //just define the margin between buttons
            int buttonMargin = 5;

            //add our buttons to the groupbox so they're visible and put them in a snazzy grid
            for(int i = 0; i < cellButtons.Count; i++)
            {
                for(int j = 0; j < cellButtons[i].Count; j++)
                {
                    CellButton thisButton = cellButtons[i][j];

                    //not 100% why exactly this works, but it does
                    int X = uxStartReference.Location.X + i * (uxStartReference.Size.Height + buttonMargin);
                    int Y = uxStartReference.Location.Y + j * (uxStartReference.Size.Width + buttonMargin) - (uxStartReference.Size.Width / 2);
                    thisButton.Location = new Point(Y, X);

                    //add our button to the groupbox so it gets displayed
                    uxGridDisplay.Controls.Add(thisButton);
                }//end looping over cell buttons
            }//end looping over lists of cell buttons
        }//end BuildButtonGrid(rows)

        /// <summary>
        /// Formats a specified CellButton based on the flags of its
        /// provided Cell. cellButton is an out parameter. Tooltip
        /// assigning is optional. To not have a tooltip, just set
        /// tip to null.
        /// </summary>
        /// <param name="cellButton">The CellButton you wish to format</param>
        /// <param name="buttonCell">the cell to assign to the button</param>
        /// <param name="tip">the tooltip used to set the button's tooltip.
        /// Set to null if you don't want a tooltip</param>
        private void FormatCellButton(out CellButton cellButton, Cell buttonCell, ToolTip tip)
        {
            //initialize the out parameter
            cellButton = new CellButton(buttonCell);

            //format the text of the button
            cellButton.Text = cellButton.Cell.RowSpan.ToString();
            //if (cellButton.Cell.Chaulkiness > 0) cellButton.Text = cellButton.Cell.Chaulkiness.ToString("N3");
            //else cellButton.Text = cellButton.Cell.Chaulkiness.ToString();

            //format color and tooltip
            if (cellButton.Cell.IsNewRowFlag)
            {
                //set button color coding
                cellButton.BackColor = Color.Black;
                cellButton.ForeColor = Color.White;

                //set tooltip
                tip?.SetToolTip(cellButton, "This cell" +
                    " simply represents a new grid row and " +
                    "doesn't contain any novel information.");
            }//end if the button represents a flag for a new row
            else if (cellButton.Cell.IsEmptyCell)
            {
                //set button color coding
                cellButton.BackColor = Color.LightSeaGreen;
                cellButton.ForeColor = Color.PaleGreen;

                //set tooltip
                tip?.SetToolTip(cellButton, "This cell is" +
                    "correctly formatted, but it doesn't have any" +
                    " information stored in it.");
            }//end if the cell is properly formatted but empty
            else if (cellButton.Cell.RowSpan == 2 && cellButton.Cell.IsFullCell)
            {
                //set button color coding
                cellButton.BackColor = Color.Green;
                cellButton.ForeColor = Color.Honeydew;

                //set tooltip
                tip?.SetToolTip(cellButton, "This cell is " +
                    "correctly formatted and has normal data, making " +
                    "it easy to calculate chaulkiness for.");
            }//end if we have a normal cell
            else if (cellButton.Cell.IsFullCell)
            {
                //set button color coding
                cellButton.BackColor = Color.LimeGreen;
                cellButton.ForeColor = Color.MintCream;

                //set tooltip
                tip?.SetToolTip(cellButton, "This cell is " +
                    "correctly formatted, but because of its data, " +
                    "it\'s difficult to use.");
            }//end else we have a properly formatted by abnormal cell
            else
            {
                //set button color coding
                cellButton.BackColor = Color.DarkMagenta;
                cellButton.ForeColor = Color.Thistle;

                //set tooltip
                tip?.SetToolTip(cellButton, "This cell is formatted " +
                    "incorrectly. This could either be a problem with processing" +
                    " this cell\'s row information, or it could mean your input data " +
                    "was corrupted somehow.");
            }//end else this cell is formatted wrong
        }//end FormatCellButton(out cellButton, buttonCell)

        /// <summary>
        /// closes the file and mostly resets things
        /// </summary>
        public void CloseRowList()
        {
            //clear the seeds displayed in the list
            uxRowListView.ClearObjects();

            //clear the text in the editing/viewing box
            uxTextViewer.Text = "";

            //reset listbox
            uxRowListView.Size = defaultListBoxSize;

            //disable the elements for editing seeds so they can't be interacted with by the user
            uxRowDisplayGroup.Enabled = false;
        }//end CloseRowList()

        /// <summary>
        /// just returns whether or not text is allowed to wrap to the next line
        /// in the textbox for viewing seed data
        /// </summary>
        /// <returns>word wrap property for seed data viewer</returns>
        public bool DoWordsWrap()
        {
            //returns whether or not text is allowed to wrap across lines currently
            return uxTextViewer.WordWrap;
        }//end DoWordsWrap()

        /// <summary>
        /// sets the WordWrap property of the textbox for viewing seed data
        /// </summary>
        /// <param name="wordWrap"></param>
        public void SetWordWrap(bool wordWrap)
        {
            //sets whether or not text is allowed to wrap across lines
            uxTextViewer.WordWrap = wordWrap;
        }//end SetWordWrap(wordWrap)

        /// <summary>
        /// Gets a new filename from the user based on what request type they specify.
        /// Currently accepted request types are OpenFile and SaveFileAs. Returns null
        /// if user selected to not select a file
        /// </summary>
        /// <param name="request">The type of request for a new filename that
        /// you are making. Either OpenFile or SaveFileAs</param>
        public string GetNewFilename(Request request)
        {
            string filename = null;
            switch (request)
            {
                case Request.OpenFile:
                    using(OpenFileDialog openDialog = new OpenFileDialog())
                    {
                        if (openDialog.ShowDialog() == DialogResult.OK) filename = openDialog.FileName;
                    }//end use of openDialog
                    break;
                case Request.SaveFileAs:
                    using(SaveFileDialog saveDialog = new SaveFileDialog())
                    {
                        if (saveDialog.ShowDialog() == DialogResult.OK) filename = saveDialog.FileName;
                    }//end use of saveDialog
                    break;
                default:
                    throw new InvalidEnumArgumentException($"The specified" +
                        $" request {request} is not a valid parameter for GetFilename.");
            }//end switch case

            return filename;
        }//end GetNewFilename(request)

        public CallMethod openFile;
        /// <summary>
        /// this method runs when the uxMenuOpenFile button is clicked. It uses
        /// a delegate to tell the Controller to open a file
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void OpenFile(object sender, EventArgs e)
        {
            //asks controller to open a new file
            openFile();
        }//end event handler for opening a file

        /// <summary>
        /// appends text to the current log. Each method call creates
        /// a new line, but at present the \n character doesn't affect
        /// things.
        /// </summary>
        /// <param name="text">the new line of text to add to
        /// the log</param>
        public void AppendTextToLog(string text)
        {
            //set up args to send through BeginInvoke
            object[] args = new object[1];
            args[0] = text;

            //assynchronously begin invokation on new thread
            this.BeginInvoke(sendLogAppendation, args);
        }//end AppendTextToLog(text)

        private SendString sendLogAppendation;
        /// <summary>
        /// Helper method for AppendTextToLog. Actually handles the
        /// appendation of new lines to the row editor, though each
        /// invokation only adds one new line, as the textbox we're
        /// appending to doesn't accept the \n character.
        /// </summary>
        private void AppendTextLogHelperMethod(string text)
        {
            string[] newLines = new string[uxHeaderLog.Lines.Length + 1];
            for (int i = 0; i < uxHeaderLog.Lines.Length; i++)
            {
                //adds new line of text along with a time-stamp
                newLines[i] = uxHeaderLog.Lines[i];
            }//end adding old lines to new

            //add new line
            newLines[newLines.Length - 1] = $"{DateTime.Now:T} " + text;

            //update the textbox
            uxHeaderLog.Lines = newLines;
        }//end AppendTextLogHelperMethod(text)

        public CallMethod saveFile;
        /// <summary>
        /// this method runs when the uxMenuSaveFile button is clicked. It uses a
        /// delegate to tell the Controller to save the file we loaded earlier
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void SaveFile(object sender, EventArgs e)
        {
            //tell controller to save our current file
            saveFile();
        }//end event handler for saving a file

        public CallMethod saveFileAs;
        /// <summary>
        /// this method runs when the uxMenuSaveFileAs button is clicked. It uses
        /// a delegate to tell the Controller to save the data loaded as a new file
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void SaveFileAs(object sender, EventArgs e)
        {
            saveFileAs();
        }//end event handler for creating a new file

        public CallMethod closeFile;
        /// <summary>
        /// this method runs when the uxMenuCloseFile button is clicked. It uses a
        /// delegate to tell the Controller to close the file currently loaded
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void CloseFile(object sender, EventArgs e)
        {
            closeFile();
        }//end event handler for closing a file

        /// <summary>
        /// updates the the uxTextViewer textbox. Copies the list
        /// provided so you don't have to worry about accidentally
        /// passing references
        /// </summary>
        /// <param name="rows">the rows to update the textbox
        /// with</param>
        /// <returns>whether or not the operation was
        /// successful</returns>
        private bool SetSelectedRows(List<Row> rows)
        {
            try
            {
                //initialize and build new array of row data
                string[] rowArray = new string[rows.Count+1];
                rowArray[0] = imageJFileHeader;
                for(int i = 1; i < rows.Count+1; i++)
                {
                    //adds a deep copy of current row to rowArray
                    rowArray[i] = rows[i-1].ToString();
                }//end copying each row over

                //update uxTextViewer with new row info
                uxTextViewer.Lines = rowArray;

                //tell whoever called us that we were successful
                return true;
            }//end trying to set the selected rows
            catch
            {
                //I guess something went wrong, so return false
                return false;
            }//end catching errors
        }//end SetSelectedRows(rows)

        /// <summary>
        /// When given a listbox, returns an integer list containing all the indices
        /// of the listbox which are selected.
        /// </summary>
        /// <param name="listBox">the listbox to pull SelectedIndices from</param>
        /// <returns>a list of integers representing the indices</returns>
        private List<int> GetSelectedIndexList(ListView listView)
        {
            List<int> indices = new List<int>();
            foreach(int index in listView.SelectedIndices)
            {
                indices.Add(index);
            }//end adding all selected indices to indices
            return indices;
        }//end GetSelectedIndexList

        public CallMethodWithInts viewRows;
        /// <summary>
        /// this method runs when the uxViewRow button is clicked. It uses a delegate
        /// to tell the Controller to send us seed data to display
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void ViewRowData(object sender, EventArgs e)
        {
            //update button enabled-ness so the user knows which mode the program is in
            if (uxEditRow.Enabled == false) uxEditRow.Enabled = true;
            uxViewRow.Enabled = false;

            //make list of selected indices and populate it with data
            List<int> indices = GetSelectedIndexList(uxRowListView);

            //tell controller to view selected indices
            viewRows(indices);
        }//end event handler for viewing seed data

        /// <summary>
        /// allows the specified rows to be viewed by the user
        /// in the text viewer/editor
        /// </summary>
        /// <param name="rows">the rows to make viewable</param>
        /// <returns>returns whether the operation was successful</returns>
        public bool SetRowViewability(List<Row> rows)
        {
            try
            {
                //update selected rows and return false if we fail
                if (!SetSelectedRows(rows)) return false;

                //make it so the user can't edit the text
                uxTextViewer.ReadOnly = true;

                //we got here, so i guess we succeeded
                return true;
            }//end try block
            catch
            {
                //an error happened, so I guess we failed
                return false;
            }//end catch block
        }//end SetRowViewable(rows)

        public CallMethodWithInts editRows;
        /// <summary>
        /// this method runs when the uxEditRow button is clicked. It uses a delegate
        /// to tell the Controller to send us row data to edit
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void EditSeedData(object sender, EventArgs e)
        {
            //update button enabled-ness so the user knows which mode the program is in
            if (uxViewRow.Enabled == false) uxViewRow.Enabled = true;
            uxEditRow.Enabled = false;

            //make list of selected indices and populate it with data
            List<int> indices = GetSelectedIndexList(uxRowListView);

            //tell controller to view selected indices
            editRows(indices);
        }//end event handler for editing seed data

        /// <summary>
        /// allows the specified rows to be edited by the user
        /// in the text viewer/editor
        /// </summary>
        /// <param name="rows">the rows to make editable</param>
        /// <returns>returns whether the operation was successful</returns>
        public bool SetRowEditability(List<Row> rows)
        {
            try
            {
                //update selected rows and return false if we fail
                if (!SetSelectedRows(rows)) return false;

                //make it so the user can edit the text
                uxTextViewer.ReadOnly = false;

                //we got here, so i guess we succeeded
                return true;
            }//end try block
            catch
            {
                //an error happened, so I guess we failed
                return false;
            }//end catch block
        }//end SetRowEditability(row)

        public CallMethodWithRowStringDictionary saveRows;
        /// <summary>
        /// this method runs when the uxSaveSeed button is clicked. It uses a delegate
        /// to tell the Controller to save the seed data we're about to send it
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void SaveSeedData(object sender, EventArgs e)
        {
            /*
             * Note: Current method of getting index from a row is a bit jank.
             * It would be good to update it with something a little cleaner in
             * the future.
             */

            //initialize dictionary to send to controller
            Dictionary<string, int> rowIndexPairs = new Dictionary<string, int>();

            List<Row> tempRowList = tlist.Objects as List<Row>;

            //start processing each row in the row viewer/editor
            for(int i = 0; i < tempRowList.Count; i++)
            {
                rowIndexPairs.Add(tempRowList[i].ToString(), i);
            }//end looping over each row in uxRowListView
            foreach(string row in uxTextViewer.Lines)
            {
                string[] rowComponents = row.Split('\t');

                //theoretically, the first item in the row should be its row number
                int index = Convert.ToInt32(rowComponents[0]);

                //convert row number to row index
                index--;

                //add new dictionary item with this row and what its index probably is
                rowIndexPairs.Add(row, index);
            }//end looping over each line in uxTextViewer

            //tell controller to save specified seed data
            saveRows(rowIndexPairs);
        }//end event handler for saving seed data

        public CallMethod formOpening;
        /// <summary>
        /// event for when the form opens. Does startup things
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void OpenForm(object sender, EventArgs e)
        {
            //tell the controller we are in the process of opening
            formOpening();
        }//end event handler for opening the form

        public CallMethod formClosing;
        public SendStringArray formClosingSaveLog;
        /// <summary>
        /// event for right before the form closes. Saves settings for next time
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            //tell the controller we want to close
            formClosing();
            //tell the controller we have log files to save
            formClosingSaveLog(uxHeaderLog.Lines);
        }//end event handler for closing the form

        /// <summary>
        /// shows a message to the user telling them that no file is loaded
        /// </summary>
        private void NoFileLoadedMessage()
        {
            //display message box to user saying that no file is loaded
            MessageBox.Show("You don't have a file loaded.", "Invalid Operation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }//end NoFileLoadedMessage()

        /// <summary>
        /// just toggles whether the text in the textbox wraps
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void ToggleWordWrap(object sender, EventArgs e)
        {
            //toggle word wrap property of uxTextViewer
            uxTextViewer.WordWrap = !uxTextViewer.WordWrap;
        }//end ToggleWordWrap event handler

        /// <summary>
        /// asks the controller to tell us to show the name of the current file
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void AskForFilename(object sender, EventArgs e)
        {
            MessageBox.Show("That button\'s functionality is currently" +
                " not implemented. Please don't click it.");
            uxCurrentFilenameRequest.Enabled = false;
        }//end AskForFilename event handler

        private void uxRowListView_CellEditFinished(object sender, CellEditEventArgs e)
        {
            /*
             * Note: Current method of getting index from a row is a bit jank.
             * It would be good to update it with something a little cleaner in
             * the future.
             */

            //initialize dictionary to send to controller
            Dictionary<string, int> rowIndexPairs = new Dictionary<string, int>();

            Row rowToEdit = (Row)e.RowObject;

            string[] rowComponents = rowToEdit.FormatData().Split('\t');
            int index = Convert.ToInt32(rowComponents[0]) - 1;
            rowComponents[e.SubItemIndex] = e.NewValue.ToString();
            StringBuilder sb = new StringBuilder();
            foreach(string element in rowComponents)
            {
                sb.Append(element);
                sb.Append("\t");
            }
            sb.Length--;
            rowIndexPairs.Add(sb.ToString(), index);

            //tell controller to save specified seed data
            saveRows(rowIndexPairs);
        }
    }//end class
}//end namespace