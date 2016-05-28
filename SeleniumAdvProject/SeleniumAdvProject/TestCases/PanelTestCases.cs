﻿using System;
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

            Chart chart = new Chart(CommonAction.GenrateRandomString(Constants.lenghtRandomString), "Name", page1.PageName);

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
            panelPage.OpenAddNewPanelPopupFromLink();

            //VP All control/form are disabled or locked when Add New Panel dialog is opening            
            Assert.AreEqual(mainPage.DivOvelayClass.Exists, true, "Controls are disabled");
        }
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
            panelPage.OpenAddNewPanelPopupFromLink().AddChart(chart);

            //VP Warning message: "Display Name is required field" show up
            Assert.AreEqual("Display Name is a required field.", mainPage.GetDialogText(), "Failed! Actual message: {0}", mainPage.GetDialogText());
        }
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
            Chart chart = new Chart("Logigear#$%", "Name", null);
            PanelsPage panelPage = mainPage.OpenPanelsPage();
            AddNewPanelPage addPanelPopup = panelPage.OpenAddNewPanelPopupFromLink();
            addPanelPopup.AddChart(chart);

            //VP Message "Invalid display name. The name can't contain high ASCII characters or any of following characters: /:*?<>|"#{[]{};" is displayed
            Assert.AreEqual("Invalid display name. The name cannot contain high ASCII characters or any of the following characters: /:*?<>|\"#[]{}=%;",
            mainPage.GetDialogText(), "Failed! Actual message: {0}", mainPage.GetDialogText());

            //8 Close Warning Message box
            mainPage.ConfirmDialog("OK");

            //9 Click Add New link
            //10 Enter value into Display Name field with special character is @            
            chart = new Chart("Logigear@", "Name", null);
            addPanelPopup.AddChart(chart);

            bool a = panelPage.IsLinkExist("Logigear@");
            //VP The new panel is created
            Assert.AreEqual(true, panelPage.IsLinkExist("Logigear@"), "Page Logigear@ cannot be created");
        }

        [TestMethod]
        public void DA_PANEL_TC031()
        {
            Console.WriteLine("DA_PANEL_TC031 - Verify that \"Category\", \"Series\" and \"Caption\" field are enabled and disabled correctly corresponding to each type of the \"Chart Type\"");

            //1 Navigate to Dashboard login page
            //2 Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click 'Login' button
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //5. Click 'Add Page' button
            //6. Enter Page Name
            //7. Click 'OK' button
            string pageName = CommonAction.GenrateRandomString(Constants.lenghtRandomString);
            Page page = new Page(pageName, "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(page);

            //8. Click 'Choose Panels' button below 'main_hung' button        
            //9. Click 'Create new panel' button
            mainPage.OpenNewPanelPopUp(page.PageName);

            //10. Click 'Chart Type' drop-down menu
            //11. Select 'Pie' Chart Type
            AddNewPanelPage addPanelPopup = new AddNewPanelPage(_webDriver);
            addPanelPopup.SelectChartType("Pie");

            //VP: Check that 'Category' and 'Caption' are disabled, 'Series' is enabled
            string actualCategoryStatus = addPanelPopup.GetCategoryStatus();
            string actualCategoryCaptionStatus = addPanelPopup.GetCategoryCaptionStatus();
            string actualSeriesStatus = addPanelPopup.GetSeriesStatus();
            string actualSeriesCaptionStatus = addPanelPopup.GetSeriesCaptionStatus();

            Assert.AreEqual("disabled", actualCategoryStatus, "Category combobox is " + actualCategoryStatus);
            Assert.AreEqual("disabled", actualCategoryCaptionStatus, "Category Caption textbox is " + actualCategoryCaptionStatus);
            Assert.AreEqual("enabled", actualSeriesStatus, "Series combobox is " + actualSeriesStatus);
            Assert.AreEqual("disabled", actualSeriesCaptionStatus, "Series Caption textbox is " + actualSeriesCaptionStatus);

            //12. Click 'Chart Type' drop-down menu
            //13. Select 'Single Bar' Chart Type
            addPanelPopup.SelectChartType("Single Bar");

            //VP: Check that 'Category' is disabled, 'Series' and 'Caption' are enabled
            string actualCategoryStatus1 = addPanelPopup.GetCategoryStatus();
            string actualCategoryCaptionStatus1 = addPanelPopup.GetCategoryCaptionStatus();
            string actualSeriesStatus1 = addPanelPopup.GetSeriesStatus();
            string actualSeriesCaptionStatus1 = addPanelPopup.GetSeriesCaptionStatus();

            Assert.AreEqual("disabled", actualCategoryStatus1, "Category combobox is " + actualCategoryStatus1);
            Assert.AreEqual("enabled", actualCategoryCaptionStatus1, "Category Caption textbox is " + actualCategoryCaptionStatus1);
            Assert.AreEqual("enabled", actualSeriesStatus1, "Series combobox is " + actualSeriesStatus1);
            Assert.AreEqual("enabled", actualSeriesCaptionStatus1, "Series Caption textbox is " + actualSeriesCaptionStatus1);

            //14. Click 'Chart Type' drop-down menu
            //15. Select 'Stacked Bar' Chart Type
            addPanelPopup.SelectChartType("Stacked Bar");

            //VP: Check that 'Category' ,'Series' and 'Caption' are enabled
            string actualCategoryStatus2 = addPanelPopup.GetCategoryStatus();
            string actualCategoryCaptionStatus2 = addPanelPopup.GetCategoryCaptionStatus();
            string actualSeriesStatus2 = addPanelPopup.GetSeriesStatus();
            string actualSeriesCaptionStatus2 = addPanelPopup.GetSeriesCaptionStatus();

            Assert.AreEqual("enabled", actualCategoryStatus2, "Category combobox is " + actualCategoryStatus2);
            Assert.AreEqual("enabled", actualCategoryCaptionStatus2, "Category Caption textbox is " + actualCategoryCaptionStatus2);
            Assert.AreEqual("enabled", actualSeriesStatus2, "Series combobox is " + actualSeriesStatus2);
            Assert.AreEqual("enabled", actualSeriesCaptionStatus2, "Series Caption textbox is " + actualSeriesCaptionStatus2);

            //16. Click 'Chart Type' drop-down menu
            //17. Select 'Group Bar' Chart Type
            addPanelPopup.SelectChartType("Group Bar");

            //VP: Check that 'Category' ,'Series' and 'Caption' are enabled
            string actualCategoryStatus3 = addPanelPopup.GetCategoryStatus();
            string actualCategoryCaptionStatus3 = addPanelPopup.GetCategoryCaptionStatus();
            string actualSeriesStatus3 = addPanelPopup.GetSeriesStatus();
            string actualSeriesCaptionStatus3 = addPanelPopup.GetSeriesCaptionStatus();

            Assert.AreEqual("enabled", actualCategoryStatus3, "Category combobox is " + actualCategoryStatus3);
            Assert.AreEqual("enabled", actualCategoryCaptionStatus3, "Category Caption textbox is " + actualCategoryCaptionStatus3);
            Assert.AreEqual("enabled", actualSeriesStatus3, "Series combobox is " + actualSeriesStatus3);
            Assert.AreEqual("enabled", actualSeriesCaptionStatus3, "Series Caption textbox is " + actualSeriesCaptionStatus3);

            //18. Click 'Chart Type' drop-down menu
            //19. Select 'Line' Chart Type
            addPanelPopup.SelectChartType("Line");

            //VP: Check that 'Category' ,'Series' and 'Caption' are enabled
            string actualCategoryStatus4 = addPanelPopup.GetCategoryStatus();
            string actualCategoryCaptionStatus4 = addPanelPopup.GetCategoryCaptionStatus();
            string actualSeriesStatus4 = addPanelPopup.GetSeriesStatus();
            string actualSeriesCaptionStatus4 = addPanelPopup.GetSeriesCaptionStatus();

            Assert.AreEqual("enabled", actualCategoryStatus4, "Category combobox is " + actualCategoryStatus4);
            Assert.AreEqual("enabled", actualCategoryCaptionStatus4, "Category Caption textbox is " + actualCategoryCaptionStatus4);
            Assert.AreEqual("enabled", actualSeriesStatus4, "Series combobox is " + actualSeriesStatus4);
            Assert.AreEqual("enabled", actualSeriesCaptionStatus4, "Series Caption textbox is " + actualSeriesCaptionStatus4);
            
            //Post-Condition
            addPanelPopup.CloseWindow();
            //mainPage.DeletePage(page.PageName);
            //mainPage.Logout();
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
