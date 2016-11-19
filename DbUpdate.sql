GO
ALTER TABLE [dbo].[User]
    ADD [Company] VARCHAR (MAX) NULL;


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'“ранзакции обновлени€ базы данных успешно завершены.'
COMMIT TRANSACTION
END
ELSE PRINT N'—бой транзакций обновлени€ базы данных.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'ќбновление завершено.';



GO

IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'Выполняется изменение [dbo].[User]...';


GO
ALTER TABLE [dbo].[User]
    ADD [CompanyPhoneNumber] VARCHAR (200) NULL,
        [Type]               VARCHAR (50)  NULL,
        [Security]           VARCHAR (50)  NULL,
        [VendorName]         VARCHAR (200) NULL,
        [AccountId]          INT           NULL;


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Выполняется создание [dbo].[FK_Account_User]...';


GO
ALTER TABLE [dbo].[User] WITH NOCHECK
    ADD CONSTRAINT [FK_Account_User] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[PersonalAccount] ([Id]);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
ALTER TABLE [dbo].[User]
    ADD [RelationType] INT NULL;

GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END
