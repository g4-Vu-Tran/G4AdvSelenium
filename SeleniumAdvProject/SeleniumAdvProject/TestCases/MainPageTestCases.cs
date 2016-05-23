using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvProject.PageObjects;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.DataObjects;
using System.Threading;

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
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page  
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
            LoginPage loginPage = new LoginPage(_webDriver).Open();

            //2. Login with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4 Enter Page Name field
            //5 Click OK button
            //6 Check "Test" page is displayed besides "Overview" page
            Page page = new Page(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(page);
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
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

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
            mainPage.OpenAddNewPage().AddPage(page);
            mainPage.OpenAddNewPage().AddPage(page1);

            //VP Check "Another Test" page is positioned besides the "Test" page                       
            Assert.AreEqual(true, mainPage.IsPageDisplayAfter(page1.PageName, page.PageName), "The {0} page is not beside the {1} page", page1.PageName, page.PageName);

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
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3 Go to Global Setting -> Add page
            //4 Enter Page Name field
            //5 Check Public checkbox
            //6 Click OK button 
            //7 Click on Log out link   
            Page page = new Page(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, "Overview", true);
            mainPage.OpenAddNewPage().AddPage(page);
            loginPage = mainPage.Logout();

            //8 Log in with another valid account           
            mainPage = loginPage.Login(Constants.Repository, Constants.UserName1, Constants.SpecialPassword);

            //VP Check newly added page is visibled
            Assert.AreEqual(true, mainPage.IsLinkExist(page.PageName));

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
            Page parentPage = new Page("Test", "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(parentPage);

            //7 Go to Global Setting -> Add page
            //8 Enter Page Name field (Test Chilld)
            //9 Click on  Select Parent dropdown list
            //10 Select specific page (Test)
            //11 Click OK button
            Page childPage = new Page("TestChild", "Test", 2, "Select page", false);
            mainPage.OpenAddNewPage().AddPage(childPage);

            //12 Click on Log out link
            mainPage.Logout();

            //13 Log in with another valid account
            mainPage = loginPage.Login(Constants.Repository, "test", "admin");

            //VP Check children is invisibled
            Assert.IsTrue(!mainPage.IsLinkExist(childPage.PageName));

            //Post-Condition
            mainPage.DeletePage(parentPage.PageName + "/" + childPage.PageName);
            mainPage.DeletePage(parentPage.PageName);
            mainPage.Logout();
        }

        [TestMethod]
        public void DA_MP_TC016()
        {
            Console.WriteLine("DA_MP_TC016 - Verify that user is able to edit the \"Public\" setting of any page successfully");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2 Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4 Enter Page Name
            //5 Click OK button           
            Page page1 = new Page(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(page1);

            //6 Go to Global Setting -> Add page
            //7 Enter Page Name
            //8 Check Public checkbox
            //9 Click OK button
            Page page2 = new Page(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, "Overview", true);
            mainPage.OpenAddNewPage().AddPage(page2);

            //10 Click on "Test" page
            //11 Click on "Edit" link
            //12 Check "Edit Page" pop up window is displayed
            //13 Check Public checkbox
            //14 Click OK button
            //15 Click on "Another Test" page
            //16 Click on "Edit" link
            //17 Check "Edit Page" pop up window is displayed
            //18 Uncheck Public checkbox
            //19 Click OK button
            //20 Click Log out link
            page1.IsPublic = true;
            page2.IsPublic = false;
            mainPage.OpenEditPage(page1.PageName).EditPage(page1);
            mainPage.OpenEditPage(page2.PageName).EditPage(page2);
            loginPage = mainPage.Logout();

            //21 Log in with another valid account
            //VP Check "Test" Page is visible and can be accessed            
            mainPage = loginPage.Login(Constants.Repository, Constants.UserName1, Constants.SpecialPassword);
            Assert.AreEqual(true, mainPage.IsLinkExist(page1.PageName));

            //VP Check "Another Test" page is invisible
            Assert.AreEqual(true, mainPage.IsLinkExist(page2.PageName), string.Format("{0} page is visible", page2.PageName));

            mainPage.DeletePage(page1.PageName);
            loginPage = mainPage.Logout();

            mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);
            mainPage.DeletePage(page2.PageName);
            mainPage.Logout();
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
            AddNewPage addNewPage = new AddNewPage(_webDriver);
            mainPage = addNewPage.AddPage(parentPage);

            //4. Add a children page of newly added page (Test Child) 
            Page childPage = new Page("Test Child", "Test", 2, "Select page", false);
            mainPage = addNewPage.AddPage(childPage);

            //5. Click on parent page
            //6. Click "Delete" link
            mainPage.DeletePage(parentPage.PageName, "No");

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
            mainPage.DeletePage(parentPage.PageName + "/" + childPage.PageName, "No");

            //VP. Check confirm message "Are you sure you want to remove this page?" appears
            //11. Click OK button
            actualMessage = mainPage.GetDialogText();
            Assert.AreEqual("Are you sure you want to remove this page?", actualMessage);
            mainPage.ConfirmDialog("OK");

            //VP. Check children page is deleted
            mainPage.ClickMenuItem(parentPage.PageName, false);
            Assert.IsTrue(!mainPage.IsLinkExist(childPage.PageName));

            //12. Click on  parent page
            //13. Click "Delete" link
            mainPage.DeletePage(parentPage.PageName);

            //VP. Check confirm message "Are you sure you want to remove this page?" appears
            //14. Click OK button
            actualMessage = mainPage.GetDialogText();
            Assert.AreEqual("Are you sure you want to remove this page?", actualMessage);
            mainPage.ConfirmDialog("OK");

            //VP. Check parent page is deleted
            Assert.IsTrue(!mainPage.IsLinkExist(parentPage.PageName));

            //15. Click on "Overview" page
            //VP. Check "Delete" link disappears
            mainPage.ClickMenuItem("Overview");
            mainPage.LblGlobalSetting.MouseOver();
            Assert.IsTrue(!mainPage.IsLinkExist("Delete"));
        }

        [TestMethod]
        public void DA_MP_TC018()
        {
            Console.WriteLine("DA_MP_TC018 - Verify that user is able to add additional sibbling pages to the parent page successfully");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Log in specific repository with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4. Enter Page Name (Test)
            //5. Click OK button
            Page parentPage = new Page("Test", "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(parentPage);

            //6. Go to Global Setting -> Add page
            //7. Enter Page Name (Test Child 1)
            //8. Click on  Parent Page dropdown list
            //9. Select a parent page (Test)
            //10. Click OK button
            Page childPage1 = new Page("TestChild1", "Test", 2, "Select page", false);
            mainPage.OpenAddNewPage().AddPage(childPage1);

            //11. Go to Global Setting -> Add page
            //12. Enter Page Name (Test Child 2)
            //13. Click on  Parent Page dropdown list
            //14. Select a parent page (Test)
            //15. Click OK button
            Page childPage2 = new Page("TestChild2", "Test", 2, "Select page", false);
            mainPage.OpenAddNewPage().AddPage(childPage2);

            //VP. Check "Test Child 2" is added successfully
            Assert.IsTrue(mainPage.IsLinkExist(childPage2.PageName));

            //Post-Condition
            mainPage.DeletePage(parentPage.PageName + "/" + childPage1.PageName);
            mainPage.DeletePage(parentPage.PageName + "/" + childPage2.PageName);
            mainPage.DeletePage(parentPage.PageName);
            mainPage.Logout();
        }

        [TestMethod]
        public void DA_MP_TC019()
        {
            Console.WriteLine("DA_MP_TC019 - Verify that user is able to add additional sibbling page levels to the parent page successfully.");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog (Page name: Page 1, Parent page: Overview)
            //5. Click OK button
            Page page = new Page("Page1", "Overview", 2, "Select page", false);
            mainPage.OpenAddNewPage().AddPage(page);

            //VP. Observe the current page
            //    User is able to add additional sibbling page levels to parent page successfully. 
            //    In this case: Overview is parent page of page 1.
            Assert.IsTrue(!mainPage.IsLinkExist(page.PageName));
            mainPage.ClickMenuItem("Overview");
            Assert.IsTrue(mainPage.IsLinkExist(page.PageName));

            //Post-Condition
            mainPage.DeletePage("Overview/" + page.PageName);
        }

        [TestMethod]
        public void DA_MP_TC022()
        {
            Console.WriteLine("DA_MP_TC022 - Verify that user is unable to duplicate the name of sibbling page under the same parent page");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Add a new page
            //4. Add a sibling page of new page
            Page page = new Page("Test", "Select parent", 2, "Overview", false);
            Page pageSibling = new Page("TestChild", "Test", 2, "Select page", false);
            mainPage.OpenAddNewPage().AddPage(page);
            mainPage.OpenAddNewPage().AddPage(pageSibling);

            //5. Go to Global Setting -> Add page
            //6. Enter Page Name
            //7. Click on  Parent Page dropdown list
            //8. Select a parent page
            //9. Click OK button

            Page pageDuplicate = new Page("TestChild", "Test", 2, "Select page", false);
            mainPage.OpenAddNewPage().AddPage(pageDuplicate);

            //VP. Check warning message "Test child already exist. Please enter a diffrerent name" appears
            string actualMessage = mainPage.GetDialogText();
            Assert.AreEqual("TestChild already exists. Please enter a diffrerent name.", actualMessage, string.Format("Message incorrect {0}", actualMessage));

            //Close message and close add new page dialog
            mainPage.ConfirmDialog("OK");
            AddNewPage newPage = new AddNewPage();
            newPage.CancelPage();

            //Post-Condition
            mainPage.DeletePage(page.PageName + "/" + pageSibling.PageName);
            mainPage.DeletePage(page.PageName);
            mainPage.Logout();

        }

        [TestMethod]
        public void DA_MP_TC023()
        {
            Console.WriteLine("DA_MP_TC023 - Verify that user is able to edit the parent page of the sibbling page successfully");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog
            Page page = new Page("Page1", "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(page);

            //5. Go to Global Setting -> Add page
            //6. Enter info into all required fields on New Page dialog
            Page pageSibling = new Page("Page2", "Page1", 2, "Select page", false);
            mainPage.OpenAddNewPage().AddPage(pageSibling);

            //5. Go to the first created page
            //6. Click Edit link
            //7. Enter another name into Page Name field
            //8. Click Ok button on Edit Page dialog
            Page pageEdit = new Page("Page3", "Select parent", 2, "Overview", false);
            mainPage.OpenEditPage(page.PageName).EditPage(pageEdit);

            //VP: User is able to edit the parent page of the sibbling page successfully
            Assert.IsTrue(mainPage.IsLinkExist(pageEdit.PageName), "User can edit the parent page of sibling page");

            //Post-Condition
            mainPage.DeletePage(page.PageName + "/" + pageSibling.PageName);
            mainPage.DeletePage(page.PageName);
            mainPage.Logout();

        }

        [TestMethod]
        public void DA_MP_TC024()
        {
            Console.WriteLine("DA_MP_TC024 - Verify that \"Bread Crums\" navigation is correct");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog
            Page page = new Page("Page1", "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(page);

            string page1URL = mainPage.GetURL();

            //5. Go to Global Setting -> Add page
            //6. Enter info into all required fields on New Page dialog
            Page pageSibling = new Page("Page2", "Page1", 2, "Select page", false);
            mainPage.OpenAddNewPage().AddPage(pageSibling);

            string page2URL = mainPage.GetURL();

            //5. Click the first breadcrums (Page1)
            //VP: The first page is navigated
            mainPage.ClickMenuItem(page.PageName);
            string actualPage1 = mainPage.GetURL();
            Assert.AreEqual(page1URL, actualPage1, "The first page is navigated");

            //6. Click the second breadcrums
            //VP: The second page is navigated
            mainPage.ClickMenuItem(pageSibling.PageName);
            string actualPage2 = mainPage.GetURL();
            Assert.AreEqual(page2URL, actualPage2, "The second page is navigated");

            //Post-Condition
            mainPage.DeletePage(page.PageName + "/" + pageSibling.PageName);
            mainPage.DeletePage(page.PageName);
            mainPage.Logout();

        }

        [TestMethod]
        public void DA_MP_TC025()
        {
            Console.WriteLine("DA_MP_TC025 - Verify that page listing is correct when user edit \"Display After\" field of a specific page");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog
            Page page = new Page("Page1", "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(page);

            //5. Go to Global Setting -> Add page
            //6. Enter info into all required fields on New Page dialog
            Page page2 = new Page("Page2", "Select parent", 2, "Page1", false);
            mainPage.OpenAddNewPage().AddPage(page2);


            //5. Click Edit link for the second created page
            //6. Change value Display After for the second created page to after Overview page
            //7. Click Ok button on Edit Page dialog
            Page pageEdit = new Page("", "", 2, "Overview", false);
            mainPage.OpenEditPage(page2.PageName).EditPage(pageEdit);

            //VP: Position of the second page follow Overview page
            bool actualResult = mainPage.GetPositionPage(page2.PageName) < mainPage.GetPositionPage("Overview") ? true : false;
            Assert.AreEqual(true, actualResult);

            //Post-Condition
            mainPage.DeletePage(page.PageName);
            mainPage.DeletePage(page2.PageName);
            mainPage.Logout();

        }

        [TestMethod]
        public void DA_MP_TC026()
        {
            Console.WriteLine("DA_MP_TC026 - Verify that page column is correct when user edit \"Number of Columns\" field of a specific page");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with valid account
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Go to Global Setting -> Add page
            //4. Enter info into all required fields on New Page dialog
            //Page name: Page 1; Number of Columns: 2
            Page page = new Page("Page1", "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(page);

            //5. Go to Global Setting -> Edit link
            //6. Edit Number of Columns for the above created page (Number of Columns: 3)
            //7. Click OK button

            Page editPage = new Page("", "", 3, "", false);
            mainPage.OpenEditPage(page.PageName).EditPage(editPage);

            //VP. There are 3 columns on the above created page

            //Post-Condition
            mainPage.DeletePage(page.PageName);
            mainPage.Logout();

        }
    }
}
