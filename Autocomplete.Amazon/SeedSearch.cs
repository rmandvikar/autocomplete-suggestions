using System.Collections.Generic;

namespace Autocomplete.Amazon
{
	public class SeedSearch
	{
		private readonly string seedSearches = "abcdefghijklmnopqrstuvwxyz";
		public IEnumerable<string> Get()
		{
			foreach (var ch in seedSearches)
			{
				yield return ch.ToString();
			}
		}
	}
}
