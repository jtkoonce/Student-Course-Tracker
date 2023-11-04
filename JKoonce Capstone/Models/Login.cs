using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKoonce_Capstone.Models
{
	public class Login 
	{
		[PrimaryKey, AutoIncrement]
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}

	public class LVerify
	{
		public static bool isVerified { get; set; }

		public Term Term
		{
			get => default;
			set
			{
			}
		}

		public Course Course
		{
			get => default;
			set
			{
			}
		}

		public Assessment Assessment
		{
			get => default;
			set
			{
			}
		}
	}
}
