using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

/*
 * Author: Nicholas Sixbury
 * File: FlagConfigurer.cs
 * Purpose: To provide more or less a single class to handle
 * the configuration of flags from files.
 */

namespace ImageJImporter
{
    /// <summary>
    /// A class which configures the flag properties of things.
    /// </summary>
    public class FlagConfigurer
    {
        /// <summary>
        /// The name of the flag.
        /// </summary>
        public string FlagName { get; set; }

        /// <summary>
        /// The value which this flag is testing for.
        /// </summary>
        public decimal FlagValue { get; set; }

        /// <summary>
        /// The raw string we got from the file.
        /// </summary>
        private string flagClassRaw = "";
        /// <summary>
        /// The class which will use this flag. If not set, will perform
        /// reflection every time to figure out class of tested object.
        /// </summary>
        public Type FlagClass { get; set; } = null;

        /// <summary>
        /// Backer variable for flag tolerance.
        /// </summary>
        private decimal flagTolerance = 0;
        /// <summary>
        /// The tolerance of this flag. Cannot be set to less than 0.
        /// </summary>
        public decimal FlagTolerance
        {
            get { return flagTolerance; }
            set { flagTolerance = value >= 0M ? value : 0M; }
        }//end FlagTolerance

        /// <summary>
        /// the raw string we got from the file.
        /// </summary>
        private string testedPropertyRaw = "";
        /// <summary>
        /// The property of FlagClass which will be tested
        /// in order to determine if an object is a flag of this
        /// type. Mus'nt be null, or flag will always be default value.
        /// </summary>
        public PropertyInfo TestedProperty { get; set; } = null;

        /// <summary>
        /// The default value of this flag if we can't find
        /// out how to calculate it normally. If not set, this is false.
        /// </summary>
        public bool DefaultFlagValue { get; set; } = false;

        /// <summary>
        /// The raw string we got from the file.
        /// </summary>
        private string backupPropertyRaw = "";
        /// <summary>
        /// The property which will be used if the usual one isn't found.
        /// If the file sets this to 'None', then this will be set to null,
        /// and the same will happen if this isn't set in the file at all.
        /// </summary>
        public PropertyInfo BackupProperty { get; set; } = null;

        /// <summary>
        /// Whether or not this flag should be displayed if the
        /// relevant property is not found anywhere.
        /// </summary>
        public bool IncludeIfColumnNotFound { get; set; } = false;

        /// <summary>
        /// Tests whether or not an object is a flag of this type.
        /// </summary>
        /// <param name="objectToTest">The object you wish to test
        /// for this flag.</param>
        /// <returns>Returns true if the object is of this flag;
        /// false otherwise. If class or property to test cannot
        /// be found, then will return default flag value.</returns>
        public bool IsFlag(object objectToTest)
        {
            if(FlagClass == null || TestedProperty == null)
            {
                return DefaultFlagValue;
            }//end if we couldn't find the property
            else
            {
                //figure out what value we're testing
                object testedValueAsObject = TestedProperty
                    .GetValue(objectToTest);

                //try to convert things
                try
                {
                    //convert the value
                    decimal testedValue = (decimal)testedValueAsObject;

                    //actually return correct answer
                    return (testedValue >= FlagValue - FlagTolerance
                        && testedValue <= FlagValue + FlagTolerance);
                }//try to cast things
                catch (InvalidCastException)
                {
                    return false;
                }//end catching errors if we can't cast things
            }//end else we know which property to test
        }//end IsFlag(memberClass, objectToTest)

        /// <summary>
        /// Initializes this class with defaults. Likely won't be able
        /// to do much of use without being set up further.
        /// </summary>
        public FlagConfigurer()
        {
            FlagName = "FlagNotConfigured";
            FlagValue = 0;
        }//end no-arg constructor

        /// <summary>
        /// Constructs this object which whatever data it can find in
        /// the serialized list of strings provided.
        /// </summary>
        /// <param name="lines">A serialized version of this object,
        /// formatted as a list of strings.</param>
        public FlagConfigurer(List<string> lines)
        {
            DeserializeObject(lines);
        }//end 1-arg constructor

        /// <summary>
        /// Serializes this object into a list of strings.
        /// </summary>
        public List<string> SerializeObject()
        {
            List<string> serialized = new List<string>();

            //add the name
            serialized.Add($"{nameof(FlagName)}:{FlagName}");

            //add the value
            serialized.Add($"{nameof(FlagValue)}:{FlagValue}");

            //add class
            serialized.Add($"{nameof(FlagClass)}:{flagClassRaw}");

            //add tolerance
            serialized.Add($"FlagUsageOptions:{nameof(FlagTolerance)}>{FlagTolerance}");

            //add tested column
            serialized.Add($"FlagUsageOptions:{nameof(TestedProperty)}>{testedPropertyRaw}");

            //add default value
            serialized.Add($"FlagUsageOptions:{nameof(DefaultFlagValue)}>{DefaultFlagValue}");

            //add backup property
            serialized.Add($"FlagUsageOptions:{nameof(BackupProperty)}>{backupPropertyRaw}");

            //add whether we want to use this if we can't find the right column
            serialized.Add($"FlagUsageOptions:{nameof(IncludeIfColumnNotFound)}>{IncludeIfColumnNotFound}");

            return serialized;
        }//end SerializeObject()

        /// <summary>
        /// Deserializes the lines provided as this object.
        /// </summary>
        /// <param name="lines">The serialized version of
        /// this object.</param>
        public void DeserializeObject(List<string> lines)
        {
            foreach(string line in lines)
            {
                char[] splittingchars = { ':', '>' };
                string[] lineComps = line.Split(splittingchars);

                //only deal with last two components
                int baselineIndex = lineComps.Length - 2;

                PropertyInfo thisProp = this
                        .GetType()
                        .GetProperty(lineComps[baselineIndex]);
                if (thisProp != null)
                {
                    thisProp.SetValue(this,
                        DynamicallyConvertType(thisProp.PropertyType,
                        lineComps[baselineIndex + 1])
                        );
                }//end if we actually found the property
            }//end looping for each line in the lines
        }//end DeserializeObject(lines)

        /// <summary>
        /// Converts a string of certain types into a number object
        /// for use with reflection stuff
        /// </summary>
        /// <param name="type">the type of the object you want</param>
        /// <param name="Object">the current object</param>
        /// <returns>the object, converted to right type, still
        /// as an object.</returns>
        private object DynamicallyConvertType(Type type, object Object)
        {
            if(type == typeof(int))
            {
                return Convert.ToInt32(Object);
            }//end if we want to convert to int
            else if(type == typeof(decimal) || type == typeof(double))
            {
                return Convert.ToDecimal(Object);
            }//end if we want to convert to decimal
            else if(type == typeof(bool))
            {
                return Convert.ToBoolean(Object);
            }//end else if we want to convert to bool
            else
            {
                return Object;
            }//end else we just won't do anything with it
        }//end DynamicallyConvertType(type, Object)
    }//end class
}//end namespace