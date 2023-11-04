namespace JKoonce_Capstone.Views;
using JKoonce_Capstone.Models;
using JKoonce_Capstone.Services;

public partial class EditCourse : ContentPage
{
	private Course currentCourse;
	public EditCourse(Course course)
	{
		InitializeComponent();
		currentCourse = course;
	}

	protected override async void OnAppearing()
	{
		CId.Text = currentCourse.CId.ToString();
		CName.Text = currentCourse.CName;
		TId.Text = currentCourse.TId.ToString();
		CEditStartPick.Date = currentCourse.CStartDate.Date;
		CEditEndPick.Date = currentCourse.CEndDate.Date;
		CNotify.IsChecked = currentCourse.CNotify;
		CStatus.SelectedItem = currentCourse.CStatus.ToString();
		CInstName.Text = currentCourse.CInstName.ToString();
		CInstPhone.Text = currentCourse.CInstPhone.ToString();
		CInstEmail.Text = currentCourse.CInstEmail.ToString();
		CNotes.Text = currentCourse.CNotes.ToString();
		if(currentCourse.CNotify == true){ CNotify.IsChecked = true; } else { CNotify.IsChecked = false; }
	}
	async void SaveCourse_Clicked(object sender, EventArgs e)
	{
		int c_Id = Convert.ToInt32(CId.Text);
		string c_Name = CName.Text;
		int t_Id = Convert.ToInt32(TId.Text);
		DateTime c_StartDate = CEditStartPick.Date;
		DateTime c_EndDate = CEditEndPick.Date;
		bool c_Notify = CNotify.IsChecked;
		string c_Status = (string)CStatus.SelectedItem;
		string c_InstName = CInstName.Text;
		string c_InstPhone = CInstPhone.Text;
		string c_InstEmail = CInstEmail.Text;
		string c_Notes = CNotes.Text;


		if (DatabaseServices.IsNull(CName.Text) && DatabaseServices.IsNull(CInstName.Text) && DatabaseServices.IsNull(CInstEmail.Text))
		{
			if (DatabaseServices.isInvalidEmail(CInstEmail.Text) != true)
			{
				if (c_StartDate < c_EndDate)
				{
					await DatabaseServices.UpdateCourse(c_Id, c_Name, t_Id, c_StartDate, c_EndDate, c_Notify, c_Status, c_InstName, c_InstPhone, c_InstEmail, c_Notes);
					LVerify.isVerified = true;


					await Navigation.PopToRootAsync();
					
				}
				else
				{
					await DisplayAlert("Invalid Date", "Please sure start date is before end date.", "OK");
				}
			}
			else
			{
				await DisplayAlert("Invalid Email", "Please make sure the email is correct.", "OK");
			}
		}
		else
		{
			await DisplayAlert("Invalid Form", "Please Make Sure All Fields Are Completed", "OK");
		}
	}

	private void CNotify_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		

	}
}