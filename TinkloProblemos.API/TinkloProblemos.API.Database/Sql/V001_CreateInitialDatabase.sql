use bakalaurinis;
CREATE TABLE Users(
	Id CHAR(38) NOT NULL,
	FirstName VARCHAR(128) NULL,
	LastName VARCHAR(128) NULL,
	UserName VARCHAR(64) NOT NULL,
	NormalizedUserName VARCHAR(64) NOT NULL,
	Email VARCHAR(64) NOT NULL,
	NormalizedEmail VARCHAR(64) NOT NULL,
	EmailConfirmed bit NOT NULL,
	PasswordHash VARCHAR(128) NULL,
	PhoneNumber VARCHAR(32) NULL,
	PhoneNumberConfirmed bit NOT NULL,
	PhotoUrl VARCHAR(128) NULL,
	Address VARCHAR(256) NULL,
	ConcurrencyStamp VARCHAR(128) NULL,
	SecurityStamp VARCHAR(128) NULL,
	RegistrationDate datetime NULL,
	LastLoginDate datetime NULL,
	LockoutEnabled bit NOT NULL,
	LockoutEndDateTimeUtc datetime NULL,
	TwoFactorEnabled bit NOT NULL,
	AccessFailedCount int NOT NULL,
   CONSTRAINT PK_IdentityUser PRIMARY KEY (Id)
);

CREATE VIEW GetTotalNumberOfUsers
AS
SELECT COUNT(*) AS TotalNumberOfUsers
FROM Users;

CREATE TABLE Roles(
	Id CHAR(38) NOT NULL,
	Name VARCHAR(32) NOT NULL,
	NormalizedName VARCHAR(32) NOT NULL,
	ConcurrencyStamp VARCHAR(128) NULL,
	CONSTRAINT PK_Roles PRIMARY KEY (Id)
);

CREATE TABLE UsersClaims(
	Id CHAR(38) NOT NULL,
	UserId CHAR(38) NOT NULL,
	ClaimType VARCHAR(65) NOT NULL,
	ClaimValue VARCHAR(65) NOT NULL,
	CONSTRAINT PK_UsersClaims PRIMARY KEY (Id)
);

CREATE TABLE UsersLogins(
	LoginProvider VARCHAR(32) NOT NULL,
	ProviderKey VARCHAR(64) NOT NULL,
	UserId CHAR(38) NOT NULL,
	ProviderDisplayName VARCHAR(32) NOT NULL,
	CONSTRAINT PK_UsersLogins PRIMARY KEY (LoginProvider, ProviderKey)
);

CREATE TABLE UsersRoles(
	UserId CHAR(38) NOT NULL,
	RoleId CHAR(38) NOT NULL,
 CONSTRAINT PK_UsersRoles PRIMARY KEY (UserId, RoleId)
 );

ALTER TABLE Users MODIFY COLUMN EmailConfirmed bit  NOT NULL DEFAULT 0;
ALTER TABLE Users MODIFY COLUMN PhoneNumberConfirmed bit  NOT NULL DEFAULT 0;
ALTER TABLE Users MODIFY COLUMN LockoutEnabled bit  NOT NULL DEFAULT 0;
ALTER TABLE Users MODIFY COLUMN TwoFactorEnabled bit  NOT NULL DEFAULT 0;
ALTER TABLE Users MODIFY COLUMN AccessFailedCount int  NOT NULL DEFAULT 0;

ALTER TABLE UsersClaims ADD CONSTRAINT FK_UsersClaims_Users FOREIGN KEY(UserId) REFERENCES Users(Id)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE UsersLogins ADD CONSTRAINT FK_UsersLogins_Users FOREIGN KEY(UserId)
REFERENCES Users (Id)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE UsersRoles  ADD CONSTRAINT FK_UsersRoles_Roles FOREIGN KEY(RoleId)
REFERENCES Roles (Id)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE UsersRoles  ADD  CONSTRAINT FK_UsersRoles_Users FOREIGN KEY(UserId)
REFERENCES Users (Id)
ON UPDATE CASCADE
ON DELETE CASCADE;

