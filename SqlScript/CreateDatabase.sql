CREATE DATABASE UserManagment_db;
GO

USE UserManagment_db;
GO

CREATE TABLE Users
(
    username VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    isActive BIT NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY (username)
);

CREATE TABLE UserProfile
(
    Firstname VARCHAR(50) NOT NULL,
    Lastname VARCHAR(50) NOT NULL,
    PersonalNumber VARCHAR(11) NOT NULL,
    CONSTRAINT PK_UserProfile PRIMARY KEY (PersonalNumber)
);