using Avalonia.ReactiveUI;
using AvaloniaFormValidation.ViewModels;

namespace AvaloniaFormValidation.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}