using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageJImporter
{
    /// <summary>
    /// this class displays information and can be interacted with by the user
    /// </summary>
    public partial class View : Form
    {
        /// <summary>
        /// function pointer for HandleFileIO in controller. set up in program.cs
        /// </summary>
        public HandleFileIO handleFileIO;

        /// <summary>
        /// function pointer for HandleSeedData in controller. set up in program.cs
        /// </summary>
        public HandleSeedData handleSeedData;

        /// <summary>
        /// function pointer for HandleOpenCloseRequest in controller. set up in
        /// program.cs
        /// </summary>
        public HandleOpenClose handleOpenClose;

        /// <summary>
        /// holds which seed index we're currently looking at. Set to -1 if not looking
        /// at any seeds (for instance if the file is closed)
        /// </summary>
        private int currentSeedIndex;

        /// <summary>
        /// holds the current seed list we're displaying. set to null if nothing
        /// displayed
        /// </summary>
        private List<Cell> currentSeedList;

        /// <summary>
        /// constructor for this class. Just initializes things
        /// </summary>
        public View()
        {
            //this basically makes all the controls visible (buttons, textbox, etc)
            InitializeComponent();

            //set local variables to initial values
            currentSeedIndex = -1;
            currentSeedList = null;
        }//end constructor

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
        public void UpdateSeedList(List<Cell> data)
        {
            /// Note about the data type of uxSeedList: 
            /// uxSeedList is a ListBox, which means that
            /// the items it displays are not updated when
            /// items in its data source are updated. Instead,
            /// whenever one or all the items are updated, you
            /// have to set the data source to null and then whatever
            /// you want it to display
            
            uxSeedList.DataSource = null;
            currentSeedList = data;
            uxSeedList.DataSource = currentSeedList;

            //makes the controls which allow editing/viewing interactable by the user
            uxSeedDisplayGroup.Enabled = true;
        }//end UpdateSeedList(data)

        /// <summary>
        /// closes the file and mostly resets things
        /// </summary>
        public void CloseSeedList()
        {
            //clear the seeds displayed in the list
            uxSeedList.DataSource = null;

            //clear the text in the editing/viewing box
            uxTextViewer.Text = "";

            //disable the elements for editing seeds so they can't be interacted with by the user
            uxSeedDisplayGroup.Enabled = false;
        }//end CloseSeedList()

        /// <summary>
        /// updates which seed should be displayed in the editing box
        /// </summary>
        /// <param name="index">the index of the seed in the list of seeds which
        /// you want to view or edit</param>
        /// <param name="request">The type of request that was made. Valid request types are
        /// ViewSeedData and EditSeedData</param>
        public void ChangeSeedSelected(int index, Request request)
        {
            //check to make sure the request is valid
            if (request == Request.ViewSeedData || request == Request.EditSeedData)
            {
                //grab correct seed from list. If item is not seed, then it will be set to null
                Cell requestedSeed = uxSeedList.Items[index] as Cell;

                //check to make sure requestedSeed isn't null (this could otherwise cause problems later)
                if (requestedSeed == null)
                {
                    throw new ArgumentNullException("Data for requested seed at index is null");
                }//end if requestedSeed is null

                //set the text to be that of the seed's data
                uxTextViewer.Text = requestedSeed.FormatData(false);

                //set the textbox to ReadOnly if request is view, editable otherwise
                uxTextViewer.ReadOnly = (request == Request.ViewSeedData);

                //update current seed index
                currentSeedIndex = index;
            }//end if request is either to view or edit seed data
            else
            {
                MessageBox.Show($"{request} is not a valid request for seed index selection.",
                        "Invalid Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//end else an invalid request was sent
        }//end ChangeSeedSelected(index)

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
        /// this method runs when the uxMenuOpenFile button is clicked. It uses
        /// a delegate to tell the Controller to open a file
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void OpenFile(object sender, EventArgs e)
        {
            //get the file name from the user
            string filename = "";

            /// so a while ago, I was having some problems in which openFileDialog.Show()
            /// would result in the program freezing with no error message or anything. It
            /// was fixed by creating openFileDialog in the using statement, which means it
            /// is disposed of before this method ends. Thanks to Max for helping me with this
            //create and use OpenFileDialog, a class provided by Microsoft for selecting a file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.ShowHelp = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog.FileName;
                }//end if dialog was not cancelled by user
            }//end use of openFileDialog

            if(filename != "")
            {
                //initialize single length array of objects
                object[] args = new object[1];

                //put the name of the file into the args
                args[0] = (object)filename;

                //tell the controller we need to open the file named in args
                handleFileIO(Request.OpenFile, args);
            }//end if filename is not blank
        }//end event handler for opening a file

        /// <summary>
        /// this method runs when the uxMenuSaveFile button is clicked. It uses a
        /// delegate to tell the Controller to save the file we loaded earlier
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void SaveFile(object sender, EventArgs e)
        {
            if (uxSeedDisplayGroup.Enabled)
            {
                //set up an array of objects to send to the controller
                object[] data = new object[1];

                //add the current list of seeds to the data
                data[0] = currentSeedList;

                //tell the controller we want to save our current file with our current cell list
                handleFileIO(Request.SaveFile, data);
            }//end if we have a file open
            else
            {
                NoFileLoadedMessage();
            }//end else we don't have anything open yet
        }//end event handler for saving a file

        /// <summary>
        /// this method runs when the uxMenuSaveFileAs button is clicked. It uses
        /// a delegate to tell the Controller to save the data loaded as a new file
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void SaveFileAs(object sender, EventArgs e)
        {
            if (uxSeedDisplayGroup.Enabled)
            {
                //set up a string variable to hold the filename
                string filename = "";

                //open saveFileDialog to ask user where to save file
                using(SaveFileDialog saveFile = new SaveFileDialog())
                {
                    //tells saveFile to warn the user if they select to overwrite a file
                    saveFile.OverwritePrompt = true;

                    //only update filename if user clicks ok
                    if(saveFile.ShowDialog() == DialogResult.OK)
                    {
                        //save the name of the file for later
                        filename = saveFile.FileName;
                    }//end if the user clicked ok
                }//end use of save file dialog

                //we only want to save if the user selected a file, otherwise nothing happens
                if(filename != "")
                {
                    //set up an array of objects to send to the controller
                    object[] data = new object[2];

                    //add the current list of seeds to the data
                    data[0] = currentSeedList;

                    //add the filename we'll save to the data
                    data[1] = filename;

                    //tell the controller we want to save our current file with our current cell list
                    handleFileIO(Request.SaveFileAs, data);
                }//end if we can continue
            }//end if we have a file open
            else
            {
                //display error message for no file loaded
                NoFileLoadedMessage();
            }//end else we don't have anything open yet
        }//end event handler for creating a new file

        /// <summary>
        /// this method runs when the uxMenuCloseFile button is clicked. It uses a
        /// delegate to tell the Controller to close the file currently loaded
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void CloseFile(object sender, EventArgs e)
        {
            //tell the controller to tell us to close the file
            handleFileIO(Request.CloseFile, null);
        }//end event handler for closing a file

        /// <summary>
        /// this method runs when the uxViewSeed button is clicked. It uses a delegate
        /// to tell the Controller to send us seed data to display
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void ViewSeedData(object sender, EventArgs e)
        {
            //create the array of arguments to send to the controller
            object[] args = new object[1];

            //add information to the args
            args[0] = uxSeedList.SelectedIndex;

            //tell the controller to tell us what to do
            handleSeedData(Request.ViewSeedData, args);
        }//end event handler for viewing seed data

        /// <summary>
        /// this method runs when the uxEditSeed button is clicked. It uses a delegate
        /// to tell the Controller to send us seed data to edit
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void EditSeedData(object sender, EventArgs e)
        {
            //create the array of arguments to send to the controller
            object[] args = new object[1];

            //add information to the args
            args[0] = uxSeedList.SelectedIndex;

            //tell the controller to tell us what to do
            handleSeedData(Request.EditSeedData, args);
        }//end event handler for editing seed data

        /// <summary>
        /// this method runs when the uxSaveSeed button is clicked. It uses a delegate
        /// to tell the Controller to save the seed data we're about to send it
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void SaveSeedData(object sender, EventArgs e)
        {
            //set up the object array we'll pass to the controller
            object[] data = new object[3];

            //add the full list to the object array
            data[0] = currentSeedList;

            //add the index of the seed to the array
            data[1] = currentSeedIndex;

            //format the text for the new seed to add to the array
            StringBuilder lineBuilder = new StringBuilder();
            Cell selectedSeed = (Cell)uxSeedList.Items[currentSeedIndex];

            //add back the seed number plus the tab after it
            lineBuilder.Append(selectedSeed.SeedNum);
            lineBuilder.Append("\t");

            //add the rest of the edited text to the line
            lineBuilder.Append(uxTextViewer.Text);

            //actually put that line into the array
            data[2] = lineBuilder.ToString();

            //tell the controller that we want it to tell us to update one of the seeds
            handleSeedData(Request.SaveSeedData, data);
        }//end event handler for saving seed data

        /// <summary>
        /// event for when the form opens. Does startup things
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void OpenForm(object sender, EventArgs e)
        {
            //tell the controller that we just started up, so it needs to send us info on what to do
            handleOpenClose(Request.StartApplication);
        }//end event handler for opening the form

        /// <summary>
        /// event for right before the form closes. Saves settings for next time
        /// </summary>
        /// <param name="sender">the object that sent this event</param>
        /// <param name="e">if there are any arguments for the event, they're
        /// stored here</param>
        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            //tell the controller than we're about to close so it can do stuff before that happens
            handleOpenClose(Request.CloseApplication);
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
            if (uxSeedDisplayGroup.Enabled)
            {
                //ask the controller to tell us to display the filename
                handleFileIO(Request.AskFilename, null);
            }//end if a file is loaded
            else
            {
                //display the message for not having a file loaded
                NoFileLoadedMessage();
            }//end else there isn't a file loaded
        }//end AskForFilename event handler
    }//end class
}//end namespace