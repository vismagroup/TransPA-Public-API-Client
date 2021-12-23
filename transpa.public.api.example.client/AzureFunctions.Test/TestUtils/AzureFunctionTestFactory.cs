using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;

namespace AzureFunctions.Test.TestUtils;

public class AzureFunctionTestFactory
{
    private static Dictionary<string, StringValues> CreateDictionary(List<Tuple<string, string>> queryParameters)
    {
        return queryParameters.ToDictionary<Tuple<string, string>, string, StringValues>(
            queryParameter => queryParameter.Item1,
            queryParameter => queryParameter.Item2);
    }

    public static HttpRequest CreateHttpRequest(List<Tuple<string, string>> queryParameter)
    {
        var context = new DefaultHttpContext();
        var request = context.Request;
        request.Query = new QueryCollection(CreateDictionary(queryParameter));
        return request;
    }

    public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
    {
        return type == LoggerTypes.List ? new ListLogger() : NullLoggerFactory.Instance.CreateLogger("Null Logger");
    }
}