/*
 * TransPA Public API
 *
 * # This API exposes functionality in Visma TransPA. ## Authentication The API can be tested without authentication against the mock server in the **Servers** pulldown list. To test against the actual backend you have to register a user on the *Visma Developer Portal* and request access to the API along with the required scopes. The scopes required are documented under each endpoint. For more information about *Visma Developer Portal* see <https://developer.visma.com>. <br/> <br/>  Authentication uses OAuth tokens from *Visma Connect*. Authorization is done on tenant level, so one OAuth token is needed per tenant.<br/> <br/> The scope `transpaapi:api` grants basic access to the API and is required for all routes. Additional scopes might be required and are described inside each route description. <br/> ## Details All monetary amounts are in the organizations local currency unless otherwise specified. <br/> <br/> ```[Not Ready]``` This is an endpoint where development has not completed and is therefore subject to change. 
 *
 * The version of the OpenAPI document: 0.1.15
 * Contact: utveckling.transpa@visma.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using Xunit;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using transpa.api.generated.Api;
using transpa.api.generated.Model;
using transpa.api.generated.Client;
using System.Reflection;
using Newtonsoft.Json;

namespace transpa.api.generated.Test.Model
{
    /// <summary>
    ///  Class for testing SalaryCreated
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the model.
    /// </remarks>
    public class SalaryCreatedTests : IDisposable
    {
        // TODO uncomment below to declare an instance variable for SalaryCreated
        //private SalaryCreated instance;

        public SalaryCreatedTests()
        {
            // TODO uncomment below to create an instance of SalaryCreated
            //instance = new SalaryCreated();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of SalaryCreated
        /// </summary>
        [Fact]
        public void SalaryCreatedInstanceTest()
        {
            // TODO uncomment below to test "IsType" SalaryCreated
            //Assert.IsType<SalaryCreated>(instance);
        }


        /// <summary>
        /// Test the property 'Title'
        /// </summary>
        [Fact]
        public void TitleTest()
        {
            // TODO unit test for the property 'Title'
        }
        /// <summary>
        /// Test the property 'ResourceUrl'
        /// </summary>
        [Fact]
        public void ResourceUrlTest()
        {
            // TODO unit test for the property 'ResourceUrl'
        }
        /// <summary>
        /// Test the property 'TenantId'
        /// </summary>
        [Fact]
        public void TenantIdTest()
        {
            // TODO unit test for the property 'TenantId'
        }

    }

}
