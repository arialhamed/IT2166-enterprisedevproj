CREATE TABLE [dbo].[Events] (
    [Id]          VARCHAR (50)  NOT NULL,
    [Name]    VARCHAR (50)  NOT NULL,
    [Description]   VARCHAR (1000)  NOT NULL,
    [CreatorId] VARCHAR (50) NOT NULL,
    [StartTime]   DATETIME2 (7) NOT NULL,
    [EndTime]  DATETIME2 (7) NOT NULL,
    [Location] VARCHAR (100) NOT NULL,
    [Sponsors] VARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);