CREATE TABLE [dbo].[CoreEntity]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ParentEntityId] INT NULL, 
	[Name] NVARCHAR(50) NULL,
	[Integer] INT NULL,
	[Decimal] DECIMAL NULL,
	[Double] FLOAT NULL,
    CONSTRAINT [FK_CoreEntity_ParentEntity] FOREIGN KEY ([ParentEntityId]) REFERENCES [ParentEntity]([Id]), 
)
