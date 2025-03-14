# ADR-002: Why RabbitMQ for Event-Driven Communication?

## **Context**
We need a way for **ProducerService** and **SubscriptionService** to communicate asynchronously.

## **Decision**
We chose **RabbitMQ** because:
- ✅ **Reliable messaging** with queues & acknowledgments
- ✅ **Scalability** (supports multiple consumers)
- ✅ **Decoupling** (services don’t need to know about each other)

## **Consequences**
- RabbitMQ must be **deployed & managed**
- Messages should be **persistent** to avoid loss
