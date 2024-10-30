CREATE DATABASE `person_db`;
CREATE TABLE person (
    Id            INT          NOT NULL  AUTO_INCREMENT,
    first_name    VARCHAR (20) NOT NULL,
    last_name     VARCHAR (50) NOT NULL,
    date_of_birth DATE         NOT NULL,
    CONSTRAINT PK_person PRIMARY KEY CLUSTERED (Id ASC)
);

INSERT INTO person (Id, first_name, last_name, date_of_birth) VALUES (1, N'Alan', N'Turing', N'1918-09-18');
INSERT INTO person (Id, first_name, last_name, date_of_birth) VALUES (2, N'Dennis', N'Ritchie', N'1914-09-09');
INSERT INTO person (Id, first_name, last_name, date_of_birth) VALUES (3, N'Anders', N'Hejlsberg ', N'1960-12-02');
INSERT INTO person (Id, first_name, last_name, date_of_birth) VALUES (4, N'James', N'Gosling', N'1955-05-19');
INSERT INTO person (Id, first_name, last_name, date_of_birth) VALUES (5, N'Bjarne', N'Stroustrup ', N'1950-12-30');
INSERT INTO person (Id, first_name, last_name, date_of_birth) VALUES (6, N'Linus', N'Torwalds', N'1969-12-28');
