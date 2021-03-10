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
        /// function pointer for UpdateSeedList in view. set up in program.cs
        /// </summary>
        public UpdateSeedList updateSeedList;

        /// <summary>
        /// function pointer for ChangeSeedSelected in view. set up in program.cs
        /// </summary>
        public ChangeSeedSelected changeSeedSelected;

        /// <summary>
        /// function pointer for DoWordsWrap in view. set up in program.cs
        /// </summary>
        public ReturnBool wordsWrap;

        /// <summary>
        /// function pointer for the SetWordWrap in view. set up in program.cs
        /// </summary>
        public SetBool setWordWrap;

        /// <summary>
        /// function pointer for the CloseSeedList in view. set up in program.cs
        /// </summary>
        public DoAThing closeSeedList;

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
        /// This method is called to handle the event of the user needing to do
        /// something with the file
        /// Warning: The type of incoming data from the args array is generally
        /// not checked, so make sure to send the right data types in the right order
        /// </summary>
        /// <param name="request">The type of request which is being made. The following
        /// types of requests are accepted by this method: OpenFile, SaveFile, SaveFileAs, 
        /// CloseFile, AskFileName</param>
        /// <param name="args">the necessary data to complete the request. Make sure data
        /// within the array is of the correct type and in the correct order. To Open a file, supply
        /// the filename as a string that you wish to open. To Save a file, provide a list of rows.
        /// To save a file as something, provide the list of rows to save as well as the filename as
        /// a string. Requests other than those specified do not require extra data.</param>
        public void HandleFileIORequest(Request request, object[] args)
        {
            switch (request)
            {
                case Request.OpenFile:
                    //grabs the seed data from the file we got from the view
                    List<Row> openFileData = fileIO.LoadFile((string)args[0]);

                    //we want to reset stuff in the view first
                    closeSeedList();

                    //send the seed data back to the view so it can show it
                    updateSeedList(openFileData);

                    //break out of the switch statement
                    break;
                case Request.SaveFile:
                    //grab the row data from the args array
                    List<Row> saveFileData = (List<Row>)args[0];

                    //tell the model to save the currently loaded file
                    fileIO.SaveFile(fileIO.file, saveFileData);

                    //break out of the switch statement
                    break;
                case Request.SaveFileAs:
                    //grab the data from the args array
                    List<Row> curList = (List<Row>)args[0];
                    string saveAsFilename = (string)args[1];

                    //tell fileIO to save the file with specified name
                    fileIO.SaveFile(saveAsFilename, curList);

                    //break out of the switch statement
                    break;
                case Request.CloseFile:
                    //tell the view to close the file
                    closeSeedList();

                    //break out of the switch statement
                    break;
                case Request.AskFilename:
                    //ask fileIO what the last file it opened was, and then save it
                    string requestedFilename = fileIO.file;

                    //tell the view to show a message box with the filename we got
                    showMessage($"The file you are currently viewing is \"{requestedFilename}\"",
                        "Filename Request", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //break out of the switch statement
                    break;
                default:

                    //break out of the switch statement
                    break;
            }//end switch case
        }//end HandleFileIORequest(request)

        /// <summary>
        /// This method is called to handle the event of the user needing access to
        /// the seed data which has already been loaded.
        /// Warning: The type of incoming data from the args array is generally
        /// not checked, so make sure to send the right data types in the right order
        /// </summary>
        /// <param name="request">The type of request which is being made. The following
        /// types of requests are accepted by this method: ViewSeedData, EditSeedData, 
        /// SaveSeedData</param>
        /// <param name="args">the necessary data to complete the request. Make sure data
        /// within the array is of the correct type and in the correct order. To view seed
        /// data, supply the index of the seed you wish to view as an int. The same goes for
        /// editing a seed. To save a seed, provide the list of rows containing the seed you
        /// wish to save, the index of the seed you wish to save as an int, and a string
        /// containing a row of values to save to the seed, in the same format as the imageJ
        /// output files.</param>
        public void HandleSeedDataRequest(Request request, object[] args)
        {
            switch (request)
            {
                case Request.ViewSeedData:
                    //take the information out of args
                    int seedIndexV = (int)args[0];

                    //tell the view to change the selected seed
                    changeSeedSelected(seedIndexV, request);

                    //break out of the switch statement
                    break;
                case Request.EditSeedData:
                    //take the information out of args
                    int seedIndexE = (int)args[0];

                    //tell the view to change the selected seed
                    changeSeedSelected(seedIndexE, request);

                    //break out of the switch statement
                    break;
                case Request.SaveSeedData:
                    //take the data outside the args array
                    List<Row> allSeeds = (List<Row>)args[0];
                    int seedIndexS = (int)args[1];
                    string seedLine = (string)args[2];

                    //generate an updated row object from the string
                    Row newSeed = new Row(seedLine);

                    //put the new seed back into the list
                    allSeeds[seedIndexS] = newSeed;

                    //tell the view to update it's list with the one we send it
                    updateSeedList(allSeeds);

                    //break out of the switch statement
                    break;
                default:

                    //break out of the switch statement
                    break;
            }//end switch case
        }//end HandleSeedDataRequest(request)

        /// <summary>
        /// This method is called to handle the event of the view being opened or closed.
        /// It mostly deals with loading and saving the settings configuratino file saved by
        /// fileIO.
        /// </summary>
        /// <param name="request">The type of request being made. For this method, StartApplication
        /// and CloseApplication are valid request types.</param>
        public void HandleOpenCloseRequest(Request request)
        {
            switch (request)
            {
                case Request.StartApplication:
                    //grab whatever we can from the config file
                    List<string> data = fileIO.LoadConfigFile();

                    if(data != null)
                    {
                        //the code below is wrapped in a try catch so any configuration errors won't stop
                        //the program from running normally
                        //try
                        //{
                            //load all the row information from the file we got from the config
                            List<Row> rows = fileIO.LoadFile(data[0]);

                            //pass that info back to the view
                            updateSeedList(rows);

                            //update fileIO's most recently used file property
                            fileIO.file = data[0];

                            //try to determine whether we should wrap text
                            bool wrapText = Convert.ToBoolean(data[1]);

                            //tell the view whether it should wrap text
                            setWordWrap(wrapText);
                        //}//end try
                        //catch
                        //{
                        //    //show a message to the user informing them of an error
                        //    showMessage("Something in the configuration file has caused an error. Default file won't be loaded.",
                        //        "Default File Load Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        //}//end catch
                    }//end if data isn't null

                    //break out of the switch statement
                    break;
                case Request.CloseApplication:
                    //tell fileIO to save whatever needs saved
                    fileIO.SaveConfigFile(wordsWrap());

                    //break out of the switch statement
                    break;
                default:
                    //shows an error message to the user without actually throwing an error
                    showMessage($"It seems something went wrong when trying to open or close the form. {request} is not " +
                        "a valid request type.", "Invalid Open/Close Request Revieved",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    //break out of the switch statement
                    break;
            }//end switch case
        }//end HandleOpenCloseRequest(request)
    }//end class
}//end namespace