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
        /// constructor for this class. Just initializes things
        /// </summary>
        public View()
        {
            //this basically makes all the controls visible (buttons, textbox, etc)
            InitializeComponent();
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
        }//end UpdateSeedList(data)

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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowHelp = true;
            string filename = "";

            using (openFileDialog)
            {
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
            handleSeedData(Request.ViewSeedData, new object[0]);
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
            handleSeedData(Request.EditSeedData, new object[0]);
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
    }//end class
}//end namespace