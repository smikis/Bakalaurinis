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
INSERT INTO dbo.UsersRoles (UserId, RoleId) VALUES ('810BAD28-CED4-427A-9A88-5A17682304DA', 'D2A35AEC-402A-4BE6-9D7F-682C0B5D3FEF');