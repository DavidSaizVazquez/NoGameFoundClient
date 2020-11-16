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


INSERT INTO Users(Username,Pwd,Age, Mail,Spam) VALUES ('a','a',0,'aTotallyRealMail@hello.com',true);
INSERT INTO Users(Username,Pwd,Age, Mail,Spam) VALUES ('b','b',33,'aTotallyRealMail@hello.com',true);

SELECT * FROM Users;