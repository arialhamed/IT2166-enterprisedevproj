CREATE TABLE [dbo].[Needs] (
    [Id]           VARCHAR (50)  NOT NULL,
	[BeneficiaryId] VARCHAR (50)  NOT NULL,
	[AssistantId] VARCHAR (50)  NOT NULL,
    [MedicalHistory] VARCHAR (MAX) NOT NULL,
	[Medications] VARCHAR (MAX) NOT NULL,
	[Conditions] VARCHAR (MAX) NOT NULL,
	[Vegetarian] INT NOT NULL,
	[FoodAllergies] VARCHAR (MAX) NOT NULL
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

