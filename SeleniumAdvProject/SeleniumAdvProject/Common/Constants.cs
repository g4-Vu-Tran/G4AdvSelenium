using OpenQA.Selenium;
using System.Net;

namespace SeleniumAdvProject.Common
{
    public class Constants
    {
        //public static IWebDriver WebDriver;
        public const string Repository = "SampleRepository";
        public const string TestRepository = "TestRepository";
        public const string UserName = "administrator";
        public const string Password = "";
        public const int WaitTimeoutShortSeconds = 5;
        public const int lenghtRandomString = 10;
        public const string UserName1 = "test1";
        public const string SpecialPassword = "!@#$%^&*()";

        //URL
        public const string LoginPageUrl = "http://groupba.dyndns.org:54000/TADashboard/login.jsp";
        public const string MainPageUrl = "/TADashboard/2f9njff6y9.page";
                
        public static string GetXpath(By by)
        {
            string[] words = by.ToString().Split(':');

            string xtype;
            switch (words[0])
            {
                case "By.Name":
                    xtype = "@name";
                    break;

                case "By.Id":
                    xtype = "@id";
                    break;

                case "By.LinkText":
                    xtype = "text()";
                    break;

                case "By.XPath":
                    xtype = "xpath";
                    break;

                case "By.CssSelector":
                    return null;

                default:
                    xtype = "xpath";
                    break;
            }

            return xtype == "xpath" ? words[1].Trim() : string.Format("//[{0}='{1}']", xtype, words[1]);
        }
    }

}
