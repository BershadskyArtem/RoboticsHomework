using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Robotics.WPF.Models.Shapes;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Robotics.WPF.Services;

public class ShapeAnalyzerService : IShapeAnalyzerService
{
    private const int PixelSize = 3;
    
    /// <summary>
    /// Analyzes image using convolution.
    /// </summary>
    /// <param name="bmd">Locked bitmap data.</param>
    /// <returns>List of shapes with the name and similarity percent</returns>
    public IEnumerable<BAbstractShape> AnalyzeImage(BitmapData bmd)
    {
        int perimeter = 0;
        int area = 0;

        if (bmd.PixelFormat != PixelFormat.Format24bppRgb)
            throw new InvalidDataException("Incorrect image format");

        //Unsafe offers significant speed up
        unsafe
        {
            //I can analyze edges in different loop but i am lazy
            for (int y = 1; y < bmd.Height - 1; y++)
            {
                byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);

                //Move away from edges 
                for (int x = 1; x < bmd.Width - 1; x++)
                {
                    //Since i support only black and white image i can work only in one channel
                    //Bitmap stores pixels in BGR order
                    byte pixelValue = row[x * PixelSize]; //Blue  0-255
                    byte* rowBefore = (byte*)bmd.Scan0 + ((y - 1) * bmd.Stride);
                    byte* rowAfter = (byte*)bmd.Scan0 + ((y + 1) * bmd.Stride);
                    //Thresholding
                    if (pixelValue < 128)
                        area++;
                    //Do edge detection convolution
                    //https://en.wikipedia.org/wiki/Kernel_(image_processing)
                    //Edge detection kernel is as follows
                    // -1 -1 -1
                    // -1  8 -1
                    // -1 -1 -1
                    float convolutedPixel = pixelValue * 8.0f - row[(x + 1) * PixelSize] - row[(x - 1) * PixelSize]
                                          - rowBefore[x * PixelSize] - rowBefore[(x + 1) * PixelSize] -
                                          rowBefore[(x - 1) * PixelSize]
                                          - rowAfter[x * PixelSize] - rowAfter[(x + 1) * PixelSize] -
                                          rowAfter[(x - 1) * PixelSize];
                    //The lower the convoluted value is the more likely it is an edge
                    //1275 because 255*8 - 255 * 3 = 1275
                    if (convolutedPixel >= -1275 - 20 && convolutedPixel <= -765 + 20 )
                        perimeter++;
                }
            }
        }

        var result = new List<BAbstractShape>()
        {
            new BSquare(area, perimeter),
            new BCircle(area, perimeter),
        };
        return result;
    }
}