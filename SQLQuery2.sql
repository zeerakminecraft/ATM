CREATE TABLE [dbo].[Users]
(
	[CardNumber] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Pin] SMALLINT NOT NULL, 
    [Balance] MONEY NOT NULL
)

INSERT INTO Users(CardNumber, Name, Pin, Balance) VALUES (1234567890, 'Puneet Nehra', 1234, 50000 )
INSERT INTO Users(CardNumber, Name, Pin, Balance) VALUES (1234567891, 'sultana Rehman', 1235, 8700000 )
INSERT INTO Users(CardNumber, Name, Pin, Balance) VALUES (1234567892, 'Asif Peer', 1236, 19087000 )
INSERT INTO Users(CardNumber, Name, Pin, Balance) VALUES (1234567893, 'Virat Kohli', 1237, 6500000000 )


select Pin from Users

select * from PuneetNehra1234567890