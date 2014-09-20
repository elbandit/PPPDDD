CREATE TABLE [dbo].[LoyaltyAccounts]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[UserId] INT NOT NULL,
	[IsActive] BIT NOT NULL,
	[Create] DATE NOT NULL,
	FOREIGN KEY (UserId) REFERENCES Users(Id)
)
