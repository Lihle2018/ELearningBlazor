# eLearning Platform - Design Documentation

## Project Overview

This eLearning platform consists of two main components:
- **ELearningApi**: A .NET 9 Web API backend
- **ELearningBlazor**: A Blazor WebAssembly frontend

The platform provides a complete learning management system with course browsing, student enrollment, progress tracking, and authentication.

## Architecture Design Approach

### 1. **Separation of Concerns (SoC)**

The project follows a clean separation between frontend and backend:

- **Backend (ELearningApi)**: Handles data persistence, business logic, authentication, and API endpoints
- **Frontend (ELearningBlazor)**: Manages user interface, state management, and client-side interactions

This separation allows for:
- Independent development and deployment
- Technology flexibility
- Better maintainability
- Scalability

### 2. **API-First Design**

The backend is designed as a RESTful API with clear endpoints:

**Benefits:**
- Clear contract between frontend and backend
- Easy to test and document
- Future mobile app compatibility
- Microservices-ready architecture

### 3. **Data Architecture**

#### Entity Framework Core with SQLite
- **Database**: SQLite for development simplicity and portability
- **ORM**: Entity Framework Core for data access abstraction
- **Migrations**: Code-first approach for database schema management

#### Domain Models
```csharp
Student -> Enrollment -> Course
```

**Key Design Decisions:**
- **JSON Storage**: Complex data (course modules, requirements) stored as JSON strings
- **Soft Deletes**: Enrollments use `IsActive` flag instead of hard deletion
- **Audit Fields**: Automatic tracking of enrollment dates and last access
- **Unique Constraints**: Prevents duplicate enrollments per student-course combination

### 4. **Authentication & Security**

#### JWT-Based Authentication
- **Stateless**: No server-side session storage
- **Secure**: Uses HMAC SHA256 with configurable secret keys
- **Claims-Based**: User information embedded in token
- **Expiration**: 7-day token lifetime for security

#### Password Security
- **BCrypt Hashing**: Industry-standard password hashing
- **No Plain Text**: Passwords never stored in plain text
- **Salt Generation**: Automatic salt generation per password

### 5. **Frontend Architecture**

#### Blazor WebAssembly (WASM)
- **Client-Side Rendering**: Full SPA experience
- **C# Everywhere**: Shared language between frontend and backend
- **Component-Based**: Reusable UI components
- **Progressive Enhancement**: Works without JavaScript for basic functionality

#### Service Layer Pattern
```csharp
IAuthService -> AuthService
ICourseService -> CourseService
IEnrollmentService -> EnrollmentService
```

**Benefits:**
- Dependency injection for testability
- Interface-based design for flexibility
- Clear separation of concerns
- Easy mocking for unit tests

### 6. **State Management**

#### Client-Side State
- **Local Storage**: Persistent user session across browser sessions
- **Service-Based**: Centralized state management through services
- **Event-Driven**: Reactive updates through event subscriptions

#### Authentication State
```csharp
public event Action? OnAuthStateChanged;
public User? CurrentUser { get; }
public bool IsAuthenticated { get; }
```

### 7. **UI/UX Design Principles**

#### Modern Web Design
- **Tailwind CSS**: Utility-first CSS framework
- **Responsive Design**: Mobile-first approach
- **Dark Mode Support**: Built-in theme switching
- **Component Library**: Reusable UI components

#### User Experience
- **Progressive Disclosure**: Information revealed as needed
- **Clear Navigation**: Intuitive sidebar navigation
- **Visual Feedback**: Loading states and hover effects
- **Accessibility**: Semantic HTML and ARIA attributes

### 8. **Data Transfer Objects (DTOs)**

#### API Response Design
```csharp
public class AuthResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string? Token { get; set; }
    public StudentDto? Student { get; set; }
}
```

**Design Benefits:**
- **Versioning**: Easy to evolve API without breaking changes
- **Security**: Only expose necessary data
- **Performance**: Optimized data transfer
- **Validation**: Clear input/output contracts

### 9. **Error Handling Strategy**

#### Backend Error Handling
- **Consistent Responses**: Standardized error response format
- **HTTP Status Codes**: Proper use of status codes
- **Validation**: Model validation with clear error messages
- **Logging**: Structured logging for debugging

#### Frontend Error Handling
- **Graceful Degradation**: Fallback UI for errors
- **User Feedback**: Clear error messages to users
- **Retry Logic**: Automatic retry for transient failures
- **Offline Support**: Basic offline functionality

### 10. **Development & Deployment**

#### Development Environment
- **Hot Reload**: Fast development iteration
- **CORS Configuration**: Proper cross-origin setup
- **Database Seeding**: Sample data for development
- **Configuration Management**: Environment-specific settings

#### Production Considerations
- **Docker Support**: Containerized deployment
- **Configuration**: Environment-based configuration
- **Security**: Production-ready security settings
- **Monitoring**: Built-in health checks and logging

## Technical Stack

### Backend (ELearningApi)
- **.NET 9**: Latest .NET framework
- **Entity Framework Core**: ORM
- **SQLite**: Database
- **JWT Bearer**: Authentication
- **BCrypt**: Password hashing
- **OpenAPI**: API documentation

### Frontend (ELearningBlazor)
- **Blazor WebAssembly**: Client-side framework
- **Tailwind CSS**: Styling
- **Bootstrap**: Additional UI components
- **System.Text.Json**: JSON serialization
- **HttpClient**: API communication

## Key Design Patterns

1. **Repository Pattern**: Through Entity Framework DbContext
2. **Service Layer Pattern**: Business logic abstraction
3. **DTO Pattern**: Data transfer optimization
4. **Dependency Injection**: Loose coupling
5. **Observer Pattern**: Event-driven updates
6. **Component Pattern**: Reusable UI elements

## Scalability Considerations

### Horizontal Scaling
- **Stateless API**: Easy to scale across multiple instances
- **Database Separation**: Can be moved to dedicated database server
- **CDN Ready**: Static assets can be served from CDN

## Security Considerations

1. **Input Validation**: Server-side validation for all inputs
2. **SQL Injection Prevention**: Entity Framework parameterized queries
3. **XSS Protection**: Proper output encoding
4. **CSRF Protection**: Token-based protection
5. **HTTPS Enforcement**: Secure communication
6. **Password Security**: BCrypt hashing with salt

## Conclusion

This eLearning platform demonstrates modern web development practices with a focus on:
- **Maintainability**: Clean architecture and separation of concerns
- **Scalability**: API-first design and stateless architecture
- **Security**: Industry-standard authentication and data protection
- **User Experience**: Modern, responsive, and accessible interface
- **Developer Experience**: Clear code structure and comprehensive documentation

The design approach prioritizes long-term maintainability while providing immediate functionality for users and developers alike.
