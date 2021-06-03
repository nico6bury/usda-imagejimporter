using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Nicholas Sixbury
 * File: Row.cs
 * Purpose: To represent the information from a single row of output from
 * ImageJProcessing.
 */

namespace ImageJImporter
{
    /// <summary>
    /// this class represents a single row, and the data for it is read from a single line
    /// </summary>
    public class Row
    {
        /// <summary>
        /// 0.01M
        /// </summary>
        public const decimal DefaultFlagTolerance = 0.01M;
        /// <summary>
        /// 121M
        /// </summary>
        public const decimal DefaultNewRowFlagValue = 121M;
        /// <summary>
        /// 81.7M
        /// </summary>
        public const decimal DefaultSeedStartFlagValue = 81.7M;
        /// <summary>
        /// 95.3M
        /// </summary>
        public const decimal DefaultSeedEndFlagValue = 95.3M;

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
        /// the Cell that currently contains this Row object.
        /// When unset, it is equal to Cell.BlankCell
        /// </summary>
        public Cell CurrentCellOwner { get; set; } = Cell.BlankCell;

        private static decimal flagTolerance = DefaultFlagTolerance;
        /// <summary>
        /// The tolerance for the various row flags.
        /// Cannot be set to anything below zero, default is 0.01
        /// </summary>
        public static decimal FlagTolerance
        {
            get { return flagTolerance; }
            set
            {
                if (value <= 0) flagTolerance = 0;
                else flagTolerance = value;
            }//end setter
        }//end FlagTolerance

        /// <summary>
        /// The value which is checked against the Area of a row in order to find out
        /// if that row is a flag for a new row of cells on the grid. This is 121 by default.
        /// </summary>
        public static decimal NewRowFlagValue { get; protected set; } = DefaultNewRowFlagValue;
        /// <summary>
        /// whether or not this row is a flag for starting a new row in the
        /// grid which the seeds sit in when they're scanned
        /// </summary>
        public bool IsNewRowFlag
        {
            get
            {
                if (Area >= NewRowFlagValue - flagTolerance
                    && Area <= NewRowFlagValue + flagTolerance)
                    return true;
                else return false;
            }//end getter
        }//end IsNewRowFlag

        /// <summary>
        /// The value which is checked against the Area of a row in order to find out if that
        /// row is a flag for the beginning of cell information. This is 81.7 by default.
        /// </summary>
        public static decimal SeedStartFlagValue { get; protected set; } = DefaultSeedStartFlagValue;
        /// <summary>
        /// whether or not this row is a flag for starting a new seed
        /// </summary>
        public bool IsSeedStartFlag
        {
            get
            {
                if (Area >= SeedStartFlagValue - flagTolerance
                    && Area <= SeedStartFlagValue + flagTolerance)
                    return true;
                else return false;
            }//end getter
        }//end IsSeedStartFlag

        /// <summary>
        /// The value which is checked against the Area of a row in order to find out if that row
        /// is a flag for the end of a cell. This is 95.3 by default.
        /// </summary>
        public static decimal SeedEndFlagValue { get; protected set; } = DefaultSeedEndFlagValue;
        /// <summary>
        /// whether or not this row is a flag for ending data for one seed
        /// </summary>
        public bool IsSeedEndFlag
        {
            get
            {
                if (Area >= SeedEndFlagValue - flagTolerance
                    && Area <= SeedEndFlagValue + flagTolerance)
                    return true;
                else return false;
            }//end getter
        }//end IsSeedEndFlag

        /// <summary>
        /// Holds a class containing the current flag
        /// properties for the row class.
        /// </summary>
        public static RowFlagProps CurrentRowFlagProperties
        {
            get
            {
                //initialize object
                RowFlagProps returnObject = new RowFlagProps();
                
                //initialize the fields
                returnObject.FlagTolerance = Row.FlagTolerance;
                returnObject.NewRowFlagValue = Row.NewRowFlagValue;
                returnObject.SeedStartFlagValue = Row.SeedStartFlagValue;
                returnObject.SeedEndFlagValue = Row.SeedEndFlagValue;
                
                //return the object we initialized
                return returnObject;
            }//end getter
        }//end CurrentRowFlagProperties

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
        /// constructor for constructing row from a string of text. Format is same as
        /// imageJ output
        /// </summary>
        /// <param name="row">the row of text to read in</param>
        public Row(string row)
        {
            //create an array holding all the values in the row
            string[] values = row.Split(new char[] { '\t' });

            //sets a variable to hold row number
            this.RowNum = Convert.ToInt32(values[0]);

            //creates an array of decimals and puts converted values into it
            decimal[] editedValues = new decimal[values.Length - 1];
            for (int i = 1; i < values.Length; i++)
            {
                editedValues[i - 1] = Convert.ToDecimal(values[i]);
            }//end looping for every value but the first one

            //put all the variable data in
            this.Area = editedValues[0];
            this.X = editedValues[1];
            this.Y = editedValues[2];
            this.Perim = editedValues[3];
            this.Major = editedValues[4];
            this.Minor = editedValues[5];
            this.Angle = editedValues[6];
            this.Circ = editedValues[7];
            this.AR = editedValues[8];
            this.Round = editedValues[9];
            this.Solidity = editedValues[10];
        }//end 1-arg constructor

