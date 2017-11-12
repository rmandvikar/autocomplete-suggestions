using System.Collections.Generic;
using Autocomplete.Common;
using rm.Extensions;
using rm.Logging;
using rm.Trie;

namespace Autocomplete.Amazon
{
	public class Crawler : ICrawler
	{
		private readonly ILogger log = Log.OfType<Crawler>();

		private readonly ISearchUrlBuilder searchUrlBuilder;
		private readonly IJsonResultParser jsonResultParser;
		private readonly ISuggestionPersister suggestionPersister;
		private readonly UrlContentFetcher urlContentFetcher;
		private readonly SeedSearch seedSearch;

		public Crawler(
			ISearchUrlBuilder searchUrlBuilder,
			IJsonResultParser jsonResultParser,
			ISuggestionPersister suggestionPersister,
			UrlContentFetcher urlContentFetcher,
			SeedSearch seedSearch
			)
		{
			searchUrlBuilder.ThrowIfArgumentNull(nameof(searchUrlBuilder));
			jsonResultParser.ThrowIfArgumentNull(nameof(jsonResultParser));
			suggestionPersister.ThrowIfArgumentNull(nameof(suggestionPersister));
			urlContentFetcher.ThrowIfArgumentNull(nameof(urlContentFetcher));
			seedSearch.ThrowIfArgumentNull(nameof(seedSearch));
			this.searchUrlBuilder = searchUrlBuilder;
			this.jsonResultParser = jsonResultParser;
			this.suggestionPersister = suggestionPersister;
			this.urlContentFetcher = urlContentFetcher;
			this.seedSearch = seedSearch;
		}

		public void Start()
		{
			log.Info("Crawling...");
			var searched = new Trie();
			var autocompleteSuggestions = new Trie();
			var qSearches = new Deque<string>();
			foreach (var s in seedSearch.Get())
			{
				qSearches.Enqueue(s);
			}
			while (!qSearches.IsEmpty())
			{
				var search = qSearches.Dequeue();
				if (searched.HasWord(search))
				{
					continue;
				}
				searched.AddWord(search);
				var json = urlContentFetcher.Fetch(searchUrlBuilder.GetUrl(search));
				var suggestions = jsonResultParser.Parse(json);
				var suggestionsNew = new List<string>();
				foreach (var suggestion in suggestions)
				{
					if (!searched.HasWord(suggestion))
					{
						qSearches.Enqueue(suggestion);
					}
					if (!autocompleteSuggestions.HasWord(suggestion))
					{
						autocompleteSuggestions.AddWord(suggestion);
						suggestionsNew.Add(suggestion);
					}
				}
				suggestionPersister.Persist(suggestionsNew);
			}
			log.Info("Crawling done.");
		}
	}
}
