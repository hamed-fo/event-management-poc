# EventPoc - Microservices-Based Event Management System

## **📝 Overview**
EventPoc is a **microservices-based** event management system built with:
- **ASP.NET Core**
- **RabbitMQ** (Message Queue)
- **Ocelot API Gateway**
- **SQLite** (Database)

✅ **Key Features:**
- 🔐 **Authentication & Authorization** with JWT (`IdentityService`)
- 📅 **Event Management** (`ProducerService`)
- 📝 **Event Registrations** (`SubscriptionService`)
- 📨 **Event-Driven Communication** with RabbitMQ
- 🌐 **API Gateway (Ocelot)** for Routing & Authentication
- 📚 **Domain-Driven Design (DDD)** Architecture

## **📚 Domain-Driven Design (DDD)**
EventPoc follows **DDD principles**:
- **Entities** (Event, Registration, User)
- **Use Cases** (Application Layer)
- **Infrastructure** (Database, Messaging, Authentication)
- **Separation of Concerns** between **Domain, Application, Infrastructure, and Presentation layers**

---

## **🚀 Getting Started**
### **1️⃣ Prerequisites**
- **Docker** & **Docker Compose**
- **.NET 9 SDK**
- **Postman (Optional, or use `curl`)**

### 2️⃣ Modify `.env` File if required

### 3️⃣ Run the Application

```docker-compose up -d --build```

All services will start on the following ports:

    API Gateway → http://localhost:5000
    IdentityService → http://localhost:5001
    ProducerService → http://localhost:5002
    SubscriptionService → http://localhost:5003
    RabbitMQ Management → http://localhost:15672 (guest/guest)

## 📖 API Documentation

➡️ [Postman Collection](postman_collection.json)

## 🛠️ ADR (Architecture Decision Records)

We document major architecture decisions in [ADR Directory](ADRs) 📁
