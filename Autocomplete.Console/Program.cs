using System;
using rm.Logging;

namespace Autocomplete.Console
{
	class Program
	{
		private static ILogger log = Log.OfType<Program>();

		static void Main(string[] args)
		{
			try
			{
				CrawlerFactory.Get(Crawler.Amazon).Start();
			}
			catch (Exception ex)
			{
				log.Fatal("Exception while crawling.", ex);
			}
		}
	}
}
