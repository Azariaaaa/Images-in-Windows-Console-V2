using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesInConsole
{
    public class PixelWriter
    {
        public Bitmap Image { get; set; }
        public int Accuracy { get; set; }

        public const int SCALING = 3;
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        public PixelWriter(Bitmap image, int accuracy)
        {
            Image = image;
            Accuracy = accuracy;
            TileWidth = Image.Width / accuracy;
            TileHeight = Image.Height / accuracy;
        }

        public void PictureAnalyzer()
        {
            for (int y = 0; y < Image.Height - TileHeight; y += TileHeight)
            {
                for (int x = 0; x < Image.Width - TileWidth; x += TileWidth)
                {
                    List<string> valuesRGB = GetAverageRGB(x, y);
                    string R = valuesRGB[0];
                    string G = valuesRGB[1];
                    string B = valuesRGB[2];

                    Console.Write("\x1b[38;2;" + R + ";" + G + ";" + B + "m██");
                }
                Console.WriteLine();
            }
        }

        public List<string> GetAverageRGB(int xStart, int yStart)
        {
            int redTotal = 0;
            int greenTotal = 0;
            int blueTotal = 0;
            int xEnd = xStart + TileWidth;
            int yEnd = yStart + TileHeight;
            int numberOfPixels = TileWidth * TileHeight;

            for (int y = yStart; y < yEnd; y++)
            {
                for (int x = xStart; x < xEnd; x++)
                {
                    Color pixelColor = Image.GetPixel(x, y);
                    redTotal += pixelColor.R;
                    greenTotal += pixelColor.G;
                    blueTotal += pixelColor.B;
                }
            }

            string averageRed = Convert.ToString(redTotal / numberOfPixels);
            string averageGreen = Convert.ToString(greenTotal / numberOfPixels);
            string averageBlue = Convert.ToString(blueTotal / numberOfPixels);

            List<string> valuesRGB = new List<string>
            {
                averageRed,
                averageGreen,
                averageBlue
            };

            return valuesRGB;
        }
    }
}
