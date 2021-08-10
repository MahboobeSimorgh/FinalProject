//namespace MyApplication.Models
namespace Models
{
	public class Company : object
	//public class User : System.Object
	{
		public Company() : base()
		{
		}

		public int Id { get; set; }

		public string Companyname { get; set; }
		//public string UserName { get; set; }

		public string Product { get; set; }
		//public string PassWord { get; set; }

		public int Age { get; set; }

		//public string IP { get; set; }

		//public System.DateTime RegisterDateTime { get; set; }
	}
}
