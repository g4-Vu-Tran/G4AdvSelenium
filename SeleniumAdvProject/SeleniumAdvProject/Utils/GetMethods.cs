using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvProject.Utils
{
    public class GetMethods
    {
        public static string GetText(IWebElement element)
        {
            return element.GetAttribute("value");
        }

        public static string GetTextFromDDL(IWebElement element)
        {
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text;
        }

        public static List<string> GetAllOptionsFromDDL(IWebElement element)
        {
            IList<IWebElement> options = new SelectElement(element).Options;
            return options.Select(option => option.Text).ToList();
        }

       
    }
}
