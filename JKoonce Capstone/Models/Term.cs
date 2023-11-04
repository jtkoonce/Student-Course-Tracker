using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace JKoonce_Capstone.Models
{
	public class Term 
	{

		[PrimaryKey, AutoIncrement]
		public int TId { get; set; }
		public string TName { get; set; }
		public DateTime TStartDate { get; set; }
		public DateTime TEndDate { get; set; }

	}
}
