using AssetsApi.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AssetsApi.App_Code
{
    public class ExcelHelper
    {

        SpreadsheetDocument app;
        System.Reflection.Missing missing = System.Reflection.Missing.Value;
        WorkbookPart part;
        public ExcelHelper()
        {
            try
            {
                var path = Path.Combine(getPath(), "report.xlsx");

                if (File.Exists(path))
                {
                    var pt = Path.Combine(getPath(), "Reporte.xlsx");
                    if (File.Exists(pt))
                    {
                        File.Delete(pt);
                    }
                    File.Copy(path, pt);
                    path = pt;
                }

                app = SpreadsheetDocument.Open(path, true);

                part = app.WorkbookPart;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        private static string getPath()
        {
            var p = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //string path = Path.Combine(p.Substring(0,p.IndexOf("bin")), "App-Code");
            string path = Path.Combine(p, "App-Code");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public string Alpha(int index)
        {
            string value = string.Empty;
            if (index >= _alpha.Length)
            {
                while (index >= _alpha.Length)
                {
                    var mod = index / _alpha.Length;
                    value += _alpha[mod > 0 ? mod - 1 : mod];
                    index -= _alpha.Length;

                }
            }
            value += _alpha[index];
            return value;
        }

        public string ExportData(List<Asset> data) {
            string path =  Path.Combine(getPath(), "Reporte.xlsx");
            foreach (Sheet s in part.Workbook.Sheets) {
                insertData(data, s);
            }
            app.Save();
            app.Close();
            return path;
        }

        private void insertLastRow(SheetData sheet, int cols) {
            Row r = new Row();

            for (int i = 1; i <= cols; i++)
            {
                Cell c = new Cell();
                c.DataType = CellValues.String;
                c.CellValue = new CellValue("ULTIMA FILA");
                r.AppendChild(c);
            }
            sheet.AppendChild(r);
        }


        private void insertData(List<Asset> input, Sheet sheet)
        {
            try
            {
                Worksheet ws = ((WorksheetPart)part.GetPartById(sheet.Id)).Worksheet;

                SheetData sh = (SheetData)ws.GetFirstChild<SheetData>();

                int currentRow = 4;


                foreach(var a in input)
                {
                    Row r = new Row();
                    Cell c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"Tomo {a.Tomo}, Folio {a.Folio} Asiento {a.Assiento}");
                    r.AppendChild(c);

                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"{a.AssetId}");
                    r.AppendChild(c);

                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"{a.Description}");
                    r.AppendChild(c);

                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"{a.Brand}");
                    r.AppendChild(c);

                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"{a.Model}");
                    r.AppendChild(c);

                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"{a.Series}");
                    r.AppendChild(c);

                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"{a.State.Name}");
                    r.AppendChild(c);

                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"{a.Location.Name}");
                    r.AppendChild(c);

                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"{a.AcquisitionMethod.Name}");
                    r.AppendChild(c);

                    var na = a.CurrentPrice > 0 ? a.CurrentPrice.ToString() : @"N/R";
                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"{na}");
                    r.AppendChild(c);

                    c = new Cell();
                    c.DataType = CellValues.String;
                    c.CellValue = new CellValue($"N/R");
                    r.AppendChild(c);

                    //sh.InsertAt(r, currentRow);
                    sh.Append(r);
                    currentRow++;
                }

                insertLastRow(sh, 11);

                ws.Save();
                part.Workbook.Save();

            }
            catch (Exception ex)
            {
                File.Delete(Path.Combine(getPath(), "Reporte.xlsx"));
                throw ex;
            }
        }


        public string GetJsonData(string filePrefix = "")
        {
            string json = string.Empty;
            FileStream fs = null;
            try
            {
                string path = getPath();
                var files = Directory.GetFiles(path);
                var tempFile = files.FirstOrDefault(x => x.Contains(filePrefix) && x.EndsWith(".json"));

                if (!string.IsNullOrEmpty(tempFile) && File.Exists(tempFile))
                {

                    var lastMod = File.GetLastWriteTime(tempFile);
                    var diff = (DateTime.Now - lastMod).TotalHours;
                    //if (diff > 24)
                    //{
                    //    return string.Empty;
                    //}

                    fs = File.OpenRead(tempFile);

                    using (StreamReader sr = new StreamReader(fs))
                    {
                        json = sr.ReadToEnd();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (fs != null)
                {
                    fs.Close();
                }
            }

            return json;
        }

    }
}
