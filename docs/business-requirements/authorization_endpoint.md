Requirements: 

- the authorization endpoint is used to interact with the resource owner
- the authorization server must first verify the identity of resource owner 
- the endpoint uri may include an "application/x-www-form-urlencoded" which MUST be retained when adding additional query parameters.  The endpoint URI MUST NOT include a fragment component.
- The endpoint URI MUST NOT include a fragment component.
- the authorization server MUST require the use of TLS as described in Section 1.6 when sending requests to the authorization endpoint.
(because of sending credentials in plain text)
- Parameters sent without a value MUST be treated as if they were omitted from the request.
- The authorization server MUST ignore unrecognized request parameters.
- Request and response parameters MUST NOT be included more than once.
- the client MUST include a redirection URI with the
   authorization request using the "redirect_uri" request parameter.
- a redirection URI is included in an authorization request, the
   authorization server MUST compare and match the value received
   against at least one of the registered redirection URIs (or URI
   components)


