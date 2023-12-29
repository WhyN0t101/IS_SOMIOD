CREATE TABLE [dbo].[Application]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Creation_dt] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Container] (
    [Id]          INT IDENTITY(1, 1)	NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [Creation_dt] DATETIME     NOT NULL,
    [Parent]      INT          NOT NULL,
	FOREIGN KEY (Parent) REFERENCES Application(Id),
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Data] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Content]     VARCHAR (150) NOT NULL,
    [Creation_dt] DATETIME     NOT NULL,
    [Parent]      INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY (Parent) REFERENCES Container(Id),
);

CREATE TABLE [dbo].[Subscription] (
    [Id]          INT          NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [Creation_dt] DATETIME     NOT NULL,
    [Parent]      INT          NOT NULL,
    [Event]       VARCHAR (50) NOT NULL,
    [Endpoint]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY (Parent)	 REFERENCES Container(Id)
);




