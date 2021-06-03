using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public string mostRecentAccessedFile = null;

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
            this.mostRecentAccessedFile = m.mostRecentAccessedFile;
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
            if (!String.IsNullOrEmpty(mostRecentAccessedFile))
                linesToOutput.Add($"FN2|{mostRecentAccessedFile}*");
            else linesToOutput.Add("NoFileSet");

            //add the level information options
            linesToOutput.Add($"LIE2|{nameof(levels.PropertyToTest)}*{levels.PropertyToTest}");

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
        /// will be saved. This object also includes the property to test</param>
        /// <param name="lastFiles">The filenames to save to the config file</param>
        public void SaveConfigFile(LevelInformation levelInformation, string[] lastFiles, Row.RowFlagProps flagProps)
        {
            //creates config file. If it already exists, we overwrite it
            using (StreamWriter scribe = new StreamWriter(configFileAbsolutePath))
            {
                //write default filename(s) to config file
                scribe.Write("FN2|");
                foreach(string filename in lastFiles)
                {
                    scribe.Write($"{filename}*");
                }//end looping for each file in lastFiles
                scribe.Write("\n");

                //write row flag information
                FieldInfo[] flagPropFields = flagProps.GetType().GetFields();
                foreach(FieldInfo field in flagPropFields)
                {
                    scribe.Write($"RF1|{field.Name}*{field.GetValue(flagProps)}\n");
                }//end looping over each field in flagProps

                //write all the levelInformation
                if(levelInformation != null)
                {
                    //string propName = levelInformation.PropertyToTest;
                    //if (String.IsNullOrEmpty(propName)) propName = "Chalk";
                    //scribe.WriteLine($"LIE2|{nameof(levelInformation.PropertyToTest)}*{propName}");
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
        /// <param name="filesToOpen">the files which according to the config file, should be opened</param>
        /// <param name="rowProps">an object holding information to set up row flags</param>
        /// <returns>returns whether a configuration file was successfully found</returns>
        public bool LoadConfigFile(out LevelInformation levelInformation,
            out string[] filesToOpen, out Row.RowFlagProps rowProps)
        {
            //initialize levelInformation out parameter
            levelInformation = new LevelInformation();
            filesToOpen = new string[0];
            rowProps = new Row.RowFlagProps();

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

                //loop through the lines to get whatever data we can find
                for(int i = 0; i < allLinesFromFile.Count; i++)
                {
                    //get all the components for this line
                    string[] componentsFromThisLine = allLinesFromFile[i].Split('|');

                    //start trying to figure out what to do with what we've got
                    if(componentsFromThisLine[0] == "LIE2" || componentsFromThisLine[0] == "LIE1")
                    {
                        string[] componentInfo = componentsFromThisLine[1].Split('*');
                        if(componentInfo[0] == nameof(levelInformation.PropertyToTest))
                        {
                            levelInformation.GetType().GetProperty(nameof(levelInformation.PropertyToTest)).SetValue(levelInformation, componentInfo[1]);
                        }//end if we can set the propertyToTest
                    }//end if we have a extended level options info here
                    else if(componentsFromThisLine[0] == "LI2" || componentsFromThisLine[0] == "LI1")
                    {
                        levelInformation.AddNewLevel(
                            LevelInformation.Level.ReadSerializedString(allLinesFromFile[i]));
                    }//end if we have level information here
                    else if(componentsFromThisLine[0] == "FN2")
                    {
                        string[] theLinesToOpen = componentsFromThisLine[1].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                        filesToOpen = theLinesToOpen;
                    }//end else if we have filename info to read
                    else if(componentsFromThisLine[0] == "RF1")
                    {
                        //this array should hold name and value of field
                        string[] componentInfo = componentsFromThisLine[1].Split('*');
                        FieldInfo rowFlagField = rowProps.GetType().GetField(componentInfo[0]);
                        if(rowFlagField != null)
                        {
                            rowFlagField.SetValue(rowProps, Convert.ToDecimal(componentInfo[1]));
                        }//end if we found a matching field for this line
                    }//end else if we have row flag information here
                    else
                    {
                        if (componentsFromThisLine[0] == "FN1")
                        {
                            string[] theLinesToOpen = componentsFromThisLine[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            filesToOpen = theLinesToOpen;
                        }//end else if we have filename info to read
                    }//end else we must be dealing with a weird outdated serialization format
                }//end looping over all the lines of configuration information

                //return that we found config info
                return true;
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

                //return that we didn't find config info, so we made our own
                return false;
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
            this.mostRecentAccessedFile = file;

            //return the list of rows to whatever called this method
            return data;
        }//end LoadFile()

        /// <summary>
        /// Saves the provided lines to the monthly log file
        /// </summary>
        /// <param name="lines">the lines from the log to save</param>
        public void SaveLinesToLog(string[] lines)
        {
            //get that directory location
            string newDirectory = GenerateLogDirectory();

            //figure out what our log file for this month is called
            StringBuilder sb = new StringBuilder();
            sb.Append($"{DateTime.Now.ToString("MMMM")}-Monthly-ImageJDataProcessing-LogFile");

            //save our new file location for the log to a variable
            string newFileLocation = newDirectory + sb.ToString() + ".txt";

            bool foundFile = File.Exists(newFileLocation);
            //make the file if it doesn't exist
            if (!foundFile)
            {
                File.Create(newFileLocation).Close();
            }//end if the file does not exist

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
        /// Generates a string for the directory for the log files
        /// </summary>
        /// <returns>returns that string</returns>
        public string GenerateLogDirectory()
        {
            //try and figure out the directory out config is in
            string configDirectory = configFileAbsolutePath != "" ? Path.GetDirectoryName(configFileAbsolutePath): Directory.GetCurrentDirectory();

            //try and do a new folder with the log file in it
            string newDirectory = $"{configDirectory}{Path.DirectorySeparatorChar}{logFileFolderName}{Path.DirectorySeparatorChar}";

            //ensure that our folder exists
            Directory.CreateDirectory(newDirectory);

            //return that directory name
            return newDirectory;
        }//end GenerateLogDirectory()

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

            ////add the timestamp
            //summaryBuilder.Append(DateTime.Now.ToString("f"));

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
            summaryBuilder.Append($", TotalSeeds:{nonFlagCount}\n");

            return summaryBuilder.ToString();
        }//end FileLevelsProcessedToOneLine(grid, levels)
    }//end class
}//end namespace