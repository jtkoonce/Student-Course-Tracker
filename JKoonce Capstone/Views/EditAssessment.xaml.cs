namespace JKoonce_Capstone.Views;
using JKoonce_Capstone.Models;
using JKoonce_Capstone.Services;

public partial class EditAssessment : ContentPage
{
	private Assessment currentAssessment;
	public EditAssessment(Assessment assessment)
	{
		InitializeComponent();
		currentAssessment = assessment;
		
	}

	protected override async void OnAppearing()
	{
		AId.Text = currentAssessment.AId.ToString();
		CId.Text = currentAssessment.CId.ToString();
		AName.Text = currentAssessment.AName.ToString();
		ANotify.IsChecked = currentAssessment.ANotify;
		AStartPicker.Date = currentAssessment.AStartDate.Date;
		AEndPicker.Date = currentAssessment.AEndDate.Date;
		AType.SelectedItem = currentAssessment.AType.ToString();
	}
	async void SaveAssessment_Clicked(object sender, EventArgs e)
	{
		int a_Id = Convert.ToInt32(AId.Text);
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
				await DatabaseServices.UpdateAssessment(a_Id, a_Name, c_Id, a_Notify, a_StartDate, a_EndDate, a_Type);
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
			await DisplayAlert("Invalid Form", "Please Make Sure All Fields Are Completed", "OK");
		}
	}

	private void ANotify_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{

	}
}