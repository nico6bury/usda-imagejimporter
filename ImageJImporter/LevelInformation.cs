using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Nicholas Sixbury
 * File: LevelInformation.cs
 * Purpose: To hold information for different levels of things. I know that's
 * super vague, but I dunno, it's a class of convenience
 */

namespace ImageJImporter
{
    /// <summary>
    /// Contains information about a single level
    /// </summary>
    public class LevelInformation
    {
        /// <summary>
        /// the levels which are apart of this object
        /// </summary>
        public List<Level> Levels { get; set; }

        /// <summary>
        /// The number of Levels in this collection
        /// </summary>
        public int Count => Levels.Count;

        private string propertyToTest = "";
        /// <summary>
        /// the name of the property we usually test for with
        /// these levels. Just used for reference by users of this
        /// class, not referenced internally. Set to "" by default.
        /// It cannot be set to null
        /// </summary>
        public string PropertyToTest
        {
            get
            {
                if (propertyToTest == null) propertyToTest = "";
                return propertyToTest;
            }//end getter
            set
            {
                propertyToTest = value;
                if (propertyToTest == null) propertyToTest = "";
            }//end setter
        }//end PropertyToTest

        /// <summary>
        /// default level information
        /// </summary>
        public static LevelInformation DefaultLevels
        {
            get
            {
                //initialize level collection to return
                LevelInformation outputLevels = new LevelInformation();

                //set default property to test
                outputLevels.PropertyToTest = "Chalk";

                //define level0 (-10 - 0)
                Level level0 = new Level(-10, 0, "Lvl0", Color.Black, Color.White);
                outputLevels.AddNewLevel(level0);

                //define level1 (0 - 10)
                Level level1 = new Level(0, 10, "Lvl1", Color.White, Color.Black);
                outputLevels.AddNewLevel(level1);

                //define level2 (10 - 25)
                Level level2 = new Level(10, 25, "Lvl2", Color.PaleGreen, Color.DarkGreen);
                outputLevels.AddNewLevel(level2);

                //define level3 (25 - 45)
                Level level3 = new Level(25, 45, "Lvl3", Color.Gold, Color.Maroon);
                outputLevels.AddNewLevel(level3);

                //define level4 ( > 45)
                Level level4 = new Level(45, 100, "Lvl4", Color.DeepPink, Color.Purple);
                outputLevels.AddNewLevel(level4);

                //return that to whoever called this
                return outputLevels;
            }//end getter
        }//end DefaultLevels

        public Level this[int index]
        {
            get { return Levels[index]; }
            set { Levels[index] = value; }
        }//end indexer

        /// <summary>
        /// initializes this object
        /// </summary>
        public LevelInformation()
        {
            Levels = new List<Level>();
        }//end no-arg constructor

        /// <summary>
        /// Adds a new level to this object
        /// </summary>
        /// <param name="start">the start of the level</param>
        /// <param name="end">the end of the level</param>
        /// <param name="name">the name of the level</param>
        public void AddNewLevel(decimal start, decimal end, string name, Color BackColor, Color ForeColor)
        {
            Levels.Add(new Level(start, end, name, BackColor, ForeColor));
        }//end AddNewLevel(start, end)

        /// <summary>
        /// Adds a new level to this object
        /// </summary>
        /// <param name="level">the level struct to add</param>
        public void AddNewLevel(Level level)
        {
            Levels.Add(level);
        }//end AddNewLevel(level)

        /// <summary>
        /// Tests a specific value against the levels in this object, and then returns
        /// the name of the level and the index in the Levels list where the value matched.
        /// For the purposes of the method, the start of the level is exclusive and the 
        /// end of the level is inclusive. If the value is outside of all levels, then
        /// null and -1 will be returned.
        /// </summary>
        /// <param name="testValue">The value to test against the levels</param>
        /// <returns>Returns a typle with a string representing the name of the level
        /// as the first item and an integer representing the index in the Levels list
        /// where the value was found</returns>
        public Tuple<string, int> FindLevel(decimal testValue)
        {
            for (int i = 0; i < Levels.Count; i++)
            {
                if(testValue > Levels[i].LevelStart && testValue <= Levels[i].LevelEnd)
                {
                    return new Tuple<string, int>(Levels[i].LevelName, i);
                }//end if the value is within this level
            }//end loop over the levels list
            return new Tuple<string, int>(null, -1);
        }//end FindLevel(testValue)

