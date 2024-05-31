using System.Text.Json.Serialization;
using System.Text.Json;

namespace Client.Files;

public static class JsonExtensions
{
	public static JsonSerializerOptions GetJsonSerializerOptions()
	{
		var options = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		};

		options.Converters.Add(new DateOnlyJsonConverter());

		return options;
	}
}