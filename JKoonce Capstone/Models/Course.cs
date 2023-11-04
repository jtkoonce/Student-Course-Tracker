using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKoonce_Capstone.Models
{
	public class Course 
	{
		[PrimaryKey, AutoIncrement]
		public int CId { get; set; }
		public string CName { get; set; }
		public int TId { get; set; }  //Foreign Key from Term Class
		public DateTime CStartDate { get; set; }
		public DateTime CEndDate { get; set; }
		public bool CNotify { get; set; }
		public string CStatus { get; set; }
		public string CInstName { get; set; }
		public string CInstPhone { get; set; }
		public string CInstEmail { get; set; }
		public string CNotes { get; set; }
	}
}
