CREATE DATABASE IF NOT EXISTS LibraryManagement;

USE LibraryManagement;

DROP TABLE IF EXISTS User;

CREATE TABLE User(
  Id INT NOT NULL AUTO_INCREMENT,
  Email VARCHAR(144) NOT NULL,
  SecretKey VARCHAR(500) NOT NULL,
  FirstName VARCHAR(500) NOT NULL,
  LastName VARCHAR(500),
  Description VARCHAR(500),
  PRIMARY KEY (Id),
  UNIQUE KEY (Email));

DROP TABLE IF EXISTS Book;

CREATE TABLE Book(
  Id INT NOT NULL AUTO_INCREMENT,
  Title VARCHAR(500) NOT NULL,
  Author VARCHAR(500) NOT NULL,
  Description VARCHAR(500),
  PRIMARY KEY (Id));

DROP TABLE IF EXISTS Rental;

CREATE TABLE Rental(
  Id INT NOT NULL AUTO_INCREMENT,
  IdBook INT NOT NULL,
  IdUser INT NOT NULL,
  Issued DATETIME NOT NULL,
  Returned DATETIME,
  PRIMARY KEY (Id),
  FOREIGN KEY (IdBook) REFERENCES Book (Id),
  FOREIGN KEY (IdUser) REFERENCES User (Id));

DROP TABLE IF EXISTS Review;

CREATE TABLE Review(
  Id INT NOT NULL AUTO_INCREMENT,
  UserId INT NOT NULL,
  BookId INT NOT NULL,
  Rate INT NOT NULL,
  Comment VARCHAR(500),
  PRIMARY KEY (Id),
  FOREIGN KEY (UserId) REFERENCES User(Id),
  FOREIGN KEY (BookId) REFERENCES Book(Id),
  CONSTRAINT CheckRate CHECK(Rate > 0 AND Rate < 10));