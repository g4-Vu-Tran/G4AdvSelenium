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

        public MainPage() { }
        public MainPage(IWebDriver webDriver) : base(webDriver) { }

        /// <summary>
        /// Deletes the page.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="confirmDelete">The confirm delete.</param>
        /// <returns></returns>
        public MainPage DeletePage(string path, string confirmDelete = "Yes")
        {           
            ClickMenuItem(path);
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
            ClickMenuItem(chart.PageName);
            OpenAddNewPanelPopup().AddChart(chart);
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
        /// Determines whether [is setting exist].
        /// </summary>
        /// <returns></returns>
        public Boolean IsSettingExist()
        {
            return LblGlobalSetting.Exists;
        }
             
        /// <summary>
        /// Determines whether [is page display after] [the specified page name1].
        /// </summary>
        /// <param name="pageName1">The page name1.</param>
        /// <param name="pageName2">The page name2.</param>
        /// <returns></returns>
        public bool IsPageDisplayAfter(string pageName1, string pageName2)
        {
            Label pageTab = new Label(_webDriver.FindElement(By.XPath(string.Format("//div[@id='main-menu']/div/ul/li[.='{0}']/preceding-sibling::li[1]", pageName2,pageName1))));
            string tempPage = pageTab.Text;
            return tempPage.Equals(pageName1);            
        }
                
        #endregion
    }
}
