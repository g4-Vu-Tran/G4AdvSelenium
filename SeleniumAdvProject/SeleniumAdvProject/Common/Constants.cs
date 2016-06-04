using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumAdvProject.Common
{
    public class Constants
    {
        public const string Repos = "SampleRepository";
        public const string TestRepos = "TestRepository";
        public const string UserName = "administrator";
        public const string Password = "";
        public const int WaitTimeoutShortSeconds = 5;
        public const int lenghtRandomString = 10;
        public const string UserName1 = "test1";
        public const string SpecialPassword = "!@#$%^&*()";
        public const string SpecialUserName = "@()";
        public const string UCPassword = "TEST";
        public const string UCUserName = "ADMIN";
        public const string LCUserUserName = "admin";

        //URL
        public const string LoginPageUrl = "http://localhost:54000/TADashboard/login.jsp";
        //public const string LoginPageUrl = "http://groupba.dyndns.org:54000/TADashboard/login.jsp";
        public const string MainPageUrl = "/TADashboard/";  
    }

    enum OpenAddPanelWay
    {
        ChoosePanel,
        GlobalSetting,
    }
}
