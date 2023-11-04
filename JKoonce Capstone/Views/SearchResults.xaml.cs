using JKoonce_Capstone.Services;
using JKoonce_Capstone.Models;


namespace JKoonce_Capstone.Views;

public partial class SearchResults : ContentPage
{
	public SearchResults(string searchText)
	{
		InitializeComponent();
		
	}

	protected override async void OnAppearing()
	{

		base.OnAppearing();
		var searchList = await DatabaseServices.GetCourses();
		SearchListView.ItemsSource = searchList;
	}

	async void SearchListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count != 0)
		{
			Course course = (Course)e.CurrentSelection[0];
			await Navigation.PushAsync(new CourseDetails(course));
		}
	}
}