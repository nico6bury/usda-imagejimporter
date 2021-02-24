using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    //break out of the switch statement
                    break;
                default:

                    //break out of the switch statement
                    break;
            }//end switch case
        }//end HandleFileIORequest(request)

        /// <summary>
        /// This method is called to handle the event of the user needing access to
        /// the seed data which has already been loaded
        /// </summary>
        /// <param name="request">The type of request which is being made</param>
        public void HandleSeedDataRequest(Request request, object[] args)
        {
            switch (request)
            {
                case Request.ViewSeedData:

                    //break out of the switch statement
                    break;
                case Request.EditSeedData:

                    //break out of the switch statement
                    break;
                case Request.SaveSeedData:

                    //break out of the switch statement
                    break;
                default:

                    //break out of the switch statement
                    break;
            }//end switch case
        }//end HandleSeedDataRequest(request)
    }//end class
}//end namespace