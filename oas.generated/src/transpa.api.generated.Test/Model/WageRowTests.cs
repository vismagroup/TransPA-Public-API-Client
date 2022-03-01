/*
 * TransPA Public API
 *
 * # This API exposes functionality in Visma TransPA. ## Authentication The API can be tested without authentication against the mock server in the **Servers** pulldown list. To test against the actual backend you have to register a user on the *Visma Developer Portal* and request access to the API along with the required scopes. The scopes required are documented under each endpoint. For more information about *Visma Developer Portal* see <https://developer.visma.com>. <br/> <br/>  Authentication uses OAuth tokens from *Visma Connect*. Authorization is done on tenant level, so one OAuth token is needed per tenant.<br/> <br/> The scope `transpaapi:api` grants basic access to the API and is required for all routes. Additional scopes might be required and are described inside each route description. <br/> ## Details All monetary amounts are in the organizations local currency unless otherwise specified. <br/> <br/> ```[Not Ready]``` This is an endpoint where development has not completed and is therefore subject to change. 
 *
 * The version of the OpenAPI document: 0.1.21
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
    ///  Class for testing WageRow
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the model.
    /// </remarks>
    public class WageRowTests : IDisposable
    {
        // TODO uncomment below to declare an instance variable for WageRow
        //private WageRow instance;

        public WageRowTests()
        {
            // TODO uncomment below to create an instance of WageRow
            //instance = new WageRow();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of WageRow
        /// </summary>
        [Fact]
        public void WageRowInstanceTest()
        {
            // TODO uncomment below to test "IsType" WageRow
            //Assert.IsType<WageRow>(instance);
        }


        /// <summary>
        /// Test the property 'PayTypeCode'
        /// </summary>
        [Fact]
        public void PayTypeCodeTest()
        {
            // TODO unit test for the property 'PayTypeCode'
        }
        /// <summary>
        /// Test the property 'UnitPrice'
        /// </summary>
        [Fact]
        public void UnitPriceTest()
        {
            // TODO unit test for the property 'UnitPrice'
        }
        /// <summary>
        /// Test the property 'Quantity'
        /// </summary>
        [Fact]
        public void QuantityTest()
        {
            // TODO unit test for the property 'Quantity'
        }
        /// <summary>
        /// Test the property 'Details'
        /// </summary>
        [Fact]
        public void DetailsTest()
        {
            // TODO unit test for the property 'Details'
        }

    }

}
