using System.Collections.Generic;
using System.Drawing.Imaging;
using Robotics.WPF.Models.Shapes;

namespace Robotics.WPF.Services;

public interface IShapeAnalyzerService
{
    public IEnumerable<BAbstractShape> AnalyzeImage(BitmapData bmd);
}