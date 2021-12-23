# transpa.api.generated.Api.EmployeesApi

All URIs are relative to *https://api.mytranspa.com/publicApi*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetEmployee**](EmployeesApi.md#getemployee) | **GET** /v1/employees/{id} | [Not ready] Get an Employee
[**GetEmployees**](EmployeesApi.md#getemployees) | **GET** /v1/employees | [Not ready] Return a list of Employees


<a name="getemployee"></a>
# **GetEmployee**
> Employee GetEmployee (string id)

[Not ready] Get an Employee

Required scope transpaapi:employees:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class GetEmployeeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new EmployeesApi(config);
            var id = id_example;  // string | Resource ID

            try
            {
                // [Not ready] Get an Employee
                Employee result = apiInstance.GetEmployee(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EmployeesApi.GetEmployee: " + e.Message );
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

[**Employee**](Employee.md)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Get a single Employee by id |  -  |
| **400** | Bad request. Invalid request made by the client |  -  |
| **403** | Insufficient access |  -  |
| **404** | Employee does not exist |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getemployees"></a>
# **GetEmployees**
> InlineResponse2001 GetEmployees (string cursor = null, int? limit = null, bool? includeInactive = null)

[Not ready] Return a list of Employees

Returns a list of Employees  Required scope transpaapi:employees:read 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using transpa.api.generated.Api;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace Example
{
    public class GetEmployeesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.mytranspa.com/publicApi";
            // Configure OAuth2 access token for authorization: visma_connect
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new EmployeesApi(config);
            var cursor = cursor_example;  // string | Pagination token used to query for the next part of a list. (optional) 
            var limit = 56;  // int? | Limits the number of Employees returned. Default and maximum is 100 (optional) 
            var includeInactive = true;  // bool? | Will include non active employees if set to true (optional)  (default to false)

            try
            {
                // [Not ready] Return a list of Employees
                InlineResponse2001 result = apiInstance.GetEmployees(cursor, limit, includeInactive);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EmployeesApi.GetEmployees: " + e.Message );
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
 **limit** | **int?**| Limits the number of Employees returned. Default and maximum is 100 | [optional] 
 **includeInactive** | **bool?**| Will include non active employees if set to true | [optional] [default to false]

### Return type

[**InlineResponse2001**](InlineResponse2001.md)

### Authorization

[visma_connect](../README.md#visma_connect)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Return a list of Employees |  -  |
| **400** | Bad request. Invalid request made by the client |  -  |
| **403** | Insufficient access |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