        public List<string> MakeLinesToSaveToFile()
        {
            List<string> output = new List<string>();

            //add line for property we want to test
            output.Add($"LIE1;{nameof(PropertyToTest)}:{PropertyToTest}");

            //add all the lines for each level
            for(int i = 0; i < Levels.Count; i++)
            {
                output.Add(Levels[i].FormatSerializedString());
            }//end looping over all the levels

            return output;
        }//end MakeLinesToSaveToFile()

        public void SaveLinesToThisObject(List<string> lines)
        {
            List<Level> levels = new List<Level>();

            //read property we want to test from file
            string[] option1 = lines[0].Split(';');
            if(option1[0] == "LIE1")
            {
                string[] option1Info = option1[1].Split(':');
                GetType().GetProperty(option1Info[0]).SetValue(this, option1Info[1]);
            }//end if we can set option level info options

            //add all the levels from the lines
            for(int i = 1; i < lines.Count; i++)
            {
                levels.Add(Level.ReadSerializedString(lines[i]));
            }//end looping over each line

            this.Levels = levels;
        }//end SaveLinesToThisObject(lines)

        /// <summary>
        /// holds some information for a level
        /// </summary>
        public class Level
        {
            /// <summary>
            /// the beginning of the level
            /// </summary>
            public decimal LevelStart { get; set; }
            
            /// <summary>
            /// the end of the level
            /// </summary>
            public decimal LevelEnd { get; set; }
            
            /// <summary>
            /// the name of this level
            /// </summary>
            public string LevelName { get; set; }

            /// <summary>
            /// the optional forcolor accosiated with this control
            /// </summary>
            public Color ForeColor { get; set; }

            /// <summary>
            /// the optional backcolor associated with this control
            /// </summary>
            public Color BackColor { get; set; }

            /// <summary>
            /// Initializes the struct
            /// </summary>
            /// <param name="levelStart">the start of the level</param>
            /// <param name="levelEnd">the end of the level</param>
            /// <param name="levelName">the name of the level</param>
            public Level(decimal levelStart, decimal levelEnd, string levelName, Color BackColor, Color ForeColor)
            {
                this.LevelStart = levelStart;
                this.LevelEnd = levelEnd;
                this.LevelName = levelName;
                this.BackColor = BackColor;
                this.ForeColor = ForeColor;
            }//end Level(levelStart, levelEnd, levelName)

            public string FormatSerializedString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"LI1");
                foreach(PropertyInfo property in this.GetType().GetProperties())
                {
                    string serializedPrintout = property.GetValue(this).ToString();

                    if(property.PropertyType == typeof(Color))
                    {
                        Color someColor = (Color)property.GetValue(this);
                        serializedPrintout = someColor.ToArgb().ToString();
                    }//end if we have a color type

                    sb.Append($";{property.Name}:{serializedPrintout}");
                }//end looping over all the properties
                return sb.ToString();
            }//end FormatSerializedString()

            public static Level ReadSerializedString(string serialized)
            {
                Level output = new Level(-1, -1, String.Empty, Color.Black, Color.Black);
                //split up each of the properties
                string[] lineComponents = serialized.Split(';');
                //loop over all the properties we found
                for(int i = 1; i < lineComponents.Length; i++)
                {
                    //get the name (index 0) and the value (index 1) as strings
                    string[] nameAndValue = lineComponents[i].Split(':');
                    //convert the name into the corresponding property
                    PropertyInfo property = typeof(Level).GetProperty(nameAndValue[0]);
                    //grab the type from the propertyInfo
                    Type type = property.PropertyType;
                    //parse the value based on the type of the property
                    if(type == typeof(decimal))
                    {
                        //parse value to decimal and set corresponding value in output
                        property.SetValue(output, Convert.ToDecimal(nameAndValue[1]));
                    }//end if it's a decimal type
                    else if(type == typeof(string))
                    {
                        //parse value to string and set corresponding value in output
                        property.SetValue(output, nameAndValue[1]);
                    }//end if it's a string type
                    else if(type == typeof(Color))
                    {
                        //parse value to color and set corresponding value in output
                        property.SetValue(output, Color.FromArgb(Convert.ToInt32(nameAndValue[1])));
                    }//end if it's a color type
                }//end looping over all the components

                //return the level object we built
                return output;
            }//end ReadSerializedString(serialized)
        }//end inner class Level
    }//end class
}//end namespace