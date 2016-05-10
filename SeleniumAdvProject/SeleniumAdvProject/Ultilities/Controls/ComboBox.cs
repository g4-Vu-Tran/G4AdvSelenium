using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.Ultilities.Controls
{
    public class ComboBox:BaseControl
    {
        private SelectElement selectElement;
        public ComboBox() { }
      
        public ComboBox(string xPath) : base(By.XPath(xPath)) { }
        
        public ComboBox(By by) : base(by) { }

        public ComboBox(IWebElement element) : base(element) { }
       
        /// <summary>
        /// get elect the control
        /// </summary>
        /// <returns></returns>
        private void GetSelectControl()
        {
            LoadControl();
            selectElement = new SelectElement(element);
        }

        /// <summary>
        /// Selects the by text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SelectByText(string text)
        {
            GetSelectControl();
            selectElement.SelectByText(text);
        }
        /// <summary>
        /// Selects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SelectByIndex(int index)
        {
            GetSelectControl();
            selectElement.SelectByIndex(index);
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        /// <returns></returns>
        public string GetSelectedText()
        {
            GetSelectControl();
            return selectElement.SelectedOption.Text;
        }
    }
}
