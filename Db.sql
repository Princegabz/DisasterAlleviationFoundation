/* Tables */
CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NULL, 
    [Surname] VARCHAR(50) NULL, 
    [Username] VARCHAR(50) NULL, 
    [Password] VARCHAR(50) NULL
)

CREATE TABLE [dbo].[Donations] (
    [DonationId]   INT           IDENTITY (100, 1) NOT NULL,
    [UserId]       INT           NULL,
    [Date]         VARCHAR (50)  NULL,
    [NoOfItems]    NUMERIC (18)  NULL,
    [Category]     VARCHAR (50)  NULL,
    [Description]  VARCHAR (100) NULL,
    [DonationType] VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([DonationId] ASC)
);


CREATE TABLE [dbo].[Disasters] (
    [DisasterId]  INT          IDENTITY (1, 1) NOT NULL,
    [StartDate]   VARCHAR (50) NULL,
    [EndDate]     VARCHAR (50) NULL,
    [Location]    VARCHAR (50) NULL,
    [Description] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([DisasterId] ASC)
	
);


