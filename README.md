# Identity Server

A custom-built OAuth 2.0 Authorization Server implemented from scratch for learning and production use.

## Overview

Identity Server is a standards-compliant OAuth 2.0 authorization server that issues access tokens to client applications, enabling secure delegated access to protected resources on behalf of resource owners.

### How It Works

```
┌──────────────┐                        ┌──────────────────┐                        ┌─────────────────────┐
│              │  1. Request auth code   │                  │                        │                     │
│  Client App  │ ────────────────────►   │  Identity Server │                        │ Protected Resources │
│              │  2. Return auth code    │                  │                        │                     │
│              │ ◄────────────────────   │                  │                        │                     │
│              │  3. Exchange for token  │                  │                        │                     │
│              │ ────────────────────►   │                  │                        │                     │
│              │  4. Return access token │                  │                        │                     │
│              │ ◄────────────────────   │                  │                        │                     │
│              │                         │                  │                        │                     │
│              │  5. Request resource ──────────────────────────────────────────────► │                     │
│              │     (with access token) │                  │  6. Validate token     │                     │
│              │                         │                  │ ◄──────────────────────│                     │
└──────────────┘                         └──────────────────┘                        └─────────────────────┘
```

> For detailed architecture diagrams, see the [C4 Model diagrams](docs/c4-model/) (PlantUML).

## Tech Stack

| Layer           | Technology       | Description                                              |
| --------------- | ---------------- | -------------------------------------------------------- |
| **Backend**     | .NET (C# / ASP.NET Core) | REST API handling OAuth 2.0 endpoints             |
| **Frontend**    | React            | SPA for login, consent, and user-facing authorization UI |
| **Storage**     | SQL Server       | Persistent storage for clients, users, tokens, grants    |
| **Static Content** | Web Server    | Serves the React SPA static assets                       |

## Architecture

The system follows the [C4 model](https://c4model.com/) for architecture documentation. Diagrams are located in [`docs/c4-model/`](docs/c4-model/).

### System Context (Level 1)

At the highest level, three actors interact:

- **Resource Owner** — End-user who owns protected resources and grants access to client applications
- **Client Application** — Requests access tokens from Identity Server to access resources on behalf of the user
- **Protected Resources** — APIs or services that validate tokens issued by Identity Server

### Containers (Level 2)

Identity Server is composed of four containers:

| Container          | Technology        | Responsibility                                                     |
| ------------------ | ----------------- | ------------------------------------------------------------------ |
| **Backend**        | ASP.NET Core API  | Handles `/authorize`, `/token`, `/userinfo`, `/metadata` endpoints |
| **UI**             | React SPA         | Displays the authorization view, login form, and consent screen    |
| **Storage**        | SQL Server        | Stores client registrations, user data, tokens, and grants         |
| **Static Content** | Web Server        | Serves the React SPA static assets to the browser                  |

### Backend Components (Level 3)

The Backend container is organized into endpoint controllers and domain services:

**Endpoints (Controllers):**

| Endpoint             | Path                                    | Responsibility                                            |
| -------------------- | --------------------------------------- | --------------------------------------------------------- |
| Authorize Endpoint   | `GET /authorize`                        | Initiates the authorization code flow, redirects to login |
| Token Endpoint       | `POST /token`                           | Exchanges authorization codes/credentials for tokens      |
| UserInfo Endpoint    | `GET /userinfo`                         | Returns authenticated user claims                         |
| Metadata Endpoint    | `GET /.well-known/oauth-authorization-server` | Exposes server metadata and endpoint discovery      |

**Services:**

| Service               | Responsibility                                                   |
| --------------------- | ---------------------------------------------------------------- |
| Token Service         | Generates, validates, and revokes access and refresh tokens      |
| Authorization Service | Validates authorization requests, manages user consent and grants |
| User Service          | Authenticates users, manages profiles and credentials            |
| Client Registry       | Validates client credentials and registered redirect URIs        |

## OAuth 2.0 Support (v1)

### Supported Grant Types

- **Authorization Code** — For server-side and SPA applications with user interaction
- **Client Credentials** — For machine-to-machine communication without user involvement

### Supported Endpoints

| Endpoint       | Method | Path                                          | RFC Reference |
| -------------- | ------ | --------------------------------------------- | ------------- |
| Authorization  | GET    | `/authorize`                                  | [RFC 6749 §3.1](https://datatracker.ietf.org/doc/html/rfc6749#section-3.1) |
| Token          | POST   | `/token`                                      | [RFC 6749 §3.2](https://datatracker.ietf.org/doc/html/rfc6749#section-3.2) |
| UserInfo       | GET    | `/userinfo`                                   | — |
| Metadata       | GET    | `/.well-known/oauth-authorization-server`     | [RFC 8414](https://datatracker.ietf.org/doc/html/rfc8414) |

### Security Considerations

- **PKCE** (Proof Key for Code Exchange) — Required for public clients ([RFC 7636](https://datatracker.ietf.org/doc/html/rfc7636))
- **State parameter** — Required to prevent CSRF attacks
- **Token expiration** — Access tokens have a configurable TTL
- **Refresh tokens** — Support for token rotation
- **HTTPS only** — All endpoints must be served over TLS

### Token Format

- Access tokens are signed **JWT** (JSON Web Tokens) per [RFC 7519](https://datatracker.ietf.org/doc/html/rfc7519)
- Token signing uses asymmetric keys (RS256)

## Project Structure

```
identity-server/
├── docs/
│   └── c4-model/                         # Architecture diagrams (PlantUML)
│       ├── software-system-diagram.wsd   # Level 1: System Context
│       ├── container-diagram.wsd         # Level 2: Containers
│       └── component-diagram-backend.wsd # Level 3: Backend Components
├── src/
│   ├── backend/                          # ASP.NET Core API
│   └── ui/                               # React SPA
└── README.md
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- [Node.js 20+](https://nodejs.org/) and npm
- [SQL Server](https://www.microsoft.com/en-us/sql-server/) (or SQL Server Express / LocalDB for development)

### Setup

```bash
# Clone the repository
git clone https://github.com/milkicz/identity-server.git
cd identity-server

# Backend
cd src/backend
dotnet restore
dotnet run

# UI (in a separate terminal)
cd src/ui
npm install
npm start
```

### Configuration

Configure the server via `appsettings.json` or environment variables:

| Setting                  | Description                          | Default            |
| ------------------------ | ------------------------------------ | ------------------ |
| `ConnectionStrings:DefaultConnection` | SQL Server connection string | `(LocalDB)`   |
| `Jwt:Issuer`             | Token issuer URI                     | `https://localhost` |
| `Jwt:AccessTokenTTL`     | Access token lifetime (minutes)      | `60`               |
| `Jwt:RefreshTokenTTL`    | Refresh token lifetime (days)        | `30`               |

## Relevant RFCs

| RFC       | Title                                          |
| --------- | ---------------------------------------------- |
| [RFC 6749](https://datatracker.ietf.org/doc/html/rfc6749)  | The OAuth 2.0 Authorization Framework          |
| [RFC 6750](https://datatracker.ietf.org/doc/html/rfc6750)  | Bearer Token Usage                             |
| [RFC 7519](https://datatracker.ietf.org/doc/html/rfc7519)  | JSON Web Token (JWT)                           |
| [RFC 7636](https://datatracker.ietf.org/doc/html/rfc7636)  | Proof Key for Code Exchange (PKCE)             |
| [RFC 8414](https://datatracker.ietf.org/doc/html/rfc8414)  | OAuth 2.0 Authorization Server Metadata        |

## License

This project is for educational and internal use.