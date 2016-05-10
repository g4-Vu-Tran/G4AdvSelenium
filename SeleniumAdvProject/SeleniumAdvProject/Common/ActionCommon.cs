using System;
using System.Threading;

namespace SeleniumAdvProject.Common
{
    public static class ActionCommon
    {
        public static string GenrateRandomString(int length)
        {
            return Guid.NewGuid().ToString().Substring(0, length);
        }

        public static string RandomDay(int day)
        {
            return DateTime.Now.AddDays(day).ToString("M/dd/yyy");
        }
    }
}
