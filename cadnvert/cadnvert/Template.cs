﻿using NPOI.SS.UserModel;
using System.IO;
using System.Net;

namespace cadnvert
{
    public static class TemplateParser
    {
        public static string GetCsvHeaders(string templatePath)
        {
            IWorkbook wb;


            using (WebClient webClient = new WebClient())
            {
                using (Stream stream = webClient.OpenRead(templatePath))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        wb = WorkbookFactory.Create(sr.BaseStream);
                    }
                }
            }

            var sheet = wb.GetSheetAt(0);// get the first / default sheet 
            var headers = "";

            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                var prefix = ",";
                if (row == 1)
                    prefix = "";
                var xlRow = sheet.GetRow(row);
                if (sheet.GetRow(row) == null) //null is when the row only contains empty cells 
                {
                    headers += prefix;
                    continue;
                }
                else
                {
                    var cellValue = sheet.GetRow(row).GetCell(4).StringCellValue;
                    if (cellValue.ToUpper() == "SPACES")
                    {
                        continue;
                    }
                    headers += prefix + cellValue;
                }
            }
            return headers;
        }
    }
}