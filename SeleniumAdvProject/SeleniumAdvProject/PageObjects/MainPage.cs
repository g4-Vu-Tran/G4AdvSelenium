﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.DataObjects;
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
        static readonly By _lnkDelete = By.XPath("//a[@class='delete']");
        static readonly By _divvOvelayClass = By.XPath("//div[@class='ui-dialog-overlay custom-overlay']");
        #endregion

        #region Elements
        public Link LnkDelete
        {
            get { return new Link(_webDriver.FindElement(_lnkDelete)); }
        }
        public Div DivOvelayClass
        {
            get { return new Div(_webDriver.FindElement(_divvOvelayClass)); }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Constructor of MainPage class
        /// </summary>
        /// <author>Vu Tran</author>
        /// <date>05/25/2015</date>
        public MainPage() { }

        /// <summary>
        /// Constructor of MainPage class
        /// </summary>
        /// <param name="webDriver">IWebDriver</param>
        /// <author>Vu Tran</author>
        /// <date>05/25/2015</date>
        public MainPage(IWebDriver webDriver) : base(webDriver) { }

        /// <summary>
        /// Delete the page
        /// </summary>
        /// <param name="pathOfPage">The path to go to the current page</param>
        /// <param name="confirmDelete">Define action Delete: click on Yes or No button, or not click on the button </param>
        /// <returns>Main Page</returns>
        /// <author>Vu Tran</author>
        /// <date>05/25/2015</date>
        public MainPage DeletePage(string pathOfPage, string confirmDelete = "Yes")
        {
            ClickDeleteLink(pathOfPage);
            ConfirmDialog(confirmDelete);
            WaitForPageLoadComplete();
            return this;
        }

        public MainPage ClickDeleteLink(string pathOfPage)
        {
            GoToPage(pathOfPage);
            LblGlobalSetting.MouseOver();
            LnkDelete.MouseOver();
            LnkDelete.Click();
            return this;
        }

        /// <summary>
        /// Adds the new panel.
        /// </summary>
        /// <param name="chart">The chart information.</param>
        /// <returns>Return main page after add new panle successfully</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public MainPage AddNewPanel(Chart chart)
        {
            GoToPage(chart.PageName);
            OpenAddNewPanelPopup().AddChart(chart);
            return this;
        }

        /// <summary>
        /// Opens the new panel popup.
        /// </summary>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public MainPage OpenNewPanelPopUp(string page)
        {
            GoToPage(page);
            BtnChoosePanel.Click();
            WaitForControlExists(By.XPath("//span[.='Create new panel']"), Constants.WaitTimeoutShortSeconds);
            BtnCreateNewPanel.Click();
            return this;
        }



        /// <summary>
        /// Gets the position page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public int GetPositionPage(string pageName)
        {
            Link page = new Link();
            page.FindElement(By.XPath(string.Format("//a[.='{0}']", pageName)));
            return page.Location.X;

        }

        /// <summary>
        /// Opens the setting.
        /// </summary>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public MainPage OpenSetting()
        {
            LblGlobalSetting.MouseOver();
            return this;
        }

        /// <summary>
        /// Verify Global Setting icon is exist
        /// </summary>
        /// <returns>True/False</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public Boolean IsSettingExist()
        {
            return LblGlobalSetting.isDisplayed();
        }

        /// <summary>
        /// Verify Page is exist
        /// </summary>
        /// <param name="pathOfPage">The path to go to the current page</param>
        /// <returns>True/False</returns>
        /// <author>Vu Tran</author>
        /// <date>05/25/2015</date>
        public Boolean IsPageExist(string pathOfPage)
        {
            string[] arrNode = pathOfPage.Split('/');
            string finalNode = arrNode[arrNode.Length - 1];
            GoToLink(pathOfPage.Substring(0, pathOfPage.Length - finalNode.Length - 1));
            string xpathOfPage = string.Format("//a[.='{0}']", CommonAction.EncodeSpace(arrNode[0]));
            for (int i = 1; i < arrNode.Length; i++)
            {
                xpathOfPage += string.Format("/..//a[.='{0}']", CommonAction.EncodeSpace(arrNode[i]));
            }
            Link LnkPage = new Link(_webDriver, By.XPath(xpathOfPage));
            return LnkPage.isDisplayed();
        }

        /// <summary>
        /// Determines whether [is page display after] [the specified page name1].
        /// </summary>
        /// <param name="pageName1">The page name1.</param>
        /// <param name="pageName2">The page name2.</param>
        /// <returns>return true if page exist and false if not exist</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public bool IsPageDisplayAfter(string pageName1, string pageName2)
        {
            Label pageTab = new Label(_webDriver.FindElement(By.XPath(string.Format("//div[@id='main-menu']/div/ul/li[.='{0}']/preceding-sibling::li[1]", pageName2, pageName1))));
            string tempPage = pageTab.Text;
            return tempPage.Equals(pageName1);
        }

        /// <summary>
        /// Verify Main Page is displayed
        /// </summary>
        /// <returns>True/False</returns>
        /// <author>Vu Tran</author>
        /// <date>05/26/2015</date>
        public bool Displayed()
        {
            Label LblUsername = new Label(_webDriver, _lblUsername);
            return LblUsername.isDisplayed();
        }

        /// <summary>
        /// Add the new page
        /// </summary>
        /// <param name="page">The page object</param>
        /// <returns>The MainPage object</returns>
        /// <author>Vu Tran</author>
        /// <date>05/26/2015</date>
        public MainPage AddPage(Page page)
        {
            AddNewPage addNewPage = this.OpenAddNewPage();
            addNewPage.AddPage(page.ParentPage, page);
            return this;
        }

        public MainPage EditPage(Page page)
        {
            AddNewPage editPage = new AddNewPage(_webDriver);
            editPage.AddPage(page.ParentPage, page);
            return this;
        }

        

        #endregion
    }
}
