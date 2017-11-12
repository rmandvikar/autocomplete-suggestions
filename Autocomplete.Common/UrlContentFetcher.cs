using System;
using System.Net;
using rm.Logging;
using rm.Extensions;

namespace Autocomplete.Common
{
	public class UrlContentFetcher
	{
		private readonly ILogger log = Log.OfType<UrlContentFetcher>();

		public string Fetch(string url)
		{
			url.ThrowIfNullOrEmpty(nameof(url));
			try
			{
				using (WebClient client = new WebClient())
				{
					var json = client.DownloadString(url);
					return json;
				}
			}
			catch (Exception ex)
			{
				log.Error($"Cannot fetch content of url '{url}'.", ex);
				return "";
			}
		}
	}
}
