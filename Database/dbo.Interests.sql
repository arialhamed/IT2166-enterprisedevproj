CREATE TABLE [dbo].[Interests] (
    [Id]           VARCHAR (50)  NOT NULL,
    [Name]         VARCHAR (50)  NOT NULL,
    [Description]  VARCHAR (MAX) NOT NULL,
    [PosterURL]    VARCHAR (80)  NULL,
    [Rating]       INT           NOT NULL,
	[Likes]        INT           NOT NULL,
	[Views]        INT           NOT NULL,
    [CreatorId]    VARCHAR (50)  NOT NULL,
    [DateModified] DATETIME2 (7) NOT NULL,
    [Approved]     INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

