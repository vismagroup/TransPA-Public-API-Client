/*
 * TransPA Public API
 *
 * # This API exposes functionality in Visma TransPA. ## Authentication The API can be tested without authentication against the mock server in the **Servers** pulldown list. To test against the actual backend you have to register a user on the *Visma Developer Portal* and request access to the API along with the required scopes. The scopes required are documented under each endpoint. For more information about *Visma Developer Portal* see <https://developer.visma.com>. <br/> <br/>  Authentication uses OAuth tokens from *Visma Connect*. Authorization is done on tenant level, so one OAuth token is needed per tenant.<br/> <br/> The scope `transpaapi:api` grants basic access to the API and is required for all routes. Additional scopes might be required and are described inside each route description. <br/> ## Details All monetary amounts are in the organizations local currency unless otherwise specified. <br/> <br/> ```[Not Ready]``` This is an endpoint where development has not completed and is therefore subject to change. 
 *
 * The version of the OpenAPI document: 0.1.21
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
    public interface IWorkTasksApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Return a list of Work Tasks
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:worktasks:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>InlineResponse2006</returns>
        InlineResponse2006 GetWorktasks();

        /// <summary>
        /// Return a list of Work Tasks
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:worktasks:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of InlineResponse2006</returns>
        ApiResponse<InlineResponse2006> GetWorktasksWithHttpInfo();
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IWorkTasksApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Return a list of Work Tasks
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:worktasks:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of InlineResponse2006</returns>
        System.Threading.Tasks.Task<InlineResponse2006> GetWorktasksAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Return a list of Work Tasks
        /// </summary>
        /// <remarks>
        /// Required scope transpaapi:worktasks:read 
        /// </remarks>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (InlineResponse2006)</returns>
        System.Threading.Tasks.Task<ApiResponse<InlineResponse2006>> GetWorktasksWithHttpInfoAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IWorkTasksApi : IWorkTasksApiSync, IWorkTasksApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class WorkTasksApi : IWorkTasksApi
    {
        private transpa.api.generated.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkTasksApi"/> class.
        /// </summary>
        /// <returns></returns>
        public WorkTasksApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkTasksApi"/> class.
        /// </summary>
        /// <returns></returns>
        public WorkTasksApi(string basePath)
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
        /// Initializes a new instance of the <see cref="WorkTasksApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public WorkTasksApi(transpa.api.generated.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="WorkTasksApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public WorkTasksApi(transpa.api.generated.Client.ISynchronousClient client, transpa.api.generated.Client.IAsynchronousClient asyncClient, transpa.api.generated.Client.IReadableConfiguration configuration)
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
        /// Return a list of Work Tasks Required scope transpaapi:worktasks:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>InlineResponse2006</returns>
        public InlineResponse2006 GetWorktasks()
        {
            transpa.api.generated.Client.ApiResponse<InlineResponse2006> localVarResponse = GetWorktasksWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// Return a list of Work Tasks Required scope transpaapi:worktasks:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of InlineResponse2006</returns>
        public transpa.api.generated.Client.ApiResponse<InlineResponse2006> GetWorktasksWithHttpInfo()
        {
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


            // authentication (visma_connect) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<InlineResponse2006>("/v1/workTasks", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetWorktasks", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Return a list of Work Tasks Required scope transpaapi:worktasks:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of InlineResponse2006</returns>
        public async System.Threading.Tasks.Task<InlineResponse2006> GetWorktasksAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            transpa.api.generated.Client.ApiResponse<InlineResponse2006> localVarResponse = await GetWorktasksWithHttpInfoAsync(cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Return a list of Work Tasks Required scope transpaapi:worktasks:read 
        /// </summary>
        /// <exception cref="transpa.api.generated.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (InlineResponse2006)</returns>
        public async System.Threading.Tasks.Task<transpa.api.generated.Client.ApiResponse<InlineResponse2006>> GetWorktasksWithHttpInfoAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

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


            // authentication (visma_connect) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<InlineResponse2006>("/v1/workTasks", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetWorktasks", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
