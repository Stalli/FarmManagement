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

	
insert into Form
(Name, Type, ControllerName, ActionName, InsertDate)
values
('Animal Expense','Expense','Expense','AnimalExpense', getdate()),
('Vehicle Expense','Expense','Expense','VehicleExpense', getdate()),
('Machinery Expense','Expense','Expense','MachineryExpense', getdate()),
('Pesticides Expense','Expense','Expense','PesticidesExpense', getdate()),
('General Expense','Expense','Expense','GeneralExpense', getdate()),
('Feed Stock in','Stock','Stock','FeedIn', getdate()),
('Pesticides Stock in','Stock','Stock','PesticidesIn', getdate())

insert into FormSetting
(FormId, RoleId, IsAdd, IsUpdate, InsertDate)
values
((select top 1 Id from Form where Name = 'Animal Expense'), 1, 1, 1, getdate()),
((select top 1 Id from Form where Name = 'Vehicle Expense'), 1, 1, 1, getdate()),
((select top 1 Id from Form where Name = 'Machinery Expense'), 1, 1, 1, getdate()),
((select top 1 Id from Form where Name = 'Pesticides Expense'), 1, 1, 1, getdate()),
((select top 1 Id from Form where Name = 'General Expense'), 1, 1, 1, getdate()),
((select top 1 Id from Form where Name = 'Feed Stock in'), 1, 1, 1, getdate()),
((select top 1 Id from Form where Name = 'Pesticides Stock in'), 1, 1, 1, getdate())