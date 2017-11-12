using System;
using System.Collections.Generic;
using System.Linq;
using Autocomplete.Common;
using log4net;
using Newtonsoft.Json.Linq;
using rm.Extensions;
using rm.Logging;

namespace Autocomplete.Amazon
{
	public class JsonResultParser : IJsonResultParser
	{
		private readonly ILogger log = Log.OfType<JsonResultParser>();

		public IEnumerable<string> Parse(string json)
		{
			var items = new List<string>();
			try
			{
				json.ThrowIfNullOrEmptyArgument(nameof(json));
				JArray completion = JArray.Parse(json);
				var search = completion[0].ToString();
				var suggestions = completion[1];
				foreach (JValue suggestion in suggestions)
				{
					items.Add(suggestion.Value.ToString());
				}
			}
			catch (Exception ex)
			{
				log.Error($"Cannot parse json '{json}'.", ex);
			}
			return items;
		}
	}
}
