using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace QEG_Classes
{
    /// <summary>
    /// 
    /// </summary>
    public static class Util
    {
        #region Fields



        #endregion

        #region Constructors



        #endregion

        #region Properties



        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Returns how many line return characters are in the provided string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int CountLines(this string str)
        {
            return str.Length - str.Replace("\n", "").Length + 1;
        }

        [SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.2")]
        public static List<int> AllIndexesOf(this string wholeString, string subString)
        {
            List<int> indexes = new List<int>();

            if (subString == string.Empty)
            {
                return indexes;
            }

            int prevStringPos = wholeString.Length;

            while (prevStringPos > -1)
            {
                prevStringPos = wholeString.LastIndexOf(subString, prevStringPos);
                indexes.Add(prevStringPos);
                prevStringPos--;
            }
            if (prevStringPos == -2)
            {
                indexes.RemoveAt(indexes.Count-1);
            }

            return indexes;
        }

        #endregion

        #region Protected Methods



        #endregion

        #region Private Methods



        #endregion

        #endregion
    }
}