using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Nicholas Sixbury
 * File: LogManager.cs
 * Purpose: To provide a unified class of functions for managing
 * log files.
 */

namespace ImageJImporter
{
    /// <summary>
    /// A class built for managing log file stuff
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// internal storage for lines from log
        /// </summary>
        protected List<string> lines1 = new List<string>();
        /// <summary>
        /// the lines which are currently stored in this object
        /// </summary>
        public virtual string[] Lines1
        {
            get
            {
                return lines1.ToArray();
            }//end getter
        }//end Lines1

        string Filename { get; set; }

        /// <summary>
        /// The directory which this object is currently outputting log files to
        /// </summary>
        public virtual string LogDirectory { get; protected set; }

        /// <summary>
        /// Constructs this object given specified output directory
        /// </summary>
        /// <param name="logDirectory">The directory in which log files should
        /// be exported to</param>
        /// <param name="logFilename">The name of the file which this log will
        /// be written to.</param>
        public LogManager(string logDirectory, string logFilename)
        {
            this.LogDirectory = logDirectory;
            this.Filename = logFilename;
        }//end 1-arg constructor

        /// <summary>
        /// Appends a single line of characters to the internal listing
        /// </summary>
        /// <param name="line">the line to add to the log</param>
        public virtual void AppendLog(string line)
        {
            this.lines1.Add(line);
        }//end AppendLog(line)

        /// <summary>
        /// Appends multiple lines of characters to the internal listing
        /// </summary>
        /// <param name="lines">the lines to add to the log</param>
        public virtual void AppendLog(IEnumerable<string> lines)
        {
            foreach (string line in lines)
                this.lines1.Add(line);
        }//end AppendLog(lines)

        /// <summary>
        /// Writes the currently stored lines to the regular log files,
        /// at the specified directory location.
        /// </summary>
        /// <returns>returns true if write was successful, false if any
        /// errors were encountered</returns>
        public virtual bool WriteToLog()
        {
            try
            {
                //write to the first one
                string firstFile = LogDirectory + Path.DirectorySeparatorChar + Filename;
                if (!File.Exists(firstFile)) File.Create(firstFile).Close();

                using(StreamWriter scribe = File.AppendText(firstFile))
                {
                    foreach(string line in lines1)
                    {
                        scribe.WriteLine(line);
                    }//end writing each line to the file
                }//end use of scribe

                return true;
            }//end trying to write stuff to the log files
            catch(IOException)
            {
                return false;
            }//end catching any exceptions that get thrown
        }//end WriteToLog()
    }//end class LogManager
}//end namespace