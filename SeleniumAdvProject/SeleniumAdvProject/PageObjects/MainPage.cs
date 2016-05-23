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
        #endregion

        #region Elements
        public Link LnkDelete
        {
            get { return new Link(_webDriver.FindElement(_lnkDelete)); }
        }
        #endregion

        #region Methods

        public MainPage() { }
        public MainPage(IWebDriver webDriver) : base(webDriver) { }

        public MainPage DeletePage(string path, string confirmDelete = "Yes")
        {           
            ClickMenuItem(path);
            LblGlobalSetting.MouseOver();
            LnkDelete.Click();
            ConfirmDialog(confirmDelete);
            WaitForPageLoadComplete();
            return this;
        }

        public MainPage AddNewPanel(Chart chart)
        {
            ClickMenuItem(chart.PageName);
            AddNewPanelPopup panelPage = OpenAddNewPanelPopup();
            PanelConfigurationPopup plCongiuration = panelPage.AddChart(chart);
            plCongiuration.SettingPanel(chart.PageName,chart.Height,chart.Folder);
            return this;
        }

        public int GetPositionPage(string pageName)
        {
            Link page = new Link();
            page.FindElement(By.XPath(string.Format("//a[.='{0}']", pageName)));
            return page.Location.X;

        }

        public MainPage OpenSetting()
        {
            LblGlobalSetting.MouseOver();
            return this;
        }

        public Boolean IsSettingExist()
        {
            return LblGlobalSetting.Exists;
        }

       

        public bool IsPageDisplayAfter(string pageName1, string pageName2)
        {
            Label pageTab = new Label(_webDriver.FindElement(By.XPath(string.Format("//div[@id='main-menu']/div/ul/li[.='{0}']/preceding-sibling::li[1]", pageName1))));
            string tempPage = pageTab.Text;
            return tempPage.Equals(pageName2);            
        }
                
        #endregion
    }
}
