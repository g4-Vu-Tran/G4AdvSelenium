using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvProject.PageObjects;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.DataObjects;

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
            dashBoad.GoToAddNewPage();

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
            dashBoad.GoToAddNewPage();

           
        }

        [TestMethod]
        public void TC015()
        {
            Console.WriteLine("DA_MP_TC015 - Verify that non \"Public\" pages can only be accessed and visible to their creators with condition that all parent pages above it are \"Public\"");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage();
            loginPage.Open();

            //2 Log in specific repository with valid account
            DashboardPage dashBoardPage = loginPage.Login(Constant.Repository, Constant.UserName, Constant.Password);

            //3. Go to Global Setting -> Add page
            //4 Enter Page Name field (Test)
            //5 Check Public checkbox
            //6 Click OK button
            Page page = new Page("Test", "Select parent", 2, "Overview", false);
            AddnEditPage addnEditPage = dashBoardPage.GoToAddNewPage();
            addnEditPage.addPage(page);

            //7 Go to Global Setting -> Add page
            //8 Enter Page Name field (Test Chilld)
            //9 Click on  Select Parent dropdown list
            //10 Select specific page (Test)
            //11 Click OK button
            Page page1 = new Page("Test Chilld", "Test", 2, "Overview", false);
            dashBoardPage.GoToAddNewPage();
            addnEditPage.addPage(page);
            //12 Click on Log out link
            //13 Log in with another valid account
            //VP Check children is invisibled
        }

    }
}
