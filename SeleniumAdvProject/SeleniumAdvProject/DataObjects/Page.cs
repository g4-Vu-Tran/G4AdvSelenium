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
        /// Initializes a new instance of the Page class.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="parentPage">The parent page.</param>
        /// <param name="numberOfColumns">The number of columns.</param>
        /// <param name="displayAfter">The display after.</param>
        /// <param name="isPublic">if set to <c>true</c> [is public].</param>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public Page(string pageName, string parentPage, int numberOfColumns, string displayAfter, bool isPublic)
        {
            this._pageName = pageName;
            this._parentPage = parentPage;
            this._numberOfColumns = numberOfColumns;
            this._displayAfter = displayAfter;
            this._isPublic = isPublic;
        }

        /// <summary>
        /// Gets or sets the display after.
        /// </summary>
        /// <value>  The page which new page display after. </value>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public string DisplayAfter
        {
            get { return _displayAfter; }
            set { _displayAfter = value; }
        }
        /// <summary>
        /// Gets or sets the number of columns.
        /// </summary>
        /// <value>
        /// The number of columns.
        /// </value>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public int NumberOfColumns
        {
            get { return _numberOfColumns; }
            set { _numberOfColumns = value; }
        }
        /// <summary>
        /// Gets or sets the parent page.
        /// </summary>
        /// <value>
        /// The parent page.
        /// </value>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public string ParentPage
        {
            get { return _parentPage; }
            set { _parentPage = value; }
        }
        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        /// <value>
        /// The name of the page.
        /// </value>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value; }
        }
        /// <summary>
        /// Gets or sets the page is public or not
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is public; otherwise, <c>false</c>.
        /// </value>
        /// <author>Huong Huynh</author>s
        /// <date>05/25/2015</date>
        public bool IsPublic
        {
            get { return _isPublic; }
            set { _isPublic = value; }
        }
       
    }
}
