IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240131094508_initIdentity', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductMoveHistories] (
    [Id] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    [Change] int NOT NULL,
    [Remaining] int NOT NULL,
    [MoveType] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ProductMoveHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductMoveHistories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductMoveHistories_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id])
);
GO

CREATE INDEX [IX_ProductMoveHistories_CategoryId] ON [ProductMoveHistories] ([CategoryId]);
GO

CREATE INDEX [IX_ProductMoveHistories_ProductId] ON [ProductMoveHistories] ([ProductId]);
GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240201170729_InitProductModel', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240202054131_modifyUser', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [CompanyId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(max) NOT NULL DEFAULT N'';
GO

CREATE TABLE [Company] (
    [CompanyId] int NOT NULL IDENTITY,
    [CompanyName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyId])
);
GO

CREATE INDEX [IX_AspNetUsers_CompanyId] ON [AspNetUsers] ([CompanyId]);
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([CompanyId]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240204035154_addCompanyAndFiristNameLastname', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Company_CompanyId];
GO

ALTER TABLE [Company] DROP CONSTRAINT [PK_Company];
GO

EXEC sp_rename N'[Company]', N'Companies';
GO

ALTER TABLE [Companies] ADD CONSTRAINT [PK_Companies] PRIMARY KEY ([CompanyId]);
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([CompanyId]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240204035407_addCompanyToContext', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Companies_CompanyId];
GO

ALTER TABLE [Companies] DROP CONSTRAINT [PK_Companies];
GO

EXEC sp_rename N'[Companies]', N'Company';
GO

ALTER TABLE [Company] ADD CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyId]);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CompanyId', N'CompanyName') AND [object_id] = OBJECT_ID(N'[Company]'))
    SET IDENTITY_INSERT [Company] ON;
INSERT INTO [Company] ([CompanyId], [CompanyName])
VALUES (1, N'S&P'),
(2, N'S2P'),
(3, N'Aceepta');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CompanyId', N'CompanyName') AND [object_id] = OBJECT_ID(N'[Company]'))
    SET IDENTITY_INSERT [Company] OFF;
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([CompanyId]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240204042521_seedData', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'CompanyId', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'FirstName', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [CompanyId], [ConcurrencyStamp], [Email], [EmailConfirmed], [FirstName], [LastName], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'1', 0, 1, N'b63bb580-ccb7-4a31-97e5-af7442557f79', N'user@example.com', CAST(1 AS bit), N'John', N'Doe', CAST(0 AS bit), NULL, N'USER@EXAMPLE.COM', N'USER', N'AQAAAAIAAYagAAAAECgweVoPFGwHxSaGLWZhyXHNbilnT3UA56h5uULg7St43fImHD6MW+8wrL62PCCq/A==', NULL, CAST(0 AS bit), N'', CAST(0 AS bit), N'user');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'CompanyId', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'FirstName', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240204045400_seeduserData', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'70e09f2c-fb74-4c53-81df-b6dbfc15e115', [PasswordHash] = N'AQAAAAIAAYagAAAAEPX42mSybWPwoX0NXsgA6G/ABYjFoZz5krE0jMb8Z0OTsm+2hVxg2Mh0orxlbyLP3g==', [PhoneNumber] = N'0918131501'
WHERE [Id] = N'1';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240204045434_seeduserData2', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUserRoles] ADD [Discriminator] nvarchar(34) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetRoles] ADD [Discriminator] nvarchar(21) NOT NULL DEFAULT N'';
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Discriminator', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Discriminator], [Name], [NormalizedName])
VALUES (N'1', NULL, N'ApplicationRole', N'Admin', N'ADMIN');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Discriminator', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'69b2b7f5-71a9-4622-84fb-65a8b772ade2', [Email] = N'admin@sw.com', [NormalizedEmail] = N'ADMIN@SW.COM', [NormalizedUserName] = N'S&P_01', [PasswordHash] = N'AQAAAAIAAYagAAAAEAL9yyQh3Rv5j3RNusOQEU7bOgCcmt9DJy8vTxkPIy2WvQgfFf2uiA6zWAMzGUfwJQ==', [PhoneNumber] = N'0918131505', [UserName] = N'S&P_01'
WHERE [Id] = N'1';
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId', N'Discriminator') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] ON;
INSERT INTO [AspNetUserRoles] ([RoleId], [UserId], [Discriminator])
VALUES (N'1', N'1', N'ApplicationUserRole');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId', N'Discriminator') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240206043918_change-rolescheme', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Discriminator', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Discriminator], [Name], [NormalizedName])
VALUES (N'2', NULL, N'ApplicationRole', N'SSaleRole', N'SSALEROLE'),
(N'3', NULL, N'ApplicationRole', N'JSaleRole', N'JSALEROLE'),
(N'4', NULL, N'ApplicationRole', N'RSaleRole', N'RSALEROLE'),
(N'5', NULL, N'ApplicationRole', N'AccountRole', N'ACCOUNTROLE');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Discriminator', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'3e2213b3-36f1-4e7b-bad6-9a90f0b4e54f', [PasswordHash] = N'AQAAAAIAAYagAAAAEGwqxZDd3zGkU0szEEn0dQ9EYzkyO68HOZ6XQ5qWVrxoanm+RXDcJW3WdA4tXU93vA=='
WHERE [Id] = N'1';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240206045147_feed-role-data', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [RefreshToken] (
    [Id] nvarchar(450) NOT NULL,
    [Token] nvarchar(max) NOT NULL,
    [Expires] datetime2 NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedByIp] nvarchar(max) NOT NULL,
    [Revoked] datetime2 NULL,
    [RevokedByIp] nvarchar(max) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshToken_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'da6c0438-b678-4d08-9d1f-97a04b8cf861', [PasswordHash] = N'AQAAAAIAAYagAAAAEBiXmem8VaUAcEtMfDZUvR+mTCum4SFHK6RWIRZJOyNUJv1EOtWVM+AOlqm3HhmMvg=='
