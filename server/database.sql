DROP DATABASE IF EXISTS GameDB;
CREATE DATABASE GameDB;

USE GameDB;

CREATE TABLE Users(
    Id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Username TEXT NOT NULL,
    Pwd TEXT NOT NULL,
    Age INT NOT NULL,
    Mail TEXT NOT NULL,
    Spam BOOLEAN NOT NULL
);

CREATE TABLE Games(
      Id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
      Count INT DEFAULT 0,
      Players TEXT,
      Score INT
);

CREATE TABLE UsersPerGame(
    Id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    GameId INT NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (GameId) REFERENCES Games(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

INSERT INTO Users(Username,Pwd,Age, Mail,Spam) VALUES ('a','a',0,'aTotallyRealMail@hello.com',true);
INSERT INTO Users(Username,Pwd,Age, Mail,Spam) VALUES ('b','b',33,'aTotallyRealMail@hello.com',true);

INSERT INTO Games(Id, Count, Players, Score) VALUES(1,0,'p*p*',2.3);

UPDATE Games SET Players='BB' WHERE Games.Id=1;

SELECT * FROM Users;    