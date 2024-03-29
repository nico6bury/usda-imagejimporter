﻿using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

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

        /// <summary>
        /// function pointer to tell the controller we got new level information
        /// </summary>
        public SendLevelInformation sendLevelInformation;

        /// <summary>
        /// the level information for this session
        /// </summary>
        private LevelInformation allLevelInformation;

        private System.Timers.Timer timer;

        /// <summary>
        /// list of the cellbuttons that are currently being displayed. Helps us
        /// close them all when we need to.
        /// </summary>
        private List<CellButtonDisplay> displays;

        /// <summary>
        /// the typed version of our OLV
        /// </summary>
        private TypedObjectListView<Row> tlist;

        private Point defaultRowDisplayGroupLocation;

        private Point defaultGridDisplayLocation;
        
        /// <summary>
        /// constructor for this class. Just initializes things
        /// </summary>
        public View()
        {
            //this basically makes all the controls visible (buttons, textbox, etc)
            InitializeComponent();

            //set some properties for the user
            uxProcessingPanel.AutoSize = true;
            uxGridDisplay.AutoSize = true;

            //resize form for similar reason as previous two statements
            this.Height = 680;

            uxGridDisplay.MaximumSize = new Size(Int32.MaxValue, this.Height - uxGridDisplay.Location.Y);

            //initialize typedObjectListView
            tlist = new TypedObjectListView<Row>(this.uxRowListView);

            //set local variables to initial values
            timer = new System.Timers.Timer(1000)
            {
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += UpdateCurrentDateTime;

            //set up some delegates
            sendDateTime = ChangeDateTimeText;
            sendLogAppendation = AppendTextLogHelperMethod;

            //initialize some display stuff
            displays = new List<CellButtonDisplay>();
            defaultGridDisplayLocation = uxGridDisplay.Location;
            defaultRowDisplayGroupLocation = uxRowDisplayGroup.Location;

            //do a bunch of weird OLV delegate schenanigans
            tlist.GetColumn(0).GroupKeyGetter = delegate (Row rowObject)
            {
                Row row = rowObject;
                return row.CurrentCellOwner;
            };
            this.rowName.GroupKeyToTitleConverter = delegate (object groupKey)
            {
                Cell cell = (Cell)groupKey;
                string cellTypePlusChalkPlusLevel = "";
                string germMessage = cell.isGerm ? " - IsGerm = true" : "";
                //determine header based on cell state
                if (cell.IsNewRowFlag) cellTypePlusChalkPlusLevel = $"New Row Flag";
                else if (!cell.IsFullCell) cellTypePlusChalkPlusLevel = $"Incomplete Cell";
                else if (cell.IsEmptyCell) cellTypePlusChalkPlusLevel = $"Empty Cell";
                else if (cell.RowSpan == -2) cellTypePlusChalkPlusLevel = $"Abnormal Cell";
                else cellTypePlusChalkPlusLevel = $"Normal Cell - Chalk = {cell.Chalk:N1}% - Level = {allLevelInformation.FindLevel(cell.Chalk).Item1}" +
                    $"{germMessage}";
                return $"{cellTypePlusChalkPlusLevel}";
            };
            this.rowName.GroupFormatter = (OLVGroup group,
                GroupingParameters parms) =>
            {
                Cell groupCell = (Cell)group.Key;
                int index = groupCell.OwningGridObject.Cells.IndexOf(groupCell);
                group.Id = groupCell[0].RowNum;
                parms.GroupComparer = Comparer<OLVGroup>.Create((x, y) => (x.Id.CompareTo(y.Id)));
            };
        }//end constructor

        /// <summary>
        /// Updates the level information saved by the controller
        /// </summary>
        /// <param name="levelInformation">the level information we
        /// got from the file</param>
        public void UpdateLevelInformation(LevelInformation levelInformation)
        {
            this.allLevelInformation = levelInformation;
            GridListItemWrapper.levels = levelInformation;
        }//end UpdateLevelInformation(levelInformation)

        public SendLevelInformation updateControllerLevelInformation;
        private void TellControllerToUpdateLevel(LevelInformation levelInformation)
        {
            updateControllerLevelInformation(levelInformation);
            GridListItemWrapper.levels = levelInformation;
        }//end TellControllerToUpdateLevel(levelInformation)

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
            CellButtonDisplay display = new CellButtonDisplay(CloseAllCellButtonDisplays, button.Cell, allLevelInformation);
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

            //resets uxProcessingPanel before we add to it
            uxProcessingPanel.Controls.Clear();
            //builds the buttons for selecting grid levels
            BuildLevelSelectionButtons(uxGridPanel, uxProcessingPanel, LevelSelectionButton_Click);

            //add the grid to the other listview?
            if (uxGridListView.Items.Count == 0)
            {
                List<Grid> tempGrids = new List<Grid>();
                tempGrids.Add(grid);
                SetOLVGrid(uxGridListView, tempGrids, this.allLevelInformation);
            }//end if there's nothing there

            //makes the button grid visible
            uxGridDisplay.Visible = true;
            
            //update list
            uxRowListView.SetObjects(tempRows);

            //tell whoever called us that we were successful
            return true;
        }//end UpdateGrid(grid)

        /// <summary>
        /// updates the GUI with information from the supplied grids
        /// </summary>
        /// <param name="grids">the grids to display</param>
        /// <returns>returns true if the operation was successful, false otherwise</returns>
        public List<bool> UpdateGrids(List<Grid> grids)
        {
            //initialize list of success outputs
            List<bool> successOutputs = new List<bool>();

            //update list view?
            SetOLVGrid(uxGridListView, grids, this.allLevelInformation);

            //just return true
            successOutputs.Add(true);

            //throw new NotImplementedException();
            return successOutputs;
        }//end UpdateGrids(grids)

        /// <summary>
        /// builds the grid of buttons that represents the current grid.
        /// No longer WIP
        /// </summary>
        /// <param name="grid">the grid that you want to build</param>
        private void BuildButtonGrid(Grid grid)
        {
            //just grab a 2d list of cells from our grid :-)
            List<List<Cell>> allCells = grid.FormatCellsAs2DList();

            //just define the margin between buttons
            int buttonMargin = 5;

            //reset the controls in the uxGridPanel
            uxGridPanel.Controls.Clear();

            Point furthestLoc = new Point();

            //add our buttons to the groupbox so they're visible and put them in a snazzy grid
            for (int i = 0; i < allCells.Count; i++)
            {
                for(int j = 0; j < allCells[i].Count; j++)
                {
                    CellButton thisButton = new CellButton(allCells[i][j]);
                    thisButton.SetLevelInformation(allLevelInformation, null);
                    thisButton.FormatCellButton(uxToolTip);
                    thisButton.Size = uxStartReference.Size;
                    thisButton.Click += GridButtonClickEvent;

                    //not 100% why exactly this works, but it does
                    int X = uxStartReference.Location.X + j * (uxStartReference.Size.Width + buttonMargin);
                    int Y = uxStartReference.Location.Y + i * (uxStartReference.Size.Height + buttonMargin);
                    thisButton.Location = new Point(X, Y);

                    //updated furthest point
                    if (furthestLoc.X < thisButton.Location.X + thisButton.Width) furthestLoc.X = thisButton.Location.X + thisButton.Width;
                    if (furthestLoc.Y < thisButton.Location.Y + thisButton.Height) furthestLoc.Y = thisButton.Location.Y + thisButton.Height;

                    //add our button to the groupbox so it gets displayed
                    uxGridPanel.Controls.Add(thisButton);
                }//end looping over cell buttons
            }//end looping over lists of cell buttons

            uxGridPanel.Size = new Size(furthestLoc.X + buttonMargin, furthestLoc.Y + buttonMargin);
        }//end BuildButtonGrid(rows)

        /// <summary>
        /// Builds all the buttons for selecting and de-selecting different levels
        /// </summary>
        /// <param name="grid">the panel of CellButtons which displays them as a grid</param>
        /// <param name="outputPanel">the flowLayoutPanel which we will output level selection buttons to</param>
        /// <param name="buttonClickHandler">the event handler for each button</param>
        private void BuildLevelSelectionButtons(Panel grid, FlowLayoutPanel outputPanel, EventHandler buttonClickHandler)
        {
            for(int i = 0; i < allLevelInformation.Count; i++)
            {
                //save current level for reference
                LevelInformation.Level thisLevel = allLevelInformation[i];

                //initialize button
                Button levelButton = new Button
                {
                    Text = thisLevel.LevelName,
                    ForeColor = thisLevel.ForeColor,
                    BackColor = thisLevel.BackColor,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Font = new Font(FontFamily.GenericSansSerif, 10),
                    FlatStyle = FlatStyle.Popup,
                };

                levelButton.Click += buttonClickHandler;

                //add this control to the panel
                outputPanel.Controls.Add(levelButton);
            }//end building button for each level in allLevelInformation

            //make the button for resetting stuff

            grid.AutoSize = false;
        }//end BuildLevelSelectionButtons(grid, outputPanel, buttonClickHandler)

        private void LevelSelectionButton_Click(object sender, EventArgs e)
        {
            if(sender is Button button)
            {
                LevelInformation.Level level = allLevelInformation[button.Text];
                foreach(Control child in uxGridPanel.Controls)
                {
                    if(child is CellButton cellButton)
                    {
                        PropertyInfo property = cellButton.Cell.GetType()
                            .GetProperty(allLevelInformation.PropertyToTest);
                        decimal propValue = (decimal)property.GetValue(cellButton.Cell);
                        if (propValue > level.LevelStart && propValue <= level.LevelEnd)
                        {
                            //just toggles the visibility
                            child.Visible = !child.Visible;
                        }//end if the value is within the level
                        else
                        {
                            //child.Visible = false;
                        }//end else the value is not the correct level
                    }//end if this is a cellButton
                }//end looping over all the children of the grid panel
            }//end if sender is a button
        }//end LevelSelectionButton_Click(sender, e)

        /// <summary>
        /// closes the file and mostly resets things
        /// </summary>
        public void CloseRowList()
        {
            //clear the seeds displayed in the list
            uxRowListView.ClearObjects();

            //clear the displayed grids
            uxGridListView.Clear();

            //disable the elements for editing seeds so they can't be interacted with by the user
            uxRowDisplayGroup.Enabled = false;

            //remove all the buttons from the grid, and make that section invisible
            uxGridPanel.Controls.Clear();
            uxGridDisplay.Visible = false;
        }//end CloseRowList()

        /// <summary>
        /// Gets a new filename from the user based on what request type they specify.
        /// Currently accepted request types are OpenFile and SaveFileAs. Returns null
        /// if user selected to not select a file
        /// </summary>
        /// <param name="request">The type of request for a new filename that
        /// you are making. Either OpenFile or SaveFileAs</param>
        public string[] GetNewFilename(Request request)
        {
            string[] filenames = null;
            switch (request)
            {
                case Request.OpenFile:
                    using(OpenFileDialog openDialog = new OpenFileDialog())
                    {
                        openDialog.Multiselect = true;
                        if (openDialog.ShowDialog() == DialogResult.OK)
                        {
                            filenames = openDialog.FileNames;
                        }//end if the user went through with the operation
                    }//end use of openDialog
                    break;
                case Request.SaveFileAs:
                    using(SaveFileDialog saveDialog = new SaveFileDialog())
                    {
                        if (saveDialog.ShowDialog() == DialogResult.OK) filenames = saveDialog.FileNames;
                    }//end use of saveDialog
                    break;
                default:
                    throw new InvalidEnumArgumentException($"The specified" +
                        $" request {request} is not a valid parameter for GetFilename.");
            }//end switch case

            return filenames;
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
            if (text != "" && text != "\n")
                newLines[newLines.Length - 1] = $"{DateTime.Now:G} " + text;
            else
                newLines[newLines.Length - 1] = text;

            //update the textbox
            uxHeaderLog.Lines = newLines;
        }//end AppendTextLogHelperMethod(text)

        public CallMethodWithIndex saveFile;
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
            saveFile(0);
        }//end event handler for saving a file

        public CallMethodWithIndex saveFileAs;
        /// <summary>
        /// this method runs when the uxMenuSaveFileAs button is clicked. It uses
        /// a delegate to tell the Controller to save the data loaded as a new file
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void SaveFileAs(object sender, EventArgs e)
        {
            saveFileAs(0);
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

        public CallMethodWithRowStringDictionaryAndIndex editCell;
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
            editCell(rowIndexPairs, 0);
        }//end CellEditFinished event handler

        /// <summary>
        /// Updates size and position of stuff based on an auto-sized control
        /// </summary>
        private void uxGridPanel_SizeChanged(object sender, EventArgs e)
        {
            //define extra margin between panels
            int panelMargin = 10;
            
            //resize the groupbox this stuff is happening in
            int width = uxGridPanel.Location.X + uxGridPanel.Width + panelMargin;
            int height = uxGridPanel.Location.Y + uxGridPanel.Height + panelMargin;
            uxGridDisplay.Size = new Size(width, height);

            //move the panel down so that it's not blocking the buttons and resize it
            int X = uxProcessingPanel.Location.X;
            int Y = uxGridPanel.Location.Y + uxGridPanel.Height + panelMargin;
            uxProcessingPanel.Location = new Point(X, Y);
            uxProcessingPanel.MaximumSize = new Size(uxGridDisplay.Width, Int32.MaxValue);

            //go ahead and move the edge of the grid display group box down to cover the panel
            int gridDisplayWidth = uxGridDisplay.Width;
            int gridDisplayHeight = uxGridDisplay.Height + uxProcessingPanel.Height + panelMargin;
            uxGridDisplay.Size = new Size(gridDisplayWidth, gridDisplayHeight);
        }//end uxGridPanel SizeChanged event handler

        private void uxMenuItemToggleListDisplay_Click(object sender, EventArgs e)
        {
            if(uxRowDisplayGroup.Visible == true)
            {
                uxRowDisplayGroup.Visible = false;
                uxGridDisplay.Location = uxRowDisplayGroup.Location;
            }//end if we should hide the row display group
            else
            {
                uxRowDisplayGroup.Visible = true;
                uxGridDisplay.Location = defaultGridDisplayLocation;
            }//end else we should show the row display group
        }

        private bool listViewGroupsCollapsed = false;
        private void uxToggleGroupsCollapsed_Click(object sender, EventArgs e)
        {
            if (listViewGroupsCollapsed)
            {
                foreach(OLVGroup group in uxRowListView.OLVGroups)
                {
                    group.Collapsed = false;
                }//end looping over all the groups
            }//end if we should expand them
            else
            {
                foreach (OLVGroup group in uxRowListView.OLVGroups)
                {
                    group.Collapsed = true;
                }//end looping over all the groups
            }//end else we should collapsed them
        }

        /// <summary>
        /// Opens a dialog form in order to make the user select colors for stuff
        /// </summary>
        private void uxConfigureColorLevelsMenuItem_Click(object sender, EventArgs e)
        {
            ColorLevelDialog cld = new ColorLevelDialog(this.allLevelInformation, TellControllerToUpdateLevel);
            cld.ShowDialog();
        }//end uxConfigureColorLevelsMenuItem Click Event Handler

        private void uxRowListView_FormatRow(object sender, FormatRowEventArgs e)
        {
            if(e.Model is Row currentRow)
            {
                Cell owningCell = currentRow.CurrentCellOwner;
                Tuple<string, int> level = allLevelInformation.FindLevel(owningCell.Chalk);
                LevelInformation.Level thisLevel = allLevelInformation[level.Item2];
                e.Item.BackColor = thisLevel.BackColor;
                e.Item.ForeColor = thisLevel.ForeColor;
            }//end if our model is a row
        }//end event handler for formatting each row in OLV

        /// <summary>
        /// this method sets up the object list view which displays all the available grids.
        /// Tbh, it's pretty janky, but it works. Call this instead of olv.SetObjects()
        /// </summary>
        /// <param name="olv">the ObjectListView you want to set up</param>
        /// <param name="grids">the grids you want to display</param>
        /// <param name="levels">the level information for the grids</param>
        private void SetOLVGrid(ObjectListView olv, List<Grid> grids, LevelInformation levels)
        {
            //reset olv columns
            olv.AllColumns.Clear();
            //set type of olv
            TypedObjectListView<GridListItemWrapper> tolv = new TypedObjectListView<GridListItemWrapper>(olv);
            //set static variable
            GridListItemWrapper.levels = levels;
            //initialize our list of gridItemWrappers
            List<GridListItemWrapper> gridItems = new List<GridListItemWrapper>();
            //initialize list of gridItemWrappers
            foreach(Grid grid in grids)
            {
                gridItems.Add(new GridListItemWrapper(grid));
            }//end looping for each grid in grids
            //initialize columns for each of the items specified in our wrapper
            for(int i = 0; i < GridListItemWrapper.OutputColumnsTitle.Count; i++)
            {
                OLVColumn column = new OLVColumn();
                //sets text of column header
                column.Text = GridListItemWrapper.OutputColumnsTitle[i];
                //tells olv what type we're storing in this column
                TypedColumn<GridListItemWrapper> tColumn = new TypedColumn<GridListItemWrapper>(column);
                //tells olv how to find the contents for a specified item under this column
                tColumn.AspectGetter = delegate (GridListItemWrapper value)
                {
                    //figures out what index of the columns list this is
                    int likelyIndex = GridListItemWrapper.OutputColumnsTitle.IndexOf(column.Text);
                    if(likelyIndex == -1)
                    {
                        //if we're here, it's likely that this column is for a level that's been renamed
                        //so therefore we'll just return some nonsense value
                        return "Names Changing, Please Wait";
                    }//end if it's likely the column headers are changing
                    //returns index of textList in object stored at this spot in olv
                    return value.OutputColumnsText[likelyIndex];
                };//end converting aspect into what we want
                //add the column to the olv so it's visible
                olv.AllColumns.Add(column);
                //tell it not to fill all the space to multiselect is better
                column.FillsFreeSpace = false;
            }//end looping for each column title
            
            //use the set objects thing
            olv.SetObjects(gridItems);
            olv.RebuildColumns();
            //hopefully forec olv to autosize the columns
            for (int i = 0; i < olv.AllColumns.Count; i++)
            {
                //set visibility and resize stuff
                olv.AllColumns[i].IsVisible = GridListItemWrapper.OutputColumnsVisibility[i];
                olv.AllColumns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                olv.AllColumns[i].FillsFreeSpace = false;
            }//end trying to set visibility and width for all the columns
            //hopefully force olv to update columns
            olv.RebuildColumns();
            olv.AutoResizeColumns();
        }//end SetOLVGrid(olv, grids, levels)

        private void uxGridListView_DoubleClick(object sender, EventArgs e)
        {
            if(sender is ObjectListView olv)
            {
                if(olv.SelectedItem.RowObject is GridListItemWrapper gridItem)
                {
                    UpdateGrid(gridItem.Grid);
                }//end else if the row has a griditem
            }//end if the sender is an object listbox
        }//end uxGridListView_DoubleClick event handler

        private void uxAskWhereStart_Click(object sender, EventArgs e)
        {
            HelpMessageGeneration.AskWhereStart();
        }//end event handler

        private void uxAskSelectFile_Click(object sender, EventArgs e)
        {
            HelpMessageGeneration.AskGridSelection();
        }//end event handler

        private void uxAskConfigFileStorage_Click(object sender, EventArgs e)
        {
            HelpMessageGeneration.AskConfigContents();
        }//end event handler

        private void uxAskAvailableOptions_Click(object sender, EventArgs e)
        {
            HelpMessageGeneration.AskAvailableOptions();
        }//end event handler

        private void uxAskLogFunction_Click(object sender, EventArgs e)
        {
            HelpMessageGeneration.AskLogFunction();
        }//end event handler

        private void uxAskListFunctions_Click(object sender, EventArgs e)
        {
            HelpMessageGeneration.AskListFunction();
        }//end event handler

        private void uxAskGridFunctions_Click(object sender, EventArgs e)
        {
            HelpMessageGeneration.AskGridFunction();
        }//end event handler

        internal CallMethod requestExcelStuff;
        private void excelEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            requestExcelStuff();
        }//end click event for showing excel stuff

        internal void ShowExcelExport(string[] excelExport)
        {
            Form tempForm = new Form
            {
                AutoSize = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Font = new Font(FontFamily.GenericSerif, 15),
            };

            RichTextBox text = new RichTextBox();
            tempForm.Controls.Add(text);

            text.Width = tempForm.Width;
            text.Height = tempForm.Height;

            text.Lines = excelExport;

            tempForm.Show();
        }//end ShowExcelExport(excelExport)
    }//end class
}//end namespace