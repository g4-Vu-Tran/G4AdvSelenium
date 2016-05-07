using OpenQA.Selenium;

namespace SeleniumAdvProject.Common
{
    public class Constant
    {
        public static IWebDriver WebDriver;
        public const string LoginPageUrl = "http://192.168.0.105/TADashboard/login.jsp";
        public const string Repository = "SampleRepository";
        public const string UserName = "administrator";
        public const string Password = "";
        public const int WaitTimeoutShortSeconds = 60;
        public const int lenghtRandomString = 10;

    }

}
