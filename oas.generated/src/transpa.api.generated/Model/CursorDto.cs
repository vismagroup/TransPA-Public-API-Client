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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = transpa.api.generated.Client.OpenAPIDateConverter;

namespace transpa.api.generated.Model
{
    /// <summary>
    /// If there are too many elements to return the entire list a cursor is provided. The cursor is used for querying for more data by using the &#39;cursor&#x3D;nextToken&#39; query parameter. 
    /// </summary>
    [DataContract(Name = "cursorDto")]
    public partial class CursorDto : IEquatable<CursorDto>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CursorDto" /> class.
        /// </summary>
        /// <param name="nextToken">Token that can be used with the &#39;cursor&#x3D;nextToken&#39; query parameter to retrieve the next elements in the list. If no value is returned it means that there is no more data available. .</param>
        public CursorDto(string nextToken = default(string))
        {
            this.NextToken = nextToken;
        }

        /// <summary>
        /// Token that can be used with the &#39;cursor&#x3D;nextToken&#39; query parameter to retrieve the next elements in the list. If no value is returned it means that there is no more data available. 
        /// </summary>
        /// <value>Token that can be used with the &#39;cursor&#x3D;nextToken&#39; query parameter to retrieve the next elements in the list. If no value is returned it means that there is no more data available. </value>
        [DataMember(Name = "nextToken", EmitDefaultValue = true)]
        public string NextToken { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CursorDto {\n");
            sb.Append("  NextToken: ").Append(NextToken).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as CursorDto);
        }

        /// <summary>
        /// Returns true if CursorDto instances are equal
        /// </summary>
        /// <param name="input">Instance of CursorDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CursorDto input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.NextToken == input.NextToken ||
                    (this.NextToken != null &&
                    this.NextToken.Equals(input.NextToken))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.NextToken != null)
                    hashCode = hashCode * 59 + this.NextToken.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
