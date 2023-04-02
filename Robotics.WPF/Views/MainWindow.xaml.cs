using System.Reactive.Disposables;
using System.Windows;
using ReactiveUI;
using Robotics.WPF.ViewModels;
using Splat;

namespace Robotics.WPF.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();

            this.WhenActivated(d =>
            {
                this.OneWayBind(
                        this.ViewModel,
                        t => t.Router,
                        t => t.ViewHost.Router)
                    .DisposeWith(d);
                // this.BindCommand(this.ViewModel,
                //     t => t.OpenPicAnalyzerCommand,
                //     t => t.PicAnalyzerBtn);
            });
        }
    }
}