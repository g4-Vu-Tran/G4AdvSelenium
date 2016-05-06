using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvProject.PageObjects;
using SeleniumAdvProject.Common;

namespace SeleniumAdvProject.TestCases
{
    [TestClass]
    public class LoginTestCases:BaseTestCase
    {
        [TestMethod]
        public void TC01()
        {
            Console.WriteLine("DA_LOGIN_TC001 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials");

            //1. Navigate to Dashboard login page
            LoginPage loginPage = new LoginPage();
            loginPage.Open();

            //2. Enter valid username and password	
            //3. Click on "Login" button
            DashboardPage dashboardPage = loginPage.Login(Constant.Repository, Constant.UserName, Constant.Password);

            //VP. Verify that Dashboard Mainpage appears
            Assert.AreEqual(Constant.UserName,dashboardPage.GetUserNameText());
        }
        
          /* [TestMethod]
        public void TC02()
        {
            Console.WriteLine("DA_LOGIN_TC002 - Verify that user fails to login specific repository successfully via Dashboard login page with incorrect credentials");

        }
         [TestMethod]
        public void TC03()
        {
            Console.WriteLine("DA_LOGIN_TC003 - Verify that user fails to log in specific repository successfully via Dashboard login page with correct username and incorrect password");

        }*/
    }
}
