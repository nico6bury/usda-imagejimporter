using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Nicholas Sixbury
 * File: GridListItemWrapper.cs
 * Purpose: To provide a dynamic wrapper around a Grid object for
 * easier use in an ObjectListView
 */

namespace ImageJImporter
{
    class GridListItemWrapper
    {
        private Grid grid;
        /// <summary>
        /// the grid stored in this object, used for calculation of various methods
        /// </summary>
        public Grid Grid
        {
            get { return grid; }
            set { grid = value; }
        }//end Grid

        /// <summary>
        /// the level information this class will use to calculate summaries
        /// </summary>
        public static LevelInformation levels { get; set; } = LevelInformation.DefaultLevels;

        /// <summary>
        /// the list of titles for each column for this grid
        /// </summary>
        public static List<string> OutputColumnsTitle
        {
            get
            {
                //initialize output list
                List<string> titles = new List<string>();

                //add filename title
                titles.Add("Filename");
                //add timestamp title
                titles.Add("Timestamp");
                //add level titles
                foreach (LevelInformation.Level level in levels.Levels)
                {
                    titles.Add(level.LevelName);
                }//end looping foreach level in levels
                //add total seed number
                titles.Add("Total");

                return titles;
            }//end getter
        }//end OutputColumnsTitle

        public static List<int> OutputColumnsWidth
        {
            get
            {
                //initialize output list
                List<int> widths = new List<int>();

                //add filename width
                widths.Add(170);
                //add timestamp width
                widths.Add(70);
                //add level widths
                foreach (LevelInformation.Level level in levels.Levels)
                {
                    widths.Add(level.LevelName.Length * 10);
                }//end looping foreach level in levels
                //add total seeds width
                widths.Add(50);

                return widths;
            }//end getter
        }//end OutputColumnsWidth

        public static List<bool> OutputColumnsVisibility
        {
            get
            {
                //initialize output list
                List<bool> visibilities = new List<bool>();

                //add filename visibility
                visibilities.Add(true);
                //add timestamp visisbility
                visibilities.Add(false);
                //add level visibilities
                foreach(LevelInformation.Level level in levels.Levels)
                {
                    //set to visible if levelstart is greater than 0
                    visibilities.Add(level.LevelStart > 0);
                }//end looping over each level
                //add total seeds visibility
                visibilities.Add(true);

                return visibilities;
            }//end getter
        }//end OutputColunsVisibility

        /// <summary>
        /// the list of text for each column for this grid
        /// </summary>
        public List<string> OutputColumnsText
        {
            get
            {
                //initialize list of outputs
                List<string> texts = new List<string>();

                //add filename text
                texts.Add(System.IO.Path.GetFileName(grid.Filename));
                //add timestamp text
                texts.Add(DateTime.Now.ToString("f"));
                //add the level information
                List<int> counters = new List<int>(levels.Count);
                foreach (LevelInformation.Level level in levels.Levels)
                {
                    counters.Add(0);
                }//end initializing list of counters
                 //count up all the levels
                int nonFlagCount = 0;
                foreach (Cell cell in grid.Cells)
                {
                    //find out what level the cell is in
                    Tuple<string, int> levelresult = levels.FindLevel((decimal)cell.GetType().GetProperty(levels.PropertyToTest).GetValue(cell));
                    //increment the corresponding counter
                    counters[levelresult.Item2]++;
                    //increment nonFLagCount
                    if (cell.IsFullCell && !cell.IsEmptyCell)
                    {
                        nonFlagCount++;
                    }//end if cell 
                }//end looping over cells in grid to add levels
                 //add all the level counts to the string
                for (int i = 0; i < counters.Count; i++)
                {
                    //find the percentage for this level
                    decimal percent = (decimal)counters[i] / (decimal)nonFlagCount * 100;
                    string percentStr = "";
                    if (levels.Levels[i].LevelStart > 0) percentStr = $"({percent.ToString("N0")}%)";
                    //add this level text
                    texts.Add($"{counters[i]}{percentStr}");
                }//end looping over counts of 

                //add total seed number
                texts.Add($"{nonFlagCount}");

                return texts;
            }//end getter
        }//end OutputColumnsText

        /// <summary>
        /// initializes this object with the specified grid object
        /// </summary>
        /// <param name="grid"></param>
        public GridListItemWrapper(Grid grid)
        {
            //initialize our grid
            this.grid = grid;
        }//end 1-arg constructor
    }//end class
}//end namespace