CREATE TABLE [dbo].[Alerts] (
    [Id]          VARCHAR (50)  NOT NULL,
    [TargetId]    VARCHAR (50)  NOT NULL,
    [AlerterId]   VARCHAR (50)  NOT NULL,
    [AlertTime]   DATETIME2 (7) NOT NULL,
    [Description] VARCHAR (300) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

