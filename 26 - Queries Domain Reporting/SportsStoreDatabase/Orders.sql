CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Date] Date NOT NULL,
	[UserId] INT NOT NULL,
	[NetProfit] DECIMAL NOT NULL,
	FOREIGN KEY (UserId) REFERENCES Users(Id)
)
