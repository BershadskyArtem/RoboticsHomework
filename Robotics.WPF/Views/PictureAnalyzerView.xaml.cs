using System.Reactive.Disposables;
using System.Windows.Controls;
using Microsoft.Win32;
using ReactiveUI;
using Robotics.WPF.ViewModels;

namespace Robotics.WPF.Views;

public partial class PictureAnalyzerView : ReactiveUserControl<PicAnalyzerViewModel>
{
    public PictureAnalyzerView()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
            this.BindCommand(this.ViewModel,
                t => t.OpenNewImageCommand,
                t => t.OpenImageButton).DisposeWith(d);
            this.OneWayBind(this.ViewModel,
                t => t.FileName,
                t => t.FileNameLabel.Content).DisposeWith(d);
            this.OneWayBind(this.ViewModel,
                t => t.ErrorMessage,
                view => view.ErrorsLabel.Content).DisposeWith(d);
            this.OneWayBind(this.ViewModel,
                t => t.Image,
                view => view.ImageImg.Source).DisposeWith(d);
            this.OneWayBind(this.ViewModel,
                t => t.Shapes,
                view => view.ShapesGrid.ItemsSource).DisposeWith(d);
            this.OneWayBind(this.ViewModel,
                t => t.GuessName,
                view => view.GuessNameLabel.Content).DisposeWith(d);
            this.OneWayBind(this.ViewModel,
                t => t.GuessValue,
                view => view.GuessValueLabel.Content).DisposeWith(d);
        });

        this.WhenActivated(d =>
        {
            d(this.ViewModel.ChooseImageFileInteraction.RegisterHandler((interaction) =>
            {
                OpenFileDialog dg = new OpenFileDialog()
                {
                    Filter = "jpeg files(*.jpeg)|*.jpeg;*.jpg|png files(*.png)|*.png|All files (*.*)|*.*"
                };

                if (dg.ShowDialog() == true)
                {
                    interaction.SetOutput(dg.FileName);
                    return;
                }

                interaction.SetOutput(string.Empty);
            }));
        });
    }
}