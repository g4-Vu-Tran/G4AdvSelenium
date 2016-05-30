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
using System.Threading;

namespace SeleniumAdvProject.PageObjects
{
    public class AddNewPanelPage : Popup
    {
        #region Locators

        static readonly By _cbbDataProfile = By.XPath("//select [@id='cbbProfile']");
        static readonly By _txtDisplayName = By.XPath("//input[@id='txtDisplayName']");
        static readonly By _txtChartTitle = By.XPath("//input[@id='txtChartTitle']");
        static readonly By _chbShowTitle = By.XPath("//input[@id='chkShowTitle']");
        static readonly By _cbbChartType = By.XPath("//select[@id='cbbChartType']");
        static readonly By _cbbCategory = By.XPath("//select[@id='cbbCategoryField']");
        static readonly By _cbbSeries = By.XPath("//select[@id='cbbSeriesField']/optgroup[@label='Action']");
        static readonly By _txtCategoryCaption = By.XPath("//input[@id='txtCategoryXAxis']");
        static readonly By _txtSeriesCaption = By.XPath("//input[@id='txtValueYAxis']");
        static readonly By _chbSeries = By.XPath("//input[@id='chkSeriesName']");
        static readonly By _chbCategories = By.XPath("//input[@id='chkCategoriesName']");
        static readonly By _chbValue = By.XPath("//input[@id='chkValue']");
        static readonly By _chbPercentage = By.XPath("//input[@id='chkPercentage']");
        static readonly By _rbChart = By.XPath("//input[@id='radPanelType0']");
        static readonly By _rbIndicator = By.XPath("//input[@id='radPanelType1']");
        static readonly By _rbReport = By.XPath("//input[@id='radPanelType2']");
        static readonly By _rbHeadMap = By.XPath("//input[@id='radPanelType3']");
        static readonly By _rb2D = By.XPath("//input[@id='rdoChartStyle2D']");
        static readonly By _rb3D = By.XPath("//input[@id='rdoChartStyle3D']");
        static readonly By _rbNone = By.XPath("//input[@id='radPlacementNone']");
        static readonly By _rbTop = By.XPath("//input[@id='radPlacementTop']");
        static readonly By _rbRight = By.XPath("//input[@id='radPlacementRight']");
        static readonly By _rbBottom = By.XPath("//input[@id='radPlacementBottom']");
        static readonly By _rlLeft = By.XPath("//input[@id='radPlacementLeft']");
        static readonly By _cbbSelectPage = By.XPath("//select[@id='cbbPages']");
        static readonly By _txtHeight = By.XPath("//input[@id='txtHeight']");
        static readonly By _txtFolder = By.XPath("//input[@id='txtFolder']");
        static readonly By _btnOKConfigurationPanel = By.XPath(" //input[@id='OK' and contains(@onclick,'Dashboard.addPanelToPage')]");
        #endregion

        #region Elements
        public ComboBox CbbDataProfile
        {
            get { return new ComboBox(_webDriver.FindElement(_cbbDataProfile)); }
        }
        public TextBox TxtDisplayName
        {
            get { return new TextBox(_webDriver.FindElement(_txtDisplayName)); }
        }
        public TextBox TxtChartTitle
        {
            get { return new TextBox(_webDriver.FindElement(_txtChartTitle)); }
        }
        public Checkbox ChbShowTitle
        {
            get { return new Checkbox(_webDriver.FindElement(_chbShowTitle)); }
        }
        public ComboBox CbbChartType
        {
            get { return new ComboBox(_webDriver.FindElement(_cbbChartType)); }
        }
        public RadioButton RbChart
        {
            get { return new RadioButton(_webDriver.FindElement(_rbChart)); }
        }
        public RadioButton RbReport
        {
            get { return new RadioButton(_webDriver.FindElement(_rbReport)); }
        }
        public RadioButton RbIndicator
        {
            get { return new RadioButton(_webDriver.FindElement(_rbIndicator)); }
        }
        public RadioButton RbHeadMap
        {
            get { return new RadioButton(_webDriver.FindElement(_rbHeadMap)); }
        }

        public ComboBox CbbCategory
        {
            get { return new ComboBox(_webDriver.FindElement(_cbbCategory)); }
        }
        public ComboBox CbbSeries
        {
            get { return new ComboBox(_webDriver.FindElement(_cbbSeries)); }
        }
        public RadioButton Rb2D
        {
            get { return new RadioButton(_webDriver.FindElement(_rb2D)); }
        }
        public RadioButton Rb3D
        {
            get { return new RadioButton(_webDriver.FindElement(_rb3D)); }
        }

