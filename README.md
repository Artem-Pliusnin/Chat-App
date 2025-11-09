# Chat App

This project was built using **ASP.NET Core (.NET 8)** and **Angular 18**.

## Key Features

### User authentication with JWT
Users can register and log in securely using **JSON Web Tokens (JWT)** for authentication and authorization.

### Real-time chat creation
Users can create personal or group chats with other users, with all messages synchronized in real time using **SignalR**.

### Send and receive messages in real time
Messages are instantly delivered to all participants in a chat, ensuring a live chat experience.

### Notifications for new messages
Users receive notifications when new messages arrive, even if they are currently viewing a different chat.

### Sentiment analysis of messages with corresponding emoji display
The application automatically analyzes the sentiment of each message using **Azure Cognitive Services** and displays a corresponding emoji next to the message, giving a visual cue of the message's mood.


## Backend Structure
The backend of ChatApp is organized into three separate projects to ensure a clean architecture and separation of concerns:

## ChatApp.API
This is the main API project, handling HTTP endpoints, authentication, services, mapping, and real-time features via **SignalR**.

### Controllers
- **AuthController.cs**  
  Handles user registration and login using **JWT tokens** for secure authentication.
- **ChatController.cs**  
  Manages creating and retrieving chats for users.
- **MessageController.cs**  
  Handles filtering, retrieving, and creating messages, including processing with **Azure Cognitive Services** for sentiment analysis.
- **UserController.cs**  
  Manages user data and provides endpoints to get user information.
- **ChatHub.cs**  
  **SignalR Hub** responsible for real-time messaging and chat creation.

## ChatApp.Data
This project manages data persistence using **Entity Framework Core**, including the database context, entity configurations, and migrations. It uses the **Repository Pattern** to handle database access.

### Key Components
- **DbContext (`ChatAppDbContext`)**  
  Represents the database session and provides `DbSet` properties for entities such as `User`, `Chat`, `Message`, and `ChatParticipant`.

- **Repositories**  
  Encapsulate CRUD operations and database access logic:
  - `UserRepository`
  - `ChatRepository`
  - `MessageRepository`
  - `MessageStatusRepository`

## ChatApp.Domain
This project contains the core domain models (entities) for the database and encapsulates the main business objects.

### Entities
- **Chat.cs**  
  Defines the `Chat` entity.
- **ChatMember.cs**  
  Represents a many-to-many relationship entity defining the participants of a chat.
- **Message.cs**  
  Defines the `Message` entity.
- **MessageStatus.cs**  
  Represents a many-to-many relationship entity that tracks whether a message has been seen by a user.
- **User.cs**  
  Defines the `User` entity.


## Chat Frontend
The frontend of ChatApp is implemented using **standalone Angular components** and is structured as follows:
### Pages
Main application pages.

### Components
- **chats:** Components for displaying chats and messages within chats.
- **new-chat:** Components for selecting participants and creating a new chat.
- **authorization:** Components for login and registration pages.

### Services
Services responsible for fetching and sending data to the backend API.

### Models
Core application models and DTOs received from the backend.

### Emitters
Event emitters used for notifying about various application events.

### Pipes
Custom Angular pipes for data transformation.

