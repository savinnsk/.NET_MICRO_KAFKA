-- this is an example of a query file

-- Criar o login no servidor
CREATE LOGIN SMUser WITH PASSWORD = 'password';
GO

-- Conectar ao banco SocialMedia
USE SocialMedia;
GO

-- Criar o usuário associado ao login
CREATE USER SMUser FOR LOGIN SMUser;
GO

-- Conceder permissões de administrador ao usuário
ALTER ROLE db_owner ADD MEMBER SMUser;
GO

/*

-- Passo 1: Verificar se o banco SocialMedia existe
SELECT name FROM sys.databases WHERE name = 'SocialMedia';

-- Passo 2: Criar o banco de dados se ele não existir
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SocialMedia')
BEGIN
    CREATE DATABASE SocialMedia;
    GO
END

-- Passo 3: Criar o login no servidor
CREATE LOGIN SMUser WITH PASSWORD = 'password';
GO

-- Passo 4: Conectar ao banco SocialMedia
USE SocialMedia;
GO

-- Passo 5: Criar o usuário associado ao login
CREATE USER SMUser FOR LOGIN SMUser;
GO

-- Passo 6: Conceder permissões de administrador ao usuário
ALTER ROLE db_owner ADD MEMBER SMUser;
GO
/*


Use SocialMedia
GO

IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'SMUser')
BEGIN
    CREATE LOGIN SMUser WITH PASSWORD=N'password',DEFAULT_DATABASE=SocialMedia
END

IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = 'SMUser')
BEGIN
    EXEC sp_adduser 'SMUser' , 'SMUser' , 'db_owner'
END