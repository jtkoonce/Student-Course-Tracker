using JKoonce_Capstone.Services;
using JKoonce_Capstone.Models;
using System.Runtime.CompilerServices;

namespace JKoonce_Capstone.Views;

public partial class AddTerm : ContentPage
{
	
	public AddTerm()
	{
		InitializeComponent();
		
		
	}
	protected override async void OnAppearing()
	{
		

		base.OnAppearing();
	}
	async void SaveTerm_Clicked(object sender, EventArgs e)
	{
		string t_Name = TermName.Text;
		DateTime t_StartDate = TermStartPicker.Date;
		DateTime t_EndDate = TermEndPicker.Date;
		await DatabaseServices.AddTerm(t_Name, t_StartDate, t_EndDate);
		LVerify.isVerified = true;
		await Navigation.PopToRootAsync();
		
	}
}