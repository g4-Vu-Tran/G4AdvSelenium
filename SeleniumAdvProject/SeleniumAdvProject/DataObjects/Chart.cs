using SeleniumAdvProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.DataObjects
{
    public class Chart:Panel
    {
        private string _chartTitle;
        private string _chartType;
        private string _category;
        private string _categoryCaption;
        private string _series;
        private string _seriesCaption;
        private string _legend;
        private string[] _dataLabel;
        private string _style;
        private bool _showTitle;
        public Chart() 
        {
            this._style = "2D";
            this._height =400;
            //this._dataLabel = new string[] {"None"}; 
            this._showTitle = false;
            this._dataLabel = new string[]{};
        }
        public Chart(string displayName, string series, string pageName)
        {
            this._displayName = displayName;
            this._series = series;
            this._pageName = pageName;
            this._style = "2D";
            this._height = 400;
        }

        public Chart(Chart chart):base(chart._dataProfile,chart._displayName,chart._pageName, chart._height,chart._folder) 
        {             
            this._chartTitle = chart.ChartTitle;
            this._chartType = chart.ChartType;
            this._category = chart.Category;
            this._categoryCaption = chart.CategoryCaption;
            this._series = chart.Series;
            this._seriesCaption = chart.SeriesCaption;
            this._legend = chart.Legend;
            this._dataLabel = chart.DataLabel;
            this._style = chart.Style;
            this._showTitle = chart.ShowTitle;
        }
        public Chart(string dataProfile, string displayName, string pageName, int height, string folder, string chartTitle,
            string chartType, string category, string categoryCaption, string series,string seriesCaption, string legend,
            string[] dataLabel, string style, bool showTitle):base(dataProfile,displayName,pageName,height,folder)
        {
            this._chartTitle = chartTitle;
            this._chartType = chartType;
            this._category = category;
            this._categoryCaption = categoryCaption;
            this._series = series;
            this._seriesCaption = seriesCaption;
            this._legend = legend;
            this._dataLabel = dataLabel;
            this._style = style;
            this._showTitle = showTitle;
        }

        public string ChartTitle
        {
            get { return _chartTitle; }
            set { _chartTitle = value; }
        }
        public string ChartType
        {
            get { return _chartType; }
            set { _chartType = value; }
        }
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
        public string CategoryCaption
        {
            get { return _categoryCaption; }
            set { _categoryCaption = value; }
        }
        public string Series
        {
            get { return _series; }
            set { _series = value; }
        }
        public string SeriesCaption
        {
            get { return _seriesCaption; }
            set { _seriesCaption = value; }
        }
        public string Legend
        {
            get { return _legend; }
            set { _legend = value; }
        }
        public string[] DataLabel
        {
            get { return _dataLabel; }
            set { _dataLabel = value; }
        }
         public string Style
        {
            get { return _style; }
            set { _style = value; }
        }
         public bool ShowTitle
        {
            get { return _showTitle; }
            set { _showTitle = value; }
        }
               
    }
}
