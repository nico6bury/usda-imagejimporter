using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Nicholas Sixbury
 * File: FileIO.cs
 * Purpose: To provide an interface for manipulating files, both for saving or 
 * loading rows of seed data from a file or using configuration files. Also provides
 * methods of parsing information from a file into usable objects.
 */

namespace ImageJImporter
{
    /// <summary>
    /// this class handles all of the actual heavy lifting when it comes
    /// to manipulating files by importing files and
    /// saving them and all that
    /// </summary>
    public class FileIO
    {
        /// <summary>
        /// this is the name of the file that we most recently used
        /// </summary>
        public string file;

        /// <summary>
        /// the name this class uses for it's config file
        /// </summary>
        private string configFilename = "imageJImporterConfig.txt";

        /// <summary>
        /// this is the absolute path of the configuration file
        /// (so it starts C//:... whatever else)
        /// </summary>
        private string configFileAbsolutePath;

        /// <summary>
        /// this is the file header to put at the top of any imageJ
        /// file we save. Formatted like this
        /// </summary>
        private string imageJFileHeader = " \tArea\tX\tY\tPerim.\tMajor\tMinor\t" +
            "Angle\tCirc.\tAR\tRound\tSolidity";

        /// <summary>
        /// the name of the folder we put log files in
        /// </summary>
        private string logFileFolderName = "LogFiles";

        /// <summary>
        /// this is the standard constructor for this class.
        /// It just initializes the variables in the normal way, nothing too special
        /// </summary>
        public FileIO()
        {
            file = "";
            configFileAbsolutePath = "";
        }//end no-arg constructor

        /// <summary>
        /// this constructor is used for copying an object of this type
        /// </summary>
        /// <param name="m">the model you wish to copy</param>
        public FileIO(FileIO m)
        {
            //sets the file variable of this class to that of the one provided
            this.file = m.file;
        }//end 1-arg constructor

        /// <summary>
        /// this method saves the configuration file so loading the program
        /// is easier next time
        /// </summary>
        public void SaveConfigFile()
        {
            //creates config file. If it already exists, we overwrite it
            using (StreamWriter scribe = new StreamWriter(configFileAbsolutePath))
            {
                //write default filename to config file
                scribe.WriteLine(file);
            }//end use of StreamWriter
        }//end SaveConfigFile()

        /// <summary>
        /// loads the configuration file in the same directory as the executable
        /// </summary>
        /// <returns>returns information from file as object array. If no file exists,
        /// returns null</returns>
        public List<string> LoadConfigFile()
        {
            if (File.Exists(configFilename))
            {
                //save absolute path of config file
                configFileAbsolutePath = Path.GetFullPath(configFilename);

                //initialize an array of objects
                List<string> data = new List<string>();

                //open a stream reader to read info from config file
                using(StreamReader reader = new StreamReader(configFilename))
                {
                    while (!reader.EndOfStream)
                    {
                        //asynchronously reads line
                        string line = reader.ReadLineAsync().Result;
                        
                        //adds line to list
                        data.Add(line);
                    }//end looping while there's still more to read
                }//end use of StreamReader

                //return information we just found
                return data;
            }//end if the configuration file exists
            else
            {
                //create config file
                File.Create(configFilename);

                //save absolute path of config file
                configFileAbsolutePath = Path.GetFullPath(configFilename);

                //return that no configuration info was found
                return null;
            }//end else the configuration file does not exist
        }//end LoadConfigFile()

        /// <summary>
        /// This method saves a .txt file with all the information from the
        /// cell list provided and the file name provided
        /// </summary>
        /// <param name="file">the name we'll save the file as. This method is
        /// totally fine with overwriting things, so be carefull</param>
        /// <param name="rows">the list of rows that will be saved</param>
        public void SaveFile(string file, List<Row> rows)
        {
            //if the file doesn't exist, we'll need to make it before doing anything else
            if (!File.Exists(file))
            {
                //creates the file and closes it so we can access it
                File.Create(file).Close();
            }//end if the file doesn't exist

            using(StreamWriter scribe = new StreamWriter(file))
            {
                //write the header to the file
                scribe.WriteLine(imageJFileHeader);

                //start adding each cell, line by line
                for(int i = 0; i < rows.Count; i++)
                {
                    //write the cell data for this line
                    scribe.WriteLine(rows[i].FormatData(true));
                }//end looping for each item in rows
            }//end use of Stream Writer
        }//end SaveFile(file)

        /// <summary>
        /// This method loads a .txt file processed by imageJ and returns 
        /// a list of the Cell objects generated.
        /// </summary>
        /// <param name="file">The file to open and read</param>
        /// <returns>returns a list of rows generated from the file</returns>
        public List<Row> LoadFile(string file)
        {
            //initializes the list we'll store the data in
            List<Row> data = new List<Row>();

            //start reading the file
            using(StreamReader reader = new StreamReader(file))
            {
                //this just pushes the header line off on the stream
                reader.ReadLine();

                //start looping until we reach the end of the file
                while (!reader.EndOfStream)
                {
                    //reads in the current row
                    string rowStr = reader.ReadLine();

                    //make new row object with information from rowStr
                    Row row = new Row(rowStr);

                    //add the cell to our list of rows so we can return it later
                    data.Add(row);
                }//end looping while not at end of stream
            }//end use of streamreader

            //sets last used text file
            this.file = file;

            //return the list of rows to whatever called this method
            return data;
        }//end LoadFile()

        /// <summary>
        /// Saves the provided lines to the monthly log file
        /// </summary>
        /// <param name="lines">the lines from the log to save</param>
        public void SaveLinesToLog(string[] lines)
        {
            //try and figure out the directory out config is in
            string configDirectory = Path.GetDirectoryName(configFileAbsolutePath);

            //figure out what our log file for this month is called
            StringBuilder sb = new StringBuilder();
            sb.Append($"{DateTime.Now.ToString("MMMM")}-Monthly-ImageJDataProcessing-LogFile");

            //try and do a new folder with the log file in it
            string newDirectory = $"{configDirectory}{Path.DirectorySeparatorChar}{logFileFolderName}{Path.DirectorySeparatorChar}";

            //ensure that our folder exists
            Directory.CreateDirectory(newDirectory);

            //save our new file location for the log to a variable
            string newFileLocation = newDirectory + sb.ToString() + ".txt";

            //make the file if it doesn't exist
            if (!File.Exists(newFileLocation)) File.Create(newFileLocation).Close();

            //start writing the lines to the file
            using (StreamWriter scribe = File.AppendText(newFileLocation))
            {
                foreach(string line in lines)
                {
                    //write this line to the file
                    scribe.WriteLine(line);
                }//end looping over all the lines we want to write
            }//end use of scribe
        }//end SaveLinesToLog(lines)
    }//end class
}//end namespace