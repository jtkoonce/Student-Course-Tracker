
using JKoonce_Capstone.Models;
using JKoonce_Capstone.Services;
using System.Linq;
using System.Security.Cryptography.X509Certificates;


namespace JKoonce_Capstone.Views;

public partial class Reports : ContentPage
{
	IEnumerable<Course> reportList { get; set; }
	public Reports()
	{
		InitializeComponent();
		
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		var courseList = await DatabaseServices.GetCourses();
		reportList = courseList.OrderBy(course => course.CStatus).ToList();
		ReportShareButton.IsVisible = false;
		ReportDateBar.IsVisible = false;
	}
	private void ReportButton_Clicked(object sender, EventArgs e)
	{
		ReportShareButton.IsVisible = true;
		ReportDateBar.IsVisible = true;
		ReportDateBar.Text = DateTime.Now.ToString();
		ReportListView.ItemsSource = reportList;


	}

	private async void ReportListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count != 0)
		{
			Course course = (Course)e.CurrentSelection[0];
			await Navigation.PushAsync(new CourseDetails(course));
		}
	}

	private async void ReportShareButton_Clicked(object sender, EventArgs e)
	{

		//string shareList =    //string.Join(",", reportList.Select(o=>o.ToString()));

		string courseString;
		string shareList = "Courses:  ";

		foreach(Course course in reportList)
		{
			courseString = $"CId:{course.CId}, CName:{course.CName}, TId: {course.TId}, CStartDate: {course.CStartDate.ToString()}, CEndDate: {course.CEndDate.ToString()}" +
				$", CNotify: {course.CNotify.ToString()}, CStatus: {course.CStatus}, CInstName: {course.CInstName}, CInstPhone: {course.CInstPhone} CInstEmail: {course.CInstEmail}, CNotes: {course.CNotes}";
			shareList = $"{shareList},,,{courseString}";
			
		}
		shareList = shareList.Replace(",", System.Environment.NewLine);
		var text = $"Date:{ReportDateBar.Text}, {shareList}";
		await Share.RequestAsync(new ShareTextRequest
		{
			Text = text,
			Title = "Course Status Report"
		});
	}
}