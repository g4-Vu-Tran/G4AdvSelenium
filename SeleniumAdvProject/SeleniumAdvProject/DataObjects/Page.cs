using SeleniumAdvProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.DataObjects
{
    public class Page
    {
        private string _pageName;
        private string _parentPage;
        private int _numberOfColumns;
        private string _displayAfter;
        private bool _isPublic;

        public Page() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="parentPage">The parent page.</param>
        /// <param name="numberOfColumns">The number of columns.</param>
        /// <param name="displayAfter">The display after.</param>
        /// <param name="isPublic">if set to <c>true</c> [is public].</param>
        public Page(string pageName, string parentPage, int numberOfColumns, string displayAfter, bool isPublic)
        {
            this._pageName = pageName;
            this._parentPage = parentPage;
            this._numberOfColumns = numberOfColumns;
            this._displayAfter = displayAfter;
            this._isPublic = isPublic;
        }

        public string DisplayAfter
        {
            get { return _displayAfter; }
            set { _displayAfter = value; }
        }
        public int NumberOfColumns
        {
            get { return _numberOfColumns; }
            set { _numberOfColumns = value; }
        }
        public string ParentPage
        {
            get { return _parentPage; }
            set { _parentPage = value; }
        }
        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value; }
        }
        public bool IsPublic
        {
            get { return _isPublic; }
            set { _isPublic = value; }
        }
       
    }
}
