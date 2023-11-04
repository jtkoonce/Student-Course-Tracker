namespace JKoonce_Capstone.Views;
using JKoonce_Capstone.Models;
using JKoonce_Capstone.Services;

public partial class CourseDetails : ContentPage
{
	private Course currentCourse;
	public CourseDetails(Course course)
	{
		InitializeComponent();
		currentCourse = course;

	}

	async void AddAssessment_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddAssessment(currentCourse));
	}

	async void EditCourse_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new EditCourse(currentCourse));
	}

	protected override async void OnAppearing()
	{

		CId.Text = currentCourse.CId.ToString();
		CName.Text = currentCourse.CName.ToString();
		TId.Text = currentCourse.TId.ToString();
		CEditStartPick.Text = currentCourse.CStartDate.Date.ToString("MM/dd/yyyy");
		CEditEndPick.Text = currentCourse.CEndDate.Date.ToString("MM/dd/yyyy");
		CNotify.BindingContext = currentCourse.CNotify;
		CStatus.BindingContext = currentCourse;
		CInstName.Text = currentCourse.CInstName.ToString();
		CInstPhone.Text = currentCourse.CInstPhone.ToString();
		CInstEmail.Text = currentCourse.CInstEmail.ToString();
		CNotes.Text = currentCourse.CNotes.ToString();
		if (currentCourse.CNotify == true) { CNotify.IsChecked = true; } else { CNotify.IsChecked = false; }

		CNotes.IsVisible = false;
		NotesHideButton.IsVisible = false;
		NotesShowButton.IsVisible = true;
		NotesShareButton.IsVisible = false;
		AssessmentListView.ItemsSource = await DatabaseServices.GetAssessments(currentCourse.CId);
		base.OnAppearing();
	}

	async void AssessmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count != 0)
		{
			Assessment assessment = (Assessment)e.CurrentSelection[0];
			await Navigation.PushAsync(new AssessmentDetails(assessment));
		}
	}

	async void DeleteCourse_Clicked(object sender, EventArgs e)
	{
		await DatabaseServices.RemoveCourse(currentCourse.CId);
		bool isVerified = true;
		await Navigation.PopToRootAsync(isVerified);
	}

	private void NotesHideButton_Clicked(object sender, EventArgs e)
	{
		CNotes.IsVisible = false;
		NotesHideButton.IsVisible = false;
		NotesShareButton.IsVisible = false;
		NotesShowButton.IsVisible = true;
	}

	private async void NotesShareButton_Clicked(object sender, EventArgs e)
	{
		var text = CNotes.Text;
		await Share.RequestAsync(new ShareTextRequest
		{
			Text = text,
			Title = "Share Text"
		});
	}

	private void NotesShowButton_Clicked(object sender, EventArgs e)
	{
		CNotes.IsVisible = true;
		NotesShareButton.IsVisible = true;
		NotesHideButton.IsVisible = true;
		NotesShowButton.IsVisible = false;
	}
}