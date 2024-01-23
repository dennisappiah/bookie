CREATE TABLE [dbo].[RoomTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NCHAR(50) NOT NULL, 
    [Description] NCHAR(200) NOT NULL, 
    [Price] MONEY NOT NULL
)
