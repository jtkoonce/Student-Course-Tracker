using JKoonce_Capstone.Models;
using JKoonce_Capstone.Services;

namespace JKoonce_Capstone.Views;

public partial class LoginPage : ContentPage
{
	IEnumerable<Login> credentials { get; set; }
	private string userName;
	private string password;
	
	public LoginPage()
	{
		InitializeComponent();
		
		
		
	}
	
	protected override async void OnAppearing()
	{

		UserNameField.Text = null;
		PasswordField.Text = null;
	}

	

	async void LoginButton_Clicked(object sender, EventArgs e)
	{
		
		credentials = await DatabaseServices.GetLogin();
		userName = UserNameField.Text;
		password = PasswordField.Text;
		foreach(Login login in credentials)
		{
			if(login.UserName == userName && login.Password == password) 
			{
				
				LVerify.isVerified = true;
				await DatabaseServices.Notifications();
				await Navigation.PopModalAsync();
				
				
			}
			else
			{
				
				LVerify.isVerified= false;
				await DisplayAlert("Wrong Credentials", "Incorrect Username or Password", "Ok");
			}
		}
    }
}