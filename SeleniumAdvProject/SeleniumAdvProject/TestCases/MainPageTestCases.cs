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
        public void DA_MP_TC011()
        {
            Console.WriteLine("DA_MP_TC011 - Verify that user is unable open more than 1 \"New Page\" dialog");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with valid account
            loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            MainPage mainPage = new MainPage(_webDriver);
            mainPage.GoToAddNewPage();

            //4. Try to go to Global Setting -> Add page again
            mainPage.OpenSetting();
            //VP. User cannot go to Global Setting -> Add page while "New Page" dialog appears
            bool actualResult = mainPage.IsSettingExist();
            Assert.AreEqual(false, actualResult, "User cannot go to Global Setting while \"New Page\" dialog appears");

        }

        [TestMethod]
        public void DA_MP_TC012()
        {
            Console.WriteLine("DA_MP_TC012 - Verify that user is able to add additional pages besides \"Overview\" page successfully");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with valid account
            loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4 Enter Page Name field
            //5 Click OK button
            //6 Check "Test" page is displayed besides "Overview" page
            Page page = new Page(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, "Overview", false);
            AddNewPage newPage = new AddNewPage(_webDriver);
            MainPage mainPage = newPage.addPage(page);
            bool actualResult = mainPage.GetPositionPage(page.PageName) < mainPage.GetPositionPage("Overview") ? true : false;
            Assert.AreEqual(true, actualResult);

            mainPage.DeletePage(page.PageName);
            mainPage.Logout();
        }

        [TestMethod]
        public void DA_MP_TC013()
        {
            Console.WriteLine("DA_MP_TC013 - Verify that the newly added main parent page is positioned at the location specified as set with \"Displayed After\" field of \"New Page\" form on the main page bar/\"Parent Page\" dropped down menu");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Log in specific repository with valid account
            loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3 Go to Global Setting -> Add page            
            //4 Enter Page Name field
            //5 Click OK button
            //6 Go to Global Setting -> Add page
            //7 Enter Page Name field
            //8 Click on  Displayed After dropdown list
            //9 Select specific page
            //10 Click OK button
            Page page = new Page(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, "Overview", false);
            Page page1 = new Page(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, page.PageName, false);

            AddNewPage newPage = new AddNewPage(_webDriver);
            MainPage mainPage = newPage.addPage(page);
            mainPage = newPage.addPage(page1);

            //VP Check "Another Test" page is positioned besides the "Test" page
            bool actualResult = mainPage.GetPositionPage(page.PageName) < mainPage.GetPositionPage(page1.PageName) ? true : false;
            Assert.AreEqual(true, actualResult);

            //Post-Condition
            mainPage.DeletePage(page.PageName);
            mainPage.DeletePage(page1.PageName);
            mainPage.Logout();

        }

        [TestMethod]
        public void DA_MP_TC014()
        {
            Console.WriteLine("DA_MP_TC014 - Verify that \"Public\" pages can be visible and accessed by all users of working repository");

            //1 Navigate to Dashboard login page
            //2 Log in specific repository with valid account         

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Log in specific repository with valid account
            loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3 Go to Global Setting -> Add page
            //4 Enter Page Name field
            //5 Check Public checkbox
            //6 Click OK button            
            Page page = new Page(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, "Overview", true);
            AddNewPage newPage = new AddNewPage(_webDriver);
            MainPage mainPage = newPage.addPage(page);

            //7 Click on Log out link                       
            //8 Log in with another valid account
            loginPage = mainPage.Logout();
            mainPage = loginPage.Login(Constants.Repository, "accont2", "");

            //VP Check newly added page is visibled
            Assert.AreEqual(true, mainPage.IsPageVisible(page.PageName));

            mainPage.DeletePage(page.PageName);
            mainPage.Logout();

        }

        [TestMethod]
        public void DA_MP_TC015()
        {
            Console.WriteLine("DA_MP_TC015 - Verify that non \"Public\" pages can only be accessed and visible to their creators with condition that all parent pages above it are \"Public\"");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2 Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4 Enter Page Name field (Test)
            //5 Check Public checkbox
            //6 Click OK button
            Page page = new Page("Test", "Select parent", 2, "Overview", false);
            AddNewPage addNewPage = mainPage.GoToAddNewPage();
            addNewPage.addPage(page);

            //7 Go to Global Setting -> Add page
            //8 Enter Page Name field (Test Chilld)
            //9 Click on  Select Parent dropdown list
            //10 Select specific page (Test)
            //11 Click OK button
            Page page1 = new Page("Test Child", "Test", 2, "Select page", false);
            mainPage.GoToAddNewPage();
            addNewPage.addPage(page1);

            //12 Click on Log out link
            mainPage.Logout();

            //13 Log in with another valid account
            loginPage.Login(Constants.Repository, "test", "admin");

            //VP Check children is invisibled
            Assert.IsTrue(mainPage.IsPageVisible(page1.PageName));
        }

        [TestMethod]
        public void DA_MP_TC017()
        {
            Console.WriteLine("DA_MP_TC017 - Verify that user can remove any main parent page except \"Overview\" page successfully and the order of pages stays persistent as long as there is not children page under it");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2 Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Add a new parent page (Test)
            Page parentPage = new Page("Test", "Select parent", 2, "Overview", false);
            AddNewPage addNewPage = mainPage.GoToAddNewPage();
            addNewPage.addPage(parentPage);

            //4. Add a children page of newly added page (Test Child) 
            Page childPage = new Page("Test Child", "Test", 2, "Select page", false);
            mainPage.GoToAddNewPage();
            addNewPage.addPage(childPage);

            //5. Click on parent page
            //6. Click "Delete" link
            mainPage.DeletePage("Test");

            //VP. Check confirm message "Are you sure you want to remove this page?" appears
            //7. Click OK button
            string actualMessage = mainPage.GetDialogText();
            Assert.AreEqual("Are you sure you want to remove this page?", actualMessage);
            mainPage.ConfirmDialog("OK");

            //VP. Check warning message "Can not delete page 'Test' since it has children page(s)" appears
            //8. Click OK button
            actualMessage = mainPage.GetDialogText();
            Assert.AreEqual("Cannot delete page 'Test' since it has child page(s).\r\n", actualMessage);
            mainPage.ConfirmDialog("OK");

            //9. Click on  children page
            //10. Click "Delete" link
            mainPage.DeletePage("Test/Test Child");

            //VP. Check confirm message "Are you sure you want to remove this page?" appears
            //11. Click OK button
            actualMessage = mainPage.GetDialogText();
            Assert.AreEqual("Are you sure you want to remove this page?", actualMessage);
            mainPage.ConfirmDialog("OK");

            //VP. Check children page is deleted
            //12. Click on  parent page
            //13. Click "Delete" link
            mainPage.DeletePage("Test");

            //VP. Check confirm message "Are you sure you want to remove this page?" appears
            //14. Click OK button
            actualMessage = mainPage.GetDialogText();
            Assert.AreEqual("Are you sure you want to remove this page?", actualMessage);
            mainPage.ConfirmDialog("OK");

            //VP. Check parent page is deleted
            //15. Click on "Overview" page
            mainPage.ClickMenuItem("Overview");
            //VP. Check "Delete" link disappears
        }

        [TestMethod]
        public void DA_MP_TC018()
        {
            Console.WriteLine("DA_MP_TC017 - Verify that user is able to add additional sibbling pages to the parent page successfully");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2 Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Add a new parent page (Test)
            Page parentPage = new Page("Test", "Select parent", 2, "Overview", false);
            AddNewPage addNewPage = mainPage.GoToAddNewPage();
            addNewPage.addPage(parentPage);

            //4. Add a children page of newly added page (Test Child) 
            Page childPage1 = new Page("Test Child1", "Test", 2, "Select page", false);
            mainPage.GoToAddNewPage();
            addNewPage.addPage(childPage1);

            //Go to Global Setting -> Add page
            //Enter Page Name
            //Click on  Parent Page dropdown list
            //Select a parent page
            //Click OK button
            Page childPage2 = new Page("Test Child2", "Test", 2, "Select page", false);
            mainPage.GoToAddNewPage();
            addNewPage.addPage(childPage2);

            //VP. Check "Test Child 2" is added successfully

        }
    }
}
