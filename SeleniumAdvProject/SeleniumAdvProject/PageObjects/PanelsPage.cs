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
    public class PanelsPage : BasePage
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

        /// <summary>
        /// Opens the add new panel popup.
        /// </summary>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>
        public AddNewPanelPage OpenAddNewPanelPopupFromLink()
        {
            LnkAddNew.Click();
            return new AddNewPanelPage(_webDriver);
        }

        public AddNewPanelPage OpenEditPanelPopup(string panelName)
        {
            Link LnkPanelName = new Link(_webDriver.FindElement(By.XPath(string.Format("//a[.='{0}']", panelName))));
            LnkPanelName.Click();
            return new AddNewPanelPage(_webDriver);
        }



        /// <summary>
        /// Adds the new panel.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>
        public PanelsPage AddNewPanel(Chart chart, bool openPopup = true)
        {
            if (openPopup)
            {
                OpenAddNewPanelPopupFromLink();
            }
            AddNewPanelPage addPanelPage = new AddNewPanelPage(_webDriver);
            addPanelPage.AddChart(chart);
            WaitForControlExists(By.XPath(string.Format("//a[.='{0}']", chart.DisplayName)));
            return this;
        }

        /// <summary>
        /// Delete all panels
        /// </summary>
        /// <returns>PanelsPage object</returns>
        /// <author>Vu Tran</author>
        /// <date>05/30/2016</date>
        public PanelsPage DeleteAllPanels()
        {
            LnkCheckAll.Click();
            LnkDelete.Click();
            ConfirmDialog("OK");
            return this;
        }

        /// Cancels the panel
        /// </summary>
        /// <returns>PanelsPage object</returns>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        public PanelsPage CancelPanel()
        {
            AddNewPanelPage addPanelPage = new AddNewPanelPage(_webDriver);
            addPanelPage.BtnCancel.Click();
            return this;
        }

        public bool IsDataProfileSOrder(string orderType)
        {
            AddNewPanelPage addPanelPage = new AddNewPanelPage(_webDriver);
            return addPanelPage.IsTheListIsSorted(addPanelPage.CbbDataProfile, orderType);
        }

        public bool IsPanelExist(string panelName)
        {
            Link LnkPanel = new Link(_webDriver,string())
        }
        #endregion
    }
}
