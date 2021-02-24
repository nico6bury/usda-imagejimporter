using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageJImporter
{
    /// <summary>
    /// this enum is used for delegates to tell one part of the program what they
    /// are requesting from another part of the program
    /// </summary>
    public enum Request
    {
        OpenFile,
        SaveFile,
        SaveFileAs,
        CloseFile,
        ViewSeedData,
        EditSeedData,
        SaveSeedData
    }//end enum Request

    /// <summary>
    /// The following global variables are called delegates. They're essentially
    /// function pointers that allow one part of the program to reference a 
    /// different part of the program. They can be named basically anything as long
    /// as their return type and parameter list are the same as the function that
    /// they point to.
    /// </summary>
    public delegate void HandleFileIO(Request request, object[] args);
    public delegate void HandleSeedData(Request request, object[] args);
    public delegate void ShowFormMessage(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
    public delegate void UpdateSeedList(List<Cell> data);
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

            //this connects the delegates to the view
            view.handleFileIO = handleFileIO;
            view.handleSeedData = handleSeedData;

            //this connects the method(s) in the view to the delegates
            ShowFormMessage showMessage = view.ShowMessage;
            UpdateSeedList updateSeedList = view.UpdateSeedList;

            //this connects the delegates to the controller
            controller.showMessage = showMessage;
            controller.updateSeedList = updateSeedList;

            //this causes the application to actually run
            Application.Run(view);
        }//end main method
    }//end class
}//end namespace