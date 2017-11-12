using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autocomplete.Common;
using rm.Extensions;

namespace Autocomplete.Amazon
{
	public class SuggestionFilePersister : ISuggestionPersister
	{
		private string filepath;

		public SuggestionFilePersister(string filepath)
		{
			filepath.ThrowIfNullOrEmptyArgument(nameof(filepath));
			this.filepath = filepath;
		}

		public void Persist(string suggestion)
		{
			suggestion.ThrowIfNullOrEmptyArgument(nameof(suggestion));
			File.AppendAllText(filepath, suggestion);
		}
		public void Persist(IEnumerable<string> suggestions)
		{
			suggestions.ThrowIfArgumentNull(nameof(suggestions));
			File.AppendAllLines(filepath, suggestions.Where(s => !s.IsNullOrWhiteSpace()));
		}
	}
}