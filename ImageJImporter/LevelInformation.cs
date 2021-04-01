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
        public List<Level> Levels { get; private set; }

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
        public void AddNewLevel(decimal start, decimal end, string name)
        {
            Levels.Add(new Level(start, end, name));
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
        /// default level information
        /// </summary>
        public static LevelInformation DefaultLevels
        {
            get
            {
                //initialize level collection to return
                LevelInformation outputLevels = new LevelInformation();

                //define level0 (-10 - 0)
                Level level0 = new Level(-10, 0, "Lvl0");
                level0.BackColor = Color.Black;
                level0.ForeColor = Color.White;
                outputLevels.AddNewLevel(level0);

                //define level1 (0 - 10)
                Level level1 = new Level(0, 10, "Lvl1");
                level1.BackColor = Color.White;
                level1.ForeColor = Color.Black;
                outputLevels.AddNewLevel(level1);

                //define level2 (10 - 25)
                Level level2 = new Level(10, 25, "Lvl2");
                level2.BackColor = Color.PaleGreen;
                level2.ForeColor = Color.DarkGreen;
                outputLevels.AddNewLevel(level2);

                //define level3 (25 - 45)
                Level level3 = new Level(25, 45, "Lvl3");
                level3.BackColor = Color.Gold;
                level3.ForeColor = Color.Maroon;
                outputLevels.AddNewLevel(level3);

                //define level4 ( > 45)
                Level level4 = new Level(45, 100, "Lvl4");
                level4.BackColor = Color.DeepPink;
                level4.ForeColor = Color.Purple;
                outputLevels.AddNewLevel(level4);

                //return that to whoever called this
                return outputLevels;
            }//end getter
        }//end DefaultLevels

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

            for(int i = 0; i < Levels.Count; i++)
            {
                output.Add(Levels[i].FormatSerializedString());
            }//end looping over all the levels

            return output;
        }//end MakeLinesToSaveToFile()

        public void SaveLinesToThisObject(List<string> lines)
        {
            List<Level> levels = new List<Level>();

            for(int i = 0; i < lines.Count; i++)
            {
                levels.Add(Level.ReadSerializedString(lines[i]));
            }//end looping over each line

            this.Levels = levels;
        }//end SaveLinesToThisObject(lines)

        /// <summary>
        /// holds some information for a level
        /// </summary>
        public struct Level
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
            public Level(decimal levelStart, decimal levelEnd, string levelName)
            {
                this.LevelStart = levelStart;
                this.LevelEnd = levelEnd;
                this.LevelName = levelName;
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
            }//end Level(levelStart, levelEnd, levelName)

            public string FormatSerializedString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"LI");
                foreach(PropertyInfo property in this.GetType().GetProperties())
                {
                    sb.Append($";{property.Name}:{property.GetValue(this)}");
                }//end looping over all the properties
                return sb.ToString();
            }//end FormatSerializedString()

            public static Level ReadSerializedString(string serialized)
            {
                Level output = new Level(-1, -1, String.Empty);
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
                        property.SetValue(output, Color.FromName(nameAndValue[1]));
                    }//end if it's a color type
                }//end looping over all the components

                //return the level object we built
                return output;
            }//end ReadSerializedString(serialized)
        }//end Level
    }//end class
}//end namespace