CREATE TABLE [dbo].[ChildEntity]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CoreEntityId] INT NULL, 
	[Name] NVARCHAR(50) NULL,
	[Integer] INT NULL,
	[Decimal] DECIMAL NULL,
	[Double] FLOAT NULL,
    CONSTRAINT [FK_ChildEntity_CoreEntity] FOREIGN KEY ([CoreEntityId]) REFERENCES [CoreEntity]([Id])
)
