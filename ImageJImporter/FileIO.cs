﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageJImporter
{
    /// <summary>
    /// this class handles all of the actual heavy lifting by importing files and
    /// saving them and all that
    /// </summary>
    public class FileIO
    {
        /// <summary>
        /// this is the name of the file that we most recently used
        /// </summary>
        public string file { get; private set; }

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
        /// <param name="wordWrap">whether or not text should wrap to the next line</param>
        public void SaveConfigFile(bool wordWrap)
        {
            //creates config file. If it already exists, we overwrite it
            using (StreamWriter scribe = new StreamWriter(configFileAbsolutePath))
            {
                //write default filename to config file
                scribe.WriteLine(file);

                scribe.WriteLine(wordWrap);
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

        public List<Cell> LoadFile(string file)
        {
            //initializes the list we'll store the data in
            List<Cell> data = new List<Cell>();

            //start reading the file
            using(StreamReader reader = new StreamReader(file))
            {
                //this just pushes the header line off on the stream
                reader.ReadLine();

                //start looping until we reach the end of the file
                while (!reader.EndOfStream)
                {
                    //reads in the current row
                    string row = reader.ReadLine();

                    //read information from the row
                    Cell cell = ParseCell(row);

                    //add the cell to our list of cells so we can return it later
                    data.Add(cell);
                }//end looping while not at end of stream
            }//end use of streamreader

            //sets last used text file
            this.file = file;

            //return the list of cells to whatever called this method
            return data;
        }//end LoadFile()

        /// <summary>
        /// Parses a row as a string to recieve cell data
        /// </summary>
        /// <param name="row">the row of characters with values for cell properties separated
        /// by tabs</param>
        /// <returns>returns a cell generated by the row</returns>
        public Cell ParseCell(string row)
        {
            //create an array holding all the values in the row
            string[] values = row.Split(new char[] { '\t' });

            //sets a variable to hold seed number
            int seedNum = Convert.ToInt32(values[0]);

            //creates an array of decimals and puts converted values into it
            decimal[] editedValues = new decimal[values.Length - 1];
            for (int i = 1; i < values.Length; i++)
            {
                editedValues[i - 1] = Convert.ToDecimal(values[i]);
            }//end looping for every value but the first one

            //create a cell object and stuff all the data into it
            Cell cell = new Cell(seedNum, editedValues);

            //return the cell we just created
            return cell;
        }//end ParseCell(row)
    }//end class
}//end namespace