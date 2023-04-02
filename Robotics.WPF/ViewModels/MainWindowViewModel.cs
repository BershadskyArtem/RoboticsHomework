using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using ReactiveUI;
using ReactiveUI.Validation.Formatters.Abstractions;
using ReactiveUI.Validation.Helpers;
using Robotics.WPF.Views;
using Splat;

namespace Robotics.WPF.ViewModels;

public class MainWindowViewModel : ReactiveValidationObject, IScreen
{
    public RoutingState Router { get; }

    public MainWindowViewModel()
    {
        Router = new RoutingState();
        
        Locator.CurrentMutable.Register(() => new PictureAnalyzerView(), typeof(IViewFor<PicAnalyzerViewModel>));
        
        OpenPicAnalyzerCommand = ReactiveCommand.Create(() =>
        {
            Router.Navigate.Execute(new PicAnalyzerViewModel());
        });
        Router.Navigate.Execute(new PicAnalyzerViewModel());
    }
    
    public ReactiveCommand<Unit,Unit> OpenPicAnalyzerCommand { get; }
    public ReactiveCommand<Unit,Unit> OpenJarvisCommand { get; }
    
}