        public TextBox TxtCategoryCaption
        {
            get { return new TextBox(_webDriver.FindElement(_txtCategoryCaption)); }
        }
        public TextBox TxtSeriesCaption
        {
            get { return new TextBox(_webDriver.FindElement(_txtSeriesCaption)); }
        }
        public Checkbox ChbSeries
        {
            get { return new Checkbox(_webDriver.FindElement(_chbSeries)); }
        }
        public Checkbox ChbCategories
        {
            get { return new Checkbox(_webDriver.FindElement(_chbCategories)); }
        }
        public Checkbox ChbValue
        {
            get { return new Checkbox(_webDriver.FindElement(_chbValue)); }
        }
        public Checkbox ChbPercentage
        {
            get { return new Checkbox(_webDriver.FindElement(_chbPercentage)); }
        }
        public RadioButton RbNone
        {
            get { return new RadioButton(_webDriver.FindElement(_rbNone)); }
        }
        public RadioButton RbTop
        {
            get { return new RadioButton(_webDriver.FindElement(_rbTop)); }
        }
        public RadioButton RbRight
        {
            get { return new RadioButton(_webDriver.FindElement(_rbRight)); }
        }
        public RadioButton RbBottom
        {
            get { return new RadioButton(_webDriver.FindElement(_rbBottom)); }
        }
        public RadioButton RbLeft
        {
            get { return new RadioButton(_webDriver.FindElement(_rlLeft)); }
        }
        public ComboBox CbbSelectPage
        {
            get { return new ComboBox(_webDriver.FindElement(_cbbSelectPage)); }
        }
        public TextBox TxtHeight
        {
            get { return new TextBox(_webDriver.FindElement(_txtHeight)); }
        }
        public RadioButton TxtFolder
        {
            get { return new RadioButton(_webDriver.FindElement(_txtFolder)); }
        }
        public Button BtnOKConfigurationPanel
        {
            get { return new Button(_webDriver.FindElement(_btnOKConfigurationPanel)); }
        }
        #endregion

        #region Methods
        public AddNewPanelPage() { }
        public AddNewPanelPage(IWebDriver webDriver) : base(webDriver) { }

        #region Private Methods

