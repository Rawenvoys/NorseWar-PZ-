
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/03/2016 16:40:43
-- Generated from EDMX file: C:\Users\marcin\Source\Repos\PracowniaZespolowa\Gra\Gra\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[BuildVersion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BuildVersion];
GO
IF OBJECT_ID(N'[dbo].[ErrorLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ErrorLog];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AccountSet'
CREATE TABLE [dbo].[AccountSet] (
    [IdAccount] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Gold] nvarchar(max)  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [Stats_IdStats] int  NOT NULL,
    [Equipment_IdEquipment] int  NOT NULL,
    [Guild_IdGuild] int  NOT NULL
);
GO

-- Creating table 'StatsSet'
CREATE TABLE [dbo].[StatsSet] (
    [IdStats] int IDENTITY(1,1) NOT NULL,
    [Str] nvarchar(max)  NOT NULL,
    [Agi] nvarchar(max)  NOT NULL,
    [Dex] nvarchar(max)  NOT NULL,
    [Vit] nvarchar(max)  NOT NULL,
    [Int] nvarchar(max)  NOT NULL,
    [Luk] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ItemSet'
CREATE TABLE [dbo].[ItemSet] (
    [IdItem] int IDENTITY(1,1) NOT NULL,
    [ItemName] nvarchar(max)  NOT NULL,
    [ItemDescription] nvarchar(max)  NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [BonusStr] nvarchar(max)  NOT NULL,
    [BonusAgi] nvarchar(max)  NOT NULL,
    [BonusDex] nvarchar(max)  NOT NULL,
    [BonusVit] nvarchar(max)  NOT NULL,
    [BonusInt] nvarchar(max)  NOT NULL,
    [BonusLuk] nvarchar(max)  NOT NULL,
    [AccountIdAccount] int  NOT NULL
);
GO

-- Creating table 'EquipmentSet'
CREATE TABLE [dbo].[EquipmentSet] (
    [IdEquipment] int IDENTITY(1,1) NOT NULL,
    [Shield] nvarchar(max)  NOT NULL,
    [Weapon] nvarchar(max)  NOT NULL,
    [Armor] nvarchar(max)  NOT NULL,
    [Boots] nvarchar(max)  NOT NULL,
    [Legs] nvarchar(max)  NOT NULL,
    [Helmet] nvarchar(max)  NOT NULL,
    [Accessory] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'GuildSet'
CREATE TABLE [dbo].[GuildSet] (
    [IdGuild] int IDENTITY(1,1) NOT NULL,
    [GuildName] nvarchar(max)  NOT NULL,
    [GuildLeader] nvarchar(max)  NOT NULL,
    [Level] nvarchar(max)  NOT NULL,
    [Gold] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FriendSet'
CREATE TABLE [dbo].[FriendSet] (
    [IdFriend] int IDENTITY(1,1) NOT NULL,
    [AccountIdAccount] int  NOT NULL,
    [FriendName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MessageSet'
CREATE TABLE [dbo].[MessageSet] (
    [IdMessage] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [AccountIdAccount] int  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [EMail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BuildVersion'
CREATE TABLE [dbo].[BuildVersion] (
    [SystemInformationID] tinyint IDENTITY(1,1) NOT NULL,
    [Database_Version] nvarchar(25)  NOT NULL,
    [VersionDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NOT NULL
);
GO

-- Creating table 'ErrorLog'
CREATE TABLE [dbo].[ErrorLog] (
    [ErrorLogID] int IDENTITY(1,1) NOT NULL,
    [ErrorTime] datetime  NOT NULL,
    [UserName] nvarchar(128)  NOT NULL,
    [ErrorNumber] int  NOT NULL,
    [ErrorSeverity] int  NULL,
    [ErrorState] int  NULL,
    [ErrorProcedure] nvarchar(126)  NULL,
    [ErrorLine] int  NULL,
    [ErrorMessage] nvarchar(4000)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdAccount] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [PK_AccountSet]
    PRIMARY KEY CLUSTERED ([IdAccount] ASC);
GO

-- Creating primary key on [IdStats] in table 'StatsSet'
ALTER TABLE [dbo].[StatsSet]
ADD CONSTRAINT [PK_StatsSet]
    PRIMARY KEY CLUSTERED ([IdStats] ASC);
GO

-- Creating primary key on [IdItem] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [PK_ItemSet]
    PRIMARY KEY CLUSTERED ([IdItem] ASC);
GO

-- Creating primary key on [IdEquipment] in table 'EquipmentSet'
ALTER TABLE [dbo].[EquipmentSet]
ADD CONSTRAINT [PK_EquipmentSet]
    PRIMARY KEY CLUSTERED ([IdEquipment] ASC);
GO

-- Creating primary key on [IdGuild] in table 'GuildSet'
ALTER TABLE [dbo].[GuildSet]
ADD CONSTRAINT [PK_GuildSet]
    PRIMARY KEY CLUSTERED ([IdGuild] ASC);
GO

-- Creating primary key on [IdFriend] in table 'FriendSet'
ALTER TABLE [dbo].[FriendSet]
ADD CONSTRAINT [PK_FriendSet]
    PRIMARY KEY CLUSTERED ([IdFriend] ASC);
GO

-- Creating primary key on [IdMessage] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [PK_MessageSet]
    PRIMARY KEY CLUSTERED ([IdMessage] ASC);
GO

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SystemInformationID] in table 'BuildVersion'
ALTER TABLE [dbo].[BuildVersion]
ADD CONSTRAINT [PK_BuildVersion]
    PRIMARY KEY CLUSTERED ([SystemInformationID] ASC);
GO

-- Creating primary key on [ErrorLogID] in table 'ErrorLog'
ALTER TABLE [dbo].[ErrorLog]
ADD CONSTRAINT [PK_ErrorLog]
    PRIMARY KEY CLUSTERED ([ErrorLogID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Stats_IdStats] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [FK_AccountStats]
    FOREIGN KEY ([Stats_IdStats])
    REFERENCES [dbo].[StatsSet]
        ([IdStats])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountStats'
CREATE INDEX [IX_FK_AccountStats]
ON [dbo].[AccountSet]
    ([Stats_IdStats]);
GO

-- Creating foreign key on [AccountIdAccount] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [FK_AccountItem]
    FOREIGN KEY ([AccountIdAccount])
    REFERENCES [dbo].[AccountSet]
        ([IdAccount])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountItem'
CREATE INDEX [IX_FK_AccountItem]
ON [dbo].[ItemSet]
    ([AccountIdAccount]);
GO

-- Creating foreign key on [Equipment_IdEquipment] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [FK_AccountEquipment]
    FOREIGN KEY ([Equipment_IdEquipment])
    REFERENCES [dbo].[EquipmentSet]
        ([IdEquipment])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountEquipment'
CREATE INDEX [IX_FK_AccountEquipment]
ON [dbo].[AccountSet]
    ([Equipment_IdEquipment]);
GO

-- Creating foreign key on [Guild_IdGuild] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [FK_AccountGuild]
    FOREIGN KEY ([Guild_IdGuild])
    REFERENCES [dbo].[GuildSet]
        ([IdGuild])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountGuild'
CREATE INDEX [IX_FK_AccountGuild]
ON [dbo].[AccountSet]
    ([Guild_IdGuild]);
GO

-- Creating foreign key on [AccountIdAccount] in table 'FriendSet'
ALTER TABLE [dbo].[FriendSet]
ADD CONSTRAINT [FK_AccountFriendList]
    FOREIGN KEY ([AccountIdAccount])
    REFERENCES [dbo].[AccountSet]
        ([IdAccount])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountFriendList'
CREATE INDEX [IX_FK_AccountFriendList]
ON [dbo].[FriendSet]
    ([AccountIdAccount]);
GO

-- Creating foreign key on [AccountIdAccount] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_AccountMessage]
    FOREIGN KEY ([AccountIdAccount])
    REFERENCES [dbo].[AccountSet]
        ([IdAccount])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountMessage'
CREATE INDEX [IX_FK_AccountMessage]
ON [dbo].[MessageSet]
    ([AccountIdAccount]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------