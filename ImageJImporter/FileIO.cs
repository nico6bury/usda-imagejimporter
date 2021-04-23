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
        public string file = null;

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
        /// creates a default configuration file with the defualt levels
        /// and all the other stuff
        /// </summary>
        /// <param name="levels">out parameter for levelInformation</param>
        /// <returns>returns the list of strings you could write to a file</returns>
        private List<string> CreateDefaultConfigFile(out LevelInformation levels)
        {
            List<string> linesToOutput = new List<string>();
            levels = LevelInformation.DefaultLevels;

            //add the current file name to the file
            linesToOutput.Add(file);

            //add the level information options
            linesToOutput.Add($"LIE1;{nameof(levels.PropertyToTest)}:{levels.PropertyToTest}");

            //add all the lines from the level information to our output list
            foreach (string line in levels.MakeLinesToSaveToFile())
            {
                linesToOutput.Add(line);
            }//end adding all the level information to output lines

            return linesToOutput;
        }//end CreateDefaultConfigFile()

        /// <summary>
        /// this method saves the configuration file so loading the program
        /// is easier next time
        /// </summary>
        /// <param name="levelInformation">The level information to save
        /// to this file. If this parameter is null, then no information
        /// will be saved.</param>
        /// <param name="cellParamName">The name of the property we're comparing
        /// using the levels</param>
        public void SaveConfigFile(LevelInformation levelInformation)
        {
            //creates config file. If it already exists, we overwrite it
            using (StreamWriter scribe = new StreamWriter(configFileAbsolutePath))
            {
                //write default filename to config file
                scribe.WriteLine(file);

                //write all the levelInformation
                if(levelInformation != null)
                {
                    string propName = levelInformation.PropertyToTest;
                    if (String.IsNullOrEmpty(propName)) propName = "Chalk";
                    scribe.WriteLine($"LIE1;{nameof(levelInformation.PropertyToTest)}:{propName}");
                    foreach(string line in levelInformation.MakeLinesToSaveToFile())
                    {
                        scribe.WriteLine(line);
                    }//end looping over the lines of level information
                }//end if levelInformation isn't null
            }//end use of StreamWriter
        }//end SaveConfigFile()

        /// <summary>
        /// loads the configuration file in the same directory as the executable
        /// </summary>
        /// <param name="levelInformation">The level information that was loaded from
        /// the configuration file</param>
        /// <returns>returns name of the file we should open</returns>
        public string LoadConfigFile(out LevelInformation levelInformation)
        {
            //initialize levelInformation out parameter
            levelInformation = new LevelInformation();

            if (File.Exists(configFilename))
            {
                //save absolute path of config file
                configFileAbsolutePath = Path.GetFullPath(configFilename);

                //initialize a list of our lines
                List<string> allLinesFromFile = new List<string>();

                //open a stream reader to read info from config file
                using(StreamReader reader = new StreamReader(configFilename))
                {
                    while (!reader.EndOfStream)
                    {
                        allLinesFromFile.Add(reader.ReadLineAsync().Result);
                    }//end getting all the lines from the file
                }//end use of StreamReader

                //get the filename from the first line
                string savedFileName = allLinesFromFile[0];

                //loop through rest of the lines to get whatever other data we want
                for(int i = 1; i < allLinesFromFile.Count; i++)
                {
                    //get all the components
                    string[] componentsFromThisLine = allLinesFromFile[i].Split(';');
                    if(componentsFromThisLine[0] == "LIE1")
                    {
                        string[] componentInfo = componentsFromThisLine[1].Split(':');
                        if(componentInfo[0] == nameof(levelInformation.PropertyToTest))
                        {
                            levelInformation.GetType().GetProperty(nameof(levelInformation.PropertyToTest)).SetValue(levelInformation, componentInfo[1]);
                        }//end if we can set the propertyToTest
                    }//end if we have a extended level options info here
                    else if(componentsFromThisLine[0] == "LI1")
                    {
                        levelInformation.AddNewLevel(
                            LevelInformation.Level.ReadSerializedString(allLinesFromFile[i]));
                    }//end if we have level information here
                }//end looping over all the lines of other configuration information

                //return information we just found
                return savedFileName;
            }//end if the configuration file exists
            else
            {
                //create config file
                File.Create(configFilename).Close();

                //start fileIO stuff
                using (StreamWriter scribe = new StreamWriter(configFilename))
                {
                    //initialize some objects
                    List<string> linesToWrite = CreateDefaultConfigFile(out levelInformation);
                    
                    //add all the lines to the files
                    foreach(string line in linesToWrite)
                    {
                        scribe.WriteLine(line);
                    }//end writing each default line to the config file
                }//end use of stream writer

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
        /// a list of the Cell objects generated. Might throw an error if
        /// the specified file does not exist
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

        /// <summary>
        /// Format all the data for one file into a one-line summary based on the 
        /// provided level information. It should be noted that counts ignore
        /// cells which are not both full and non-empty, as does seed total.
        /// Also adds the percent
        /// </summary>
        /// <param name="filename">the file you got the grid info from</param>
        /// <param name="grid">the grid of data</param>
        /// <param name="levels">the object which tells us what level a cell
        /// belongs to</param>
        /// <returns>returns a string with all the summary information formatted into a
        /// single line</returns>
        public static string FileLevelsProcessedToOneLine(string filename, Grid grid, LevelInformation levels)
        {
            //initialize stringbuilder for making output
            StringBuilder summaryBuilder = new StringBuilder();
            
            //add the fileID
            summaryBuilder.Append($"  {Path.GetFileName(filename)}  ");

            //add the timestamp
            summaryBuilder.Append(DateTime.Now.ToString("f"));

            //find sums for each level
            List<int> counters = new List<int>(levels.Count);
            foreach(LevelInformation.Level level in levels.Levels)
            {
                counters.Add(0);
            }//end initializing list of counters
            //count up all the levels
            int nonFlagCount = 0;
            foreach(Cell cell in grid.Cells)
            {
                //find out what level the cell is in
                Tuple<string, int> levelresult = levels.FindLevel((decimal)cell.GetType().GetProperty(levels.PropertyToTest).GetValue(cell));
                //increment the corresponding counter
                counters[levelresult.Item2]++;
                //increment nonFLagCount
                if(cell.IsFullCell && !cell.IsEmptyCell)
                {
                    nonFlagCount++;
                }//end if cell 
            }//end looping over cells in grid to add levels
            //add all the level counts to the string
            for(int i = 0; i < counters.Count; i++)
            {
                //find the percentage for this level
                decimal percent = (decimal)counters[i] / (decimal)nonFlagCount * 100;
                string percentStr = "";
                if(levels.Levels[i].LevelStart > 0) percentStr = $"({percent.ToString("N0")}%)";
                //add this to the output stringbuilder
                summaryBuilder.Append($"  {levels.Levels[i].LevelName}:{counters[i]}{percentStr}");
            }//end looping over counts of 

            //add total seed number
            summaryBuilder.Append($", TotalSeeds:{nonFlagCount}");

            return summaryBuilder.ToString();
        }//end FileLevelsProcessedToOneLine(grid, levels)
    }//end class
}//end namespace