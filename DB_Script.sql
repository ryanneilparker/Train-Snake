Use master;
GO

Create Database TrainSnakeDB;
GO

Create Table player (
	playerId int NOT NULL PRIMARY KEY,
	username varchar(100) NOT NULL,
	firstName varchar(100) NOT NULL,
	lastName varchar(100) NOT NULL,
	score int
);
