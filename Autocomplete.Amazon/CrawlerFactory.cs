using System;
using Autocomplete.Common;

namespace Autocomplete.Amazon
{
	public static class CrawlerFactory
	{
		public static ICrawler Get()
		{
			return new Crawler
			(
				new SearchUrlBuilder(),
				new JsonResultParser(),
				new SuggestionFilePersister(
					$"amazon.suggestions-{DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ss.fffZ")}.txt"
					),
				new UrlContentFetcher(),
				new SeedSearch()
			);
		}
	}
}
