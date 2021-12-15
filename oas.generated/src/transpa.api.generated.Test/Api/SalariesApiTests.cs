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
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using Xunit;

using transpa.api.generated.Client;
using transpa.api.generated.Api;
// uncomment below to import models
//using transpa.api.generated.Model;

namespace transpa.api.generated.Test.Api
{
    /// <summary>
    ///  Class for testing SalariesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class SalariesApiTests : IDisposable
    {
        private SalariesApi instance;

        public SalariesApiTests()
        {
            instance = new SalariesApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of SalariesApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' SalariesApi
            //Assert.IsType<SalariesApi>(instance);
        }

        /// <summary>
        /// Test CreateSubscriptionSalaries
        /// </summary>
        [Fact]
        public void CreateSubscriptionSalariesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //InlineObject inlineObject = null;
            //instance.CreateSubscriptionSalaries(inlineObject);
        }

        /// <summary>
        /// Test CreateUnsubscribtionSalaries
        /// </summary>
        [Fact]
        public void CreateUnsubscribtionSalariesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.CreateUnsubscribtionSalaries();
        }

        /// <summary>
        /// Test GetSalary
        /// </summary>
        [Fact]
        public void GetSalaryTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //var response = instance.GetSalary(id);
            //Assert.IsType<Salary>(response);
        }
    }
}
