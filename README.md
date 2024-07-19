# ClayBackend

#MAIN DATABASE
The choice was between Postgres, MariaDB and MSSQL. MSSQL is the worst one between those for scalability with Maria and Postgres being close. Ultimately, I chose Postgres because of my experience with it and the better support. I think Maria would have worked as well. Comparison


#DOOR ACTIVITY LOG DB
My initial idea was to save all of them in a MongoDB collection, ultimately, I chose to use the main DB because of time constraints and simplicity.


#AUTHENTICATION / AUTHORIZATION
I think the better solution would have been to offload the whole module onto services like Auth0 or Clerk using JWTs for the sake of the locks as well. I went with ASP.NET Identity to better manage my own users and roles. Implementing a JWT auth would have been really time expensive so I chose to use a simple bearer token with the idea of offloading everything when scaling.


#REDIS OR NO REDIS
The initial plan was to implement 2 Redis instances, one for caching and the other to maybe persistently save door permissions. The one used for caching would have been used to transfer messages on singular channels by lockId.
I did consider RabbitMQ as a broker, but I didn’t really want to introduce too many technologies.


#TESTING
Unfortunately I couldn’t implement testing in time, had I done it I would have done mostly integration tests. There’s not a lot of unit testable functions So I would skip those until we have more business logic to justify a service layer.
