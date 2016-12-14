CREATE TABLE [dbo].[CoreEntity]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ParentEntityId] INT NULL, 
    [GuidIdEntityId] NVARCHAR(50) NULL, 
    [StringIdEntityId] NVARCHAR(100) NULL, 
	[Name] NVARCHAR(50) NULL,
	[Integer] INT NULL,
	[Decimal] DECIMAL NULL,
    CONSTRAINT [FK_CoreEntity_ParentEntity] FOREIGN KEY ([ParentEntityId]) REFERENCES [ParentEntity]([Id]), 
    CONSTRAINT [FK_CoreEntity_GuidIdEntity] FOREIGN KEY ([GuidIdEntityId]) REFERENCES [GuidIdEntity]([Id]), 
    CONSTRAINT [FK_CoreEntity_StringIdEntity] FOREIGN KEY ([StringIdEntityId]) REFERENCES [StringIdEntity]([Id])
)
