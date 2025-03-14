# ADR-003: Why Microservices Architecture?

## **Context**
We needed a scalable architecture to support an event management system.

## **Decision**
We chose **Microservices Architecture** because:
- ✅ **Scalability**: Services can scale independently.
- ✅ **Flexibility**: Different teams can work on different services.
- ✅ **Resilience**: Failures in one service do not affect others.
- ✅ **Decoupled Development**: Easier to maintain & update.

## **Consequences**
- Requires **service discovery & API Gateway**.
- Introduces **message queue complexity** for event-driven communication.
