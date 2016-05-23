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

        [TestMethod]
        public void DA_PANEL_TC027()
        {
            Console.WriteLine("DA_LOGIN_TC027 - Verify that when \"Choose panels\" form is expanded all pre-set panels are populated and sorted correctly");

            //1 Navigate to Dashboard login page
            //2 Login with valid account
                        LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);
            //3 Go to Global Setting -> Add page
            //4 Enter page name to Page Name field.
            //5 Click OK button
            Page page1 = new Page(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Select parent", 2, "Overview", false);
            mainPage.OpenAddNewPage().AddPage(page1);

            //6 Go to Global Setting -> Create Panel     
            //7 Enter display name into Display Name textbox
            //8 Select any value in Series dropdown list
            //9 Click OK button on PanelConfiguration popup and then click on Chose Panel menu icon
           
            Chart chart =new Chart(ActionCommon.GenrateRandomString(Constants.lenghtRandomString), "Name" ,page1.PageName);

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
            Console.WriteLine("DA_LOGIN_TC028 - Verify that when \"Add New Panel\" form is on focused all other control/form is disabled or locked.");

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
            panelPage.LnkCheckAll.Click();
            Assert.AreEqual(panelPage.LnkCheckAll.Enabled, true, "Controls are disabled");
        }
        [TestMethod]
        public void DA_PANEL_TC029()
        {
            Console.WriteLine("DA_LOGIN_TC029 - Verify that user is unable to create new panel when (*) required field is not filled");

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
        [TestMethod]
        public void DA_PANEL_TC030()
        {
            Console.WriteLine("DA_LOGIN_TC030 - Verify that no special character except '@' character is allowed to be inputted into \"Display Name\" field");

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
            AddNewPanelPopup addPanelPopup = panelPage.OpenAddNewPanelPopup();
            addPanelPopup.AddChart(chart);

            //VP Message "Invalid display name. The name can't contain high ASCII characters or any of following characters: /:*?<>|"#{[]{};" is displayed
            Assert.AreEqual("Invalid display name. The name cannot contain high ASCII characters or any of the following characters:&nbsp/:*?<>|\"#[]{}=%;",
            mainPage.GetDialogText(), "Failed! Actual message: {0}",mainPage.GetDialogText());   
     
            //8 Close Warning Message box
            mainPage.ConfirmDialog("OK");
            addPanelPopup.BtnCancel.Click();

            //9 Click Add New link
            //10 Enter value into Display Name field with special character is @            
            chart = new Chart("Logigear@","Name","Excution Dashboard");
            addPanelPopup.AddChart(chart);

            //VP The new panel is created
            Assert.AreEqual(true,panelPage.IsLinkExist("Logigear@"),"Page Logigear@ cannot be created");            
        }
    }
}
