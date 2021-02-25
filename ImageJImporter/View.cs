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
        /// constructor for this class. Just initializes things
        /// </summary>
        public View()
        {
            //this basically makes all the controls visible (buttons, textbox, etc)
            InitializeComponent();

            //set local variables to initial values
            currentSeedIndex = -1;
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
            MessageBox.Show(text, caption, buttons, icon);
        }//end ShowMessage

        /// <summary>
        /// updates the seed list in the view with new cell data
        /// </summary>
        /// <param name="data"></param>
        public void UpdateSeedList(List<Cell> data)
        {
            uxSeedList.DataSource = null;
            uxSeedList.DataSource = data;
            uxSeedDisplayGroup.Enabled = true;
        }//end UpdateSeedList(data)

        /// <summary>
        /// updates which seed should be displayed in the editing box
        /// </summary>
        /// <param name="index"></param>
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
            return uxTextViewer.WordWrap;
        }//end DoWordsWrap()

        /// <summary>
        /// sets the WordWrap property of the textbox for viewing seed data
        /// </summary>
        /// <param name="wordWrap"></param>
        public void SetWordWrap(bool wordWrap)
        {
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
            handleFileIO(Request.SaveFile, new object[0]);
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
            handleFileIO(Request.SaveFileAs, new object[0]);
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
            handleFileIO(Request.CloseFile, new object[0]);
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
            handleSeedData(Request.SaveSeedData, new object[0]);
        }//end event handler for saving seed data

        private void OpenForm(object sender, EventArgs e)
        {
            handleOpenClose(Request.StartApplication);
        }//end event handler for opening the form

        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            handleOpenClose(Request.CloseApplication);
        }//end event handler for closing the form

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
    }//end class
}//end namespace