WHERE [Id] = N'1';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_RefreshToken_UserId] ON [RefreshToken] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240207061548_AddRefreshTokens', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RefreshToken]') AND [c].[name] = N'RevokedByIp');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [RefreshToken] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [RefreshToken] ALTER COLUMN [RevokedByIp] nvarchar(max) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'f9dcf45f-954b-40f9-ac7f-8e5e5a7d383f', [PasswordHash] = N'AQAAAAIAAYagAAAAEC1dZjGNJNZwgf9D2Mk3k0gxYifC7BWXYa7Thi/RkQHAWNId1Hzg1l1qLD1ib1JHHw=='
WHERE [Id] = N'1';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240207062807_revokedByIdCanNull', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserRoles]') AND [c].[name] = N'Discriminator');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AspNetUserRoles] DROP COLUMN [Discriminator];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetRoles]') AND [c].[name] = N'Discriminator');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetRoles] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [AspNetRoles] DROP COLUMN [Discriminator];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'e556b175-b042-4e9d-8213-4693583d3a3c', [PasswordHash] = N'AQAAAAIAAYagAAAAEE7P05T6ttVJYYYjYTK2E/CUvvDBZQC/dLYvPScivGBk3ovbZlnmTPzYh6etLQgnNg=='
WHERE [Id] = N'1';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240209040443_modify-role', N'8.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'89ee3dd3-a2d9-4e03-ab1d-bc6eb45213c7', [PasswordHash] = N'AQAAAAIAAYagAAAAEGFsBDdkG7rylwt9bmThCotcPFpFoGY9rfXg5Dco7SgDGLvkncxTCwmGFJQq1MNQwA=='
WHERE [Id] = N'1';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240210011709_add-conversion', N'8.0.1');
GO

COMMIT;
GO

