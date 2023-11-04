namespace JKoonce_Capstone.Views;

using JKoonce_Capstone.Services;
using Models;
using System.Collections.ObjectModel;
using Plugin.LocalNotification;

public partial class Dashboard : ContentPage
{

	
	public Dashboard()
	{
		
		InitializeComponent();

		
		
		
	}

	protected override async void OnAppearing()
	{

		

		TermListView.ItemsSource = await DatabaseServices.GetTerms();
		
		SearchBar.Text = null;
		if (LVerify.isVerified == false)
		{
			await Navigation.PushModalAsync(new LoginPage());

		}

	}

		


	
	async void AddTerm_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddTerm());
	}


	async void TermListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if(e.CurrentSelection.Count != 0)
		{
			Term term = (Term)e.CurrentSelection[0];
			await Navigation.PushAsync(new TermDetails(term));
		}
		
		
	}

	async void SearchButton_Clicked(object sender, EventArgs e)
	{
		
		if (SearchBar.Text != null) 
		{
			await Navigation.PushAsync(new SearchResults(SearchBar.Text));
		}
		
		
	}

	async void Reports_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Reports());
	}

	async void Settings_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AppSettings());
	}

	async void LogOut_Clicked(object sender, EventArgs e)
	{
		LVerify.isVerified = false;
		await Navigation.PushModalAsync(new LoginPage());
	}
}


