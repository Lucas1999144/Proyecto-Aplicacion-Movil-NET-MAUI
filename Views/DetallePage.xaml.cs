using Parcial_Moviles.ViewModels;

namespace Parcial_Moviles.Views;

public partial class DetallePage : ContentPage
{
	public DetallePage(DetalleViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private async void OnVolverClicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("..");
    }
}