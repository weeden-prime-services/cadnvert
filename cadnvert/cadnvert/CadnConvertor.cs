﻿using System.IO;

namespace cadnvert
{
    public static class CadnConvertor
    {
        public static string ConvertToCsv(string cadnFile, string templateFile, string outputFileName)
        {
            try
            {
                using var writer = new StreamWriter(File.Create(outputFileName));
                using var reader = File.OpenText(cadnFile);
                string line = reader.ReadLine(); // ignore first line 
                writer.WriteLine(
                    TemplateParser.GetCsvHeaders(templateFile)); // write headers to the output file 
                while ((line = reader.ReadLine()) != null)
                {
                    line = ConvertToCsvLine(line);
                    writer.WriteLine(line);
                }
            }
            catch
            {
                return null;
            }

            return outputFileName;
        }

        private static string ConvertToCsvLine(string cadnLine)
        {
            var fields = cadnLine.Split("|");
            var csvLine = "";
            var prefix = "";
            foreach (var field in fields)
            {
                csvLine += $"{prefix}\"{field}\"";
                prefix = ",";
            }

            return csvLine;
        }
    }
}

