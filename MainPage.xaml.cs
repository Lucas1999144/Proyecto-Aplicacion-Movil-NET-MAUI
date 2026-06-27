using Parcial_Moviles.ViewModels;

namespace Parcial_Moviles;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

