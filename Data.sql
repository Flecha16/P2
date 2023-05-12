
USE SportsAnalytics;

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

INSERT INTO `Teams` (`Id`,`Name`,`LeagueId`,`Stadium`,`City`,`Country`) VALUES (1,'FC Barcelona',1,'Spotify Camp Nou','Barcelona','Spain');
INSERT INTO `Teams` (`Id`,`Name`,`LeagueId`,`Stadium`,`City`,`Country`) VALUES (3,'Juventus',4,'Juventus Stadium','Turín','Italy');
INSERT INTO `Teams` (`Id`,`Name`,`LeagueId`,`Stadium`,`City`,`Country`) VALUES (4,'Liga De Quito',2,'Rodrigo Paz Delgado','Quito','Ecuador');
INSERT INTO `Teams` (`Id`,`Name`,`LeagueId`,`Stadium`,`City`,`Country`) VALUES (5,'Mushuc Runa',6,'Echaleche','Ambato','Ecuador');

INSERT INTO `Players` (`Id`,`FirstName`,`LastName`,`DateOfBirth`,`Nationality`,`Position`,`Valoration`,`LeagueId`,`TeamId`) VALUES (1,'Juan','Calvopiña','2002-03-19 00:00:00.000000',0,0,99,1,1);
INSERT INTO `Players` (`Id`,`FirstName`,`LastName`,`DateOfBirth`,`Nationality`,`Position`,`Valoration`,`LeagueId`,`TeamId`) VALUES (2,'Sebastián','Villacrés','2002-05-11 00:00:00.000000',1,3,90,4,3);
INSERT INTO `Players` (`Id`,`FirstName`,`LastName`,`DateOfBirth`,`Nationality`,`Position`,`Valoration`,`LeagueId`,`TeamId`) VALUES (10,'Lionel','Messi','1987-06-24 00:00:00.000000',0,2,99,1,1);

