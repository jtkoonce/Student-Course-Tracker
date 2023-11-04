using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKoonce_Capstone.Models
{
	public class Assessment
	{
		[PrimaryKey, AutoIncrement]
		public int AId { get; set; }
		public string AName { get; set; }
		public bool ANotify { get; set; }
		public int CId { get; set; } //Foreign Key from Courses Class
		public DateTime AStartDate { get; set; }
		public DateTime AEndDate { get; set; }
		public string AType { get; set; }
	}
}
