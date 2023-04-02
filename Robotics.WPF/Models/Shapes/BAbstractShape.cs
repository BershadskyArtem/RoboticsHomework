using System;
using System.Dynamic;

namespace Robotics.WPF.Models.Shapes;

public abstract class BAbstractShape
{
    public readonly int Area;
    public readonly int Perimeter;

    protected BAbstractShape(int area, int perimeter)
    {
        Area = area;
        Perimeter = perimeter;
    }

    protected virtual float Normalize(float probability)
    {
        return (float)(1.0 - (Math.Abs(probability - 1.0)));
    }

    public string ShapeName { get; init; } = string.Empty;

    public abstract float GetShapeSimilarity();

}