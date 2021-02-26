using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageJImporter
{
    /// <summary>
    /// this enum is used for delegates to tell one part of the program what they
    /// are requesting from another part of the program
    /// </summary>
    public enum Request
    {
        /// <summary>
        /// Request to open a file and read its contents
        /// </summary>
        OpenFile,

        /// <summary>
        /// Request to save the currently opened file without changing
        /// the name or anything
        /// </summary>
        SaveFile,

        /// <summary>
        /// Request to save a new file somewhere
        /// </summary>
        SaveFileAs,

        /// <summary>
        /// Request to close a file without making changes
        /// </summary>
        CloseFile,

        /// <summary>
        /// Request for the name of the file which is currently open
        /// </summary>
        AskFilename,

        /// <summary>
        /// Request to view the data of a single seed
        /// </summary>
        ViewSeedData,

        /// <summary>
        /// Request to edit the data of a single seed
        /// </summary>
        EditSeedData,

        /// <summary>
        /// Request to save the data of a single seed to the program's
        /// current internal list of seeds
        /// </summary>
        SaveSeedData,

        /// <summary>
        /// Request to perform startup actions
        /// </summary>
        StartApplication,

        /// <summary>
        /// Request to perform actions related to closing the application
        /// </summary>
        CloseApplication
    }//end enum
}//end namespace