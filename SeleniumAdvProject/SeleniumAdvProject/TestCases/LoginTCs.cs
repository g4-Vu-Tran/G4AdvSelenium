using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvProject.PageObjects.HomePage;
using SeleniumAdvProject.PageObjects.GeneralPage;
using SeleniumAdvProject.Common;

namespace SeleniumAdvProject.TestCases
{
    [TestClass]
    public class LoginTCs: BaseTestCase
    {
        /// <summary>
        /// Verify that user can login specific repository successfully via Dashboard login page with correct credentials
        /// </summary>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        [TestMethod]
        public void DA_LOGIN_TC001()
        {
            Console.WriteLine("DA_LOGIN_TC001 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver).Open();

            //2. Enter valid username and password	
            //3. Click on "Login" button
            MainPage mainPage = loginPage.Login(Constants.Repos, Constants.UserName, Constants.Password);

            //VP. Verify that Dashboard Mainpage appears
            Assert.AreEqual(Constants.UserName, mainPage.GetUserNameText(), "Login is not successfully");

            //Post-Condition
            mainPage.Logout();
        }

        /// <summary>
        /// Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials
        /// </summary>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        [TestMethod]
        public void DA_LOGIN_TC002()
        {
            Console.WriteLine("DA_LOGIN_TC002 - Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage(_webDriver).Open();

            //2. Enter invalid username and password
            //3. Click on "Login" button
            string actualMsg = loginPage.LoginWithExpectedError(Constants.Repos, "abc", "abc");

            //4. Verify that Dashboard Error message "Username or password is invalid" appears
            string expectedMsg = "Username or password is invalid";
            Assert.AreEqual(expectedMsg, actualMsg, string.Format("Message incorrect {0}", actualMsg));
        }
    }
}
