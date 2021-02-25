using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        /// <summary>
        /// This method is called to handle the event of the user needing to do
        /// something with the file
        /// </summary>
        /// <param name="request">The type of request which is being made</param>
        public void HandleFileIORequest(Request request, object[] args)
        {
            switch (request)
            {
                case Request.OpenFile:
                    //grabs the seed data from the file we got from the view
                    List<Cell> data = fileIO.LoadFile((string)args[0]);

                    //send the seed data back to the view
                    updateSeedList(data);

                    //break out of the switch statement
                    break;
                case Request.SaveFile:

                    //break out of the switch statement
                    break;
                case Request.SaveFileAs:

                    //break out of the switch statement
                    break;
                case Request.CloseFile:
                    //tell the view to close the file
                    closeSeedList();

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
        /// <param name="request">The type of request which is being made</param>
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
                    List<Cell> allSeeds = (List<Cell>)args[0];
                    int seedIndexS = (int)args[1];
                    string seedLine = (string)args[2];

                    //generate an updated cell object from the string
                    Cell newSeed = fileIO.ParseCell(seedLine);

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
                        try
                        {
                            //load all the cell information from the file we got from the config
                            List<Cell> cells = fileIO.LoadFile(data[0]);

                            //pass that info back to the view
                            updateSeedList(cells);

                            //try to determine whether we should wrap text
                            bool wrapText = Convert.ToBoolean(data[1]);

                            //tell the view whether it should wrap text
                            setWordWrap(wrapText);
                        }//end try
                        catch
                        {
                            //show a message to the user informing them of an error
                            showMessage("Something in the configuration file has caused an error. Default file won't be loaded.",
                                "Default File Load Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        }//end catch
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