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
                      Players TEXT,
                      Score INT,
                      day DATE
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

INSERT INTO Games(Id, Players, Score,day) VALUES(1,'p*p',2.3,'2020-1-2');
INSERT INTO Games(Id, Players, Score,day) VALUES(2,'p*p',2.3,'2020-1-3');
INSERT INTO Games(Id, Players, Score,day) VALUES(3,'p*p',2.3,'2020-1-3');
INSERT INTO Games(Id, Players, Score,day) VALUES(4,'p*p',2.3,'2020-1-3');
INSERT INTO Games(Id, Players, Score,day) VALUES(5,'p*p',2.3,'2020-1-4');