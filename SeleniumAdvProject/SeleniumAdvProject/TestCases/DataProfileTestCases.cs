using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumAdvProject.PageObjects;
using SeleniumAdvProject.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.Text.RegularExpressions;
using SeleniumAdvProject.DataObjects;

namespace SeleniumAdvProject.TestCases
{
    [TestClass]
    public class DataProfileTestCases : BaseTestCase
    {
        /// <summary>
        /// Verify that all Pre-set Data Profiles are populated correctly
        /// </summary>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        [TestMethod]
        public void DA_DP_TC065()
        {
            Console.WriteLine("DA_DP_TC065 - Verify that all Pre-set Data Profiles are populated correctly");

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click Login
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //5. Click Administer->Data Profiles
            //VP. Check Pre-set Data Profile are populated correctly in profiles page
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();
            Assert.IsTrue(DPPage.IsPresetDataProfilePopulated(), "Pre-set Data Profile aren't populated correctly");

        }

        /// <summary>
        /// Verify that all Pre-set Data Profiles are populated correctly
        /// </summary>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        [TestMethod]
        public void DA_DP_TC066()
        {
            Console.WriteLine("DA_DP_TC066 - Verify that all Pre-set Data Profiles are populated correctly");

            //1. Navigate to Dashboard login page
            //2. Select a specific repository 
            //3. Enter valid Username and Password
            //4. Click Login
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //5. Click Administer->Data Profiles
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();

            //VP. Check there is no 'Delele' or 'Edit' link appears in Action section of Pre-set Data Profiles
            //6. Click on Pre-set Data Profile name
            //VP. Check there is no link on Pre-set Data Profile name
            //VP. Check there is no checkbox appears in the left of Pre-set Data Profiles
            foreach (string presetDP in Constants.presetDataProfile)
            {
                Assert.IsFalse(DPPage.IsLinkExists(presetDP, "Edit"), string.Format("Edit link is exists for preset data profile \"{0}\"", presetDP));
                Assert.IsFalse(DPPage.IsLinkExists(presetDP, "Delete"), string.Format("Delete link is exists for preset data profile \"{0}\"", presetDP));
                Assert.IsFalse(DPPage.IsLinkExists(presetDP, presetDP), string.Format("{0} link is exists for preset data profile \"{0}\"", presetDP));
                Assert.IsFalse(DPPage.IsCheckBoxExists(presetDP), string.Format("CheckBox is exists for preset data profile \"{0}\"", presetDP));
            }
        }

