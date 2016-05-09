using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvProject.PageObjects;
using SeleniumAdvProject.Common;

namespace SeleniumAdvProject.TestCases
{
    [TestClass]
    public class MainPageTestCases : BaseTestCase
    {
        [TestMethod]
        public void TC011()
        {
            Console.WriteLine("DA_MP_TC011 - Verify that user is unable open more than 1 \"New Page\" dialog");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage();
            loginPage.Open();

            //2. Login with valid account
            loginPage.Login(Constant.Repository, Constant.UserName, Constant.Password);

            //3. Go to Global Setting -> Add page
            DashboardPage dashBoad = new DashboardPage();
            dashBoad.GoToNewPage();

            //4. Try to go to Global Setting -> Add page again
            dashBoad.OpenSetting();
            //VP. User cannot go to Global Setting -> Add page while "New Page" dialog appears
            bool actualResult = dashBoad.IsSettingExist();
            Assert.AreEqual(false, actualResult, "User cannot go to Global Setting while \"New Page\" dialog appears");
        }

        [TestMethod]
        public void TC012()
        {
            Console.WriteLine("DA_MP_TC012 - Verify that user is able to add additional pages besides \"Overview\" page successfully");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage();
            loginPage.Open();

            //2. Login with valid account
            loginPage.Login(Constant.Repository, Constant.UserName, Constant.Password);

            //3. Go to Global Setting -> Add page
            DashboardPage dashBoad = new DashboardPage();
            dashBoad.GoToNewPage();

           
        }

    }
}
