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

        [TestMethod]
        public void DA_PANEL_TC027()
        {
            Console.WriteLine("DA_LOGIN_TC027 - Verify that when \"Choose panels\" form is expanded all pre-set panels are populated and sorted correctly");

            //1 Navigate to Dashboard login page
            //2 Login with valid account
            //1. Navigate to Dashboard login page
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
            //9Click OK button on PanelConfiguration popup and then click on Chose Panel menu icon

             PanelPage panelPage = mainPage.OpenPanelPage();

            //VP Verify that all pre-set panels are populated and sorted correctly
            bool actualResult = panelPage.IsTheListIsSorted(panelPage.CbbDataProfile, "ASC");
            Assert.AreEqual(true, actualResult, "Data Profile is not sorted correctly");

            panelPage.BtnCancel.Click();

            //Post-Condition
            //Logout			
            //Close Dashboard            
            mainPage.DeletePage(page1.PageName, "Yes");
            mainPage.Logout();
        }       
    }
}
