using OpenQA.Selenium;
using System.Net;

namespace SeleniumAdvProject.Common
{
    public class Constants
    {
        public static IWebDriver WebDriver;
        public const string LoginPageUrl = "http://192.168.0.101:80/TADashboard/login.jsp";
        public const string Repository = "SampleRepository";
        public const string TestRepository = "TestRepository";
        public const string UserName = "administrator";
        public const string Password = "";
        public const int WaitTimeoutShortSeconds = 60;
        public const int lenghtRandomString = 10;

        //http://groupba.dyndns.org:54000/TADashboard/login.jsp
        //repo1: SampleRepository
        //repo2: TestRepository
        // user: test1, pass: !@#$%^&*()
        //user: test, pass: admin
        //user, test@logigear.com, pass rong


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