INSERT INTO Roles(Id, Name, NormalizedName, ConcurrencyStamp) VALUES ('642A2EBD-1B2F-4321-B294-028C3A27E0A2', 'User', 'USER', '1FE5B345-A5E0-496E-8956-B48D21AA37CC');
INSERT INTO Roles(Id, Name, NormalizedName, ConcurrencyStamp) VALUES ('D2A35AEC-402A-4BE6-9D7F-682C0B5D3FEF', 'Administrator', 'ADMINISTRATOR', 'BD480BB1-7D73-4393-AA09-51B11AAC949E');

INSERT INTO Users (Id, FirstName, LastName, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, PhoneNumber, PhoneNumberConfirmed, PhotoUrl, Address, ConcurrencyStamp, SecurityStamp, RegistrationDate, LastLoginDate, LockoutEnabled, LockoutEndDateTimeUtc, TwoFactorEnabled, AccessFailedCount) VALUES ('810BAD28-CED4-427A-9A88-5A17682304DA', 'Tomas', 'Valiunas', 'admin@dadmin.com', 'ADMIN@ADMIN.com', 'admin@admin.com', 'ADMIN@ADMIN.com', 1, 'AQAAAAEAACcQAAAAEFKjcOnLHrAhew3z9YUjO/vGiExMDrNi54ZyUKACsW8rn1vDTleOUvJeNoEG3JU4fQ==', '+306981908600', 1, NULL, 'Kyprou 14, Agia Varvara', '70C14A94-36D7-4CF7-973E-0425742980A8', 'A1991EC9-C0F3-40A5-8457-61032D13B1CD', '2018-04-08', NULL, 0, NULL, 0, 0);
INSERT INTO UsersRoles (UserId, RoleId) VALUES ('810BAD28-CED4-427A-9A88-5A17682304DA', '642A2EBD-1B2F-4321-B294-028C3A27E0A2');
INSERT INTO UsersRoles (UserId, RoleId) VALUES ('810BAD28-CED4-427A-9A88-5A17682304DA', 'D2A35AEC-402A-4BE6-9D7F-682C0B5D3FEF');

CREATE TABLE Category (Id int(10) NOT NULL AUTO_INCREMENT, Name varchar(255) NOT NULL, Description varchar(512), PRIMARY KEY (Id));
CREATE TABLE Category_Problem (CategoryId int(10) NOT NULL, ProblemId int(10) NOT NULL, PRIMARY KEY (CategoryId, ProblemId));
CREATE TABLE Comment (Id int(10) NOT NULL AUTO_INCREMENT, Text varchar(5000) NOT NULL, UserId CHAR(38) NOT NULL, ProblemId int(10) NOT NULL, PRIMARY KEY (Id));
CREATE TABLE InternetUser (Id int(10) NOT NULL AUTO_INCREMENT, FirstName varchar(255) NOT NULL, LastName varchar(255) NOT NULL, Description varchar(512), Location varchar(255), IpAddress varchar(255), PRIMARY KEY (Id));
CREATE TABLE InternetUserDevice (Id int(10) NOT NULL AUTO_INCREMENT, Name varchar(255) NOT NULL, Manufacturer varchar(255), MacAddress varchar(255), WarrantyExpiration date, Description varchar(512), InternetUserId int(10) NOT NULL, PRIMARY KEY (Id));
CREATE TABLE Problem (Id int(10) NOT NULL AUTO_INCREMENT, Name varchar(255) NOT NULL, Description varchar(9000) NOT NULL, Location varchar(512), Created datetime NOT NULL, AssignedUser CHAR(38), StatusId int(10) NOT NULL, InternetUserId int(10), CONSTRAINT Id PRIMARY KEY (Id));
CREATE TABLE Problem_Tag (TagId int(10) NOT NULL, ProblemId int(10) NOT NULL, PRIMARY KEY (TagId, ProblemId));
CREATE TABLE Status (Id int(10) NOT NULL AUTO_INCREMENT, Name varchar(50) NOT NULL, PRIMARY KEY (Id));
CREATE TABLE `Tag` (Id int(10) NOT NULL AUTO_INCREMENT, Name varchar(50) NOT NULL, PRIMARY KEY (Id));
CREATE TABLE TimeSpent (Id int(10) NOT NULL AUTO_INCREMENT, HoursSpent decimal(2, 0) DEFAULT 0 NOT NULL, Description varchar(512), DateRecorded timestamp DEFAULT NOW() NOT NULL, UserId CHAR(38) NOT NULL, ProblemId int(10) NOT NULL, PRIMARY KEY (Id));
ALTER TABLE Problem ADD CONSTRAINT FKProblem611514 FOREIGN KEY (AssignedUser) REFERENCES `Users` (Id);
ALTER TABLE Comment ADD CONSTRAINT FKComment537324 FOREIGN KEY (UserId) REFERENCES `Users` (Id);
ALTER TABLE Comment ADD CONSTRAINT FKComment818660 FOREIGN KEY (ProblemId) REFERENCES Problem (Id);
ALTER TABLE Problem_Tag ADD CONSTRAINT FKProblem_Ta147632 FOREIGN KEY (ProblemId) REFERENCES Problem (Id);
ALTER TABLE Problem_Tag ADD CONSTRAINT FKProblem_Ta741275 FOREIGN KEY (TagId) REFERENCES `Tag` (Id);
ALTER TABLE TimeSpent ADD CONSTRAINT FKTimeSpent911204 FOREIGN KEY (UserId) REFERENCES `Users` (Id);
ALTER TABLE TimeSpent ADD CONSTRAINT FKTimeSpent807458 FOREIGN KEY (ProblemId) REFERENCES Problem (Id);
ALTER TABLE Category_Problem ADD CONSTRAINT FKCategory_P899810 FOREIGN KEY (CategoryId) REFERENCES Category (Id);
ALTER TABLE Category_Problem ADD CONSTRAINT FKCategory_P650586 FOREIGN KEY (ProblemId) REFERENCES Problem (Id);
ALTER TABLE Problem ADD CONSTRAINT FKProblem836090 FOREIGN KEY (StatusId) REFERENCES Status (Id);
ALTER TABLE Problem ADD CONSTRAINT FKProblem951214 FOREIGN KEY (InternetUserId) REFERENCES InternetUser (Id);
ALTER TABLE InternetUserDevice ADD CONSTRAINT FKInternetUs985626 FOREIGN KEY (InternetUserId) REFERENCES InternetUser (Id);

