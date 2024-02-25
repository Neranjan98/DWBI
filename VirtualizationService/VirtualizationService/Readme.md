Service Data Virutalization - Single Interface for all database in an enterprise
=================================================================

The purpose of this project is to provide a single interface for retrieving all connection strings in a distributed system setting.

## ***Architecture Benefits*** 

- **Centralized Connection Strings**: Eliminates the need to maintain connection strings in multiple applications. All applications can point to this service to resolve their database connections.

- **Enhanced Monitoring and Health Checks**: Centralized storage allows for the addition of health checks and reporting, improving monitoring and maintenance of database connections.

- **Security**: While the connection strings are currently encoded in Base64 for basic privacy, in an enterprise setting, they could be encrypted using keys from Azure KeyVault or AWS Key Management Service, enhancing security.

- **Simplified Updates**: Any updates to database server information in the architecture need only be done in one database for services to use, simplifying maintenance and reducing the risk of errors.

- **Performant**: Out of the box implementation for Distributed Caching for increasing performance. Intentionally cached only the GetConnectionByType as that would be the endpoint that is called often.

## ***Future Scope*** 

- **Centralized Command Executor**: The main idea behind this project was to have a single function which handles all the Commands when provided with a  Connection type and a SQL Query which would be executed using Dapper library. Primary use case here is to update all the administrative tables on execution of a service. 












