# User API Dev Guide
The Zip.WebAPI project helps manage Zip Pay users and their accounts. 
Currently the API has three endpoints to achieve the following:
- Create a user
- Create an account for a given a user (provided they meet the business requirements)
- Get all accounts for a given user

The roadmap for this API is to add more endpoints to list, delete and update both Users and Accounts.
Future improvements may include simplifying model validation, implementing pipeline behaviours, creating integration 
tests, and splitting our database into two so they can be separately optimised for Read and Write operations.

## Build & Deploy
There are two steps to running this application successfully.

### Step 1: Deploy Database
The application relies on a Docker image containing a PostgreSQL database. This is configured in the 
`docker-compose.yml` file and can be connected to locally after running (it will create a database with no tables).

- Create a Docker Compose Run Configuration and hit Run
- or use the command line to run docker compose

Using a database tool (such as DBeaver), you can connect to the 
database using the connection string found in `appsettings.json` to view it (not required to deploy).

### Step 2: Deploy API
This step uses the database migrations in the Migrations folder to build and populate the database tables. 
The `app.ApplyMigrations()` call in our `Startup.cs` file takes care of this.

- Deploy the API by using a Run Configuration or the command line.

If you're in a development environment, your web browser should automatically open a SwaggerUI interface to interact 
with the API. Using your database tool, you can now see the database is populated with two tables (users and accounts), 
constraints and data.

## Testing
Tests are split by feature, resulting in two test files: AccountsTests and UsersTests.

The focus of the unit tests is the business logic in the command and query handlers, alongside their resulting model 
validation. 

### How to run
The tests can be run together or separately by right clicking and selecting "Run unit tests" on either:
- The Zip.Tests project
- An individual test file within the Zip.Tests project

## Additional Information
This application is built using a Vertical Slice Architecture where instead of coupling across layers,  
the application couples across each vertical slice, known as a Feature. 

In our case, our features are Users and Accounts. Each feature folder contains it's API Controller, CQRS handlers and 
Services that interact with the database.
This format of separation means when changes are required to a given feature, the bulk of the work is encapsulated in 
it's own domain to avoid making changes in many areas of the API.