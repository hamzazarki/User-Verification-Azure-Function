using System;
using System.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using EPPlus.Core;
using EPPlus;
using System.IO;

namespace AzureB2CUserFunction.Helpers
{
    /*

public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
{
        using var pck = new OfficeOpenXml.ExcelPackage();
        using (var stream = File.OpenRead(path))
        {
            pck.Load(stream);
        }
        var ws = pck.Workbook.Worksheets.First();
        DataTable tbl = new DataTable();
        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        {
            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
        }
        var startRow = hasHeader ? 2 : 1;
        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        {
            var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
            DataRow row = tbl.Rows.Add();
            foreach (var cell in wsRow)
            {
                row[cell.Start.Colum-1] = cell.Text;
            }
        }
        return tbl;
    }

    public class ExcelReaderHelper
    {
        
        var dtContent = GetDataTableFromExcel(@"c:\temp\Text.xlsx");
//var res = from DataRow dr in dtContent.Rows
// where (string)dr[“Name”] == “Gil”
// select ((string)dr[“Section”]).FirstOrDefault();
foreach(DataRow dr in dtContent.Rows)
{
//Console.WriteLine(dr[“Name”].ToString());
}
   // Console.ReadLine();
}
    */
}
