using JKoonce_Capstone.Services;
using System.Diagnostics;

namespace JKoonce_Capstone.Views;

public partial class AppSettings : ContentPage
{
	public AppSettings()
	{
		InitializeComponent();
	}



	private void ReloadButton_Clicked(object sender, EventArgs e)
	{
		 DatabaseServices.LoadSampleData();
	}

	private async void ClearButton_Clicked_1(object sender, EventArgs e)
	{
		bool answer = await DisplayAlert("Caution!", "Deleted Data Cannot Be Reversed.", "Yes", "Cancel");
		if (answer == true)
		{
			await DatabaseServices.ClearSampleData();
		}
	}
}