        /// <summary>
        /// Verify that all fields are displayed correctly
        /// </summary>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        [TestMethod]
        public void DA_DP_TC077()
        {
            Console.WriteLine("DA_DP_TC077 - Verify that all fields are displayed correctly");

            //1. Navigate to Dashboard login page
            //2. Log in specific repository with valid account
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Click on "Administer" llink
            //4. Click on "Data Profiles" link
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();

            //5. Click "Add New" link
            //6. Enter Name field
            //7. Click Item Type listbox
            //8. Select specific Item Type
            //9. Click on "Next" button
            //VP. Check all fields are displayed correctly 
            DisplayFieldsPage displayFieldsPage = (DisplayFieldsPage)DPPage.GoToGeneralSettingPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), null, null);
            string[] displayField = { "Name", "Description", "Source", "Assigned user", "Status", "Last updated by", "Created by", "Check out by", "Location", "Recent result", "Version", "Priority", "Last update date", "Creation date", "Notes", "URL" };
            bool actualresult = false;
            foreach (string field in displayField)
            {
                actualresult = displayFieldsPage.IsFieldDisplayed(field);
                if (!actualresult)
                    break;
            }
            Assert.IsTrue(actualresult, "Display Fields displays uncorrectly");
        }

        /// <summary>
        /// Verify that all fields are pre-fixed with check boxes
        /// </summary>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        [TestMethod]
        public void DA_DP_TC078()
        {
            Console.WriteLine("DA_DP_TC078 - Verify that all fields are pre-fixed with check boxes");

            //1. Log in Dashboard
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //2. Navigate to Data Profiles page
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();

            //3. Click on Data Profile "A"
            //4. Click on Next button
            //5. Check each field listed is prefixed with checkbox
            DisplayFieldsPage displayFieldsPage = (DisplayFieldsPage)DPPage.GoToGeneralSettingPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), null, null);
            string[] displayField = { "Name", "Description", "Source", "Assigned user", "Status", "Last updated by", "Created by", "Check out by", "Location", "Recent result", "Version", "Priority", "Last update date", "Creation date", "Notes", "URL" };
            bool actualresult = false;
            foreach (string field in displayField)
            {
                actualresult = displayFieldsPage.IsCheckBoxFieldDisplayed(field);
                if (!actualresult)
                    break;
            }
            Assert.IsTrue(actualresult, "Display Fields displays uncorrectly");

        }

        /// <summary>
        /// Verify that Check All / Uncheck All Links are working correctly
        /// </summary>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        [TestMethod]
        public void DA_DP_TC079()
        {
            Console.WriteLine("DA_DP_TC079 - Verify that Check All / Uncheck All Links are working correctly");

            //1. Navigate to Dashboard login page
            //2. Log in with valid account
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //3. Click on "Administer" llink
            //4. Click on "Data Profiles" link
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();

            //5. Click "Add New" link
            //6. Enter Name field
            //7. Click on "Next" button
            DisplayFieldsPage displayFieldsPage = (DisplayFieldsPage)DPPage.GoToGeneralSettingPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), null, null);

            //8. Click on "Check All" link
            //VP.Verify that all checkbox is checked
            displayFieldsPage.ClickCheckAll();
            string[] displayField = { "Name", "Description", "Source", "Assigned user", "Status", "Last updated by", "Created by", "Check out by", "Location", "Recent result", "Version", "Priority", "Last update date", "Creation date", "Notes", "URL" };
            bool actualresult = false;
            foreach (string field in displayField)
            {
                actualresult = displayFieldsPage.IsCheckBoxChecked(field);
                if (!actualresult)
                    break;
            }
            Assert.IsTrue(actualresult, "All checkboxes aren't checked");

            //9. Click on "Uncheck All" link
            //VP. Verify that all checkbox is unchecked
            displayFieldsPage.ClickUnCheckAll();
            foreach (string field in displayField)
            {
                actualresult = displayFieldsPage.IsCheckBoxChecked(field);
                if (actualresult)
                    break;
            }
            Assert.IsFalse(actualresult, "All checkboxes aren't unchecked");

        }

        /// <summary>
        /// Verify that all fields are listed in the "Field" dropped down menu
        /// </summary>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        [TestMethod]
        public void DA_DP_TC080()
        {
            Console.WriteLine("DA_DP_TC080 - Verify that all fields are listed in the \"Field\" dropped down menu");

            //1. Log in Dashboard
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //2. Navigate to Data Profiles page
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();

            //5. Click "Add New" link
            //6. Enter Name field
            //7. Click on "Next" button
            DisplayFieldsPage displayFieldsPage = (DisplayFieldsPage)DPPage.GoToGeneralSettingPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), null, null);

            
            //3. Input to "Name *" field
            //4. Click "Item Type" dropped down menu and choose Test Modules
            //5. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            //6. Navigate to General Settings page
            //7. Click "Item Type" dropped down menu and choose Test Cases
            //8. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            //9. Navigate to General Settings page
            //10. Click "Item Type" dropped down menu and choose Test Objectives
            //11. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            //12. Navigate to General Settings page
            //13. Click "Item Type" dropped down menu and choose Data Sets
            //14. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            //15. Navigate to General Settings page
            //16. Click "Item Type" dropped down menu and choose Actions
            //17. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            //18. Navigate to General Settings page
            //19. Click "Item Type" dropped down menu and choose Interface Entities
            //20. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            //22. Navigate to General Settings page
            //23. Click "Item Type" dropped down menu and choose Test Results
            //24. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            //25. Navigate to General Settings page
            //26. Click "Item Type" dropped down menu and choose Test Case Results
            //27. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            
        }
        /// <summary>
        /// DA_DP_TC072 - Verify that all data profile types are listed under \"Item Type\" dropped down menu
        /// </summary>
        /// <author>Huong Huynh</author>
        /// <date>06/08/2016</date>
        [TestMethod]
        public void DA_DP_TC072()
        {
            Console.WriteLine("DA_DP_TC072 - Verify that all data profile types are listed under \"Item Type\" dropped down menu");

            //1 Navigate to Dashboard login page
            //2 Select a specific repository 
            //3 Enter valid Username and Password
            //4 Click Login
            //5 Click Administer->Data Profiles
            //6 Click 'Add New' link
            string []itemsList = new string[]{"Test Modules","Test Cases","Test Objectives","Data Sets","Actions","Interface Entities","Test Results","Test Case Results"};
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            DataProfilePage dataProfilePage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password).GoToDataProfilePage();
            GeneralSettingsPage genralSettingPage = dataProfilePage.GoToGeneralSettingPage();

           //VP All data profile types are listed under "Item Type" dropped down menu
            //+ Test Modules
            //+ Test Cases
            //+ Test Objectives
            //+ Data Sets
            //+ Actions
            //+ Interface Entities
            //+ Test Results
            //+ Test Case Results
            Assert.AreEqual(true, genralSettingPage.isComboboxContainsItems(genralSettingPage.CbbItemType, itemsList));

            genralSettingPage.Logout();
        }
        /// <summary>
        /// DA_DP_TC072 - Verify that all data profile types are listed under \"Item Type\" dropped down menu
        /// </summary>
        /// <author>Huong Huynh</author>
        /// <date>06/08/2016</date>
        [TestMethod]
        public void DA_DP_TC073()
        {
            Console.WriteLine("DA_DP_TC073 - Verify that all data profile types are listed in priority order under \"Item Type\" dropped down menu");

            //1 Log in Dashboard
            //2 Navigate to Data Profiles page
            //3 Click on "Add New"
            //4 Click on "Item Type" dropped down menu          

            string[] itemsList = new string[] { "Test Modules", "Test Cases", "Test Objectives", "Data Sets", "Actions", "Interface Entities", "Test Results", "Test Case Results" };
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            DataProfilePage dataProfilePage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password).GoToDataProfilePage();
            GeneralSettingsPage genralSettingPage = dataProfilePage.GoToGeneralSettingPage();

            //VP "Item Type" items are listed in priority order: Test Modules>Test Cases> Test Objectives> Data Sets> Actions> Interface Entities> Test Results> Test Cases results

            Assert.AreEqual(true, genralSettingPage.isComboboxContainsItems(genralSettingPage.CbbItemType, itemsList));

            genralSettingPage.Logout();
        }

    }
}

