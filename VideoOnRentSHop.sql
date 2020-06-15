USE VIDEORENTALDB;


CREATE TABLE Customer (
  [CustID] int NOT NULL identity primary key,
  [FirstName] varchar(25)  DEFAULT NULL,
  [LastName] varchar(25)  DEFAULT NULL,
  [Address] varchar(25)  DEFAULT NULL,
  [Phone] int DEFAULT NULL
)  ;


CREATE TABLE VIDEOS (
  [videoID] int  NOT NULL identity primary key,
  [Title] varchar(25) DEFAULT NULL,
  [Year] varchar(25) DEFAULT NULL,
  [Rental_Cost] int DEFAULT NULL,
  [Genre] varchar(50) DEFAULT NULL
)  ;


CREATE TABLE RENTEDVIDEOS (
  [RMID] int  NOT NULL identity primary key,
  [videoIDFK] int DEFAULT NULL,
  [CustIDFK] int DEFAULT NULL,
  [DateRENTED] datetime2(0) DEFAULT NULL,
  [DateReturned] datetime2(0) DEFAULT NULL,
  CONSTRAINT [FK_CustIDFk] FOREIGN KEY ([CustIDFK]) REFERENCES Customer ([CustID]),
  CONSTRAINT [FK_videoIDFk] FOREIGN KEY ([videoIDFK]) REFERENCES VIDEOS ([videoID])
)  ;

INSERT INTO customer (FirstName, LastName, Address, Phone) VALUES ('nick', 'fox', '12 don Street', '1354221');
INSERT INTO customer (FirstName, LastName, Address, Phone) VALUES ('albert', 'kal', '11 game Street', '3212312');
INSERT INTO customer (FirstName, LastName, Address, Phone) VALUES ('rick', 'sanchez', '344 gaint Street', '245668');
INSERT INTO customer (FirstName, LastName, Address, Phone) VALUES ('stu', 'blanc', '11 tall Lane', '41345352');

INSERT INTO VIDEOS (Title, Year,Rental_Cost,Genre) VALUES ('scent of a woman', '1990',2,'drama');
INSERT INTO VIDEOS (Title, Year,Rental_Cost,Genre) VALUES ('lord of the rings', '2000',2,'adventure, drama');
INSERT INTO VIDEOS (Title, Year,Rental_Cost,Genre) VALUES ('ocean 11', '2000',2,'Action, adventure');
INSERT INTO VIDEOS (Title, Year,Rental_Cost,Genre) VALUES ('detective pikachu', '2019',5,'Adventure, Sci-Fi');


INSERT INTO RENTEDVIDEOS (videoIDFK, CustIDFK, DateRENTED) VALUES (3, 1, '2019-09-01T21:15:38.793');
INSERT INTO RENTEDVIDEOS (videoIDFK, CustIDFK, DateRENTED) VALUES (4, 2, '2019-08-31T21:12:39.793');
INSERT INTO RENTEDVIDEOS (videoIDFK, CustIDFK, DateRENTED, DateReturned) VALUES (1, 3, '2018-04-09T22:11:15.232', '2019-01-02T10:20:10.211');
INSERT INTO RENTEDVIDEOS (videoIDFK, CustIDFK, DateRENTED, DateReturned) VALUES (2, 3, '2017-03-10T9:22:30.793', '2018-04-09T15:30:03.447');