        /// <summary>
        /// copies all the information of this row.
        /// sets CurrentCellOwner to Cell.BlankCell
        /// </summary>
        /// <param name="row"></param>
        public Row(Row row)
        {
            this.RowNum = row.RowNum;
            this.Area = row.Area;
            this.X = row.X;
            this.Y = row.Y;
            this.Perim = row.Perim;
            this.Major = row.Major;
            this.Minor = row.Minor;
            this.Angle = row.Angle;
            this.Circ = row.Circ;
            this.AR = row.AR;
            this.Round = row.Round;
            this.Solidity = row.Solidity;
            this.CurrentCellOwner = Cell.BlankCell;
        }//end 1-arg copy constructor

        /// <summary>
        /// constructor to initialize variables. Allows you to input decimal properties
        /// as an array. Requires array to be formatted for variable values in this order:
        /// Area, X, Y, Perim, Major, Minor, Angle, Circ, AR, Round, Solidity
        /// </summary>
        /// <param name="rowNum">the number identifying this seed</param>
        /// <param name="properties">the array holding the values for all the decimal
        /// properties for the new Cell</param>
        public Row(int rowNum, decimal[] properties)
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
        /// as an list. Requires list to be formatted for variable values in this order:
        /// Area, X, Y, Perim, Major, Minor, Angle, Circ, AR, Round, Solidity
        /// </summary>
        /// <param name="rowNum">the number identifying this seed</param>
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
        /// <param name="rowNum">the number identifying this seed</param>
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
        public Row(int rowNum, decimal area, decimal x, decimal y, decimal perim,
            decimal major, decimal minor, decimal angle, decimal circ, decimal ar,
            decimal round, decimal solidity)
        {
            this.RowNum = rowNum;
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
        /// returns string representation of this row
        /// </summary>
        /// <returns>string representation of this row</returns>
        public override string ToString()
        {
            return FormatData(true);
        }//end ToString()

        /// <summary>
        /// returns whether the specified row is equal to this one.
        /// Equality in this case means all properties (except maybe
        /// RowNum) are equal
        /// </summary>
        /// <param name="row">The row you're testing</param>
        /// <returns>true if they're equal and false otherwise</returns>
        public bool Equals(Row row)
        {
            if (this.Area != row.Area) return false;
            if (this.X != row.X) return false;
            if (this.Y != row.Y) return false;
            if (this.Perim != row.Perim) return false;
            if (this.Major != row.Major) return false;
            if (this.Minor != row.Minor) return false;
            if (this.Angle != row.Angle) return false;
            if (this.Circ != row.Circ) return false;
            if (this.AR != row.AR) return false;
            if (this.Round != row.Round) return false;
            if (this.Solidity != row.Solidity) return false;
            return true;
        }//end Equals(row)

        /// <summary>
        /// returns a string formatted in the same way as the imageJ output files
        /// </summary>
        /// <returns>string formatted in same way as imageJ output files</returns>
        public string FormatData()
        {
            return FormatData(true);
        }//end FormatData()

        /// <summary>
        /// returns all the properties of this row, but
        /// with each one as an index in an array instead of
        /// in one string, separated by tabs
        /// </summary>
        public string[] GetRowPropertyArray()
        {
            string[] output = new string[12];
            output[0] = RowNum.ToString();
            output[1] = Area.ToString("N1");
            output[2] = X.ToString("N1");
            output[3] = Y.ToString("N1");
            output[4] = Perim.ToString("N1");
            output[5] = Major.ToString("N1");
            output[6] = Minor.ToString("N1");
            output[7] = Angle.ToString("N1");
            output[8] = Circ.ToString("N1");
            output[9] = AR.ToString("N1");
            output[10] = Round.ToString("N1");
            output[11] = Solidity.ToString("N1");
            return output;
        }//end GetRowPropertyArray()

        /// <summary>
        /// returns a string formatted in the same way as teh imageJ output files.
        /// Gives you the option to not include the seed number in the result
        /// </summary>
        /// <param name="includeRowNum">pass true if you want to include
        /// the seed number in the output, or false otherwise</param>
        /// <returns>string formatted in same way as imageJ output files</returns>
        public string FormatData(bool includeRowNum)
        {
            //initializes the string builder
            StringBuilder sb = new StringBuilder();

            //adds the seed num if we need to
            if (includeRowNum)
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

        /// <summary>
        /// Sets the static flag values and tolerance for those values for this class.
        /// </summary>
        /// <param name="flagTolerance">The tolerance level for the flags.</param>
        /// <param name="newRowFlagValue">The value of Area which indicates a row
        /// is a flag for a new row of cells in a grid.</param>
        /// <param name="seedStartFlagValue">The value of Area which indicates the
        /// beginning of seed data.</param>
        /// <param name="seedEndFlagValue">The value of Area which indicates the
        /// end of seed data.</param>
        public static void SetFlagsAndTolerances(decimal flagTolerance, decimal newRowFlagValue,
            decimal seedStartFlagValue, decimal seedEndFlagValue)
        {
            Row.FlagTolerance = flagTolerance;
            Row.NewRowFlagValue = newRowFlagValue;
            Row.SeedStartFlagValue = seedStartFlagValue;
            Row.SeedEndFlagValue = seedEndFlagValue;
        }//end SetFlagsAndTolerances(flagTolerance, newRowFlagValue, seedStartFlagValue, seedEndFlagValue)

        /// <summary>
        /// Sets the static flag values and tolerance for those values for this class
        /// </summary>
        /// <param name="rowFlagProps">An object holding all the flag values and
        /// tolerances which you wish to set.</param>
        public static void SetFlagsAndTolerances(RowFlagProps rowFlagProps)
        {
            Row.FlagTolerance = rowFlagProps.FlagTolerance;
            Row.NewRowFlagValue = rowFlagProps.NewRowFlagValue;
            Row.SeedStartFlagValue = rowFlagProps.SeedStartFlagValue;
            Row.SeedEndFlagValue = rowFlagProps.SeedEndFlagValue;
        }//end SetFlagsAndTolerances(rowFlagProps)

        /// <summary>
        /// A simple class for holding the properties concering row flags
        /// and their tolerance level.
        /// </summary>
        public class RowFlagProps
        {
            /// <summary>
            /// The tolerance that should be applied to each flag.
            /// </summary>
            public decimal FlagTolerance;
            /// <summary>
            /// The value of Area which indicates a row is a flag for
            /// a new row of cells in the grid.
            /// </summary>
            public decimal NewRowFlagValue;
            /// <summary>
            /// The value of Area which indicates a row is a flag for the beginning
            /// of seed data.
            /// </summary>
            public decimal SeedStartFlagValue;
            /// <summary>
            /// The value of Area which indicates a row is a flag for
            /// the end of seed data.
            /// </summary>
            public decimal SeedEndFlagValue;

            /// <summary>
            /// Initializes properties of this class with their default values
            /// </summary>
            public RowFlagProps()
            {
                this.FlagTolerance = Row.DefaultFlagTolerance;
                this.NewRowFlagValue = Row.DefaultNewRowFlagValue;
                this.SeedStartFlagValue = Row.DefaultSeedStartFlagValue;
                this.SeedEndFlagValue = Row.DefaultSeedEndFlagValue;
            }//end no-arg constructor

            /// <summary>
            /// The copy constructor for this class
            /// </summary>
            /// <param name="other">the RowFlagProps you wish to copy</param>
            public RowFlagProps(RowFlagProps other)
            {
                this.FlagTolerance = other.FlagTolerance;
                this.NewRowFlagValue = other.NewRowFlagValue;
                this.SeedStartFlagValue = other.SeedStartFlagValue;
                this.SeedEndFlagValue = other.SeedEndFlagValue;
            }//end 1-arg copy constructor

            /// <summary>
            /// A constructor for initializing this class with all of its data specified.
            /// </summary>
            /// <param name="flagTolerance">The tolerance that
            /// should be applied to each flag.</param>
            /// <param name="newRowFlagValue">The value of Area
            /// which indicates a row is a flag for a new row of
            /// cells in the grid.</param>
            /// <param name="seedStartFlagValue">The value of Area
            /// which indicates a row is a flag for the beginning
            /// of seed data.</param>
            /// <param name="seedEndFlagValue">The value of Area
            /// which indicaes a row is a flag for the end of
            /// seed data.</param>
            public RowFlagProps(decimal flagTolerance, decimal newRowFlagValue,
                decimal seedStartFlagValue, decimal seedEndFlagValue)
            {
                this.FlagTolerance = flagTolerance;
                this.NewRowFlagValue = newRowFlagValue;
                this.SeedStartFlagValue = seedStartFlagValue;
                this.SeedEndFlagValue = seedEndFlagValue;
            }//end 4-arg constructor for making a new class 
        }//end RowFlagProps
    }//end class
}//end namespace