/*
 * TransPA Public API
 *
 * # This API exposes functionality in Visma TransPA. ## Authentication The API can be tested without authentication against the mock server in the **Servers** pulldown list. To test against the actual backend you have to register a user on the *Visma Developer Portal* and request access to the API along with the required scopes. The scopes required are documented under each endpoint. For more information about *Visma Developer Portal* see <https://developer.visma.com>. <br/> <br/>  Authentication uses OAuth tokens from *Visma Connect*. Authorization is done on tenant level, so one OAuth token is needed per tenant.<br/> <br/> The scope `transpaapi:api` grants basic access to the API and is required for all routes. Additional scopes might be required and are described inside each route description. <br/> ## Details All monetary amounts are in the organizations local currency unless otherwise specified. <br/> <br/> ```[Not Ready]``` This is an endpoint where development has not completed and is therefore subject to change. 
 *
 * The version of the OpenAPI document: 0.1.21
 * Contact: utveckling.transpa@visma.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using Newtonsoft.Json.Converters;

namespace transpa.api.generated.Client
{
    /// <summary>
    /// Formatter for 'date' openapi formats ss defined by full-date - RFC3339
    /// see https://github.com/OAI/OpenAPI-Specification/blob/master/versions/3.0.0.md#data-types
    /// </summary>
    public class OpenAPIDateConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenAPIDateConverter" /> class.
        /// </summary>
        public OpenAPIDateConverter()
        {
            // full-date   = date-fullyear "-" date-month "-" date-mday
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
