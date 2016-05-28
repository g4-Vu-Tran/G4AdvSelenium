using System;
using System.Threading;

namespace SeleniumAdvProject.Common
{
    public static class CommonAction
    {

        /// <summary>
        /// Genrates the random string
        /// </summary>
        /// <param name="length">The length of string</param>
        /// <returns>String</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        public static string GenrateRandomString(int length = Constants.lenghtRandomString)
        {
            return Guid.NewGuid().ToString().Substring(0, length);
        }

        /// <summary>
        /// Randoms the string 
        /// </summary>
        /// <param name="day">The day.</param>
        /// <returns>String</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        public static string RandomDay(int day)
        {
            return DateTime.Now.AddDays(day).ToString("M/dd/yyy");
        }

        /// <summary>
        /// Encodes the space to \u00a0
        /// </summary>
        /// <param name="original">The original string</param>
        /// <returns>String</returns>
        /// <author>Vu Tran</author>
        /// <date>05/26/2015</date>
        public static string EncodeSpace(string original)
        {
            return original.Replace(' ', '\u00a0');
        }
    }
}
