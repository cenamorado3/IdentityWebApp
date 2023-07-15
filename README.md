# IdentityWebApp
A fun and slightly goofy spin on handling API security. This leverages a PostgreSQL database and creates a scehma with the script found at: https://github.com/cenamorado3/msIdentity4Postgres/blob/main/createIdentity4pg.sql
This scehma matches the ASPNET schema for SQLServer as closely as possible. No functionality exist for creating users or permissions ie claims/roles, the values must be inserted into the respective tables. Meaning, if you would like to get the weather 
from the mock endpoint, you will need to create a user and claims based on their user_id by inserting the values, to access the "API" they must be an "admin" within the scope of this application.

With a properly configured user, they may request a token, which must be attached to the Authorization header for future request, the endpoints will validate the user token has the claim as defined by the policy in the startup file and that the token
is valid.

Additionally, this compares two means of getting user identities from the database, one endpoint leverages LINQ statements via EntityFramework, where first time loads experienced noticably slower execution times, generally between 3-5seconds.
Taking a different approach and executing SQL as string, where the SQL statements where backloaded to a postgres routine, ie a function, reduced loadtimes to <500ms, generally being at least 6 times faster. This reduced the cognitive overload of the app and its developers, which may be a boon to security, at the cost of adding overhead, as the database must be managed seperately, as well as requiring more code.

Subequent loads which allowed the query to be cached by EntityFramework had similiar speeds. From a different perspective, prepared SQL statements performs better, however you must write the SQL, which you do not have to do with EF at the cost of performance.
