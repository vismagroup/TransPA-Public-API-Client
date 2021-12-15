/*
 * TransPA Public API
 *
 * # This API exposes functionality in Visma TransPA. ## Authentication The API can be tested without authentication against the mock server in the **Servers** pulldown list. To test against the actual backend you have to register a user on the *Visma Developer Portal* and request access to the API along with the required scopes. The scopes required are documented under each endpoint. For more information about *Visma Developer Portal* see <https://developer.visma.com>. <br/> <br/>  Authentication uses OAuth tokens from *Visma Connect*. Authorization is done on tenant level, so one OAuth token is needed per tenant.<br/> <br/> The scope `transpaapi:api` grants basic access to the API and is required for all routes. Additional scopes might be required and are described inside each route description. <br/> ## Details All monetary amounts are in the organizations local currency unless otherwise specified. <br/> <br/> ```[Not Ready]``` This is an endpoint where development has not completed and is therefore subject to change. 
 *
 * The version of the OpenAPI document: 0.1.15
 * Contact: utveckling.transpa@visma.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using transpa.api.generated.Client;
using transpa.api.generated.Model;

namespace transpa.api.generated.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISalariesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Subscribe to a WebHook for the active tenant
        /// </summary>
        /// <remarks>
        /// Start subscribing on the provided url for updates regarding the salaries resource. &lt;br/&gt; Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="inlineObject"></param>
        /// <returns></returns>
        void CreateSubscriptionSalaries(InlineObject inlineObject);

        /// <summary>
        /// Subscribe to a WebHook for the active tenant
        /// </summary>
        /// <remarks>
        /// Start subscribing on the provided url for updates regarding the salaries resource. &lt;br/&gt; Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="inlineObject"></param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> CreateSubscriptionSalariesWithHttpInfo(InlineObject inlineObject);
        /// <summary>
        /// Unsubscribe the WebHook for the active tenant
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns></returns>
        void CreateUnsubscribtionSalaries();

        /// <summary>
        /// Unsubscribe the WebHook for the active tenant
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> CreateUnsubscribtionSalariesWithHttpInfo();
        /// <summary>
        /// [Not Ready] Get a salary
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Resource ID</param>
        /// <returns>Salary</returns>
        Salary GetSalary(string id);

        /// <summary>
        /// [Not Ready] Get a salary
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Resource ID</param>
        /// <returns>ApiResponse of Salary</returns>
        ApiResponse<Salary> GetSalaryWithHttpInfo(string id);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISalariesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Subscribe to a WebHook for the active tenant
        /// </summary>
        /// <remarks>
        /// Start subscribing on the provided url for updates regarding the salaries resource. &lt;br/&gt; Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="inlineObject"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task CreateSubscriptionSalariesAsync(InlineObject inlineObject, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Subscribe to a WebHook for the active tenant
        /// </summary>
        /// <remarks>
        /// Start subscribing on the provided url for updates regarding the salaries resource. &lt;br/&gt; Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="inlineObject"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> CreateSubscriptionSalariesWithHttpInfoAsync(InlineObject inlineObject, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Unsubscribe the WebHook for the active tenant
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task CreateUnsubscribtionSalariesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Unsubscribe the WebHook for the active tenant
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> CreateUnsubscribtionSalariesWithHttpInfoAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// [Not Ready] Get a salary
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Resource ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Salary</returns>
        System.Threading.Tasks.Task<Salary> GetSalaryAsync(string id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// [Not Ready] Get a salary
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:salaries:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Resource ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Salary)</returns>
        System.Threading.Tasks.Task<ApiResponse<Salary>> GetSalaryWithHttpInfoAsync(string id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISalariesApi : ISalariesApiSync, ISalariesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class SalariesApi : ISalariesApi
    {
        private transpa.api.generated.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalariesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SalariesApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalariesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SalariesApi(string basePath)
        {
            this.Configuration = transpa.api.generated.Client.Configuration.MergeConfigurations(
                transpa.api.generated.Client.GlobalConfiguration.Instance,
                new transpa.api.generated.Client.Configuration { BasePath = basePath }
            );
            this.Client = new transpa.api.generated.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new transpa.api.generated.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = transpa.api.generated.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalariesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public SalariesApi(transpa.api.generated.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = transpa.api.generated.Client.Configuration.MergeConfigurations(
                transpa.api.generated.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new transpa.api.generated.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new transpa.api.generated.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = transpa.api.generated.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalariesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public SalariesApi(transpa.api.generated.Client.ISynchronousClient client, transpa.api.generated.Client.IAsynchronousClient asyncClient, transpa.api.generated.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = transpa.api.generated.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public transpa.api.generated.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public transpa.api.generated.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public transpa.api.generated.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public transpa.api.generated.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Subscribe to a WebHook for the active tenant Start subscribing on the provided url for updates regarding the salaries resource. &lt;br/&gt; Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="inlineObject"></param>
        /// <returns></returns>
        public void CreateSubscriptionSalaries(InlineObject inlineObject)
        {
            CreateSubscriptionSalariesWithHttpInfo(inlineObject);
        }

        /// <summary>
        /// Subscribe to a WebHook for the active tenant Start subscribing on the provided url for updates regarding the salaries resource. &lt;br/&gt; Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="inlineObject"></param>
        /// <returns>ApiResponse of Object(void)</returns>
        public transpa.api.generated.Client.ApiResponse<Object> CreateSubscriptionSalariesWithHttpInfo(InlineObject inlineObject)
        {
            // verify the required parameter 'inlineObject' is set
            if (inlineObject == null)
                throw new transpa.api.generated.Client.ApiException(400, "Missing required parameter 'inlineObject' when calling SalariesApi->CreateSubscriptionSalaries");

            transpa.api.generated.Client.RequestOptions localVarRequestOptions = new transpa.api.generated.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/problem+json"
            };

            var localVarContentType = transpa.api.generated.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = transpa.api.generated.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = inlineObject;

            // authentication (visma_connect) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/v1/subscribe/salaries", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateSubscriptionSalaries", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Subscribe to a WebHook for the active tenant Start subscribing on the provided url for updates regarding the salaries resource. &lt;br/&gt; Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="inlineObject"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task CreateSubscriptionSalariesAsync(InlineObject inlineObject, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await CreateSubscriptionSalariesWithHttpInfoAsync(inlineObject, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to a WebHook for the active tenant Start subscribing on the provided url for updates regarding the salaries resource. &lt;br/&gt; Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="inlineObject"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<transpa.api.generated.Client.ApiResponse<Object>> CreateSubscriptionSalariesWithHttpInfoAsync(InlineObject inlineObject, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'inlineObject' is set
            if (inlineObject == null)
                throw new transpa.api.generated.Client.ApiException(400, "Missing required parameter 'inlineObject' when calling SalariesApi->CreateSubscriptionSalaries");


            transpa.api.generated.Client.RequestOptions localVarRequestOptions = new transpa.api.generated.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/problem+json"
            };


            var localVarContentType = transpa.api.generated.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = transpa.api.generated.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = inlineObject;

            // authentication (visma_connect) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/v1/subscribe/salaries", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateSubscriptionSalaries", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Unsubscribe the WebHook for the active tenant Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns></returns>
        public void CreateUnsubscribtionSalaries()
        {
            CreateUnsubscribtionSalariesWithHttpInfo();
        }

        /// <summary>
        /// Unsubscribe the WebHook for the active tenant Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of Object(void)</returns>
        public transpa.api.generated.Client.ApiResponse<Object> CreateUnsubscribtionSalariesWithHttpInfo()
        {
            transpa.api.generated.Client.RequestOptions localVarRequestOptions = new transpa.api.generated.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/problem+json"
            };

            var localVarContentType = transpa.api.generated.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = transpa.api.generated.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);


            // authentication (visma_connect) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/v1/unsubscribe/salaries", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateUnsubscribtionSalaries", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Unsubscribe the WebHook for the active tenant Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task CreateUnsubscribtionSalariesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await CreateUnsubscribtionSalariesWithHttpInfoAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Unsubscribe the WebHook for the active tenant Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<transpa.api.generated.Client.ApiResponse<Object>> CreateUnsubscribtionSalariesWithHttpInfoAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            transpa.api.generated.Client.RequestOptions localVarRequestOptions = new transpa.api.generated.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/problem+json"
            };


            var localVarContentType = transpa.api.generated.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = transpa.api.generated.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);


            // authentication (visma_connect) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/v1/unsubscribe/salaries", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateUnsubscribtionSalaries", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// [Not Ready] Get a salary Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Resource ID</param>
        /// <returns>Salary</returns>
        public Salary GetSalary(string id)
        {
            transpa.api.generated.Client.ApiResponse<Salary> localVarResponse = GetSalaryWithHttpInfo(id);
            return localVarResponse.Data;
        }

        /// <summary>
        /// [Not Ready] Get a salary Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Resource ID</param>
        /// <returns>ApiResponse of Salary</returns>
        public transpa.api.generated.Client.ApiResponse<Salary> GetSalaryWithHttpInfo(string id)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                throw new transpa.api.generated.Client.ApiException(400, "Missing required parameter 'id' when calling SalariesApi->GetSalary");

            transpa.api.generated.Client.RequestOptions localVarRequestOptions = new transpa.api.generated.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = transpa.api.generated.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = transpa.api.generated.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", transpa.api.generated.Client.ClientUtils.ParameterToString(id)); // path parameter

            // authentication (visma_connect) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Salary>("/v1/salaries/{id}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetSalary", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// [Not Ready] Get a salary Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Resource ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Salary</returns>
        public async System.Threading.Tasks.Task<Salary> GetSalaryAsync(string id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            transpa.api.generated.Client.ApiResponse<Salary> localVarResponse = await GetSalaryWithHttpInfoAsync(id, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// [Not Ready] Get a salary Required scope transpaapi:salaries:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Resource ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Salary)</returns>
        public async System.Threading.Tasks.Task<transpa.api.generated.Client.ApiResponse<Salary>> GetSalaryWithHttpInfoAsync(string id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'id' is set
            if (id == null)
                throw new transpa.api.generated.Client.ApiException(400, "Missing required parameter 'id' when calling SalariesApi->GetSalary");


            transpa.api.generated.Client.RequestOptions localVarRequestOptions = new transpa.api.generated.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };


            var localVarContentType = transpa.api.generated.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = transpa.api.generated.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", transpa.api.generated.Client.ClientUtils.ParameterToString(id)); // path parameter

            // authentication (visma_connect) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<Salary>("/v1/salaries/{id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetSalary", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
