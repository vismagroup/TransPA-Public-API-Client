# transpa.api.generated.Api.SalariesApi

All URIs are relative to *https://api.mytranspa.com/publicApi*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateSubscriptionSalaries**](SalariesApi.md#createsubscriptionsalaries) | **POST** /v1/subscribe/salaries | Subscribe to a WebHook for the active tenant
[**CreateUnsubscribtionSalaries**](SalariesApi.md#createunsubscribtionsalaries) | **POST** /v1/unsubscribe/salaries | Unsubscribe the WebHook for the active tenant
[**GetSalary**](SalariesApi.md#getsalary) | **GET** /v1/salaries/{id} | [Not Ready] Get a salary


<a name="createsubscriptionsalaries"></a>
# **CreateSubscriptionSalaries**
> void CreateSubscriptionSalaries (InlineObject inlineObject)

Subscribe to a WebHook for the active tenant

Start subscribing on the provided url for updates regarding the salaries resource. <br/> Required scope transpaapi:salaries:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class CreateSubscriptionSalariesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new SalariesApi(config);
            var inlineObject = new InlineObject(); // InlineObject | 

            try
            {
                // Subscribe to a WebHook for the active tenant
                apiInstance.CreateSubscriptionSalaries(inlineObject);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SalariesApi.CreateSubscriptionSalaries: " + e.Message );
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
 **inlineObject** | [**InlineObject**](InlineObject.md)|  | 

### Return type

void (empty response body)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | WebHook created |  -  |
| **403** | Insufficient access |  -  |
| **409** | Resource attribute conflict |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="createunsubscribtionsalaries"></a>
# **CreateUnsubscribtionSalaries**
> void CreateUnsubscribtionSalaries ()

Unsubscribe the WebHook for the active tenant

Required scope transpaapi:salaries:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class CreateUnsubscribtionSalariesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new SalariesApi(config);

            try
            {
                // Unsubscribe the WebHook for the active tenant
                apiInstance.CreateUnsubscribtionSalaries();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SalariesApi.CreateUnsubscribtionSalaries: " + e.Message );
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

void (empty response body)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | WebHook removed |  -  |
| **403** | Insufficient access |  -  |
| **404** | No WebHook |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getsalary"></a>
# **GetSalary**
> Salary GetSalary (string id)

[Not Ready] Get a salary

Required scope transpaapi:salaries:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class GetSalaryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new SalariesApi(config);
            var id = id_example;  // string | Resource ID

            try
            {
                // [Not Ready] Get a salary
                Salary result = apiInstance.GetSalary(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SalariesApi.GetSalary: " + e.Message );
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
 **id** | **string**| Resource ID | 

### Return type

[**Salary**](Salary.md)

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
| **404** | Salary does not exist |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

