CREATE TABLE [dbo].[Character]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Desc] VARCHAR(500) NOT NULL, 
    [Type] INT NOT NULL, 
    [Class] INT NOT NULL, 
    [Rarity] INT NOT NULL, 
    [Cost] INT NOT NULL, 
    [Price] INT NOT NULL, 
    [MaxLevel] INT NOT NULL, 
    [MaxExp] INT NOT NULL, 
    [minHP] INT NOT NULL, 
    [maxHp] INT NOT NULL, 
    [minAtk] INT NOT NULL, 
    [maxAtk] INT NOT NULL, 
    [minRcv] INT NOT NULL, 
    [maxRcv] INT NOT NULL, 
    [Special] INT NULL, 
    [Ability] INT NULL, 
    [Global] BIT NOT NULL, 
    CONSTRAINT [FK_Character_ToType] FOREIGN KEY ([Type]) REFERENCES [Type]([Id]),
    CONSTRAINT [FK_Character_ToClass] FOREIGN KEY ([Class]) REFERENCES [Class]([Id]),
    CONSTRAINT [FK_Character_ToSpecial] FOREIGN KEY ([Special]) REFERENCES [Special]([Id]),
    CONSTRAINT [FK_Character_ToAbility] FOREIGN KEY ([Ability]) REFERENCES [Ability]([Id]),
)

GO

CREATE INDEX [IX_Character_Summary] ON [dbo].[Character] ([Id],[Name],[Type],[Class])
