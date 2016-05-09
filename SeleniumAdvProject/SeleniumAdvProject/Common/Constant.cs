using OpenQA.Selenium;
using System.Net;

namespace SeleniumAdvProject.Common
{
    public class Constant
    {
        public static IWebDriver WebDriver;
        public const string LoginPageUrl = "http://172.16.17.27:54000/TADashboard/login.jsp";
        public const string Repository = "SampleRepository";
        public const string TestRepository = "TestRepository";
        public const string UserName = "administrator";
        public const string Password = "";
        public const int WaitTimeoutShortSeconds = 60;
        public const int lenghtRandomString = 10;

    }

}
