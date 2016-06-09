using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.Ultilities.Controls
{
    class Span : BaseControl
    {
        public Span() { }
        public Span(string xPath) : base(By.XPath(xPath)) { }
        public Span(IWebElement element) : base(element) { }
        public Span(By by) : base(by) { }
        public Span(IWebDriver webDriver, By by) : base(webDriver, by) { }

    }
}
