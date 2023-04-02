using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Robotics.WPF.ViewModels;

public class ShapeViewModel : ReactiveObject
{
    [Reactive] public string Name { get; set; }

    [Reactive] public string Similarity { get; set; }

    [Reactive] public int Area { get; set; }

    [Reactive] public int Perimeter { get; set; }

    public ShapeViewModel(string name, float similarity, int area, int perimeter)
    {
        this.Name = name;
        this.Similarity = similarity.ToString();
        this.Area = area;
        this.Perimeter = perimeter;
    }
}