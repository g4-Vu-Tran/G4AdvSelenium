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
        //public const string LoginPageUrl = "192.168.0.105/TADashboard/login.jsp";
        public const string LoginPageUrl = "http://groupba.dyndns.org:54000/TADashboard/login.jsp";
        public const string MainPageUrl = "/TADashboard/2f9njff6y9.page";                
       
    }

}
