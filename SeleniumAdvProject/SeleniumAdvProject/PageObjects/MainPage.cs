using OpenQA.Selenium;
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
            GoToPage(pathOfPage);
            LblGlobalSetting.MouseOver();
            LnkDelete.Click();
            ConfirmDialog(confirmDelete);
            WaitForPageLoadComplete();
            return this;
        }

        /// <summary>
        /// Adds the new panel.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <returns></returns>
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
        public MainPage OpenNewPanelPopUp()
        {
            BtnChoosePanel.Click();
            BtnCreateNewPanel.Click();
            return this;
        }

           

        /// <summary>
        /// Gets the position page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns></returns>
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
            return LblGlobalSetting.Exists;
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
            bool result = false;
            string[] arrNode = pathOfPage.Split('/');
            string xpathOfPage = string.Format("//a[.='{0}']", CommonAction.EncodeSpace(arrNode[0]));
            for (int i = 1; i < arrNode.Length; i++)
            {
                xpathOfPage += string.Format("/..//a[.='{0}']", CommonAction.EncodeSpace(arrNode[i]));
            }
            result = _webDriver.FindElement(By.XPath(xpathOfPage)).Displayed;
            return result;
        }

        /// <summary>
        /// Determines whether [is page display after] [the specified page name1].
        /// </summary>
        /// <param name="pageName1">The page name1.</param>
        /// <param name="pageName2">The page name2.</param>
        /// <returns></returns>
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
            bool isMainPageUrl = _webDriver.Url.EndsWith(Constants.MainPageUrl);
            return isMainPageUrl && LblUsername.Exists;
        }

        #endregion
    }
}
