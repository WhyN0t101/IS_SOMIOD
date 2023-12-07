CREATE TABLE [dbo].[Application]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [name] NVARCHAR(50) NOT NULL, 
    [creation_dt] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Container] (
    [Id]          INT IDENTITY(1, 1)	NOT NULL,
    [name]        VARCHAR (50) NOT NULL,
    [creation_dt] DATETIME     NOT NULL,
    [parent]      INT          NOT NULL,
	FOREIGN KEY (parent) REFERENCES Application(Id),
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Data] (
    [Id]          INT          NOT NULL,
    [content]     VARCHAR (150) NOT NULL,
    [creation_dt] DATETIME     NOT NULL,
    [parent]      INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY (parent) REFERENCES Container(Id),
);

CREATE TABLE [dbo].[Subscription] (
    [Id]          INT          NOT NULL,
    [name]        VARCHAR (50) NOT NULL,
    [creation_dt] DATETIME     NOT NULL,
    [parent]      INT          NOT NULL,
    [event]       VARCHAR (50) NOT NULL,
    [endpoint]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY (parent)	 REFERENCES Container(Id)
);




