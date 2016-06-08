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

            //3. Input to "Name *" field
            //4. Click "Item Type" dropped down menu and choose Test Modules
            //5. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            DisplayFieldsPage displayFieldsPage = (DisplayFieldsPage)DPPage.GoToGeneralSettingPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), "test modules", null);
            SortFieldsPage SortFieldsPage = displayFieldsPage.GoToSortFieldsPage();
            string[] expectedItemType1 = { "--- Select field ---", "Name", "Location", "Description", "Recent result", "Source", "Version", "Assigned user", "Priority", "Status", "Last update date", "Last updated by", "Creation date", "Created by", "Notes", "Check out by", "URL" };
            Assert.IsTrue(SortFieldsPage.IsItemTypeListed(expectedItemType1), "\"Item Type\" item aren't listed under the \"Field\" dropped down menu");

            //6. Navigate to General Settings page
            //7. Click "Item Type" dropped down menu and choose Test Cases
            //8. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            SortFieldsPage.GoToGeneralSettingsPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), "test cases", null);
            displayFieldsPage.GoToSortFieldsPage();
            string[] expectedItemType2 = { "--- Select field ---", "ID", "Location", "Title", "Recent result", "Notes", "Source", "URL" };
            Assert.IsTrue(SortFieldsPage.IsItemTypeListed(expectedItemType2), "\"Item Type\" item aren't listed under the \"Field\" dropped down menu");

            //9. Navigate to General Settings page
            //10. Click "Item Type" dropped down menu and choose Test Objectives
            //11. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            SortFieldsPage.GoToGeneralSettingsPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), "test objectives", null);
            displayFieldsPage.GoToSortFieldsPage();
            Assert.IsTrue(SortFieldsPage.IsItemTypeListed(expectedItemType2), "\"Item Type\" item aren't listed under the \"Field\" dropped down menu");

            //12. Navigate to General Settings page
            //13. Click "Item Type" dropped down menu and choose Data Sets
            //14. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            SortFieldsPage.GoToGeneralSettingsPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), "data sets", null);
            displayFieldsPage.GoToSortFieldsPage();
            string[] expectedItemType3 = { "--- Select field ---", "Name", "Location", "Description", "Version", "Assigned user", "Status", "Last update date", "Last updated by", "Creation date", "Created by", "Notes", "Check out by", "URL" };
            Assert.IsTrue(SortFieldsPage.IsItemTypeListed(expectedItemType3), "\"Item Type\" item aren't listed under the \"Field\" dropped down menu");

            //15. Navigate to General Settings page
            //16. Click "Item Type" dropped down menu and choose Actions
            //17. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            SortFieldsPage.GoToGeneralSettingsPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), "actions", null);
            displayFieldsPage.GoToSortFieldsPage();
            string[] expectedItemType4 = { "--- Select field ---", "Name", "Location", "Description", "Version", "Assigned user", "Status", "Last update date", "Last updated by", "Creation date", "Created by", "Notes", "Check out by", "URL" };
            Assert.IsTrue(SortFieldsPage.IsItemTypeListed(expectedItemType4), "\"Item Type\" item aren't listed under the \"Field\" dropped down menu");

            //18. Navigate to General Settings page
            //19. Click "Item Type" dropped down menu and choose Interface Entities
            //20. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            SortFieldsPage.GoToGeneralSettingsPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), "interface entities", null);
            displayFieldsPage.GoToSortFieldsPage();
            string[] expectedItemType5 = { "--- Select field ---", "Name", "Location", "Description", "Version", "Assigned user", "Status", "Last update date", "Last updated by", "Creation date", "Created by", "Notes", "Check out by", "URL" };
            Assert.IsTrue(SortFieldsPage.IsItemTypeListed(expectedItemType5), "\"Item Type\" item aren't listed under the \"Field\" dropped down menu");

            //22. Navigate to General Settings page
            //23. Click "Item Type" dropped down menu and choose Test Results
            //24. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            SortFieldsPage.GoToGeneralSettingsPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), "test results", null);
            displayFieldsPage.GoToSortFieldsPage();
            string[] expectedItemType6 = { "--- Select field ---", "Name", "Location", "Reported by", "Date of run", "Start time", "End time", "Duration", "Comment", "Variation", "Result", "Passed", "Failed", "Warnings", "Errors", "Automated/Manual", "Run Machine", "Notes", "URL", "Baseline", "OS", "Device OS", "Device Name", "Build Number", "AUTVersion" };
            Assert.IsTrue(SortFieldsPage.IsItemTypeListed(expectedItemType6), "\"Item Type\" item aren't listed under the \"Field\" dropped down menu");

            //25. Navigate to General Settings page
            //26. Click "Item Type" dropped down menu and choose Test Case Results
            //27. Navigate to Sort Fields page
            //VP. Check all fields of selected "Item Type" item are listed under the "Field" dropped down menu 
            SortFieldsPage.GoToGeneralSettingsPage().SetGeneralSettingsValue(CommonAction.GenerateDataProfileName(), "test case results", null);
            displayFieldsPage.GoToSortFieldsPage();
            string[] expectedItemType7 = { "--- Select field ---", "Name", "Location", "Description", "Version", "Assigned user", "Status", "Last update date", "Last updated by", "Creation date", "Created by", "Notes", "Check out by", "URL" };
            Assert.IsTrue(SortFieldsPage.IsItemTypeListed(expectedItemType7), "\"Item Type\" item aren't listed under the \"Field\" dropped down menu");

        }

        /// <summary>
        /// Verify that Data Profiles are listed alphabetically
        /// </summary>
        /// Author: Tu Nguyen
        [TestMethod]
        public void DA_DP_TC067()
        {
            Console.WriteLine("DA_DP_TC067 - Verify that Data Profiles are listed alphabetically");

            //1. Log in Dashboard
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //2. Click Administer->Data Profiles
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();

            //VP: Check Data Profiles are listed alphabetically
            bool actualResult = DPPage.IsDataProfileContentSorted("panel_tag1", "ASC");
            Assert.AreEqual(true, actualResult, "Data Profiles are not listed alphabetically");

        }

        /// <summary>
        /// Verify that Check Boxes are only present for non-preset Data Profiles.
        /// </summary>
        [TestMethod]
        public void DA_DP_TC068()
        {
            Console.WriteLine("DA_DP_TC068 - Verify that Check Boxes are only present for non-preset Data Profiles");

            //1. Log in Dashboard
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //2. Click Administer->Data Profiles
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();

            //3. Create a new Data Profile
            DPPage.GoToGeneralSettingPage().SetGeneralSettingsValue("TuProfile", null, null, "Finish");

            //VP: Check Check Boxes are only present for non-preset Data Profiles
            bool isProfileCheckBox = DPPage.IsCheckBoxExists("TuProfile");
            Assert.AreEqual(true, isProfileCheckBox, "This Data Profile does not have checkbox");

            //Post-Condition
            DPPage.DeleteAllDataProfiles();
        }

        /// <summary>
        /// Verify that user is unable to proceed to next step or finish creating data profile if  "Name *" field is left empty
        /// </summary>
        /// Author: Tu Nguyen
        [TestMethod]
        public void DA_DP_TC069()
        {
            Console.WriteLine("DA_DP_TC069 - Verify that user is unable to proceed to next step or finish creating data profile if  \"Name *\" field is left empty");

            //1. Log in Dashboard
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //2. Navigate to Data Profiles page
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();

            //3. Click on "Add New"
            //4. Click on "Next Button"
            //VP: dialog message "Please input profile name" appears
            string actualMessage = DPPage.GoToGeneralSettingPage().SetGeneralSettingsWithExpectedError(" ");
            Assert.AreEqual("Please input profile name.", actualMessage,
                           string.Format("Failed! Actual message is: {0}", actualMessage));
            DPPage.ConfirmDialog("OK");
            
            //5. Click on "Finish Button"
            //VP: dialog message "Please input profile name" appears
            string actualMessageFinish = DPPage.GoToGeneralSettingPage().SetGeneralSettingsWithExpectedError(" ", "Finish");
            Assert.AreEqual("Please input profile name.", actualMessageFinish,
                           string.Format("Failed! Actual message is: {0}", actualMessageFinish));
            DPPage.ConfirmDialog("OK");
        }

        /// <summary>
        /// Verify that special characters ' /:*?<>|"#[ ]{}=%; 'is not allowed for input to "Name *" field
        /// </summary>
        /// Author: Tu Nguyen
        [TestMethod]
        public void DA_DP_TC070()
        {
            Console.WriteLine("DA_DP_TC070 - Verify that special characters ' /:*?<>|\"#[ ]{}=%; 'is not allowed for input to \"Name *\" field");

            //1. Log in Dashboard
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);
             //2. Navigate to Data Profiles page
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();

            //3. Click on "Add New"
            //4. Input special character
            //5. Click on "Next Button"
            string actualMessage = DPPage.GoToGeneralSettingPage().SetGeneralSettingsWithExpectedError("a123:\"/{}!@$\u005c", "Next");

            //VP: Check dialog message indicates invalid characters: /:*?<>|"#[ ]{}=%; is not allowed as input for name field appears
            Assert.AreEqual("Invalid name. The name cannot contain high ASCII characters or any of the following characters: /:*?<>|\"#[]{}=%;"
                , actualMessage, string.Format("Failed! Actual message is: {0}", actualMessage));
            DPPage.ConfirmDialog("OK");
        }
        /// <summary>
        /// Verify that Data Profile names are not case sensitive
        /// </summary>
        /// Author: Tu Nguyen
        [TestMethod]
        public void DA_DP_TC071()
        {
            Console.WriteLine("DA_DP_TC071 - Verify that Data Profile names are not case sensitive");

            //1. Log in Dashboard
            LoginPage loginPage = new LoginPage(_webDriver).Open();
            MainPage mainPage = loginPage.Login(Constants.Repository, Constants.UserName, Constants.Password);

            //Pre-Condition: Create a data profile
            DataProfilePage DPPage = mainPage.GoToDataProfilePage();
            DPPage.GoToGeneralSettingPage().SetGeneralSettingsValue("tu", null, null, "Finish");

            //2. Navigate to Data Profiles page
            //3. Click on "Add New"
            //4. Input charater uppercase name into "Name *" field
            //5. Click "Next" button 
            string actualMessage = DPPage.GoToGeneralSettingPage().SetGeneralSettingsWithExpectedError("TU", "Next");

            //VP: Check dialog message "Data Profile name already exists"
            Assert.AreEqual("Data profile name already exists."
                , actualMessage, string.Format("Failed! Actual message is: {0}", actualMessage));
            DPPage.ConfirmDialog("OK");

            //Post-condition
            mainPage.GoToDataProfilePage();
            DPPage.DeleteAllDataProfiles();
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
        }

    }
}

