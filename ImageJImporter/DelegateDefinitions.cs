using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Author: Nicholas Sixbury
 * File: DelegateDefinitions.cs
 * Purpose: To provide type definitions for delegates used throughout the program
 */

namespace ImageJImporter
{
    /// <summary>
    /// The following global variables are called delegates. They're essentially
    /// function pointers that allow one part of the program to reference a 
    /// different part of the program in a way that can be dynamically changed at run time.
    /// They can be named basically anything as long
    /// as their return type and parameter list are the same as the function that
    /// they point to.
    /// </summary>

    /// <summary>
    /// Points to function for handling requests related to fileIO
    /// </summary>
    /// <param name="request">the request being made to whatever function
    /// this delegate points to. Will probably have "File" in it somewhere</param>
    /// <param name="args">the data needed to complete the request. Check the
    /// function to find out what data is needed</param>
    public delegate void HandleFileIO(Request request, object[] args);

    /// <summary>
    /// Points to function for handling requests involving specific row data
    /// </summary>
    /// <param name="request">the request being made to whatever function
    /// this delegate points to. Will probably have "Seed" in it somewhere</param>
    /// <param name="args">the data needed to complete the request. Check the
    /// function to find out what data is needed</param>
    public delegate void HandleSeedData(Request request, object[] args);

    /// <summary>
    /// Points to a function for showing a message box
    /// </summary>
    /// <param name="text">the text in the message box shown</param>
    /// <param name="caption">the title shown at the top of the message box</param>
    /// <param name="buttons">the buttons shown on the message box</param>
    /// <param name="icon">the icon shown to the left of the text in the message
    /// box</param>
    public delegate void ShowFormMessage(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);

    /// <summary>
    /// Points to a function for updating a list of rows (stored in Row objects)
    /// </summary>
    /// <param name="data">the list of rows which will be used to update
    /// something</param>
    public delegate void UpdateSeedList(List<Row> data);

    /// <summary>
    /// Points to a function for updating which row is selected
    /// </summary>
    /// <param name="index">the index of the row you want to change</param>
    /// <param name="request">the type of request being made. This will probably
    /// be either ViewSeedData or EditSeedData</param>
    public delegate void ChangeSeedSelected(int index, Request request);

    /// <summary>
    /// Points to a function for handling requests related to opening or closing
    /// the application
    /// </summary>
    /// <param name="request">the type of request being made. This will probably
    /// be either StartApplication or CloseApplication</param>
    public delegate void HandleOpenClose(Request request);

    /// <summary>
    /// Points to a function that returns true or false. Very generic
    /// </summary>
    /// <returns>the boolean returned by the function pointed to by this
    /// delegate</returns>
    public delegate bool ReturnBool();

    /// <summary>
    /// Points to a function that only reqires a boolean. Very generic
    /// </summary>
    /// <param name="value">The boolean supplied to the function this delegate
    /// points to</param>
    public delegate void SetBool(bool value);

    /// <summary>
    /// Points to a function that either has a non-dynamic function or just doesn't
    /// need external parameters and also doesn't return anything.
    /// </summary>
    public delegate void DoAThing();
}//end namespace