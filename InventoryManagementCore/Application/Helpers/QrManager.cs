using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;

namespace InventoryManagementCore.Application.Helpers
{
    public class QRManager
    {
        private string heading;
        private string footer;
        private string qrData;
        private Bitmap qrCodeWithHeadingAndFooter;

        public void LoadQRInfo(string heading, string footer, string qrData)
        {
            this.heading = heading;
            this.footer = footer;
            this.qrData = qrData;
        }

        public void GenerateQRCode()
        {
            if (string.IsNullOrEmpty(qrData))
            {
                throw new InvalidOperationException("QR data cannot be null or empty.");
            }
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            qrCodeWithHeadingAndFooter = AddHeadingAndFooterToQRCode(qrCodeImage);
        }

        public byte[] GetQRCodeByteArray()
        {
            if (qrCodeWithHeadingAndFooter == null)
            {
                throw new InvalidOperationException("QR code has not been generated yet.");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeWithHeadingAndFooter.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private Bitmap AddHeadingAndFooterToQRCode(Bitmap qrCodeImage)
        {
            int width = qrCodeImage.Width;
            int height = qrCodeImage.Height + 100; // Adding space for the heading and footer

            Bitmap finalImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(finalImage))
            {
                g.Clear(Color.White);

                // Draw the heading
                if (!string.IsNullOrEmpty(heading))
                {
                    using (Font font = new Font("Arial", 20, FontStyle.Bold))
                    {
                        SizeF textSize = g.MeasureString(heading, font);
                        PointF textPosition = new PointF((width - textSize.Width) / 2, 10); // Centering the text
                        g.DrawString(heading, font, Brushes.Black, textPosition);
                    }
                }

                // Draw the QR code
                g.DrawImage(qrCodeImage, new Point(0, 50)); // QR code below the heading

                // Draw the footer
                if (!string.IsNullOrEmpty(footer))
                {
                    using (Font font = new Font("Arial", 20, FontStyle.Bold))
                    {
                        SizeF textSize = g.MeasureString(footer, font);
                        PointF textPosition = new PointF((width - textSize.Width) / 2, qrCodeImage.Height + 60); // Centering the text
                        g.DrawString(footer, font, Brushes.Black, textPosition);
                    }
                }
            }

            return finalImage;
        }
    }
}