        /// <summary>
        /// Selects the legend.
        /// </summary>
        /// <param name="legend">The legend.</param>
        private void SelectLegend(string legend)
        {
            switch (legend)
            {
                case "Top":
                    RbTop.Click();
                    break;
                case "Right":
                    RbRight.Click();
                    break;
                case "Button":
                    RbRight.Click();
                    break;
                case "Left":
                    RbLeft.Click();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Selects the data labels.
        /// </summary>
        /// <param name="dataLabel">The data label.</param>
        private void SelectDataLabels(string[] dataLabel)
        {
            if ((dataLabel == null))
                return;
            for (int i = 0; i < dataLabel.Length; i++)
            {
                if (dataLabel[i].Equals("Series"))
                    ChbSeries.Click();
                if (dataLabel[i].Equals("Categories"))
                    ChbCategories.Click();
                if (dataLabel[i].Equals("Value"))
                    ChbValue.Click();
                if (dataLabel[i].Equals("Percentage"))
                    ChbPercentage.Click();
            }
        }
        /// <summary>
        /// Selects the style.
        /// </summary>
        /// <param name="style">The style.</param>
        private void SelectStyle(string style)
        {
            switch (style)
            {
                case "3D":
                    Rb3D.Click();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Gets the category status.
        /// </summary>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public string GetCategoryStatus()
        {
            string status = "enabled";
            string temp = CbbCategory.GetAttribute("disabled");
            switch (temp)
            {
                case "true":
                    status = "disabled";
                    break;
                case "false":
                    status = "enabled";
                    break;
            }
            return status;
        }

        /// <summary>
        /// Gets the category caption status.
        /// </summary>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public string GetCategoryCaptionStatus()
        {
            string status = "enabled";
            string temp = TxtCategoryCaption.GetAttribute("disabled");
            switch (temp)
            {
                case "true":
                    status = "disabled";
                    break;
                case "false":
                    status = "enabled";
                    break;
            }
            return status;
        }

        /// <summary>
        /// Gets the Series status.
        /// </summary>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public string GetSeriesStatus()
        {
            string status = "enabled";
            string temp = CbbSeries.GetAttribute("disabled");
            switch (temp)
            {
                case "true":
                    status = "disabled";
                    break;
                case "false":
                    status = "enabled";
                    break;
            }
            return status;
        }

        /// <summary>
        /// Gets the series caption status.
        /// </summary>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public string GetSeriesCaptionStatus()
        {
            string status = "enabled";
            string temp = TxtSeriesCaption.GetAttribute("disabled");
            switch (temp)
            {
                case "true":
                    status = "disabled";
                    break;
                case "false":
                    status = "enabled";
                    break;
            }
            return status;
        }

        /// <summary>
        /// Gets the categories CheckBox status.
        /// </summary>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public string GetCategoriesCheckBoxStatus()
        {
            string status = "enabled";
            string temp = ChbCategories.GetAttribute("disabled");
            switch (temp)
            {
                case "true":
                    status = "disabled";
                    break;
                case "false":
                    status = "enabled";
                    break;
            }
            return status;
        }

        /// <summary>
        /// Gets the series CheckBox status.
        /// </summary>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public string GetSeriesCheckBoxStatus()
        {
            string status = "enabled";
            string temp = ChbSeries.GetAttribute("disabled");
            switch (temp)
            {
                case "true":
                    status = "disabled";
                    break;
                case "false":
                    status = "enabled";
                    break;
            }
            return status;
        }

        /// <summary>
        /// Gets the value CheckBox status.
        /// </summary>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public string GetValueCheckBoxStatus()
        {
            string status = "enabled";
            string temp = ChbValue.GetAttribute("disabled");
            switch (temp)
            {
                case "true":
                    status = "disabled";
                    break;
                case "false":
                    status = "enabled";
                    break;
            }
            return status;
        }

        /// <summary>
        /// Gets the percentage CheckBox status.
        /// </summary>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public string GetPercentageCheckBoxStatus()
        {
            string status = "enabled";
            string temp = ChbPercentage.GetAttribute("disabled");
            switch (temp)
            {
                case "true":
                    status = "disabled";
                    break;
                case "false":
                    status = "enabled";
                    break;
            }
            return status;
        }
        /// <summary>
        /// Selects the type of the chart.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// Author: Tu Nguyen
        public AddNewPanelPage SelectChartType(string type)
        {
            switch (type)
            {
                case "Pie":
                    CbbChartType.SelectByIndex(0);
                    break;
                case "Single Bar":
                    CbbChartType.SelectByIndex(1);
                    break;
                case "Stacked Bar":
                    CbbChartType.SelectByIndex(2);
                    break;
                case "Group Bar":
                    CbbChartType.SelectByIndex(3);
                    break;
                case "Line":
                    CbbChartType.SelectByIndex(4);
                    break;
            }

            return this;
        }

        /// <summary>
        /// Closes the panel dialog.
        /// </summary>
        /// Author: Tu Nguyen
        public void ClosePanelDialog()
        {
            BtnCancel.Click();
        }

        #endregion
        /// <summary>
        /// Adds the chart.
        /// </summary>
        /// <param name="pChart">The p chart.</param>
        /// <returns></returns>
        public MainPage AddChart(Chart pChart)
        {
            RbChart.Click();
            CbbDataProfile.SelectByText(pChart.DataProfile);
            TxtDisplayName.SendKeys(pChart.DisplayName);
            TxtChartTitle.SendKeys(pChart.ChartTitle);
            CbbChartType.SelectByText(pChart.ChartType);
            if (pChart.ShowTitle)
                ChbShowTitle.Check();
            else
                ChbShowTitle.Uncheck();
            CbbCategory.SelectByText(pChart.Category);
            TxtCategoryCaption.SendKeys(pChart.CategoryCaption);
            CbbSeries.SelectByTextFromGroup(pChart.Series);
            TxtSeriesCaption.SendKeys(pChart.SeriesCaption);
            SelectLegend(pChart.Legend);
            SelectDataLabels(pChart.DataLabel);
            SelectStyle(pChart.Style);
            BtnOk.Click();
            SettingPanel(pChart.PageName, pChart.Height, pChart.Folder);
            return new MainPage(_webDriver);
        }

        /// <summary>
        /// Settings the panel.
        /// </summary>
        /// <param name="selectPage">The select page.</param>
        /// <param name="height">The height.</param>
        /// <param name="folder">The folder.</param>
        public void SettingPanel(string selectPage, int height, string folder)
        {
            if (selectPage != null)
            {
                CbbSelectPage.SelectByText(selectPage);
                TxtHeight.SendKeys(height.ToString());
                TxtFolder.SendKeys(folder);
                BtnOKConfigurationPanel.Click();
            }
        }


        /// <summary>
        /// Determines whether [is the list is sorted] [the specified combobox].
        /// </summary>
        /// <param name="combobox">The combobox.</param>
        /// <param name="sortType">Type of the sort.</param>
        /// <returns></returns>
        public bool IsTheListIsSorted(ComboBox combobox, string sortType)
        {
            IList<string> listValues = combobox.OptionStrings;

            int rowCount = listValues.Count;
            bool flag = false;

            // start from 1 to skip the table header row run to 'i < rowCount - 1' because we check
            // a pair of row at a time
            for (int i = 1; i < rowCount - 1; i++)
            {
                if (sortType == "DESC")
                {
                    if (listValues[i].CompareTo(listValues[i + 1]) >= 0)
                        flag = true;
                }
                else if (sortType == "ASC")
                {
                    if (listValues[i].CompareTo(listValues[i + 1]) <= 0)
                        flag = true;
                }
            }
            return flag;
        }

        


  
        #endregion


    }
}
