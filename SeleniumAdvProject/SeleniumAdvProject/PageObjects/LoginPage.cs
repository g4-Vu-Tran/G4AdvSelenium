using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumAdvProject.Common;
using OpenQA.Selenium.Support.UI;
using System.Net;
using SeleniumAdvProject.Ultilities.Controls;

namespace SeleniumAdvProject.PageObjects
{
    public class LoginPage:BasePage
    {
        #region Locators

        static readonly By _ddlRepository = By.XPath("//select[@name='repository']");
        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//div[@class='btn-login']");
        
        #endregion
        #region Elements     
   
        public ComboBox DdlRepsitory
        {
            get { return new ComboBox(Constants.WebDriver.FindElement(_ddlRepository)); }                  
            
        }
       

        public TextBox TxtUsername
        {
           get { return new TextBox(Constants.WebDriver.FindElement(_txtUsername)); }             
        }

        public TextBox TxtPassword
        {
            get { return new TextBox(Constants.WebDriver.FindElement(_txtPassword)); }
             //get { return new TextBox(_txtPassword); }                  
        }

        public Button BtnLogin
        {
            get { return new Button(Constants.WebDriver.FindElement(_btnLogin)); }
             //get { return new Button(_btnLogin); }           
        }
                
        #endregion

        #region Methods
        public LoginPage Open()
        {
            Constants.WebDriver.Navigate().GoToUrl(Constants.LoginPageUrl);
            return this;
        }
        public MainPage Login(string repository,string username, string password)
        {
            DdlRepsitory.SelectByText(repository);
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            return new MainPage();            
        }
        
        public string LoginWithExpectedError(string repository, string username, string password)
        {
            DdlRepsitory.SelectByText(repository);
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            return this.GetDialogText();
        }

        public LoginPage LoginWithOutAccount(string repository)
        {
            DdlRepsitory.SelectByText(repository);
            BtnLogin.Click();
            return this;        
        }
       
        #endregion

    }
}
