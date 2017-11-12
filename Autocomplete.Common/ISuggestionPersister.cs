using System.Collections.Generic;

namespace Autocomplete.Common
{
	public interface ISuggestionPersister
	{
		void Persist(string suggestion);
		void Persist(IEnumerable<string> suggestions);
	}
}
