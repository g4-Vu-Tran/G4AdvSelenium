using OpenQA.Selenium;
using SeleniumAdvProject.Ultilities.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.PageObjects
{
    public class PanelConfigurationPopup : Popup
    {
        #region Locators
        static readonly By _cbbSelectPage = By.XPath("//select[@id='cbbPages']");
        static readonly By _txtHeight = By.XPath("//input[@id='txtHeight']");
        static readonly By _txtFolder = By.XPath("//input[@id='txtFolder']");        
        #endregion

        #region Elements
        public ComboBox CbbSelectPage
        {
            get { return new ComboBox(_webDriver.FindElement(_cbbSelectPage)); }
        }
        public TextBox TxtHeight
        {
            get { return new TextBox(_webDriver.FindElement(_txtHeight)); }
        }
        public RadioButton TxtFolder
        {
            get { return new RadioButton(_webDriver.FindElement(_txtFolder)); }
        }
        #endregion


        #region Methods
        public PanelConfigurationPopup() { }
        public PanelConfigurationPopup(IWebDriver webDriver) : base(webDriver) { }
        public MainPage SettingPanel(string selectPage, int height, string folder)
        {
          //  RefreshCurrentPage();
            CbbSelectPage.SelectByText(selectPage);
            TxtHeight.SendKeys(height.ToString());
            TxtFolder.SendKeys(folder);
          //  Button BtnOk = new Button(By.XPath("//input[@id='OK']"));
            BtnOk.WaitForControlDisplay(13);
            BtnOk.Click();
            return new MainPage(_webDriver);
        }

        #endregion


    }
}
