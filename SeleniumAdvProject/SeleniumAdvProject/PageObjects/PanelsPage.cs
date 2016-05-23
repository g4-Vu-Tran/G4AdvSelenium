using OpenQA.Selenium;
using SeleniumAdvProject.DataObjects;
using SeleniumAdvProject.Ultilities.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.PageObjects
{
    public class PanelsPage:BasePage
    {
        #region Locators
        static readonly By _lnkAddNew = By.XPath("//a[.='Add New']");
        static readonly By _lnkDelete = By.XPath("//a[.='Delete']");
        static readonly By _lnkCheckAll = By.XPath("//a[.='Check All']");
        #endregion

        #region Elements
        public Link LnkAddNew
        {
            get { return new Link(_webDriver.FindElement(_lnkAddNew)); }
        }
        public Link LnkDelete
        {
            get { return new Link(_webDriver.FindElement(_lnkDelete)); }
        }
        public Link LnkCheckAll
        {
            get { return new Link(_webDriver.FindElement(_lnkCheckAll)); }
        }
        #endregion
        #region Methods

        public PanelsPage() { }
        public PanelsPage(IWebDriver webDriver) : base(webDriver) { }

        public AddNewPanelPage OpenAddNewPanelPopup()
        {
            LnkAddNew.Click();
            return new AddNewPanelPage(_webDriver);
        }
        public PanelsPage AddNewPanel(Chart chart)
        {            
            OpenAddNewPanelPopup().AddChart(chart);
            return this;
        }
        
        #endregion
    }
}
