using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvProject.PageObjects;
using SeleniumAdvProject.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.Text.RegularExpressions;
using SeleniumAdvProject.DataObjects;

namespace SeleniumAdvProject.TestCases
{
    [TestClass]
    public class PanelTestCase : BaseTestCase
    {

        /// <summary>
        /// Verify that when "Choose panels" form is expanded all pre-set panels are populated and sorted correctly
        /// </summary>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        //[TestMethod]
        public void DA_PANEL_TC027()
        {
            Console.WriteLine("DA_PANEL_TC027 - Verify that when \"Choose panels\" form is expanded all pre-set panels are populated and sorted correctly");

            //1 Navigate to Dashboard login page
            //2 Login with valid account
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);
            //3 Go to Global Setting -> Add page
            //4 Enter page name to Page Name field.
            //5 Click OK button
            Page page1 = new Page(CommonAction.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(page1);

            //6 Go to Global Setting -> Create Panel     
            //7 Enter display name into Display Name textbox
            //8 Select any value in Series dropdown list
            //9 Click OK button on PanelConfiguration popup and then click on Chose Panel menu icon
           
            Chart chart =new Chart(CommonAction.GenrateRandomString(Constants.lenghtRandomString), "Name" ,page1.PageName);

            //Chart chart = new Chart("Action Implementation By Status", ActionCommon.GenrateRandomString(Constants.lenghtRandomString), page1.PageName, 0, null, null, "Pie", null, null, "Name", null, "None", dataLabel, "2D", false);
            mainPage.AddNewPanel(chart);

            ////VP Verify that all pre-set panels are populated and sorted correctly
            //bool actualResult = panelPage.IsTheListIsSorted(panelPage.CbbDataProfile, "ASC");
            //Assert.AreEqual(true, actualResult, "Data Profile is not sorted correctly");

            //panelPage.BtnCancel.Click();

            ////Post-Condition
            ////Logout			
            ////Close Dashboard            
            //mainPage.DeletePage(page1.PageName, "Yes");
            //mainPage.Logout();
        }

        /// <summary>
        /// Verify that when "Add New Panel" form is on focused all other control/form is disabled or locked
        /// </summary>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        [TestMethod]
        public void DA_PANEL_TC028()
        {
            Console.WriteLine("DA_PANEL_TC028 - Verify that when \"Add New Panel\" form is on focused all other control/form is disabled or locked.");

            //1 Navigate to Dashboard login page
            //2 Login with valid account

            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3 Click Administer link
            //4 Click Panel link
            //5 Click Add New link
            //6 Try to click other controls when Add New Panel dialog is opening            

            PanelsPage panelPage = mainPage.OpenPanelsPage();
            panelPage.OpenAddNewPanelPopup();

            //VP All control/form are disabled or locked when Add New Panel dialog is opening            
            Assert.AreEqual(mainPage.DivOvelayClass.Exists, true, "Controls are disabled");
        }

        /// <summary>
        /// Verify that user is unable to create new panel when (*) required field is not filled
        /// </summary>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        [TestMethod]
        public void DA_PANEL_TC029()
        {
            Console.WriteLine("DA_PANEL_TC029 - Verify that user is unable to create new panel when (*) required field is not filled");

            //1 Navigate to Dashboard login page
            //2 Select specific repository
            //3 Enter valid username and password
            //4 Click on Login button
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3 Click Administer/Panels link            
            //5 Click Add New link
            //6 Click on OK button    
            Chart chart = new Chart();
            PanelsPage panelPage = mainPage.OpenPanelsPage();
            panelPage.OpenAddNewPanelPopup().AddChart(chart);            

            //VP Warning message: "Display Name is required field" show up
            Assert.AreEqual("Display Name is a required field.", mainPage.GetDialogText(), "Failed! Actual message: {0}",mainPage.GetDialogText());        
        }

        /// <summary>
        /// Verify that no special character except '@' character is allowed to be inputted into "Display Name" field
        /// </summary>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        [TestMethod]
        public void DA_PANEL_TC030()
        {
            Console.WriteLine("DA_PANEL_TC030 - Verify that no special character except '@' character is allowed to be inputted into \"Display Name\" field");

            //1 Navigate to Dashboard login page
            //2 Login with valid account
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3 Click Administer link
            //4 Click Panel link
            //5 Click Add New link
            //6 Enter value into Display Name field with special characters except "@"
            //7 Click on OK button 
            Chart chart = new Chart("Logigear#$%","Name",null);
            PanelsPage panelPage=mainPage.OpenPanelsPage();
            AddNewPanelPage addPanelPopup = panelPage.OpenAddNewPanelPopup();
            addPanelPopup.AddChart(chart);

            //VP Message "Invalid display name. The name can't contain high ASCII characters or any of following characters: /:*?<>|"#{[]{};" is displayed
            Assert.AreEqual("Invalid display name. The name cannot contain high ASCII characters or any of the following characters: /:*?<>|\"#[]{}=%;",
            mainPage.GetDialogText(), "Failed! Actual message: {0}",mainPage.GetDialogText());   
     
            //8 Close Warning Message box
            mainPage.ConfirmDialog("OK");

            //9 Click Add New link
            //10 Enter value into Display Name field with special character is @            
            chart = new Chart("Logigear@","Name",null);
            addPanelPopup.AddChart(chart);

            bool a = panelPage.IsLinkExist("Logigear@");
            //VP The new panel is created
            Assert.AreEqual(true,panelPage.IsLinkExist("Logigear@"),"Page Logigear@ cannot be created");            
        }

        //[TestMethod]
        public void DA_PANEL_TC042()
        {
            Console.WriteLine("DA_PANEL_TC042 - Verify that all pages are listed correctly under the \"Select page *\" dropped down menu of \"Panel Configuration\" form/ control");

            //1 Navigate to Dashboard login page
            //2 Select a specific repository 
            //3 Enter valid Username and Password
            //4 Click 'Login' button
            LoginPage loginPage = new LoginPage(_webDriver);
            MainPage mainPage = loginPage.Open().Login(Constants.Repository, Constants.UserName, Constants.Password);

            //5 Click 'Add Page' button
            //6 Enter Page Name
            //7 Click 'OK' button
            //8 Click 'Add Page' button
            //9 Enter Page Name
            //10 Click 'OK' button
            //11 Click 'Add Page' button
            //12 Enter Page Name
            //13 Click 'OK' button
            Page page1 = new Page("main_hung1", "Select parent", 2, "Overview", false);
            Page page2 = new Page("main_hung2", "Select parent", 2, "Overview", false);
            Page page3 = new Page("main_hung3", "Select parent", 2, "Overview", false);            
            mainPage.OpenAddNewPage().AddPage(page1);
            mainPage.OpenAddNewPage().AddPage(page2);
            mainPage.OpenAddNewPage().AddPage(page3);

            //14 Click 'Choose panels' button
            //15 Click on any Chart panel instance
            mainPage.BtnChoosePanel.Click();            
            mainPage.ClickLinkText("Test Case Execution Results");
            //16 Click 'Select Page*' drop-down menu

            //17 Check that 'Select Page*' drop-down menu contains 3 items: 'main_hung1', 'main_hung2' and 'main_hung3'


            // Assert.AreEqual(true, panelPage.IsLinkExist("Logigear@"), "Page Logigear@ cannot be created");

            //Post-Condition
            mainPage.DeletePage(page1.PageName);
            mainPage.DeletePage(page2.PageName);
            mainPage.DeletePage(page3.PageName);

            mainPage.Logout();
        }
    }
}
