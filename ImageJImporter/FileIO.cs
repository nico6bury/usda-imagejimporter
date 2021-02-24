using System;
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
        /// this is the standard constructor for this class.
        /// It just initializes the variables in the normal way, nothing too special
        /// </summary>
        public FileIO()
        {
            file = "";
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

                    //create an array holding all the values in the row
                    string[] values = row.Split(new char[] { '\t' });

                    //sets a variable to hold seed number
                    int seedNum = Convert.ToInt32(values[0]);

                    //creates an array of decimals and puts converted values into it
                    decimal[] editedValues = new decimal[values.Length - 1];
                    for(int i = 1; i < values.Length; i++)
                    {
                        editedValues[i - 1] = Convert.ToDecimal(values[i]);
                    }//end looping for every value but the first one

                    //create a cell object and stuff all the data into it
                    Cell cell = new Cell(seedNum, editedValues);

                    //add the cell to our list of cells so we can return it later
                    data.Add(cell);
                }//end looping while not at end of stream
            }//end use of streamreader

            //return the list of cells to whatever called this method
            return data;
        }//end LoadFile()
    }//end class
}//end namespace