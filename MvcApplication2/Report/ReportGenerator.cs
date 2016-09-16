using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Reporting.WebForms;



namespace MvcApplication2.Report
{
    public static class ReportGenerator 
    {
        public static string Report = "MvcApplication2.Report.ReportJv.rdlc";

        public static Task GeneratePDF(List<TransMain> datasource, string filePath)
        {
         
            return Task.Run(() =>
            {
                string binPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
                var assembly = Assembly.Load(System.IO.File.ReadAllBytes(binPath + "\\MvcApplication2.dll"));
                 
                using (Stream stream = assembly.GetManifestResourceStream(Report))
                {
                    var viewer = new ReportViewer();
                    viewer.LocalReport.EnableExternalImages = true;
                    viewer.LocalReport.LoadReportDefinition(stream);

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetContacts", datasource));

                    viewer.LocalReport.Refresh();

                    byte[] bytes = viewer.LocalReport.Render(
                        "PDF", null, out mimeType, out encoding, out filenameExtension,
                        out streamids, out warnings);

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
            });
        }
    }
}
