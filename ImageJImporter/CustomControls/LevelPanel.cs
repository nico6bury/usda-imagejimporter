using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Author: Nicholas Sixbury
 * File: LevelPanel.cs
 * Purpose: To provide a panel that can also contain a
 * Level object.
 */

namespace ImageJImporter
{
    class LevelPanel : Panel
    {
        public LevelInformation.Level Level { get; set; }
        public int LevelInformationIndex {get;set; }

        public LevelPanel(LevelInformation.Level level, int index) : base()
        {
            this.Level = level;
            this.LevelInformationIndex = index;
        }//end 1-arg constructor
    }//end class
}//end namespace