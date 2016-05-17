﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvProject.PageObjects;
using SeleniumAdvProject.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.Text.RegularExpressions;

namespace SeleniumAdvProject.TestCases
{
    [TestClass]
    public class LoginTestCases : BaseTestCase
    {

        [TestMethod]
        public void DA_LOGIN_TC001()
        {

            Console.WriteLine("DA_LOGIN_TC001 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Enter valid username and password	
            //3. Click on "Login" button
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //VP. Verify that Dashboard Mainpage appears
            Assert.AreEqual(Constants.UserName, mainPage.GetUserNameText(),"Login is not successfully");

            //Post-Condition
            //Logout			
            //Close Dashboard	
            mainPage.Logout();
        }

        [TestMethod]
        public void DA_LOGIN_TC002()
        {
            Console.WriteLine("DA_LOGIN_TC002 - Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials");
            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Enter invalid username and password
            //3. Click on "Login" button
            string actualMessage = loginPage.LoginWithExpectedError(Constants.Repository, "abc", "abc");

            //4. Verify that Dashboard Error message "Username or password is invalid" appears            
            Assert.AreEqual("Username or password is invalid", actualMessage,string.Format("Message incorrect {0}",actualMessage));
        }

        [TestMethod]
        public void DA_LOGIN_TC003()
        {
            Console.WriteLine("DA_LOGIN_TC003 - Verify that user fails to log in specific repository successfully via Dashboard login page with correct username and incorrect password");
            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();
            //2 Enter valid username and invalid password (administrator / abc)
            //3 Click on "Login" button
            string a = loginPage.LoginWithExpectedError(Constants.Repository, Constants.UserName, "abc");

            //4 Verify that Dashboard Error message "Username or password is invalid" appears
            string actualMessage = loginPage.GetDialogText();
            Assert.AreEqual("Username or password is invalid", actualMessage, string.Format("Message incorrect {0}", actualMessage));

        }

        [TestMethod]
        public void DA_LOGIN_TC004()
        {
            Console.WriteLine("DA_LOGIN_TC004 - Verify that user is able to log in different repositories successfully after logging out current repository");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2 Enter valid username and password of default repository (administrator / <blank>)
            //3 Click on "Login" button
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //4 Click on "Logout" button
            mainPage.Logout();

            //5 Select a different repository (TestRepository)
            //6 Enter valid username and password of this repository (administrator / <blank>)
            loginPage.Login(Constants.TestRepository, Constants.UserName, Constants.Password);

            //VP Verify that Dashboard Mainpage appears
            Assert.AreEqual(Constants.UserName, mainPage.GetUserNameText(), "Main page does not appear");

            //Post-Condition
            //Logout			
            //Close Dashboard	
            mainPage.Logout();
        }

        [TestMethod]
        public void DA_LOGIN_TC005()
        {
            Console.WriteLine("DA_LOGIN_TC005 - Verify that there is no Login dialog when switching between 2 repositories with the same account");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with valid account for the first repository
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Choose another repository in Repository list (TestRepository)
            mainPage.SelectRepository(Constants.TestRepository);

            //VP Observe the current page
            //- There is no Login Repository dialog
            String actualURL = _webDriver.Url.ToString();
            Assert.IsFalse(actualURL.Equals(Constants.LoginPageUrl));

            //- The Repository menu displays name of switched repository
            Assert.AreEqual(Constants.TestRepository, mainPage.GetCurrentRepositoryText());
        }

        [TestMethod]
        public void DA_LOGIN_TC006()
        {
            Console.WriteLine("DA_LOGIN_TC006 - Verify that \"Password\" input is case sensitive");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with the account has uppercase password
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //VP. Observe the current page
            // Main page is displayed
            String actualURL = _webDriver.Url.ToString();
            Assert.IsTrue(actualURL.Contains(Constants.MainPageUrl));

            //3. Logout TA Dashboard
            mainPage.Logout();

            //4. Login with the above account but enter lowercase password
            loginPage.Login(Constants.Repository, "test", "admin");

            //VP. Observe the current page
            Assert.IsTrue(actualURL.Contains(Constants.MainPageUrl));
        }

        [TestMethod]
        public void DA_LOGIN_TC008()
        {
            Console.WriteLine("DA_LOGIN_TC008 - Verify that password with special characters is working correctly");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Login with account that has special characters password
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName1, Constants.SpecialPassword);

            //VP. Main page is displayed
            Assert.AreEqual(Constants.UserName, mainPage.GetUserNameText());

            //Post-Condition
            //Logout			
            //Close Dashboard	
            mainPage.Logout();
        }

        [TestMethod]
        public void DA_LOGIN_TC010()
        {
            Console.WriteLine("DA_LOGIN_TC010 - Verify that the page works correctly for the case when no input entered to Password and Username field");

            //1 Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver);
            loginPage.Open();

            //2. Click Login button without entering data into Username and Password field
            loginPage.LoginWithOutAccount(Constants.Repository);
            string actualMessage = loginPage.GetDialogText();
            string expectMessage = "Please enter username";
            Assert.AreEqual(expectMessage, actualMessage, "There is a bug here. Missing ! behind the text");

        }
    }
}
