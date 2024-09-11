//using AspNetCore;
using CNewsProject.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Globalization;
using System.Net.Http;


namespace CNewsProject.Models.Statistics
{
	public class Statistics
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HttpClient _httpClient;
		public Statistics(ApplicationDbContext dbContext, HttpClient httpClient)
		{
			_dbContext = dbContext;
			_httpClient = httpClient;
		}

		public int countNewCustomer()
		{
			int number = _dbContext.Users.Count();          // How many who has a acount
			List<AppUser> users = _dbContext.Users.OrderByDescending(u=>u.TimeCreateCustomer).ToList();
			int timeA = DateTime.Now.Year;
			int timeB = DateTime.Now.Month;
			
			int count = 0;

			foreach (var user in users)
			{
				if (timeB == 1)
				{
					if ((timeA-1) == user.TimeCreateCustomer.Year)
					{
						int timeC = 12;
						if (timeC == user.TimeCreateCustomer.Month)
						{
							count++;
						}

					}

				}
				else if (timeA == user.TimeCreateCustomer.Year)
				{
					if ((timeB - 1) == user.TimeCreateCustomer.Month)
					{
						count++;
					}
				}
		
			}

			return count;
		}



			public class Statisticscounter
		{
			////	<!-- Kopiera hela koden och klistra in där du vill ha din besöksräknare //-->
			//	<a href = "http://www.seoett.com/raknare/" >
			//	<img border = "0" alt= "" src= "http://www.seoett.com/raknare/count3.php?url=minNyaSida.com&cs=13" /></ a >

			public int FetchNumberOfVisitors(int count)

			{
				int i = count;
				// Hämta antal besökare per sida/artikel
				// skapa beräkningar till procent.
				// skapa upp diagram.

				return i;
			}

			public class WeatherHistory
			{


				// sök väder i historia 45 år bakåt 
				//https://history.openweathermap.org/data/2.5/history/city?lat={lat}&lon={lon}&type=hour&start={start}&end={end}&appid={API key}

			}
		}






	}
}

		


	
	

