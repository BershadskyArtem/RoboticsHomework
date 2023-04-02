using System;

namespace Robotics.WPF.Models.Shapes;

public class BCircle : BAbstractShape
{
    public BCircle(int area, int perimeter) : base(area, perimeter)
    {
        ShapeName = "Круг";
    }

    public override float GetShapeSimilarity()
    {
        double radius = Math.Sqrt(Area / Math.PI);
        var arcLength = 2 * radius * Math.PI;
        var nonNormalized = (float)(arcLength / Perimeter);
       
        return Normalize(nonNormalized);
    }
}