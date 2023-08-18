CREATE DATABASE `SportsAnalytics` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE SportsAnalytics;

CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `NormalizedName_UNIQUE` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FirstName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) DEFAULT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `AspNetUserClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Leagues` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Country` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Players` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DateOfBirth` datetime(6) NOT NULL,
  `Nationality` int NOT NULL,
  `Position` int NOT NULL,
  `Valoration` int NOT NULL,
  `LeagueId` int NOT NULL,
  `TeamId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Players_LeagueId` (`LeagueId`),
  KEY `IX_Players_TeamId` (`TeamId`),
  CONSTRAINT `FK_Players_Leagues_LeagueId` FOREIGN KEY (`LeagueId`) REFERENCES `Leagues` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Players_Teams_TeamId` FOREIGN KEY (`TeamId`) REFERENCES `Teams` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `AspNetRoles` (`Id`,`Name`,`NormalizedName`,`ConcurrencyStamp`) VALUES ('1ed038f4-b073-4606-845f-559e58e48e0a','Manager','MANAGER',NULL);
INSERT INTO `AspNetRoles` (`Id`,`Name`,`NormalizedName`,`ConcurrencyStamp`) VALUES ('66c7975a-375b-409c-bbd3-5da0ac1f70bd','Admin','ADMIN','5d93577d-5e02-459f-ac90-c777320d48f5');
INSERT INTO `AspNetRoles` (`Id`,`Name`,`NormalizedName`,`ConcurrencyStamp`) VALUES ('eb221acc-9d11-4d3b-bb90-4716f2738124','User','USER','d434c2e8-74ee-41a0-95cd-227f8eb30864');

INSERT INTO `AspNetUsers` (`Id`,`FirstName`,`LastName`,`UserName`,`NormalizedUserName`,`Email`,`NormalizedEmail`,`EmailConfirmed`,`PasswordHash`,`SecurityStamp`,`ConcurrencyStamp`,`PhoneNumber`,`PhoneNumberConfirmed`,`TwoFactorEnabled`,`LockoutEnd`,`LockoutEnabled`,`AccessFailedCount`) VALUES ('601f6d3f-6219-4086-91a0-bd3a08a397cb','Juan','Calvopiña','juan.calvopina@udla.edu.ec','JUAN.CALVOPINA@UDLA.EDU.EC','juan.calvopina@udla.edu.ec','JUAN.CALVOPINA@UDLA.EDU.EC',1,'AQAAAAIAAYagAAAAEC3LoMiEO0xiFWdX0j239GXMJlRjlyw6Spbs2vowvXysfpsFEOtE6rxXBwWgMVFjDA==','3AKL5ET37BRB5IT7NX75WFGVVV6LH27U','5805c45f-30a4-4645-b0c8-769b7ab66e7a','0987612857',0,0,NULL,1,0);
INSERT INTO `AspNetUsers` (`Id`,`FirstName`,`LastName`,`UserName`,`NormalizedUserName`,`Email`,`NormalizedEmail`,`EmailConfirmed`,`PasswordHash`,`SecurityStamp`,`ConcurrencyStamp`,`PhoneNumber`,`PhoneNumberConfirmed`,`TwoFactorEnabled`,`LockoutEnd`,`LockoutEnabled`,`AccessFailedCount`) VALUES ('d6ac55cd-27fd-484a-bcc4-73b9fd381213','Sebastián','Vaca','juanflecha16@outlook.com','JUANFLECHA16@OUTLOOK.COM','juanflecha16@outlook.com','JUANFLECHA16@OUTLOOK.COM',0,'AQAAAAIAAYagAAAAEPbxGBzjiKurTEL1p7GW5JZ8H8wYOQJMLRtAWa0V3ceh1SoP1Ww0cj58og6kVWcAcw==','UNIMHD3Y6J56CUVRTXSQXTCNDTYZJU72','438864a0-f777-42c7-8ef7-337de4836380',NULL,0,0,NULL,1,0);
INSERT INTO `AspNetUsers` (`Id`,`FirstName`,`LastName`,`UserName`,`NormalizedUserName`,`Email`,`NormalizedEmail`,`EmailConfirmed`,`PasswordHash`,`SecurityStamp`,`ConcurrencyStamp`,`PhoneNumber`,`PhoneNumberConfirmed`,`TwoFactorEnabled`,`LockoutEnd`,`LockoutEnabled`,`AccessFailedCount`) VALUES ('e1823c36-bede-49a7-b287-3248b552107e','Sebastián','Calvopiña','juanflecha16@icloud.com','JUANFLECHA16@ICLOUD.COM','juanflecha16@icloud.com','JUANFLECHA16@ICLOUD.COM',0,'AQAAAAIAAYagAAAAEGZB+Z01UozcHF/82WjvetWZVakv/mFzzyIbmIFa/IGMqBpGYCkYh1zjlnCO5xofOw==','24IBE5VY6GMJC4EGJWET2P35OCV3TZCJ','e2775905-3fd8-4269-809e-8401298aaa58',NULL,0,0,NULL,1,0);

INSERT INTO `AspNetUserRoles` (`UserId`,`RoleId`) VALUES ('d6ac55cd-27fd-484a-bcc4-73b9fd381213','1ed038f4-b073-4606-845f-559e58e48e0a');
INSERT INTO `AspNetUserRoles` (`UserId`,`RoleId`) VALUES ('601f6d3f-6219-4086-91a0-bd3a08a397cb','66c7975a-375b-409c-bbd3-5da0ac1f70bd');
INSERT INTO `AspNetUserRoles` (`UserId`,`RoleId`) VALUES ('e1823c36-bede-49a7-b287-3248b552107e','eb221acc-9d11-4d3b-bb90-4716f2738124');

INSERT INTO `AspNetUserTokens` (`UserId`,`LoginProvider`,`Name`,`Value`) VALUES ('601f6d3f-6219-4086-91a0-bd3a08a397cb','[AspNetUserStore]','AuthenticatorKey','K6XPA4ZHGQ65XLWH27BF2W3SXOVENCEJ');

INSERT INTO `Leagues` (`Name`,`Country`) VALUES ('La Liga','Spain');
INSERT INTO `Leagues` (`Name`,`Country`) VALUES ('Premier League','England');
INSERT INTO `Leagues` (`Name`,`Country`) VALUES ('Ligue 1','France');
INSERT INTO `Leagues` (`Name`,`Country`) VALUES ('Serie A','Italy');
INSERT INTO `Leagues` (`Name`,`Country`) VALUES ('Bundesliga','Germany');

