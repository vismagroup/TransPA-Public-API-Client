# Examples in Postman

## Authentication
In this folder you will find a Postman Collection the exemplifies the Authentication flow when accessing the TransPA Public API.

### Prerequistes
You will need the client id and secret for you application that you've set up inside the [Developer portal](https://oauth.developers.visma.com/). Inside the portal there is a documentation tab where should be able to find all the information you need. A pointer is to read this section about [Service Application](https://oauth.developers.visma.com/service-registry/documentation/authentication#serviceApp) since our API currently only supports Machine-to-Machine integrations.

Also you need to have been granted access to the Sandbox tenant.
Today this is a manual procerdure, that will be automated in the future.

### The collection
The Collection consits of two folders: 

#### Basic
- Authenticates and only authorizes for transpaapi:api scope which is required for any access to the API.
- Reads the Alive call

#### Example - Vehicle Resource
- Authenticates and authorizes for the scopes transpaapi:api and transpaapi:vehicles:read
- Reads vehicles

#### The environment file
In order for the example to work you need to provide your client id and client secret, together with the tenant id you want to access. 
In the example the environment file is pre-filled with the Sandbox tenant, so all you need to do is adding the client id and secret.