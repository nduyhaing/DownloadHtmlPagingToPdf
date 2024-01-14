using System.Net;

namespace DownloadHtmlPagingToPdf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = args[0]; // URL to download HTML from
            string outputPath = args[1]; // output folder path
            int maxPage = Convert.ToInt32(args[2]); // max page
            IronPdf.License.LicenseKey = "TODO"; 
            for (int i = 1; i <= maxPage; i++)
            {
                if (!File.Exists(outputPath + $"/file{i}.pdf")){
                    Console.WriteLine($"Proces page {i}");
                    try
                    {
                        string html = DownloadHtml(url + $"/page/{i}/");

                        if (!string.IsNullOrEmpty(html))
                        {
                            var x = ConvertHtmlToPdf(html, outputPath + $"/file{i}.pdf");
                            x.SaveAs(outputPath + $"/file{i}.pdf");
                        }
                        else
                        {
                            Console.WriteLine("Failed to download HTML from the provided URL.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    Thread.Sleep(2000);
                }
               
            }

        }

        static string DownloadHtml(string url)
        {
            using (var client = new System.Net.WebClient())
            {
                client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
                client.Headers.Add("Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                return client.DownloadString(url);
            }
        }

        static PdfDocument ConvertHtmlToPdf(string html, string outputPath)
        {
            var renderer = new HtmlToPdf();
            var pdf = renderer.RenderHtmlAsPdf(html);
            return pdf;
            //pdf.SaveAs(outputPath);
        }
    }
}
