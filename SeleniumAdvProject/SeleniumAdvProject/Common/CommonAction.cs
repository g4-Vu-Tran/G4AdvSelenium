using System;
using System.Threading;

namespace SeleniumAdvProject.Common
{
    public static class CommonAction
    {
        /// <summary>
        /// Genrates the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GenrateRandomString(int length)
        {
            return Guid.NewGuid().ToString().Substring(0, length);
        }

        /// <summary>
        /// Randoms the day.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <returns></returns>
        public static string RandomDay(int day)
        {
            return DateTime.Now.AddDays(day).ToString("M/dd/yyy");
        }

        public static string EncodeSpace(string original)
        {
            return original.Replace(' ', '\u00a0');
        }
    }
}
