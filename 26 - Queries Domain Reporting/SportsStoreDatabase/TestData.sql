/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
insert into Users
values (1), (2)

insert into LoyaltyAccounts (Id, UserId, IsActive)
values (1, 1, 1), (2, 2, 0)

insert into Orders (Id, UserId, [Date], NetProfit)
values (1, 1, '2014-02-05', 25.25), (2, 2, '2014-02-10', 44.55)
