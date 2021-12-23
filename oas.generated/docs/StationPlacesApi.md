# transpa.api.generated.Api.StationPlacesApi

All URIs are relative to *https://api.mytranspa.com/publicApi*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetStationplaces**](StationPlacesApi.md#getstationplaces) | **GET** /v1/stationPlaces | Return a list of Station Places


<a name="getstationplaces"></a>
# **GetStationplaces**
> InlineResponse2002 GetStationplaces ()

Return a list of Station Places

Required scope transpaapi:stationplaces:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class GetStationplacesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new StationPlacesApi(config);

            try
            {
                // Return a list of Station Places
                InlineResponse2002 result = apiInstance.GetStationplaces();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StationPlacesApi.GetStationplaces: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**InlineResponse2002**](InlineResponse2002.md)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **403** | Insufficient access |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

