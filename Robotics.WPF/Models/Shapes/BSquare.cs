using System;

namespace Robotics.WPF.Models.Shapes;

public class BSquare : BAbstractShape
{
   
    public override float GetShapeSimilarity()
    {
        float sqPer = (ulong)Perimeter * (ulong)Perimeter;
        var nonNormalized = (sqPer / Area) / 16.0f;
        return Normalize(nonNormalized);
    }

    
    
    public BSquare(int area, int perimeter) : base(area, perimeter)
    {
        ShapeName = "Квадрат";
    }

 
}