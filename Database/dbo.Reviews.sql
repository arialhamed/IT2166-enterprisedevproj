CREATE TABLE [dbo].[Reviews]
(
	[Id] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [ItemId] VARCHAR(50) NOT NULL, 
    [ReviewerId] VARCHAR(10) NOT NULL, 
    [Description] VARCHAR(500) NULL, 
    [Rating] INT NOT NULL, 
    [HelpfulRate] INT NOT NULL,
    [ReviewDate] DATETIME NOT NULL, 
)
