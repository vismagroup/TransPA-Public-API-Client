# transpa.api.generated.Api.VehicleGroupsApi

All URIs are relative to *https://api.mytranspa.com/publicApi*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetVehiclegroups**](VehicleGroupsApi.md#getvehiclegroups) | **GET** /v1/vehicleGroups | Return a list of Vehicle Groups


<a name="getvehiclegroups"></a>
# **GetVehiclegroups**
> InlineResponse2005 GetVehiclegroups ()

Return a list of Vehicle Groups

Required scope transpaapi:vehiclegroups:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class GetVehiclegroupsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new VehicleGroupsApi(config);

            try
            {
                // Return a list of Vehicle Groups
                InlineResponse2005 result = apiInstance.GetVehiclegroups();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VehicleGroupsApi.GetVehiclegroups: " + e.Message );
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

[**InlineResponse2005**](InlineResponse2005.md)

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

