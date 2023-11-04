namespace JKoonce_Capstone.Views;
using JKoonce_Capstone.Services;
using JKoonce_Capstone.Models;

public partial class AddAssessment : ContentPage
{
	private Course currentCourse;
	public AddAssessment(Course course)
	{
		InitializeComponent();
		currentCourse = course;
		CId.Text = currentCourse.CId.ToString();
		
		
	}

	async void SaveAssessment_Clicked(object sender, EventArgs e)
	{
		string a_Name = AName.Text;
		int c_Id = Convert.ToInt32(CId.Text);
		bool a_Notify = ANotify.IsChecked;
		DateTime a_StartDate = AStartPicker.Date;
		DateTime a_EndDate = AEndPicker.Date;
		string a_Type = (string)AType.SelectedItem;

		if (DatabaseServices.IsNull(AName.Text))
		{
				if (a_StartDate < a_EndDate)
				{
					await DatabaseServices.AddAssessment(a_Name, c_Id, a_Notify, a_StartDate, a_EndDate, a_Type);
					bool isVerified = true;
					await Navigation.PopToRootAsync(isVerified);
				}
				else
				{
					await DisplayAlert("Invalid Date", "Please sure start date is before end date.", "OK");
				}
		}
		else
		{
			await DisplayAlert("Invalid Form", "Please Make Sure All Fields Are Completed", "OK");
		}
	}

	private void ANotify_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{

	}
}