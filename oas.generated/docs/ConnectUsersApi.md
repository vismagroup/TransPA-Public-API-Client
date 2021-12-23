# transpa.api.generated.Api.ConnectUsersApi

All URIs are relative to *https://api.mytranspa.com/publicApi*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetConnectUserController**](ConnectUsersApi.md#getconnectusercontroller) | **GET** /v1/connectUsers | Returns a list of ConnectUsers for every SystemUser in TransPA


<a name="getconnectusercontroller"></a>
# **GetConnectUserController**
> InlineResponse200 GetConnectUserController (string cursor = null, int? limit = null)

Returns a list of ConnectUsers for every SystemUser in TransPA

Returns a list of ConnectUsers for every SystemUser in TransPA.  This route is intended for internal Visma use only.  Required scope transpaapi:connectusers:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class GetConnectUserControllerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ConnectUsersApi(config);
            var cursor = cursor_example;  // string | Pagination token used to query for the next part of a list. (optional) 
            var limit = 56;  // int? | Limits the number of SystemUsers returned. Default and maximum is 100 (optional) 

            try
            {
                // Returns a list of ConnectUsers for every SystemUser in TransPA
                InlineResponse200 result = apiInstance.GetConnectUserController(cursor, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ConnectUsersApi.GetConnectUserController: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **cursor** | **string**| Pagination token used to query for the next part of a list. | [optional] 
 **limit** | **int?**| Limits the number of SystemUsers returned. Default and maximum is 100 | [optional] 

### Return type

[**InlineResponse200**](InlineResponse200.md)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Return a list of ConnectUsers |  -  |
| **400** | Validation fail (e.g., bad limit or non existing cursor) |  -  |
| **403** | Insufficient access |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

