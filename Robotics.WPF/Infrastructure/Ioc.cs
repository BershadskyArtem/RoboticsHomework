using System;
using System.Reflection;
using ReactiveUI;
using Robotics.WPF.Services;
using Robotics.WPF.ViewModels;
using Robotics.WPF.Views;
using Splat;

namespace Robotics.WPF.Infrastructure;

public class Ioc
{
    static Ioc()
    {
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        //View models
        Locator.CurrentMutable.Register<MainWindowViewModel>(() => new MainWindowViewModel());
        Locator.CurrentMutable.Register<PicAnalyzerViewModel>(() => new PicAnalyzerViewModel());
        Locator.CurrentMutable.Register<IShapeAnalyzerService>(()=> new ShapeAnalyzerService());
        //Views and viewmodels
        //Locator.CurrentMutable.Register(() => new PictureAnalyzerView(), typeof(IViewFor<PicAnalyzerViewModel>));
    }
    public static T Resolve<T>()
    {
        var result = Locator.Current.GetService<T>();
        if (result == null)
            throw new NullReferenceException($"Service of type {typeof(T).Name} is not registered in Ioc.");
        return result;
    }
}