alter table problem ADD FULLTEXT INDEX (Name,Description,Location);

ALTER TABLE `bakalaurinis`.`internetuser` 
ADD COLUMN `InternetPlan` VARCHAR(255) NULL DEFAULT NULL AFTER `IpAddress`,
ADD COLUMN `Created` DATE NULL DEFAULT NOW() AFTER `InternetPlan`;

ALTER TABLE `bakalaurinis`.`internetuser` 
CHANGE COLUMN `Description` `Description` NVARCHAR(512) NULL DEFAULT NULL ;

ALTER TABLE `bakalaurinis`.`timespent` 
CHANGE COLUMN `Description` `Description` NVARCHAR(512) NULL DEFAULT NULL ;

alter table internetuser ADD FULLTEXT INDEX (FirstName,LastName);

ALTER TABLE `bakalaurinis`.`users` 
CHANGE COLUMN `Address` `Address` NVARCHAR(256) NULL DEFAULT NULL ;

ALTER TABLE `bakalaurinis`.`internetuser` 
CHANGE COLUMN `FirstName` `FirstName` NVARCHAR(255) NOT NULL ,
CHANGE COLUMN `LastName` `LastName` NVARCHAR(255) NOT NULL ;

ALTER TABLE `bakalaurinis`.`problem` 
CHANGE COLUMN `Name` `Name` NVARCHAR(255) NOT NULL ,
CHANGE COLUMN `Description` `Description` NVARCHAR(9000) NOT NULL ,
CHANGE COLUMN `Location` `Location` NVARCHAR(512) NULL DEFAULT NULL ;

ALTER TABLE `bakalaurinis`.`category` 
CHANGE COLUMN `Name` `Name` NVARCHAR(255) NOT NULL ,
CHANGE COLUMN `Description` `Description` NVARCHAR(512) NULL DEFAULT NULL ;

CREATE TABLE `bakalaurinis`.`ping_results` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `internetUserId` INT NOT NULL,
  `ipAddress` VARCHAR(255) NOT NULL,
  `time` BIGINT NULL,
  `recorded` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  CONSTRAINT `fk_ping_internetuser`
    FOREIGN KEY (`internetUserId`)
    REFERENCES `bakalaurinis`.`internetuser` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
