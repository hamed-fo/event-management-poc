
---

# ADR-001: Why JWT for Authentication?

## **Context**
We need a secure authentication mechanism for our microservices-based system.

## **Decision**
We chose **JWT (JSON Web Tokens)** for authentication because:
- ✅ Stateless authentication (no need for session storage)
- ✅ Works well with API Gateway (Ocelot)
- ✅ Easily validated using public/private keys

## **Consequences**
- JWTs must be **manually invalidated**
- Tokens should have a **short expiry** for security
