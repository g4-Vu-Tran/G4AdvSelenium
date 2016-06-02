using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.Utils
{
    public class TableData
    {
        private static List<TableDataColection> _tableDataColections = new List<TableDataColection>();

        private static void ReadTable(IWebElement table)
        {
            //Get all the columns from the table
            var columns = table.FindElements(By.TagName("th"));

            //Get all the rows from the table
            var rows = table.FindElements(By.TagName("tr"));

            //Create row index
            int rowIndex = 0;

            foreach (var row in rows)
            {
                int colIndex = 0;
                var colDatas = row.FindElements(By.TagName("td"));

                foreach (var colValue in colDatas)
                {
                    _tableDataColections.Add(new TableDataColection
                    {
                        RowNumber = rowIndex,
                        ColumnName = columns[colIndex].Text != "" ?
                                      columns[colIndex].Text : colIndex.ToString(),
                        ColumnValue = colValue.Text,
                        ColumnSpecialValues = colValue.Text != "" ? colValue.FindElements(By.TagName("input")) : colValue.FindElements(By.TagName("a"))
                    });

                    //Move to next column
                    colIndex++;
                }
                rowIndex++;
            }
        }

        public static string ReadCell(IWebElement table, string columnName, int rowNumber)
        {
            ReadTable(table);
            var data = (from e in _tableDataColections
                        where e.ColumnName == columnName && e.RowNumber == rowNumber
                        select e.ColumnValue).SingleOrDefault();
            return data;
        }

        public static void PerformActionOnCell(IWebElement table, string columnIndex, string refColumnName, string refColumnValue, string controlToOperate = null)
        {
            ReadTable(table);
            foreach (int rowNumber in GetRowNumber(refColumnName, refColumnValue))
            {
                var cell = (from e in _tableDataColections
                            where e.ColumnName == columnIndex && e.RowNumber == rowNumber
                            select e.ColumnSpecialValues).SingleOrDefault();


                if (controlToOperate != null && cell != null)
                {
                    var returnedControl = (from c in cell
                                           where c.GetAttribute("value") == controlToOperate
                                           select c).SingleOrDefault();
                    returnedControl.Click();
                }
                else
                {
                    cell.First().Click();
                }
            }
        }

        private static IEnumerable GetRowNumber(string columnName, string columnValue)
        {
            foreach (var table in _tableDataColections)
            {
                if (table.ColumnName == columnName && table.ColumnValue == columnValue)
                    yield return table.RowNumber;
            }
        }
    }

    public class TableDataColection
    {
        public int RowNumber { get; set; }

        public string ColumnName { get; set; }

        public string ColumnValue { get; set; }

        public IEnumerable<IWebElement> ColumnSpecialValues { get; set; }

    }
}
