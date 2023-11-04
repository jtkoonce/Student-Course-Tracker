using JKoonce_Capstone.Models;
using JKoonce_Capstone.Services;

namespace JKoonce_Capstone.Views;

public partial class AssessmentDetails : ContentPage
{
	private Assessment currentAssessment;
	public AssessmentDetails(Assessment assessment)
	{
		InitializeComponent();
		currentAssessment = assessment;
	}

	protected override async void OnAppearing()
	{
		AId.Text = currentAssessment.AId.ToString();
		AName.Text = currentAssessment.AName.ToString();
		CId.Text = currentAssessment.CId.ToString();
		ANotify.IsChecked = currentAssessment.ANotify;
		AStartPicker.Text = currentAssessment.AStartDate.Date.ToString("MM/dd/yyyy");
		AEndPicker.Text = currentAssessment.AEndDate.Date.ToString("MM/dd/yyyy");
		AType.BindingContext = currentAssessment;
		base.OnAppearing();
	}
	async void EditAssessment_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new EditAssessment(currentAssessment));
	}

	async void DeleteAssessment_Clicked(object sender, EventArgs e)
	{
		await DatabaseServices.RemoveAssessment(currentAssessment.AId);
		LVerify.isVerified = true;
		await Navigation.PopToRootAsync();
	}
}