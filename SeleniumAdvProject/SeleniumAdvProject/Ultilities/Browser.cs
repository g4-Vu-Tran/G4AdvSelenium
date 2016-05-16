using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.Ultilities
{
    public class Browser
    {
        private IWebDriver _webDriver;

        public Browser() { }

        public Browser(IWebDriver webDriver)
        {
            this._webDriver = webDriver;
        }


    }
}
