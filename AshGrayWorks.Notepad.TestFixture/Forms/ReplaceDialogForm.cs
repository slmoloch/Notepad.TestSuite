using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AshGrayWorks.UiTests.Base;
using TestStack.White.UIItems;

namespace AshGrayWorks.Notepad.TestFixture.Forms
{
    public class ReplaceDialogForm : FlowForm
    {
        [ByAutomationId("1152")]
        public TextBox FindWhat { get; set; }

        [ByAutomationId("1153")]
        public TextBox ReplaceWith { get; set; }

        [ByAutomationId("1024")]
        public Button Replace { get; set; }

        [ByAutomationId("1025")]
        public Button ReplaceAll { get; set; }

        [ByAutomationId("1")]
        public Button FindNext { get; set; }

        [ByAutomationId("2")]
        public Button Cancel { get; set; }

        public void Close()
        {
            Wnd.Close();
        }

        public byte[] GetAppearence()
        {
            return Encode(Wnd.VisibleImage);
        }

        private static byte[] Encode(Bitmap myBitmap)
        {
            myBitmap = new Bitmap(@"F:\gitw\workspace\UiTesting\Notepad-Test-Suite\Notepad.TestSuite\Notepad.TestSuite\ReplaceTextTest\1.bmp");

            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;


            // Get an ImageCodecInfo object that represents the TIFF codec.
            myImageCodecInfo = GetEncoderInfo("image/bmp");

            // Create an Encoder object based on the GUID 
            // for the Compression parameter category.
            myEncoder = Encoder.Compression;

            // Create an EncoderParameters object. 
            // An EncoderParameters object has an array of EncoderParameter 
            // objects. In this case, there is only one 
            // EncoderParameter object in the array.
            myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap as a TIFF file with LZW compression.
            myEncoderParameter = new EncoderParameter(
                myEncoder,
                (long)EncoderValue.CompressionLZW);
            myEncoderParameters.Param[0] = myEncoderParameter;

            var ms = new MemoryStream();

            myBitmap.Save(ms, myImageCodecInfo, myEncoderParameters);

            return ms.GetBuffer();
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}