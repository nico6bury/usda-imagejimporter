using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageJImporter
{
    /// <summary>
    /// This class is the only one directly called when the program starts. It contains
    /// the main method and aggregates or references all other classes.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //I don't know what this does, but Visual Studio added it, so it probably shouldn't be removed
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //this is just the initialization of the classes we'll work with
            View view = new View();
            FileIO fileIO = new FileIO();
            Controller controller = new Controller(fileIO);

            //this connects the methods in the controller to the delegates
            HandleFileIO handleFileIO = controller.HandleFileIORequest;
            HandleSeedData handleSeedData = controller.HandleSeedDataRequest;
            HandleOpenClose handleOpenClose = controller.HandleOpenCloseRequest;

            //this connects the delegates to the view
            view.handleFileIO = handleFileIO;
            view.handleSeedData = handleSeedData;
            view.handleOpenClose = handleOpenClose;

            //this connects the method(s) in the view to the delegates
            ShowFormMessage showMessage = view.ShowMessage;
            UpdateSeedList updateSeedList = view.UpdateSeedList;
            ChangeSeedSelected changeSeedSelected = view.ChangeSeedSelected;
            ReturnBool wordsWrap = view.DoWordsWrap;
            SetBool wordWrap = view.SetWordWrap;
            DoAThing closeFile = view.CloseSeedList;

            //this connects the delegates to the controller
            controller.showMessage = showMessage;
            controller.updateSeedList = updateSeedList;
            controller.changeSeedSelected = changeSeedSelected;
            controller.wordsWrap = wordsWrap;
            controller.setWordWrap = wordWrap;
            controller.closeSeedList = closeFile;

            //this causes the application to actually run
            Application.Run(view);
        }//end main method
    }//end class
}//end namespace