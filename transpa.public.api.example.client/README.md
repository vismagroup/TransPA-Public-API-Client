# Salary export example
This example shows how you can export salary information via TransPA Public API into another system. This example is an actual integration that is being used. It integrates between the TransPA Public API and another Visma API.
The implementation is a .Net 6 leveraging Azure Functions that acts as a WebHook.

## Generate code
This implementation uses generated code dto's based on the Open API Specification. For this we have used the tool [OpenAPI Generator](https://openapi-generator.tech).