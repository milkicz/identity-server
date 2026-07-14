Business requirements:

Client registration:
    - clients can be registered
    - Clients need to have redirect_uri, client_type properties
    - clients can have one of two client_types (or both): confidential, public
    - The authorization server SHOULD NOT make assumptions about the client type. 
    - confidential clients are the web applications running on a web server, which can securely store the client credentials (e.g client secret)
    - public apps can either be user-agent-based-applications which code is downloaded from a web server and executed within user-agent (like browser) or native so a public client app installed on the device used by resource owner. 
    - the authorization server should issue client identifier for each client
    - The authorization server SHOULD document the size of any identifier it issues.
    - The authorization server MAY accept any form of client authentication meeting its security requirements.
    -  Clients in possession of a client password MAY use the HTTP Basic authentication
    - The client identifier is encoded using the "application/x-www-form-urlencoded" encoding algorithm per Appendix B, and the encoded value is used as the username; 
    - the client password is encoded using the same algorithm and used as the password.
    - The authorization server MUST support the HTTP Basic authentication scheme for authenticating clients that were issued a client password.
    - The authentication parameters can only be transmitted in the request-body and MUST NOT be included in the request URI.
    - The authorization server MUST require the use of TLS when sending requests using password authentication.
    - Since this client authentication method involves a password, the authorization server MUST protect any endpoint utilizing it against brute force attacks.
    -  The authorization server MAY support any suitable HTTP authentication scheme matching its security requirements. When using other authentication methods, the authorization server MUST define a mapping between the client identifier (registration record) and authentication scheme.
    - The authorization server MUST require the following clients to register their redirection endpoint:
   o  Public clients.
   o  Confidential clients utilizing the implicit grant type
    - The authorization server SHOULD require all clients to register their redirection endpoint prior to utilizing the authorization endpoint.
    - The authorization server SHOULD require the client to provide the
   complete redirection URI (the client MAY use the "state" request
   parameter to achieve per-request customization)
    - If requiring the registration of the complete redirection URI is not possible, the authorization server SHOULD require the registration of the URI scheme, authority, and path (allowing the client to dynamically vary only the query component of the redirection URI when requesting authorization).
    -  The authorization server MAY allow the client to register multiple redirection endpoints.
    