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
        static readonly By _lnkDelete = By.XPath("//a[@class='delete']");

        #endregion

        #region Elements

        public Link LnkDelete
        {
            get { return new Link(_webDriver.FindElement(_lnkDelete)); }
        }
        public Label LblUsername
        {
            get { return new Label(_webDriver.FindElement(_lblUsername)); }
        }
        public Label LblRepository
        {
            get { return new Label(_webDriver.FindElement(_lblRepository)); }
        }

        public Label LblCurrentRepository
        {
            get { return new Label(_webDriver.FindElement(_lblCurrentRepository)); }
        }
        public Label LblSetting
        {
            get { return new Label(_webDriver.FindElement(_lblSetting)); }
        }

        public Link LnkLogout
        {
            get { return new Link(_webDriver.FindElement(_lnkLogout)); }

        }

        public Link LnkAdd
        {
            get { return new Link(_webDriver.FindElement(_lnkAdd)); }
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
            Actions mouseAction = new Actions(_webDriver);
            mouseAction.MoveToElement(LblRepository).Perform();
            mouseAction.MoveToElement(_webDriver.FindElement(By.XPath("//ul[@id='ulListRepositories']//a[.='" + repositoryName + "']"))).Click().Perform();
            return this;
        }
        public MainPage DeletePage(string pageName)
        {
            Link page = new Link();
            page.FindElement(By.XPath(string.Format("//a[.='{0}']", pageName)));
            page.Click();
            LblSetting.MouseOver();
            LnkDelete.Click();
            return this;
        }

        public int GetPositionPage(string pageName)
        {
            Link page = new Link();
            page.FindElement(By.XPath(string.Format("//a[.='{0}']", pageName)));
            return page.Location.X;

        }

        public AddNewPage GoToAddNewPage()
        {
            LblSetting.MouseOver();
            LnkAdd.Click();
            return new AddNewPage();
        }

        public MainPage OpenSetting()
        {
            LblSetting.MouseOver();
            return this;
        }

        public Boolean IsSettingExist()
        {
            bool _isExist = false;
            _isExist = LblSetting.Exists;
            return _isExist;
        }

        public bool IsPageVisible(string pageName)
        {
            Link page = new Link();
            page.FindElement(By.XPath(string.Format("//a[.='{0}']", pageName)));
            return page.Enabled;
        }

        #endregion
    }
}
