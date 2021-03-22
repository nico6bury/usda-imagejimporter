using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Author: Nicholas Sixbury
 * File: Program.cs
 * Purpose: To set up the classes used by the rest of the application and show the form to the user
 */

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
            RequestString requestFileName = controller.GiveCurrentFilename;

            //this connects the delegates to the view
            view.openFile = controller.OpenDataFile;
            view.saveFile = controller.SaveCurrentFile;
            view.saveFileAs = controller.SaveCurrentListAsNewFile;
            view.closeFile = controller.CloseCurrentFile;
            view.viewRows = controller.ViewRowData;
            view.editRows = controller.EditRowData;
            view.saveRows = controller.SaveRowData;
            view.formOpening = controller.OpenView;
            view.formClosing = controller.CloseView;
            view.requestFileName = requestFileName;

            //this connects the method(s) in the view to the delegates
            ShowFormMessage showMessage = view.ShowMessage;
            SendRowList updateSeedList = view.UpdateRowList;
            ReturnBool wordsWrap = view.DoWordsWrap;
            SetBool wordWrap = view.SetWordWrap;
            CallMethod closeFile = view.CloseRowList;

            //this connects the delegates to the controller
            controller.showMessage = view.ShowMessage;
            controller.updateGrid = view.UpdateGrid;
            controller.wordsWrap = view.DoWordsWrap;
            controller.setWordWrap = view.SetWordWrap;
            controller.closeSeedList = view.CloseRowList;
            controller.viewRows = view.SetRowViewability;
            controller.editRows = view.SetRowEditability;
            controller.appendTextLog = view.AppendTextToLog;
            controller.getNewFilename = view.GetNewFilename;

            //this causes the application to actually run
            Application.Run(view);
        }//end main method
    }//end class
}//end namespace