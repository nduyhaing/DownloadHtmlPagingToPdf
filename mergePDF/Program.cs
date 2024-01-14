using System;
using IronPdf;

namespace mergePDF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string outputPath = args[0];
            int maxPage = Convert.ToInt32(args[1]); // max page
            IronPdf.License.LicenseKey = "TODO";

            List<PdfDocument> documents = new List<PdfDocument>();
            for (int i = 1; i <= maxPage; i++)
            {
                documents.Add(PdfDocument.FromFile(outputPath + $"/file{i}.pdf"));
            }

            // Create a new PDF document
            var merged = PdfDocument.Merge(documents);

            // Save the merged PDF to the specified output file path
            merged.SaveAs(outputPath + $"/merge_final_lib.pdf");
        }
    }
}
