using Autocomplete.Common;
using rm.Extensions;

namespace Autocomplete.Amazon
{
	public class SearchUrlBuilder : ISearchUrlBuilder
	{
		public const string UrlFormat = "http://completion.amazon.com/search/complete?search-alias=aps&client=amazon-search-ui&mkt=1&q={0}";

		public string GetUrl(string search)
		{
			search.ThrowIfNullOrEmptyArgument(nameof(search));
			return string.Format(UrlFormat, search);
		}
	}
}
