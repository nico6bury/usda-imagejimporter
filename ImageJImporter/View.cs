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

        private Brush selectedTextColor = Brushes.LightSkyBlue;

        private SolidBrush selectedBackgroundColor = new SolidBrush(Color.DodgerBlue);

        private System.Timers.Timer timer;

        /// <summary>
        /// constructor for this class. Just initializes things
        /// </summary>
        public View()
        {
            //this basically makes all the controls visible (buttons, textbox, etc)
            InitializeComponent();

            //set local variables to initial values
            defaultListBoxSize = uxRowListView.Size;
            uxRowListView.SelectedIndexChanged += SelectedRowInListChanged;
            //uxRowList.AutoSize = true;
            //uxRowDisplayGroup.AutoSize = true;
            //uxRowList.DrawMode = DrawMode.OwnerDrawFixed;
            //uxRowList.DrawItem += new DrawItemEventHandler(DynamicallySetRowColor);
            //uxRowList.SelectedIndexChanged += UxSeedList_SelectedIndexChanged;
            timer = new System.Timers.Timer(1000)
            {
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += UpdateCurrentDateTime;

            sendDateTime = ChangeDateTimeText;
            sendLogAppendation = AppendTextLogHelperMethod;
        }//end constructor

        private SendString sendDateTime;
        private void UpdateCurrentDateTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            object[] args = new object[1];
            args[0] = DateTime.Now.ToString();
            this.BeginInvoke(sendDateTime, args);
        }//end timer elapsed event args

        private void ChangeDateTimeText(string text)
        {
            uxCurrentDateTime.Text = text;
        }//end ChangeDateTimeText(text)

        private void DynamicallySetRowColor(object sender, DrawItemEventArgs e)
        {
            try
            {
                //draw the background?
                e.DrawBackground();
                //get the graphics from the event args
                Graphics g = e.Graphics;
                //default text color
                Brush textBrush = Brushes.Black;
                //default background color
                SolidBrush backBrush = new SolidBrush(Color.Silver);

                //get the current row from the listbox
                Row row = (Row)(((ListBox)sender).Items[e.Index]);

                ListBox listBox = (ListBox)sender;

                //dynamically set color
                if (listBox.SelectedIndices.Contains(e.Index))
                {
                    textBrush = selectedTextColor;
                    backBrush = selectedBackgroundColor;
                }//end if this row is selected
                else if (row.IsNewRowFlag)
                {
                    textBrush = Brushes.Silver;
                    backBrush.Color = Color.Black;
                }//end else if IsNewRowFlag
                else if (row.IsSeedStartFlag)
                {
                    textBrush = Brushes.Aquamarine;
                    backBrush.Color = Color.DarkSeaGreen;
                }//end else if IsSeedStartFlag
                else if (row.IsSeedEndFlag)
                {
                    textBrush = Brushes.DarkSeaGreen;
                    backBrush.Color = Color.Aquamarine;
                }//end else if IsSeedEndFlag

                //draw the background as whatever backBrush is set to
                g.FillRectangle(backBrush, e.Bounds);

                //actually draw the text stuff
                e.Graphics.DrawString(row.ToString(),
                    e.Font, textBrush, e.Bounds);

                //draw other stuff
                e.DrawFocusRectangle();
            }//end trying to change the row color
            catch
            {

            }//end catching errors
        }//end DynamicallySetRowColor handler

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

        /// <summary>
        /// updates the seed list in the view with new cell data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>returns whether the operation was successful</returns>
        public bool UpdateRowList(List<Row> data)
        {
            /// Note about the data type of uxRowList: 
            /// uxRowList is a ListBox, which means that
            /// the items it displays are not updated when
            /// items in its data source are updated. Instead,
            /// whenever one or all the items are updated, you
            /// have to set the data source to null and then whatever
            /// you want it to display

            List<Row> tempRows = data;

            uxRowListView.Size = defaultListBoxSize;
            //var tempIndexList = uxRowList.SelectedIndices;
            //uxRowList.DataSource = null;
            //uxRowList.DataSource = tempRows;

            uxRowListView.Items.Clear();
            foreach(Row row in tempRows)
            {
                ListViewItem item = new ListViewItem(row.GetRowPropertyArray());
                uxRowListView.Items.Add(item);
            }//end adding each row to listview
            //uxRowList.SelectedIndices = tempIndexList;

            //if(uxRowList.Items.Count > 30)
            //{
            //    uxRowList.Size = new Size(defaultListBoxSize.Width, 20 * uxRowList.Items.Count);
            //}//end if we should resize the listbox
            //else
            //{
            //    uxRowList.Size = defaultListBoxSize;
            //}//end else we want to reset the size

            //makes the controls which allow editing/viewing interactable by the user
            uxRowDisplayGroup.Enabled = true;

            //build the button grid
            BuildButtonGrid(data);

            //tell whoever called us that the operation was successful
            return true;
        }//end SendRowList(data)

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
        /// <param name="rows">the list of rows to build as a grid</param>
        private void BuildButtonGrid(List<Row> rows)
        {
            //set up our 2d list
            List<List<RowButton>> buttonGrid = new List<List<RowButton>>();
            //for(int i = 0; i < buttonGrid.Count; i++)
            //{
            //    buttonGrid[i] = new List<Button>();
            //}//end initializing all lists in buttonGrid

            //actually try to start putting info in them
            int firstDimensionIndex = 0;
            buttonGrid.Add(new List<RowButton>());
            //int secondDimensionIndex = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                RowButton button = new RowButton(rows[i]);
                button.Size = uxStartReference.Size;
                button.Click += GridButtonClickEvent;
                //buttonGrid[firstDimensionIndex].Add(button);

                if (rows[i].IsNewRowFlag)
                {
                    button.BackColor = Color.Black;
                    button.ForeColor = Color.White;
                    firstDimensionIndex++;
                    buttonGrid.Add(new List<RowButton>());
                }//end if this row if a new row flag
                else if (rows[i].IsSeedStartFlag)
                {
                    button.BackColor = Color.Aquamarine;
                    button.ForeColor = Color.DarkSeaGreen;
                }//end if this row is a seed start flag
                else if (rows[i].IsSeedEndFlag)
                {
                    button.BackColor = Color.DarkSeaGreen;
                    button.ForeColor = Color.Aquamarine;
                }//end if this row is a seed end flag

                //putting this after the other stuff makes the row flags appear first
                buttonGrid[firstDimensionIndex].Add(button);
            }//end looping to populate 2d list of buttons

            //just define the margin between buttons
            int buttonMargin = 5;

            //now we need to display those buttons by adding them to the groupbox
            for(int i = 0; i < buttonGrid.Count; i++)
            {
                for(int j = 0; j < buttonGrid[i].Count; j++)
                {
                    RowButton thisButton = buttonGrid[i][j];

                    int X = uxStartReference.Location.X + i * (uxStartReference.Size.Height + buttonMargin);
                    int Y = uxStartReference.Location.Y + j*(uxStartReference.Size.Width + buttonMargin);
                    thisButton.Location = new Point(Y, X);

                    uxGridDisplay.Controls.Add(thisButton);
                }//end looping over second dimension
            }//end looping over first dimension
        }//end BuildButtonGrid(rows)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void GridButtonClickEvent(object sender, EventArgs e)
        {
            //get our button from sender and don't do anything if not right type
            RowButton button = sender as RowButton;
            if (button == null) return;

            //display the button's row
            MessageBox.Show($"{button.Row}", $"Row Data of Row {button.Row.RowNum}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//end GridButton Click Event

        /// <summary>
        /// closes the file and mostly resets things
        /// </summary>
        public void CloseRowList()
        {
            //clear the seeds displayed in the list
            uxRowListView.Items.Clear();

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
                string[] rowArray = new string[rows.Count];
                for(int i = 0; i < rows.Count; i++)
                {
                    //adds a deep copy of current row to rowArray
                    rowArray[i] = rows[i].ToString();
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

            //start processing each row in the row viewer/editor
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
    }//end class
}//end namespace