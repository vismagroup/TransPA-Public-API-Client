# transpa.api.generated.Api.VehiclesApi

All URIs are relative to *https://api.mytranspa.com/publicApi*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DeleteVehicle**](VehiclesApi.md#deletevehicle) | **DELETE** /v1/vehicles/{id} | Delete a vehicle
[**GetVehicle**](VehiclesApi.md#getvehicle) | **GET** /v1/vehicles/{id} | Get a vehicle
[**GetVehicles**](VehiclesApi.md#getvehicles) | **GET** /v1/vehicles | Return a list of vehicles
[**PostVehicle**](VehiclesApi.md#postvehicle) | **POST** /v1/vehicles | Create a vehicle
[**PutVehicle**](VehiclesApi.md#putvehicle) | **PUT** /v1/vehicles/{id} | Update a vehicle


<a name="deletevehicle"></a>
# **DeleteVehicle**
> void DeleteVehicle (string id)

Delete a vehicle

Delete a vehicle  Required scope transpaapi:vehicles:delete 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class DeleteVehicleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new VehiclesApi(config);
            var id = id_example;  // string | Resource ID

            try
            {
                // Delete a vehicle
                apiInstance.DeleteVehicle(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VehiclesApi.DeleteVehicle: " + e.Message );
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

void (empty response body)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | Removed |  -  |
| **403** | Insufficient access |  -  |
| **404** | Vehicle does not exist |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getvehicle"></a>
# **GetVehicle**
> Vehicle GetVehicle (string id)

Get a vehicle

Get a single vehicle by id  Required scope transpaapi:vehicles:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class GetVehicleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new VehiclesApi(config);
            var id = id_example;  // string | Resource ID

            try
            {
                // Get a vehicle
                Vehicle result = apiInstance.GetVehicle(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VehiclesApi.GetVehicle: " + e.Message );
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

[**Vehicle**](Vehicle.md)

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
| **404** | Vehicle does not exist |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getvehicles"></a>
# **GetVehicles**
> InlineResponse2004 GetVehicles (string cursor = null, int? limit = null, bool? includeInactive = null)

Return a list of vehicles

Retrieves all vehicles the tenant have access to.  Required scope transpaapi:vehicles:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class GetVehiclesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new VehiclesApi(config);
            var cursor = cursor_example;  // string | Pagination token used to query for the next part of a list. (optional) 
            var limit = 56;  // int? | Limits the number of Vehicles returned. Default and maximum is 100 (optional) 
            var includeInactive = true;  // bool? | Will include non active vehicles if set to true (optional)  (default to false)

            try
            {
                // Return a list of vehicles
                InlineResponse2004 result = apiInstance.GetVehicles(cursor, limit, includeInactive);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VehiclesApi.GetVehicles: " + e.Message );
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
 **limit** | **int?**| Limits the number of Vehicles returned. Default and maximum is 100 | [optional] 
 **includeInactive** | **bool?**| Will include non active vehicles if set to true | [optional] [default to false]

### Return type

[**InlineResponse2004**](InlineResponse2004.md)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Return a list of available vehicles |  -  |
| **400** | Bad request. Invalid request made by the client |  -  |
| **403** | Insufficient access |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="postvehicle"></a>
# **PostVehicle**
> Vehicle PostVehicle (Vehicle vehicle)

Create a vehicle

Create a Vehicle  Required scope transpaapi:vehicles:write 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class PostVehicleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new VehiclesApi(config);
            var vehicle = new Vehicle(); // Vehicle | 

            try
            {
                // Create a vehicle
                Vehicle result = apiInstance.PostVehicle(vehicle);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VehiclesApi.PostVehicle: " + e.Message );
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
 **vehicle** | [**Vehicle**](Vehicle.md)|  | 

### Return type

[**Vehicle**](Vehicle.md)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** |  |  -  |
| **400** | Bad request. Invalid request made by the client |  -  |
| **403** | Insufficient access |  -  |
| **409** | Resource attribute conflict |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="putvehicle"></a>
# **PutVehicle**
> Vehicle PutVehicle (string id)

Update a vehicle

Update a vehicle  Required scope transpaapi:vehicles:write 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class PutVehicleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new VehiclesApi(config);
            var id = id_example;  // string | Resource ID

            try
            {
                // Update a vehicle
                Vehicle result = apiInstance.PutVehicle(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VehiclesApi.PutVehicle: " + e.Message );
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

[**Vehicle**](Vehicle.md)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** | Bad request. Invalid request made by the client |  -  |
| **403** | Insufficient access |  -  |
| **404** | Vehicle does not exist |  -  |
| **409** | Resource attribute conflict |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

