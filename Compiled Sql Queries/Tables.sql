CREATE TABLE Accounts(
	AccountNo INT Primary Key,
	Username VARCHAR(25) COLLATE Latin1_General_CS_AS NOT NULL UNIQUE,
	AccPassword VARCHAR(15) COLLATE Latin1_General_CS_AS NOT NULL
);


CREATE TABLE Customers(
	CustomerID INT Primary Key,
	CustName VARCHAR(50) NOT NULL,
	PhoneNo VARCHAR(13) UNIQUE,
	AccountNo INT NOT NULL,

	CONSTRAINT Accounts_FK Foreign Key(AccountNo) references Accounts(AccountNo)
);

ALTER TABLE Customers ALTER COLUMN PhoneNo VARCHAR(11);
ALTER TABLE Customers ADD CONSTRAINT PhoneNo_Format_Customer check(PhoneNo like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]');

ALTER TABLE Staff ALTER COLUMN PhoneNo VARCHAR(11);
ALTER TABLE Staff ADD CONSTRAINT PhoneNo_Format_Staff check(PhoneNo like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]');

CREATE TABLE Staff(
	StaffID INT Primary Key,
	StaffName VARCHAR(50) NOT NULL,
	PhoneNo VARCHAR(13) NOT NULL UNIQUE,
	Salary INT NOT NULL,
	AccountNo INT NOT NULL,

	CONSTRAINT Accounts_FK_Staff Foreign Key(AccountNo) references Accounts(AccountNo)
);

ALTER TABLE Staff ADD CONSTRAINT Accounts_FK_Staff Foreign Key(AccountNo) references Accounts(AccountNo);

ALTER TABLE Staff ADD Position VARCHAR(25) NOT NULL;
ALTER TABLE Staff ADD Supervisor_ID INT;
ALTER TABLE Staff ADD CONSTRAINT Supervisor_FK Foreign Key(Supervisor_ID) references Staff(StaffID);

CREATE TABLE Computers(
	ComputerID INT Primary Key,
	CPU VARCHAR(25),
	GPU VARCHAR(25),
	RAM INT,
	NetSpeed FLOAT
);

CREATE TABLE Seats(
	SeatNo INT Primary Key,
	CurrentStatus VARCHAR(15) NOT NULL,
	Premium_YES_NO BIT NOT NULL,
	ComputerID INT,

	CONSTRAINT Computers_FK Foreign Key(ComputerID) references Computers(ComputerID) ON DELETE SET NULL
);

CREATE TABLE Bookings(
	SeatNo INT,
	Date_of_Booking DATE NOT NULL,
	Start_Time DATETIME,
	End_Time DATETIME,
	Amount_Paid INT,
	CustomerID INT,

	CONSTRAINT Seats_FK Foreign Key(SeatNo) references Seats(SeatNo) ON DELETE CASCADE,
	CONSTRAINT Customers_FK Foreign Key(CustomerID) references Customers(CustomerID),
	CONSTRAINT Bookings_PK Primary Key(SeatNo, Start_Time, End_Time, CustomerID)
);

CREATE TABLE Games(
	GameID INT Primary Key,
	GameName VARCHAR(50) NOT NULL,
	Genre VARCHAR(25),
	GameDescription VARCHAR(100),
	Popularity INT
);

ALTER TABLE Games ADD CONSTRAINT UniqueGameName Unique(GameName);

CREATE TABLE Events(
	EventID INT Primary Key,
	EventName VARCHAR(50) NOT NULL,
	Start_Time DATETIME NOT NULL,
	End_Time DATETIME NOT NULL,
	GameID INT,
	Max_Participants INT,
	Poster_link VARCHAR(100),

	CONSTRAINT Games_FK Foreign Key(GameID) references Games(GameID) ON DELETE CASCADE
);

CREATE TABLE Leaderboard(
	GameID INT NOT NULL,
	CustomerID INT NOT NULL,
	GameRank INT,

	CONSTRAINT Leaderboard_PK Primary Key(GameID, CustomerID),
	CONSTRAINT Games_FK_Leaderboard Foreign Key(GameID) references Games(GameID) ON DELETE CASCADE,
	CONSTRAINT Customers_FK_Leaderboard Foreign Key(CustomerID) references Customers(CustomerID)
);

ALTER TABLE Staff ADD CONSTRAINT Unique_Staff_Account Unique(AccountNo);
ALTER TABLE Customers ADD CONSTRAINT Unique_Customer_Account Unique(AccountNo);