using System;
using Autocomplete.Amazon;
using Autocomplete.Common;

namespace Autocomplete.Console
{
	public static class CrawlerFactory
	{
		public static ICrawler Get(Crawler crawler)
		{
			switch (crawler)
			{
				case Crawler.Amazon:
					return new Amazon.Crawler
					(
						new SearchUrlBuilder(),
						new JsonResultParser(),
						new SuggestionFilePersister(
							$"amazon.suggestions-{DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ss.fffZ")}.txt"
							),
						new UrlContentFetcher(),
						new SeedSearch()
					);
				default:
					throw new ArgumentOutOfRangeException(crawler.ToString());
			}
		}
	}
}
