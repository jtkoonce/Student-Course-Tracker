using JKoonce_Capstone.Models;
using JKoonce_Capstone.Services;

namespace JKoonce_Capstone.Views;

public partial class TermDetails : ContentPage
{
	private Term currentTerm;
	public TermDetails(Models.Term term)
	{
		InitializeComponent();
		currentTerm = term;
		
	}

	async void AddCourse_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddCourse(currentTerm));
	}
	async void EditTerm_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new EditTerm(currentTerm));
	}
	protected override async void OnAppearing()
	{
		TermID.BindingContext = currentTerm;
		TermName.BindingContext = currentTerm;
		TermStartPicker.BindingContext = currentTerm;
		TermEndPicker.BindingContext = currentTerm;

		CourseListView.ItemsSource = await DatabaseServices.GetCourses(currentTerm.TId);
	}

	async void DeleteTerm_Clicked(object sender, EventArgs e)
	{
		await DatabaseServices.RemoveTerm(currentTerm.TId);
		LVerify.isVerified = true;
		await Navigation.PopToRootAsync();
	}

	async void CourseListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count != 0)
		{
			Course course = (Course)e.CurrentSelection[0];
			await Navigation.PushAsync(new CourseDetails(course));
		}
	}
}