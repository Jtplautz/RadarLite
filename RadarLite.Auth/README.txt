Vue.ts -> IdentityServer that uses microsoft aspnet identity. 

1. Get the Vue client correctly configured
2. Configure the Identity server to correctly issue auth
3. Replace in memory data using SQL and Microsoft Identity.




Setting up Identity;
	1. Duende Site for templates/guidance boilerplate.
	2. CWJ youtube

Set up the context first.
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb -s RadarLite.Auth
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb -RadarLite.Auth

Configure the AspIdentityContext (the main one you built)
add-migration ApplicationUserOne -c RadarLiteIdentityContext -s RadarLite.Auth
update-database -context RadarLiteIdentityContext -s RadarLite.Auth