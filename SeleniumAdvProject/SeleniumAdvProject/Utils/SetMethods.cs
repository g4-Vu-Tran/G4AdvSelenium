using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;

namespace SeleniumAdvProject.Utils
{
    class SetMethods
    {
        public static void EnterText(IWebElement element, string value)
        {
            if (value != null)
                element.SendKeys(value);
        }

        public static void Clicks(IWebElement element)
        {
            element.Click();
        }

        public static void SelectDropDown(IWebElement element, string value)
        {
            if (value != null)
                new SelectElement(element).SelectByText(value);
        }

        public static void SelectOptionByLabel(IWebElement element, string label, string option)
        {
            if (label == null)
            {
                SelectDropDown(element, option);
                return;
            }
            IList<IWebElement> optgroups = element.FindElements(By.TagName("optgroup"));
            foreach (IWebElement optgroup in optgroups)
            {
                if (optgroup.GetAttribute("label").Equals(label))
                {
                    IList<IWebElement> options = optgroup.FindElements(By.TagName("option"));
                    foreach (IWebElement opt in options)
                    {
                        if (opt.GetAttribute("value").Equals(option))
                        {
                            opt.Click();
                            return;
                        }
                    }
                }
            }
        }

        public static void Check(IWebElement element)
        {
            if (!element.Selected)
                element.Click();
        }

        public static void UnCheck(IWebElement element)
        {
            if (element.Selected)
                element.Click();
        }

        public static void MouseTo(IWebElement element)
        {
            IWebDriver webDriver = ((RemoteWebElement)element).WrappedDriver;
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(element).Build().Perform();
        }


    }
}
