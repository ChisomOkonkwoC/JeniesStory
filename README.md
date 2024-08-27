### DOCUMENTATION

Jenies Story
A C# blog platform that allows user registration, story creation, and commenting. The platform includes admin functionality to approve and publish stories and comments. The service integrates Mailjet to send confirmation emails to users upon registration and uses JWT tokens for authentication.


Table of Contents
1. Description
2. Features
3. Installation
4. Usage
5. Configuration
6. API Endpoints
7. Technologies Used
8. Contact


1. Description
This blog project is built using C# and ASP.NET Core. It allows users to register, create stories, comment on stories, and interact with other users. An admin role is implemented to moderate content by approving stories and comments before publication. The service integrates Mailjet to send confirmation emails to users upon registration and uses JWT tokens for secure authentication.


2. Features
User Registration: Users can register and receive email confirmations via Mailjet.

Admin Role: Admins can approve or reject stories and comments.

Story Creation: Registered users can create and publish stories.

Commenting: Users can comment on stories, pending admin approval.

JWT Authentication: Secure user authentication using JWT tokens.



3. Installation
Prerequisites
i. .NET 6 SDK
ii. SQL Server

Setup
Clone the repository:
git clone https://github.com/yourusername/blog-project.git cd blog-project

Restore dependencies:
dotnet restore

Set up the database:
Update the appsettings.json with your local SQL Server connection string:
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb);Database=JeniesStoryDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

Run the migrations to create the database:
  dotnet ef database update

Configure Mailjet for email services:
Update the appsettings.json with the Mailjet API key and secret:
"Mailjet": {
  "ApiKey": "eda4c097d06c86eb2f45e6d261452e97",
  "ApiSecret": "def7b627587ce1c001254f09865a0ce1"
}

Run the application:
dotnet run



Usage

Register a User
Navigate to http://localhost:5000/register to create a new user account.
Check your email for the confirmation link sent via Mailjet.

Admin Approvals
Admins can log in at http://localhost:5000/approveStory and http://localhost:5000/approveCommentsByAdmin  to approve or reject stories and comments.

JWT Authentication
All API calls to protected endpoints require a JWT token obtained upon user login.

Configuration
Mailjet: Used for sending confirmation emails. Ensure API keys are correctly set in appsettings.json.
JWT Tokens: Used for authenticating users. Secret keys should be kept secure and configured in appsettings.json.


API Endpoints
User Registration: POST /api/authentication/register
User Login: POST /api/authentication/login
Create Story: POST /api/story/createStory
Comment on Story: POST /api/comment/createComment
Admin Approve Story: POST /api/admin/approveStory
Admin Approve Comment: POST /api/admin/approveCommentsByAdmin


Technologies Used

ASP.NET Core 6: Web framework for building the application.
Entity Framework Core: ORM for database access and migrations.
SQL Server: Database for storing application data.
Mailjet: Email service for sending confirmation emails.
JWT (JSON Web Tokens): Authentication and authorization of users.


Contact
Maintainer: Chisom Okonkwo
Email: okonkwochisom3024@gmail.com




