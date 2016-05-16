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
    public class PanelPage : BasePage
    {

        #region Locators       
        static readonly By _cbbDataProfile = By.XPath("//select [@id='cbbProfile']");       
        static readonly By _btnOk = By.XPath("//input[@id='OK']");
        static readonly By _btnCancel = By.XPath("//input[@id='Cancel']");
        #endregion

        #region Elements
       
        public ComboBox CbbDataProfile
        {
            get { return new ComboBox(_webDriver.FindElement(_cbbDataProfile)); }
        }
        
        public Button BtnOk
        {
            get { return new Button(_webDriver.FindElement(_btnOk)); }
        }
        public Button BtnCancel
        {
            get { return new Button(_webDriver.FindElement(_btnOk)); }
        }
        #endregion

        #region Methods

        public PanelPage() { }
        public PanelPage(IWebDriver webDriver) : base(webDriver) { }
        public bool IsTheListIsSorted(ComboBox combobox,string sortType)
		{
             IList<string> listValues =combobox.OptionStrings;
            			
			int rowCount = listValues.Count;
            bool flag = false;

			// start from 1 to skip the table header row run to 'i < rowCount - 1' because we check
			// a pair of row at a time
			for (int i = 1; i < rowCount-1; i++)
			{
                if (sortType == "DESC")
                {
                     if(listValues[i].CompareTo(listValues[i+1]) >= 0)
                         flag = true;
                }
                else if (sortType == "ASC")
                {
                    if(listValues[i].CompareTo(listValues[i+1]) <= 0)
                         flag = true;
                }
				
			}
            return flag;
		}

        #endregion


    }
}
