namespace JKoonce_Capstone;

using JKoonce_Capstone.Services;
using Views;
using Models;

public partial class App : Application
{
	
	public App()
	{
		InitializeComponent();

		LVerify.isVerified = false;
		var navigationPage = new NavigationPage(new Dashboard());
		MainPage = navigationPage;
		DatabaseServices.CheckActiveData();

	}

}
