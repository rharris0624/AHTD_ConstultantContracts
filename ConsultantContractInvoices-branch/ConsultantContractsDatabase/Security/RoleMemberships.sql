ALTER ROLE [db_datawriter] ADD MEMBER [AHTD\HttpDevWeb];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [AHTD\DeveloperView];


GO
ALTER ROLE [db_datareader] ADD MEMBER [AHTD\HttpDevWeb];


GO
ALTER ROLE [db_datareader] ADD MEMBER [AHTD\DeveloperView];

