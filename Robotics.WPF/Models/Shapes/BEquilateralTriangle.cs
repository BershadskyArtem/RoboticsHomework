using System;

namespace Robotics.WPF.Models.Shapes;

public class BEquilateralTriangle : BAbstractShape
{
    public BEquilateralTriangle(int area, int perimeter) : base(area, perimeter)
    {
        ShapeName = "Равносторонний треугольник";
    }

    public override float GetShapeSimilarity()
    {
        var side = Math.Sqrt(4 * Area / Math.Sqrt(3));
        var sideFromPerimeter = Perimeter / 3;
        var nonNormalized = (float)(side / sideFromPerimeter);
        return Normalize(nonNormalized);
    }
}