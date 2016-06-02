using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvProject.Common;

namespace SeleniumAdvProject.TestCases
{

    [TestClass]
    public class LoginTestCases
    {
        [TestMethod]
        public void TC01()
        {
            Console.WriteLine("TC01 - User can log into Railway with valid username and password");

            //1. Navigate to QA Railway Website
            Constant.WebDriver.Navigate().GoToUrl("http://192.168.0.17:54000/TADashboard/login.jsp");

            ////2. Click on "Login" tab
            //LoginPage loginPage = homePage.GotoLoginPage();

            ////3. Enter valid Email and Password
            ////4. Click on "Login" button
            ////VP. User is logged into Railway. Welcome user message is displayed.
            //string actualMsg = loginPage.Login(Constant.UserName, Constant.Password).GetWelcomMessage();
            //string expectedMsg = "Welcome " + Constant.UserName;
            //Assert.AreEqual(expectedMsg, actualMsg, "Error: \n[Actual] \"" + actualMsg + "\"message displays. \n[Expected] \"" + expectedMsg + "\" message displays.");

            ////5. Click on "Log out" Tab
            //loginPage.TabLogout.Click();

        }
    }
}
