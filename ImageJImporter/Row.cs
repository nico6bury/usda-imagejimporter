using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageJImporter
{
    /// <summary>
    /// this class represents a single seed, and the data for it is read from a single line
    /// </summary>
    public class Row
    {
        public int RowNum;
        public decimal Area;
        public decimal X;
        public decimal Y;
        public decimal Perim;
        public decimal Major;
        public decimal Minor;
        public decimal Angle;
        public decimal Circ;
        public decimal AR;
        public decimal Round;
        public decimal Solidity;

        /// <summary>
        /// normal constructor which initializes all fields as 0
        /// </summary>
        public Row()
        {
            RowNum = 0;
            Area = 0;
            X = 0;
            Y = 0;
            Perim = 0;
            Major = 0;
            Minor = 0;
            Angle = 0;
            Circ = 0;
            AR = 0;
            Round = 0;
            Solidity = 0;
        }//end no-arg constructor

        /// <summary>
        /// constructor to initialize variables. Allows you to input decimal properties
        /// as an array. Requires array to be formatted for variable values in this order:
        /// Area, X, Y, Perim, Major, Minor, Angle, Circ, AR, Round, Solidity
        /// </summary>
        /// <param name="seedNum">the number identifying this seed</param>
        /// <param name="properties">the array holding the values for all the decimal
        /// properties for the new Cell</param>
        public Row(int seedNum, decimal[] properties)
        {
            this.RowNum = seedNum;
            this.Area = properties[0];
            this.X = properties[1];
            this.Y = properties[2];
            this.Perim = properties[3];
            this.Major = properties[4];
            this.Minor = properties[5];
            this.Angle = properties[6];
            this.Circ = properties[7];
            this.AR = properties[8];
            this.Round = properties[9];
            this.Solidity = properties[10];
        }//end 2-arg constructor

        /// <summary>
        /// constructor to initialize variables. Allows you to input decimal properties
        /// as an list. Requires list to be formatted for variable values in this order:
        /// Area, X, Y, Perim, Major, Minor, Angle, Circ, AR, Round, Solidity
        /// </summary>
        /// <param name="seedNum">the number identifying this seed</param>
        /// <param name="properties">the list holding the values for all the decimal
        /// properties for the new Cell</param>
        public Row(int rowNum, List<decimal> properties)
        {
            this.RowNum = rowNum;
            this.Area = properties[0];
            this.X = properties[1];
            this.Y = properties[2];
            this.Perim = properties[3];
            this.Major = properties[4];
            this.Minor = properties[5];
            this.Angle = properties[6];
            this.Circ = properties[7];
            this.AR = properties[8];
            this.Round = properties[9];
            this.Solidity = properties[10];
        }//end 2-arg constructor

        /// <summary>
        /// constructor to initialize variables. Allows you to input decimal properties
        /// individually.
        /// </summary>
        /// <param name="seedNum">the number identifying this seed</param>
        /// <param name="area"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="perim"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="angle"></param>
        /// <param name="circ"></param>
        /// <param name="ar"></param>
        /// <param name="round"></param>
        /// <param name="solidity"></param>
        public Row(int seedNum, decimal area, decimal x, decimal y, decimal perim,
            decimal major, decimal minor, decimal angle, decimal circ, decimal ar,
            decimal round, decimal solidity)
        {
            this.RowNum = seedNum;
            this.Area = area;
            this.X = x;
            this.Y = y;
            this.Perim = perim;
            this.Major = major;
            this.Minor = minor;
            this.Angle = angle;
            this.Circ = circ;
            this.AR = ar;
            this.Round = round;
            this.Solidity = solidity;
        }//end 12-arg constructor

        /// <summary>
        /// returns the number of this seed as a string
        /// </summary>
        /// <returns>string representation of the number of this seed</returns>
        public override string ToString()
        {
            return FormatData();
            //return $"Row {RowNum}";
        }//end ToString()

        /// <summary>
        /// returns a string formatted in the same way as the imageJ output files
        /// </summary>
        /// <returns>string formatted in same way as imageJ output files</returns>
        public string FormatData()
        {
            return FormatData(true);
        }//end FormatData()

        /// <summary>
        /// returns a string formatted in the same way as teh imageJ output files.
        /// Gives you the option to not include the seed number in the result
        /// </summary>
        /// <param name="includeSeedNum">pass true if you want to include
        /// the seed number in the output, or false otherwise</param>
        /// <returns>string formatted in same way as imageJ output files</returns>
        public string FormatData(bool includeSeedNum)
        {
            //initializes the string builder
            StringBuilder sb = new StringBuilder();

            //adds the seed num if we need to
            if (includeSeedNum)
            {
                sb.Append(RowNum);
                sb.Append("\t");
            }//end if we need to include the seed number

            //adds tab plus area value to one decimal place
            sb.Append(Area.ToString("N1"));

            //adds tab plus x value to one decimal place
            sb.Append("\t");
            sb.Append(X.ToString("N1"));

            //adds tab plus y value to one decimal place
            sb.Append("\t");
            sb.Append(Y.ToString("N1"));

            //adds tab plus perim value to one decimal place
            sb.Append("\t");
            sb.Append(Perim.ToString("N1"));

            //adds tab plus major value to one decimal place
            sb.Append("\t");
            sb.Append(Major.ToString("N1"));

            //adds tab plus minor value to one decimal place
            sb.Append("\t");
            sb.Append(Minor.ToString("N1"));

            //adds tab plus angle value to one decimal place
            sb.Append("\t");
            sb.Append(Angle.ToString("N1"));

            //adds tab plus circ value to one decimal place
            sb.Append("\t");
            sb.Append(Circ.ToString("N1"));

            //adds tab plus ar value to one decimal place
            sb.Append("\t");
            sb.Append(AR.ToString("N1"));

            //adds tab plus round value to one decimal place
            sb.Append("\t");
            sb.Append(Round.ToString("N1"));

            //adds tab plus solidity value to one decimal place
            sb.Append("\t");
            sb.Append(Solidity.ToString("N1"));

            return sb.ToString();
        }//end FormatData()
    }//end class
}//end namespace