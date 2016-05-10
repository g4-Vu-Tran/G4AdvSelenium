using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.DataObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvProject.Ultilities.Controls;

namespace SeleniumAdvProject.PageObjects
{
    public class AddNewPage : MainPage
    {
        #region Locators
        static readonly By _txtPageName = By.XPath("//input[@id='name']");
        static readonly By _cbbParentPage = By.XPath("//select [@id='parent']");
        static readonly By _cbbNumberOfColumns = By.XPath("//select[@id='columnnumber']");
        static readonly By _cbbDisplayAfter = By.XPath("//select[@id='afterpage']");
        static readonly By _chkPublic = By.XPath("//input[@id='ispublic']");
        static readonly By _btnOk = By.XPath("//input[@id='OK']");
        static readonly By _btnCancel = By.XPath("//input[@id='Cancel']");
        #endregion

        #region Elements
        public TextBox TxtPageName
        {
            get { return new TextBox(_txtPageName); }
        }
        public ComboBox CbbParentPage
        {
            get { return new ComboBox(_cbbParentPage); }
        }
        public ComboBox CbbNumberOfColumns
        {
            get { return new ComboBox(_cbbNumberOfColumns); }
        }
        public ComboBox CbbDisplayAfter
        {
            get { return new ComboBox(_cbbDisplayAfter); }
        }
        public Checkbox ChkPublic
        {
            get { return new Checkbox(_cbbDisplayAfter); }
        }

        public Button BtnOk
        {
            get { return new Button(_btnOk); }
        }
        public Button BtnCancel
        {
            get { return new Button(_btnCancel); }
        }
        #endregion

        #region Methods
        public MainPage addPage(Page page)
        {
            TxtPageName.SendKeys(page.PageName);
            CbbParentPage.SelectByText(page.ParentPage);
            CbbNumberOfColumns.SelectByText(page.NumberOfColumns.ToString());
            CbbDisplayAfter.SelectByText(page.DisplayAfter);
            CbbNumberOfColumns.SelectByText(page.NumberOfColumns.ToString());
            return new MainPage();
        }
        #endregion


    }
}
