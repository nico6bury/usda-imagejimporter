using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Author: Nicholas Sixbury
 * File: HelpMessageGeneration.cs
 * Purpose: To provide one location for a bunch of
 * static methods with text for help messages.
 */

namespace ImageJImporter
{
    /// <summary>
    /// static class containing methods for generating help messages
    /// </summary>
    public static class HelpMessageGeneration
    {
        /*
         * Some things that would be good to write help messages for
         * 
         * Where to Start?
         * Selecting A File/Grid
         * What's in Config File?
         * What Options Are Available?
         * What's the Log do?
         * What Functions Does the Row List have?
         * What Functions Does the Button Grid have?
         */

        /// <summary>
        /// shows message box explaining where to start
        /// </summary>
        public static void AskWhereStart()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("The main parts of the program you should probably familiarize yourself with are " +
                "the tool strip at the top, the list of rows, the grid, and the log.\n\nThe tool strip contains " +
                "functions for opening files to display in the program, customizing how the program looks, and" +
                " also getting help with what everything does. There's also a list of different files loaded " +
                "into the program which you can switch between at will.");
            MessageBox.Show(sb.ToString(), "Where to Start", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//end AskWhereStart()

        /// <summary>
        /// shows message box explaining how to select a grid
        /// </summary>
        public static void AskGridSelection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("In order to select a grid to display, you must first open the file which contains the data " +
                "for that grid. This can be done by clicking File->Open on the toolstrip at the top.\n\nKeep in mind " +
                "that you can open how every many files you'd like, but the files loaded into the program at any one " +
                "time must be in the same directory, and even if they are all loaded in, you can only display one " +
                "at a time.\n\nOnce they are loaded into the program, simply double click the grid you wish to display " +
                "in the list of grids right below the log.\n\n");
            sb.Append("It should also be noted that you can right click the column headers to control which columns are displayed, " +
                "and even select and copy lines out of the list. Ctrl + A is useful for getting all the rows, and Ctrl + C " +
                "can be used to copy. When copying out of the grid list, only the columns you have selected will be copied, so" +
                "you can control which data will appear when pasting.");
            MessageBox.Show(sb.ToString(), "Selecting A Grid for a File", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//end AskGridSelection()

        /// <summary>
        /// shows message box explaining the config file
        /// </summary>
        public static void AskConfigContents()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("The configuration file, \"imageJImporterConfig.txt\", located in the same directory as the executable, ");
            sb.Append("has several settings that are stored between sessions to make things easier to use.\nEach line in the configuration ");
            sb.Append("file has a header that tells the program at startup what the information in the rest of the line is supposed to be");
            sb.Append(" used for. Each header is composed of a a few capital letters that are supposed to stand for something and a number ");
            sb.Append("which indicates version number. The version numbers are used so that the program can load a configuration file with ");
            sb.Append("an older format quickly and easily thanks to it knowing right away that the format is old.\n\nA header of \"FN\" ");
            sb.Append("indicates that a line holds filename information. For instance, the program stores the most recently used group ");
            sb.Append("of files and automatically opens them again when the program is re-opened. A header of \"LI\" indicates that the line ");
            sb.Append("defines the boundaries and display properties for one level. It\'s generally unnecessary to edit these lines from the ");
            sb.Append("config file because they can be edited much easier by changing settings in the program through the \"configure color levels\" option, ");
            sb.Append(" considering that the program can automatically resave that information in the correct format by itself.\n\n");
            sb.Append("On lines with the \"LI\" (level information) header, each property specified is listed with the name of the property ");
            sb.Append("within the program along with the value it should be set to, separated by the \'*\' character. \n\nAdditionally, the \"LIE\" ");
            sb.Append("header indicates that a line holds extra level information, such as the property of a particular cell which is tested ");
            sb.Append("to determine it\'s level. By default, this is \"Chalk\".\n\nIn most cases of ordinary program use, editing or viewing the config");
            sb.Append(" file should not be necessary.\nIt should additionally be noted that if the program runs without a config file found in the ");
            sb.Append("same directory as the executable with the correct name, a new file with default settings will be generated, so if the settings");
            sb.Append(" in a config file get messed up, you can always delete it and have the program generate a new one.");
            MessageBox.Show(sb.ToString(), "How the Config File Works", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//end AskConfigContents()

        /// <summary>
        /// shows message box explaining what options are available
        /// </summary>
        public static void AskAvailableOptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("There are several options available to customize how the program acts or appears." +
                " They are available by clicking the \"Options\" menu at the top of the program.\n\n");
            sb.Append("The \"Configure Color Levels\" option allows you to configure almost any aspect of how " +
                "various levels are calculated and displayed without needing to edit the config file directly. ");
            sb.Append("Any changes made will be saved for your next session assuming you don't remove the config file, and " +
                "also assuming that the program exits normally (ie doesn\'t crash). You can change the thresholds for " +
                "any of the levels, remove or add new levels, change the color of each level, and also change their name. " +
                "Upon exiting the dialogue box for doing these things, all levels will be recalculated.\n\n");
            sb.Append("The other two currently available options simply change the positioning of things and are not " +
                "saved to any external file for persistence between sessions.\n");
            sb.Append("The \"Toggle List Display\" option toggles whether or not the list of rows is displayed. When it it " +
                "toggled off, it becomes invisible, and the grid display is moved over to take its place. This can be helpful " +
                "for multi-tasking, as it moves all the most useful information to once side of the window.\n");
            sb.Append("The \"Toggle Collapsed-ness of Groups\" option is simply a quick way to collapse or un-collapse all " +
                "the \"groups\" in the list display. Each group is essentially either a flag for a new row, or all rows between " +
                "a start and end seed flag, including those flag rows. This allows you to just see the summary info for each cell " +
                "without the specific row data getting in the way.");
            MessageBox.Show(sb.ToString(), "Customization Options", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//end AskAvailableOptions()

        /// <summary>
        /// shows message box explaining what the log does
        /// </summary>
        public static void AskLogFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("The log at the top right of the program window stores summary information on what happens in each session as it happens.");
            sb.Append("All the lines in the log are stored to monthly summary logs in folders in the same directory as the executable. These " +
                "are present for data keeping purposes and general ease of use. At the moment, it's possible to edit the text in the log in case " +
                "you\'d like to add extra messages. This, along with how log files are stored, may change in the future.");
            MessageBox.Show(sb.ToString(), "What the Log Does", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//end AskLogFunction()

        /// <summary>
        /// shows message box explaining what the list can do
        /// </summary>
        public static void AskListFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("The list of rows found below the list of files holds several useful functions. It allows you to see the " +
                "raw data from a file more or less how it appeared in the file. By using Ctrl + A to select all lines and then " +
                "Ctrl + C to copy those selected lines, you can also copy all the data to a new file, with the pasted text appearing " +
                "exactly as it did in the file that data was inported from.\n\nEach group, or cell, of rows is color coded based on level, " +
                "and the header of each group displays what the program recognizes that group as, along with Chalk information.\n\nIt\'s " +
                "also possible to customize which columns are displayed. Simply right click the header of the list (where the column titles are), " +
                "and you can toggle which columns show up. It should be noted that regardless of which columns are selected, information from all" +
                "the columns plus the headers will always be copied when copy and pasting information out of the list.\n\n");
            sb.Append("It should also be noted that you can right click column headers to change grouping and sorting settings." +
                " This allows you to even turn off groups if you\'d like in order to just see the color coded rows. This can be turned back on " +
                "by clicking the Row header and choosing \"Group by Row\". Unfortunately many of " +
                "these functions largely do not work because the only thing set up is sorting and grouping by row. The ability to select different " +
                "options is still provided however, just in case someone finds some use for it.");
            MessageBox.Show(sb.ToString(), "What the List Display Does", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//end AskListFunction()

        /// <summary>
        /// shows message box explaining what the grid can do
        /// </summary>
        public static void AskGridFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("The grid is a very useful component of the program for visually identifying leveled seeds from the original " +
                "scanned grid. It is only visible when a particular file is loaded, and it has many functions.\n\n");
            sb.Append("In particular, the grid displays all the cells from the selected file in the same grid-like format they " +
                "were scanned in. Each cells is colored according to which level it\'s in. With the default settings, empty cells, " +
                "corner cells, and new line flag cells are all assigned to level 0. However, it is possible to change the level settings" +
                "to separate them into different levels for the purposes of differentiating between them in the grid. For reference, levels" +
                " are calculated as being greater than or equal to the lower bound, and less than the upper bound. Also, new line flags " +
                "have their Chalk set to -2, whereas empty cells (without a seed) have their Chalk set to -0.1.\n\n");
            sb.Append("In order to control which level of cells are visible, several buttons are located beneath the grid. Each button " +
                "corresponds to a level, and clicking on it will toggle whether or not that button is visible. At the moment, if you " +
                "want to only display cells of one level, you must toggle off the visibility of all the other levels.\n\n");
            sb.Append("In order to visually distinguish between cells which seem to contain a germ (so germDetect is true), cells " +
                "in the grid which do not contain a germ have a flat appearance with the color extending to the edge of the box, while " +
                "cells which do contain a germ have a small grey border around them.\n\n");
            sb.Append("Additionally, each cell has a number on it. This is the number of rows between the cell start and end flags. New line " +
                "flags have their number set to -1, empty cells have it set to 0, and so on.\n\n");
            sb.Append("By clicking on a cell in the grid, you can open a dialgue box which shows the rows for that cell. The rows in this " +
                "menu have the same appearance as the corresponding rows in the list display, and you can have as many of these windows open " +
                "as you like. There\'s a button at the bottom to close either only the current window or all of them. They also contain " +
                "more detailed information on how the program is calculating the Chalk for a particular cell, including all the data that " +
                "would have been calculated by a column in the spreadsheet. This is provided for debugging information as well as to double " +
                "check within the program whether a value is actually correct in case something seems off.");
            MessageBox.Show(sb.ToString(), "What the Grid Display Does", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }//end AskGridFunction()
    }//end class
}//end namespace