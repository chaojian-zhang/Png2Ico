using System.Drawing;
using System.Runtime.Versioning;

namespace Png2Ico
{
    [SupportedOSPlatform("windows")]
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PngToIcoConverter <sourceFilePath> <destinationFilePath>");
                return;
            }

            string sourceFilePath = args[0];
            string destinationFilePath = args[1];

            if (!File.Exists(sourceFilePath))
            {
                Console.WriteLine($"Error: Source file '{sourceFilePath}' does not exist.");
                return;
            }

            try
            {
                ConvertPngToIco(sourceFilePath, destinationFilePath);
                Console.WriteLine($"'{sourceFilePath}' has been successfully converted to '{destinationFilePath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void ConvertPngToIco(string sourceFilePath, string destinationFilePath)
        {
            using (FileStream stream = File.OpenWrite(destinationFilePath))
            {
                Bitmap bitmap = FixSize((Bitmap)Image.FromFile(sourceFilePath));
                Icon.FromHandle(bitmap.GetHicon()).Save(stream);
            }
        }

        private static Bitmap FixSize(Bitmap old)
        {
            if (old.Size.Width > 256 || old.Size.Height > 256)
                return new Bitmap(old, new Size(256, 256));
            return old;
        }
    }
}