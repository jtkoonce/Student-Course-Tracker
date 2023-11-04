using JKoonce_Capstone.Services;
using JKoonce_Capstone.Models;

namespace JKoonce_Capstone.Views;

public partial class EditTerm : ContentPage
{
	private Term currentTerm;

	public EditTerm(Models.Term term)
	{
		InitializeComponent();
		currentTerm = term;
	}
	protected override async void OnAppearing()
	{
		TId.Text = currentTerm.TId.ToString();
		TName.Text = currentTerm.TName;
		TEditStartPick.Date = currentTerm.TStartDate.Date;
		TEditEndPick.Date = currentTerm.TEndDate.Date;

	}
	async void EditTerm_Clicked(object sender, EventArgs e)
	{
		int t_Id = Convert.ToInt32(TId.Text);
		string t_Name = TName.Text;
		DateTime t_StartDate = TEditStartPick.Date;
		DateTime t_EndDate = TEditEndPick.Date;
		await DatabaseServices.UpdateTerm(t_Id, t_Name, t_StartDate, t_EndDate);
		LVerify.isVerified = true;
		await Navigation.PopToRootAsync();

	}
}