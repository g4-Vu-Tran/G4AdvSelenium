using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.DataObjects
{
    public class Page
    {
        private string pageName;
        private string parentPage;
        private int numberOfColumns;
        private string displayAfter;
        private bool _public;

        public Page() { }

        public Page(string pageName, string parentPage, int numberOfColumns, string displayAfter, bool _public)
        {
            this.pageName = pageName;
            this.parentPage = parentPage;
            this.numberOfColumns = numberOfColumns;
            this.displayAfter = displayAfter;
            this._public = _public;
        }

        public string DisplayAfter
        {
            get { return displayAfter; }
            set { displayAfter = value; }
        }
        public int NumberOfColumns
        {
            get { return numberOfColumns; }
            set { numberOfColumns = value; }
        }
        public string ParentPage
        {
            get { return parentPage; }
            set { parentPage = value; }
        }
        public string PageName
        {
            get { return pageName; }
            set { pageName = value; }
        }
        public bool _public1
        {
            get { return _public; }
            set { _public = value; }
        }
    }
}
