
﻿//using AspNetCore;
using CNewsProject.Migrations;
using MailKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Data;
using System.Globalization;
using System.Net.Http;


namespace CNewsProject.Models.Statistics
{
	public class Statistics
	{
		public TwoArrays CountNewCustomer(IEnumerable<Subscription> users)
		{
			int timeA = DateTime.Now.Year;
			int timeB = DateTime.Now.Month;
			timeA = timeA - 1;
			int i=0,j=0,k = 0, l=0;
			int[] subCount = new int[12];
            int[] subNewCount = new int[12];
            foreach (var user in users)
			{
				if (user.RenewedDate.Year < timeA && user.RenewedDate.Month < timeB)
				{
					i++;
				}
                if (user.ExpiresDate.Year < timeA && user.ExpiresDate.Month < timeB)
                {
                    i--;
                }

            }
            subCount[k] = i;
            k++;	
			while (DateTime.Now.Year != timeA || DateTime.Now.Month != timeB)
				{
				foreach (var user in users)
				{
					if (timeA == user.RenewedDate.Year)
					{
						if (timeB == user.RenewedDate.Month)
						{
							j++;
						}
					}
				}
                subNewCount[l] = j;
                l++;
				if (k < 12)
					{ 
					i = i + j;
					subCount[k] = i;
					k++;
					}
			    j = 0;
                timeB = timeB + 1;
                if (timeB == 13)
                {
					timeB = 1;
                    timeA++;
                }              
            }
			TwoArrays twoArrays = new() { NewSubCount = subNewCount, SubCount = subCount };
			return twoArrays;
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
		}
	}

    public class TwoArrays
    {
        public int[] SubCount { get; set; }
        public int[] NewSubCount { get; set; }

    }
}



