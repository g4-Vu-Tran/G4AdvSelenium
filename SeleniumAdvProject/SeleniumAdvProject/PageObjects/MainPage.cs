using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.Ultilities.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.PageObjects
{
    public class MainPage : BasePage
    {
        #region Locators

        static readonly By _lblUsername = By.XPath("//a[@href='#Welcome']");
        static readonly By _lblRepository = By.XPath("//a[@href='#Repository']");
        static readonly By _lblCurrentRepository = By.XPath("//a[@href='#Repository']/span");
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");
        static readonly By _lblSetting = By.XPath(".//li[@class='mn-setting']/a");
        static readonly By _lnkAdd = By.XPath("//a[@class='add']");

        #endregion

        #region Elements


        public Label LblUsername
        {
            get { return new Label(_lblUsername); }
        }
        public Label LblRepository
        {            
             get { return new Label(_lblRepository); }
        }

        public Label LblCurrentRepository
        {           
             get { return new Label(_lblCurrentRepository); }
        }
        public Label LblSetting
        {           
             get { return new Label(_lblSetting); }
        }
        
        public Link LnkLogout
        {
           
             get { return new Link(_lnkLogout); }
        }
        
        public Link LnkAdd
        {
            get { return new Link(_lnkAdd); }
        }

        #endregion

        #region Methods
        public string GetUserNameText()
        {
            return LblUsername.Text;
        }

        public string GetCurrentRepositoryText()
        {
            return LblCurrentRepository.Text;
        }

        public LoginPage Logout()
        {
            LblUsername.MouseOver();
            LnkLogout.Click();
            return new LoginPage();
        }

        public MainPage SelectRepository(String repositoryName)
        {

            LblRepository.MouseOver();


            Actions mouseAction = new Actions(Constants.WebDriver);

            mouseAction.MoveToElement(LblRepository).Perform();
            mouseAction.MoveToElement(Constants.WebDriver.FindElement(By.XPath("//ul[@id='ulListRepositories']//a[.='" + repositoryName + "']"))).Click().Perform();
            return this;
        }

        public AddNewPage GoToAddNewPage()
        {
            LblSetting.MouseOver();
            LnkAdd.Click();
            return new AddNewPage();
        }

        public MainPage OpenSetting()
        {
            Actions mouseAction = new Actions(Constants.WebDriver);
            mouseAction.MoveToElement(LblSetting).Perform();
            return this;
        }

        public Boolean IsSettingExist()
        {
            bool _isExist = false;
            _isExist = LblSetting.Exists;
            return _isExist;
        }


        #endregion
    }
}
