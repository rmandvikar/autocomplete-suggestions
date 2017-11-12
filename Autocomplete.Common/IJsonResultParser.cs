using System.Collections.Generic;

namespace Autocomplete.Common
{
	public interface IJsonResultParser
	{
		IEnumerable<string> Parse(string json);
	}
}
