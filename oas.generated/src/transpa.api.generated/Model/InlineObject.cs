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
    /// InlineObject
    /// </summary>
    [DataContract(Name = "inline_object")]
    public partial class InlineObject : IEquatable<InlineObject>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineObject" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected InlineObject() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineObject" /> class.
        /// </summary>
        /// <param name="createCallbackUrl">createCallbackUrl (required).</param>
        /// <param name="customHeaders">customHeaders (required).</param>
        public InlineObject(string createCallbackUrl = default(string), List<HttpHeader> customHeaders = default(List<HttpHeader>))
        {
            // to ensure "createCallbackUrl" is required (not null)
            if (createCallbackUrl == null) {
                throw new ArgumentNullException("createCallbackUrl is a required property for InlineObject and cannot be null");
            }
            this.CreateCallbackUrl = createCallbackUrl;
            // to ensure "customHeaders" is required (not null)
            if (customHeaders == null) {
                throw new ArgumentNullException("customHeaders is a required property for InlineObject and cannot be null");
            }
            this.CustomHeaders = customHeaders;
        }

        /// <summary>
        /// Gets or Sets CreateCallbackUrl
        /// </summary>
        [DataMember(Name = "createCallbackUrl", IsRequired = true, EmitDefaultValue = false)]
        public string CreateCallbackUrl { get; set; }

        /// <summary>
        /// Gets or Sets CustomHeaders
        /// </summary>
        [DataMember(Name = "customHeaders", IsRequired = true, EmitDefaultValue = false)]
        public List<HttpHeader> CustomHeaders { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InlineObject {\n");
            sb.Append("  CreateCallbackUrl: ").Append(CreateCallbackUrl).Append("\n");
            sb.Append("  CustomHeaders: ").Append(CustomHeaders).Append("\n");
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
            return this.Equals(input as InlineObject);
        }

        /// <summary>
        /// Returns true if InlineObject instances are equal
        /// </summary>
        /// <param name="input">Instance of InlineObject to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InlineObject input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.CreateCallbackUrl == input.CreateCallbackUrl ||
                    (this.CreateCallbackUrl != null &&
                    this.CreateCallbackUrl.Equals(input.CreateCallbackUrl))
                ) && 
                (
                    this.CustomHeaders == input.CustomHeaders ||
                    this.CustomHeaders != null &&
                    input.CustomHeaders != null &&
                    this.CustomHeaders.SequenceEqual(input.CustomHeaders)
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
                if (this.CreateCallbackUrl != null)
                    hashCode = hashCode * 59 + this.CreateCallbackUrl.GetHashCode();
                if (this.CustomHeaders != null)
                    hashCode = hashCode * 59 + this.CustomHeaders.GetHashCode();
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
