using OpenQA.Selenium;
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
    public class PanelsPage : BasePage
    {
        #region Locators
        static readonly By _lnkAddNew = By.XPath("//a[.='Add New']");
        static readonly By _lnkDelete = By.XPath("//a[.='Delete']");
        static readonly By _lnkCheckAll = By.XPath("//a[.='Check All']");
        static readonly By _lnkUnCheckAll = By.XPath("//a[.='UnCheck All']");
        #endregion

        #region Elements
        public Link LnkAddNew
        {
            get { return new Link(FindElement(_lnkAddNew)); }
        }

        public Link LnkUnCheckAll
        {
            get { return new Link(FindElement(_lnkUnCheckAll)); }
        }

        public Link LnkDelete
        {
            get { return new Link(FindElement(_lnkDelete)); }
        }
        public Link LnkCheckAll
        {
            get { return new Link(FindElement(_lnkCheckAll)); }
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

        /// <summary>
        /// Opens the edit panel popup.
        /// </summary>
        /// <param name="panelName">Name of the panel.</param>
        /// <returns>Add New Panel Page</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>s
        public AddNewPanelPage OpenEditPanelPopup(string panelName)
        {
            Link LnkPanelName = new Link(FindElement(By.XPath(string.Format("//a[.='{0}']", panelName))));
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
        /// Adds the new panel with expected error
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <returns></returns>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        public AddNewPanelPage AddChartWithExpectedError(Chart chart)
        {
            OpenAddNewPanelPage("Panels Page");
            AddNewPanelPage addPanelPage = new AddNewPanelPage(_webDriver);
            addPanelPage.AddChart(chart);
            return new AddNewPanelPage(_webDriver);
        }

        public AddNewPanelPage AddNewPanelPage()
        {
            LnkAddNew.Click();
            return new AddNewPanelPage(_webDriver);
        }


        /// <summary>
        /// Delete all panels
        /// </summary>
        /// <param name="panelName">input a panel name to delete a panel or "All" to detele all panel</param>
        /// <returns> PanelsPage object </returns>
        /// <author> Vu Tran </author>
        /// <editor>Huong Huynh-update delete with special panel or all panel</editor>
        /// <date>06/03/2016</date>
        public PanelsPage DeletePanels(string panelName)
        {
            if (panelName.Equals("All"))
            {
                LnkCheckAll.Click();
                LnkDelete.Click();
            }
            else
            {
                Checkbox chkPanel = new Checkbox(FindElement(By.XPath(string.Format("//td[.='{0}']//preceding-sibling::td/input[@id='chkDelPanel']", panelName))));
                chkPanel.Check();
                Link lnkEdit = new Link(FindElement(By.XPath(string.Format("//td[.='{0}']//following-sibling::td/a[.='Delete']", panelName))));
                lnkEdit.Click();
            }

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
            Link LnkPanelName = new Link(FindElement(By.XPath(string.Format("//a[.='{0}']", panelName))));
            return LnkPanelName.Displayed;
        }


        public bool IsCheckBoxExists(string[] paneList)
        {
            for (int i = 0; i < paneList.Length; i++)
            {
                Checkbox chkDataProfile = new Checkbox(FindElement(By.XPath(string.Format("//td[.='{0}']//preceding-sibling::td/input[@id='chkDel']", CommonAction.EncodeSpace(paneList[i])))));
                if (chkDataProfile == null)
                    return false;
            }
            return true;
        }


        #endregion
    }
}
