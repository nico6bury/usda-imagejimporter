﻿using System;
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
    public delegate bool SendRowList(List<Row> data);

    /// <summary>
    /// Points to a function that needs a grid and tells you whether it
    /// was successful or not.
    /// </summary>
    /// <param name="grid">The grid you're sending</param>
    /// <returns></returns>
    public delegate bool SendGrid(Grid grid);

    /// <summary>
    /// Points to a function that needs multiple grids and tells you whether
    /// that function was successful or not for each grid
    /// </summary>
    /// <param name="grids">the grids you want to send</param>
    /// <returns>a list of booleans to tell you if each grid
    /// was successful</returns>
    public delegate List<bool> SendGrids(List<Grid> grids);

    /// <summary>
    /// Points to a function that needs a string array
    /// </summary>
    /// <param name="array">the string array you're sending</param>
    public delegate void SendStringArray(string[] array);

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
    public delegate void CallMethod();

    /// <summary>
    /// calls a method with an index
    /// </summary>
    /// <param name="index"></param>
    public delegate void CallMethodWithIndex(int index);

    /// <summary>
    /// points to a function that requires a list of integers as a parameter
    /// </summary>
    public delegate void CallMethodWithInts(List<int> integers);

    /// <summary>
    /// points to a function that requires a dictionary containing a bunch of
    /// rows as strings and their corresponding indices
    /// </summary>
    /// <param name="rowIndexPairs">The dictionary of rows and their corresponding index
    /// which will be sent</param>
    public delegate void CallMethodWithRowStringDictionary(Dictionary<string, int> rowIndexPairs);

    /// <summary>
    /// points to a function that requires a dictionary containing a bunch of
    /// rows as strings and their corresponding indices
    /// </summary>
    /// <param name="rowIndexPairs">The dictionary of rows and their corresponding index
    /// which will be sent</param>
    /// <param name="index">the index of the grid (or whatever) you want to change or do whatever with</param>
    public delegate void CallMethodWithRowStringDictionaryAndIndex(Dictionary<string, int> rowIndexPairs, int index);

    /// <summary>
    /// Allows you to request a string
    /// </summary>
    /// <returns>hopefully returns the string you requested</returns>
    public delegate string RequestString();

    /// <summary>
    /// Allows you to request a specific string based on request type
    /// </summary>
    /// <param name="request">the type of string you're requesting</param>
    /// <returns>hopefully the string you requested</returns>
    public delegate string RequestSpecificString(Request request);

    /// <summary>
    /// Allows you to request a specific string array based on request
    /// type
    /// </summary>
    /// <param name="request">the type of strings you want</param>
    /// <returns>hopefully the strings you wanted</returns>
    public delegate string[] RequestSpecificStrings(Request request);

    /// <summary>
    /// Allows you to send a string somewhere
    /// </summary>
    /// <param name="text">the string you want to send</param>
    public delegate void SendString(string text);

    public delegate void SendStrings(string[] strings);

    /// <summary>
    /// Allows you to send a collection of level information to...
    /// somewhere
    /// </summary>
    /// <param name="levelInformation">the collection of level information you
    /// want to send</param>
    public delegate void SendLevelInformation(LevelInformation levelInformation);

    /// <summary>
    /// allows you to send a level somewhere
    /// </summary>
    /// <param name="level">the level you want to send</param>
    public delegate void SendLevel(LevelInformation.Level level);
}//end namespace