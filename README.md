# IdentityWebApp
A fun and slightly goofy spin on handling API security. This leverages a PostgreSQL database and creates a scehma with the script found at: https://github.com/cenamorado3/msIdentity4Postgres/blob/main/createIdentity4pg.sql
This scehma matches the ASPNET schema for SQLServer as closely as possible. No functionality exist for creating users or permissions ie claims/roles, the values must be inserted into the respective tables. Meaning, if you would like to get the weather 
from the mock endpoint, you will need to create a user and claims based on their user_id by inserting the values, to access the "API" they must be an "admin" within the scope of this application.

With a properly configured user, they may request a token, which must be attached to the Authorization header for future request, the endpoints will validate the user token has the claim as defined by the policy in the startup file and that the token
is valid.
