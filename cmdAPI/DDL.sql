use CmdApi;
-- Insert rows into table 'CommandItens'
INSERT INTO CommandItems
( -- columns to insert data into
 [HowTo], [Plataform], [Commandline]
)
VALUES
( -- first row: values for the columns in the list above
 'How to generate a migration in EF Core', '.Net Core EF', 'dotnet ef migrations add <name of migration>'
),
( -- second row: values for the columns in the list above
 'How to update the database (run migration)', '.Net Core EF', 'dotnet ef database update'
)
-- add more rows here
GO