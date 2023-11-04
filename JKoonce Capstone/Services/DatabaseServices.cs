using JKoonce_Capstone.Models;
using JKoonce_Capstone.Views;
using Plugin.LocalNotification;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JKoonce_Capstone.Services
{
	public static class DatabaseServices
	{
		private static SQLiteAsyncConnection _db;
		private static SQLiteConnection _dbConnection;
		public static Term selectedTerm {  get; set; }
		public static Course selectedCourse { get; set; }
		public static Assessment selectedAssessment { get; set; }

		public static async Task Init()
		{
			
			if (_db != null)
			{
				return;
			}


			//Getting absolute path to DB file
			var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "wguApp.db");

			_db = new SQLiteAsyncConnection(databasePath);
			_dbConnection = new SQLiteConnection(databasePath);

			await _db.CreateTableAsync<Assessment>();
			await _db.CreateTableAsync<Course>();
			await _db.CreateTableAsync<Term>();
			await _db.CreateTableAsync<Login>();

		}

		#region Assessment Methods

		public static async Task AddAssessment(string aName, int cId, bool aNotify, DateTime aStartDate, DateTime aEndDate, string aType)
		{
			await Init();
			var assessment = new Assessment()
			{
				AName = aName,
				CId = cId,
				ANotify = aNotify,
				AStartDate = aStartDate,
				AEndDate = aEndDate,
				AType = aType

			};

			await _db.InsertAsync(assessment);

			var id = assessment.AId;
		}
		public static async Task RemoveAssessment(int aId)
		{
			await Init();
			await _db.DeleteAsync<Assessment>(aId);
		}
		public static async Task<IEnumerable<Assessment>> GetAssessments(int cId)
		{
			await Init();
			var assessments = await _db.Table<Assessment>().Where(i => i.CId == cId).ToListAsync();
			return assessments;
		}
		public static async Task<IEnumerable<Assessment>> GetAssessments()
		{
			await Init();
			var assessments = await _db.Table<Assessment>().ToListAsync();
			return assessments;
		}
		public static async Task UpdateAssessment(int aId, string aName, int cId, bool aNotify, DateTime aStartDate, DateTime aEndDate, string aType)
		{
			await Init();
			var Assessquery = await _db.Table<Assessment>()
				.Where(i => i.AId == aId)
				.FirstOrDefaultAsync();
			if (Assessquery != null)
			{
				Assessquery.AName = aName;
				Assessquery.CId = cId;
				Assessquery.ANotify = aNotify;
				Assessquery.AStartDate = aStartDate;
				Assessquery.AEndDate = aEndDate;
				Assessquery.AType = aType;

				await _db.UpdateAsync(Assessquery);
			}
		}
		#endregion

		#region Course Methods
		public static async Task AddCourse(string cName, int tId, DateTime cStartDate, DateTime cEndDate, bool cNotify, string cStatus, string cInstName, string cInstPhone, string cInstEmail, string notes)
		{
			await Init();
			var course = new Course
			{
				CName = cName,
				TId = tId,
				CStartDate = cStartDate,
				CEndDate = cEndDate,
				CNotify = cNotify,
				CStatus = cStatus,
				CInstName = cInstName,
				CInstPhone = cInstPhone,
				CInstEmail = cInstEmail,
				CNotes = notes

			};
			await _db.InsertAsync(course);
			var id = course.CId;
		}
		public static async Task RemoveCourse(int CId)
		{
			await Init();
			await _db.DeleteAsync<Course>(CId);
		}
		public static async Task<IEnumerable<Course>> GetCourses(int tId) 
        {
            await Init();
            var courses = await _db.Table<Course>().Where(i => i.TId == tId).ToListAsync();
            return courses;
        }

		public static async Task<IEnumerable<Course>> GetCourses()
		{
			await Init();
			var courses = await _db.Table<Course>().ToListAsync();
			return courses;
		}
		public static async Task UpdateCourse(int cId, string cName, int tId, DateTime cStartDate, DateTime cEndDate, bool cNotify, string cStatus, string cInstName, string cInstPhone, string cInstEmail, string notes)
		{
			await Init();
			var courseQuery = await _db.Table<Course>()
				.Where(i => i.CId == cId)
				.FirstOrDefaultAsync();

			if (courseQuery != null)
			{
				courseQuery.CName = cName;
				courseQuery.CStartDate = cStartDate;
				courseQuery.CEndDate = cEndDate;
				courseQuery.CNotify = cNotify;
				courseQuery.CStatus = cStatus;
				courseQuery.CInstName = cInstName;
				courseQuery.CInstPhone = cInstPhone;
				courseQuery.CInstEmail = cInstEmail;
				courseQuery.CNotes = notes;

				await _db.UpdateAsync(courseQuery);
			}
		}
		public static async Task<IEnumerable<Course>> CourseSearchResults(string searchText)
		{
			var courseList = await GetCourses();
			var searchList = new List<Course>();
			foreach (Course course in courseList)
			{
				if (course.CName.ToString().Contains(searchText) || course.CName.ToString().ToLower().Contains(searchText))
				{
					searchList.Add(course);
				}
			}

			return searchList;
		}
		#endregion

		#region Term Methods
		public static async Task AddTerm(string tName, DateTime tStartDate, DateTime tEndDate)
		{
			await Init();
			var term = new Term()
			{
				TName = tName,
				TStartDate = tStartDate,
				TEndDate = tEndDate,
			};
			await _db.InsertAsync(term);
			selectedTerm = term;
		}
		public static async Task RemoveTerm(int tId)
		{
			await Init();
			
			await _db.DeleteAsync<Term>(tId);
		}
		public static async Task<IEnumerable<Term>> GetTerms() 
        {
            await Init();
            var terms = await _db.Table<Term>().ToListAsync();
            return terms;
        }
		public static async Task UpdateTerm(int tId, string tName, DateTime tStartDate, DateTime tEndDate)
		{
			await Init();
			var termquery = await _db.Table<Term>()
				.Where(i => i.TId == tId)
				.FirstOrDefaultAsync();
			if (termquery != null)
			{
				termquery.TName = tName;
				termquery.TStartDate = tStartDate;
				termquery.TEndDate = tEndDate;

				await _db.UpdateAsync(termquery);
			}
		}
		public static async Task<int> GetTermCountAsync(int selectedTermId)
		{
			int termCount = await _db.ExecuteScalarAsync<int>($" Count(*) from Term where TId =?", selectedTermId);
			return termCount;
		}
		#endregion

		#region Login Methods

		public static async Task<IEnumerable<Login>> GetLogin()
		{
			await Init();
			var cred = await _db.Table<Login>().ToListAsync();
			return cred;
		}

		#endregion

		#region Demo Data

		public static async void LoadSampleData()
		{
			await Init();
			Term term1 = new Term
			{
				TName = "Term 1",
				TStartDate = DateTime.Now,
				TEndDate = DateTime.Now.AddMonths(6),
			};
			await _db.InsertAsync(term1);
			

			
			Course course1 = new Course
			{
				CName = "Course 1",
				TId = term1.TId,
				CStartDate = DateTime.Now,
				CEndDate = DateTime.Now.AddMonths(2),
				CNotify = true,
				CStatus = "Active",
				CInstName = "John Smith",
				CInstPhone = "123-456-7890",
				CInstEmail = "john.smith@testmail.com",
				CNotes = "Test Software Course"
			};
			await _db.InsertAsync(course1);
			


			Assessment assessment1 = new Assessment
			{
				AName = "Assessment 1",
				CId = course1.CId,
				ANotify = true,
				AStartDate = DateTime.Now,
				AEndDate = DateTime.Now.AddMonths(2),
				AType = "Objective"
			};
			await _db.InsertAsync(assessment1);
			Assessment assessment2 = new Assessment
			{
				AName = "Assessment 2",
				CId = course1.CId,
				ANotify = false,
				AStartDate = DateTime.Now,
				AEndDate = DateTime.Now.AddMonths(2),
				AType = "Performance"
			};
			await _db.InsertAsync(assessment2);
		}
		public static async Task ClearSampleData()
		{
			await Init();
			await _db.DropTableAsync<Term>();
			await _db.DropTableAsync<Course>();
			await _db.DropTableAsync<Assessment>();
			await _db.DropTableAsync<Login>();
			_db = null;
			_dbConnection = null;
		}
		
		public static async Task LoadLoginInfo()
		{
			await Init();
			Login testUser = new Login
			{
				UserName = "Test",
				Password = "Test"
			};
			await _db.InsertAsync(testUser);
		}


		#endregion

		#region Data Check

		public static bool IsNull(string value)
		{
			if (!String.IsNullOrEmpty(value))
			{
				return true;
			}
			else
			{
				return false;
			}	
		}

		public static bool isInvalidEmail(string email)
		{
			try
			{
				var address = new System.Net.Mail.MailAddress(email);
				return !(address.Address == email);
			}
			catch
			{
				return true;
			}
		}

		public static async Task Notifications()
		{
			var courseList = await GetCourses();
			var assessmentList = await GetAssessments();
			var ranNotify = new Random();
			var notifyId = ranNotify.Next(1000);
			string message = null;
			
			foreach (Course course in courseList)
			{
				if (course.CNotify == true)
				{
					if (course.CStartDate == DateTime.Today)
					{
						message = $"{course.CName} Begins";
						var request = new NotificationRequest
						{
							NotificationId = notifyId,
							Title = $"{message} Today!",
							Subtitle = "Alert",
							Schedule = new NotificationRequestSchedule
							{
								NotifyTime = DateTime.Now
							}
						};
						await LocalNotificationCenter.Current.Show(request);

					}
					if (course.CEndDate == DateTime.Today)
					{
						
						message = $"{course.CName} Ends";
						var request = new NotificationRequest
						{
							NotificationId = notifyId,
							Title = $"{message} Today!",
							Subtitle = "Alert",
							Schedule = new NotificationRequestSchedule
							{
								NotifyTime = DateTime.Now
							}
						};
						await LocalNotificationCenter.Current.Show(request);

					}
				}
			}
			foreach (Assessment assessment in assessmentList)
			{
				if (assessment.ANotify == true)
				{
					if (assessment.AStartDate == DateTime.Today)
					{
						
						message = $"{assessment.AName} Begins";
						var request = new NotificationRequest
						{
							NotificationId = notifyId,
							Title = $"{message} Today!",
							Subtitle = "Alert",
							Schedule = new NotificationRequestSchedule
							{
								NotifyTime = DateTime.Now
							}
						};
						await LocalNotificationCenter.Current.Show(request);

					}
					if (assessment.AEndDate == DateTime.Today)
					{

						message = $"{assessment.AName} Ends";
						var request = new NotificationRequest
						{
							NotificationId = notifyId,
							Title = $"{message} Today!",
							Subtitle = "Alert",
							Schedule = new NotificationRequestSchedule
							{
								NotifyTime = DateTime.Now
							}
						};
						await LocalNotificationCenter.Current.Show(request);

					}
				}
			}
		}

		public static void CheckActiveData()
		{
			Init();
			
			if (_db == null)
			{
				LoadSampleData();
				LoadLoginInfo();
			}
		}
		#endregion

		
	}
}
