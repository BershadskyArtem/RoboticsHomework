using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Helpers;
using Robotics.WPF.Infrastructure;
using Robotics.WPF.Services;
using Splat;

namespace Robotics.WPF.ViewModels;

public class PicAnalyzerViewModel : ReactiveValidationObject, IRoutableViewModel
{
    public Interaction<Unit, string> ChooseImageFileInteraction { get; }

    public string? UrlPathSegment { get; } = "PicViewHost";
    public IScreen? HostScreen { get; }

    private readonly IShapeAnalyzerService? _shapeAnalyzerService;
    
    [Reactive]
    public string FileName { get; set; }

    [Reactive]
    public string ErrorMessage { get; set; }
    
    [Reactive]
    public BitmapImage Image { get; set; }
    
    [Reactive]
    public string GuessName { get; set; }
    [Reactive]
    public string GuessValue { get; set; }

    public ObservableCollection<ShapeViewModel> Shapes;

    public PicAnalyzerViewModel(IScreen screen = null)
    {
        _shapeAnalyzerService = Ioc.Resolve<IShapeAnalyzerService>();
        HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        ChooseImageFileInteraction = new Interaction<Unit, string>();
        Shapes = new ObservableCollection<ShapeViewModel>();
        OpenNewImageCommand = ReactiveCommand.CreateFromTask(OpenImageFile);
    }
    
    private async Task OpenImageFile()
    {
        var fileName = await ChooseImageFileInteraction.Handle(Unit.Default);
        FileName = fileName;
        if (string.IsNullOrWhiteSpace(fileName))
            return;
        try
        {
            BitmapImage image = new BitmapImage(new Uri(fileName, UriKind.Absolute));
            this.Image = image;
            await UpdateEstimation();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    private async Task UpdateEstimation()
    {
        var bmp = new Bitmap(FileName);
        var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
            System.Drawing.Imaging.ImageLockMode.ReadOnly,
            PixelFormat.Format24bppRgb);
        var values = _shapeAnalyzerService.AnalyzeImage(data);
        bmp.UnlockBits(data);
        bmp.Dispose();
        Shapes.Clear();
        values = values.OrderByDescending(x => x.GetShapeSimilarity());
        GuessName = values.First().ShapeName;
        GuessValue = values.First().GetShapeSimilarity().ToString();
        var shapeViewModels = values.Select(x => 
            new ShapeViewModel(x.ShapeName, x.GetShapeSimilarity(), x.Area, x.Perimeter)).ToList();
        Shapes.AddRange(shapeViewModels);
    }
    
    public ReactiveCommand<Unit, Unit> OpenNewImageCommand { get; }
}