using System.Diagnostics;

namespace InventoryManagementCore.Application.Helpers
{
    public class DocumentConverter
    {
        public static async Task<byte[]> ConvertDocxToPdfAsync(byte[] docxBytes)
        {
            var tempName = Guid.NewGuid().ToString("N");
            await File.WriteAllBytesAsync($"Templates/{tempName}.docx", docxBytes);
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "libreoffice",
                    Arguments = $" --headless --convert-to pdf /app/Templates/{tempName}.docx --outdir  /app/TEMP",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = new Process { StartInfo = processInfo };
                process.Start();

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    throw new Exception("Pdf convertion failed");
                }

                var x = File.ReadAllBytes(@"TEMP/" + tempName + ".pdf");
                File.Delete($"Templates/{tempName}.docx");
                File.Delete(@"TEMP/" + tempName + ".pdf");
                return x;
            }

            catch (Exception ex)
            {
                throw new Exception("Pdf convertion failed " + ex.Message);
            }
        }

        public static async Task<byte[]> Combine2PdfAsync(byte[] pdfBytesOne, byte[] pdfBytesTwo)
        {
            var pdfOne = Guid.NewGuid().ToString("N");
            await File.WriteAllBytesAsync($"TEMP/{pdfOne}.pdf", pdfBytesOne);

            var pdfTwo = Guid.NewGuid().ToString("N");
            await File.WriteAllBytesAsync($"TEMP/{pdfTwo}.pdf", pdfBytesTwo);

            var output = Guid.NewGuid().ToString("N");
            byte[] outputBytes;
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "pdftk",
                    Arguments = $"/app/TEMP/{pdfOne}.pdf /app/TEMP/{pdfTwo}.pdf cat output /app/TEMP/{output}.pdf",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };


                var process = new Process { StartInfo = processInfo };
                process.Start();

                string processOut = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    throw new Exception("Pdf convertion failed");
                }

                outputBytes = File.ReadAllBytes(@"TEMP/" + output + ".pdf");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            File.Delete(@"TEMP/" + pdfOne + ".pdf");
            File.Delete(@"TEMP/" + output + ".pdf");
            File.Delete(@"TEMP/" + pdfTwo + ".pdf");
            return outputBytes;
        }

        public static async Task<byte[]> LocallyConvertDocxToPdfLocalyAsync(byte[] docxBytes)
        {
            var tempName = Guid.NewGuid().ToString("N");
            await File.WriteAllBytesAsync($"Templates/{tempName}.docx", docxBytes);
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\LibreOffice\program\soffice.exe",
                    Arguments = $" --headless --convert-to pdf:writer_pdf_Export C:/Users/AdarshS/source/repos/Document-Core/Document.API/Templates/{tempName}.docx --outdir  C:/Users/AdarshS/source/repos/Document-Core/Document.API/Templates",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = new Process { StartInfo = processInfo };
                process.Start();

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    throw new Exception("Pdf conversation failed");
                }

                var x = File.ReadAllBytes(@"Templates/" + tempName + ".pdf");
                File.Delete($"Templates/{tempName}.docx");
                File.Delete(@"Templates/" + tempName + ".pdf");
                return x;
            }

            catch (Exception ex)
            {
                throw new Exception("Pdf conversation failed " + ex.Message);
            }
        }

    }
}
