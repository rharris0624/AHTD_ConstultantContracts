CREATE TABLE [dbo].[Consultant] (
    [ConsultantId]                INT            IDENTITY (1, 1) NOT NULL,
    [TaxId]                       NCHAR (10)     NULL,
    [SeqNo]                       INT            NULL,
    [Name]                        NVARCHAR (50)  NOT NULL,
    [PrimaryContactFirstName]     NVARCHAR (50)  NOT NULL,
    [PrimaryContactLastName]      NVARCHAR (50)  NOT NULL,
    [SecondaryContactFirstName]   NVARCHAR (50)  NULL,
    [SecondaryContactLastName]    NVARCHAR (50)  NULL,
    [PhysicalAddress]             NVARCHAR (50)  NOT NULL,
    [City]                        NVARCHAR (50)  NOT NULL,
    [State]                       NVARCHAR (50)  NOT NULL,
    [CountryCode]                 NVARCHAR (50)  NOT NULL,
    [PostalCode]                  NVARCHAR (50)  NOT NULL,
    [EmailAddress]                NVARCHAR (200) NULL,
    [HomeOfficeOverheadRateMax]   DECIMAL (7, 4) NULL,
    [FCCM]                        DECIMAL (7, 4) NULL,
    [FieldServiceOverheadRateMax] DECIMAL (7, 4) NULL,
    [Multiplier]                  DECIMAL (4, 2) NULL,
    [CityOrCounty]                BIT            CONSTRAINT [DF_Consultant_CityOrCounty] DEFAULT ((0)) NOT NULL,
    [Inactive]                    BIT            CONSTRAINT [DF_Consultant_Inactive] DEFAULT ((0)) NOT NULL,
    [LastUpdateDate]              DATETIME2 (7)  NOT NULL,
    [LastUpdateUser]              NCHAR (10)     NOT NULL,
    [BusinessPhoneNumber] NCHAR(10) NULL, 
    [ContactPhoneNumber] NCHAR(10) NULL, 
    CONSTRAINT [PK_Consultant] PRIMARY KEY CLUSTERED ([ConsultantId] ASC)
);



