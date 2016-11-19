Create table Role
(
  Id int IDENTITY(1,1) CONSTRAINT PK_Role PRIMARY KEY
 ,RoleName varchar(200) not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
)

Create table [User]
(
   Id int IDENTITY(1,1) CONSTRAINT PK_User PRIMARY KEY
  ,UserName varchar(200)
  ,Password varchar(Max)
  ,RoleId INT NOT NULL CONSTRAINT FK_Role_User FOREIGN KEY (RoleId) REFERENCES Role(Id)
  ,Name varchar(200) not null
  ,FatherName  varchar(200) not null
  ,CNIC  varchar(200) not null
  ,Gender  varchar(200) not null
  ,MaritalStatus  varchar(50) not null
  ,DateOfBirth datetime not null
  ,PlaceOfBirth  varchar(200)
  ,ContactNo  varchar(200) not null
  ,Landline  varchar(200)
  ,Nationality  varchar(200)
  ,Email  varchar(200)
  ,PermanentAddress  varchar(Max) not null
  ,CurrentAddress  varchar(Max)
  ,DateOfJoining  datetime not null
  ,ResignationDate datetime
  ,Status  varchar(50)
  ,Picture varchar(Max) not null
  ,Position varchar(200)
  ,IsTemporaryEmployee bit not null
  ,ScannedDocument varchar(Max)
  ,CNICFrontSide  varchar(Max)
  ,CNICBackSide  varchar(Max)
  ,AgreementDocument  varchar(Max)
  ,IsUser bit not null
  ,IsAdmin bit not null 
  ,InsertDate datetime not null
  ,UpdateDate datetime
  ,LastLogin  datetime
  ,FarmId INT NOT NULL CONSTRAINT FK_Farm_User FOREIGN KEY (FarmId) REFERENCES Farm(Id)
  ,Department varchar(200) not null
)

Create table Form
(
  Id int IDENTITY(1,1) CONSTRAINT PK_Form PRIMARY KEY
 ,Name varchar(200) not null
 ,Type varchar(200) not null
 ,ControllerName varchar(200) not null
 ,ActionName varchar(200) not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
)

Create table FormSetting
(
  Id int IDENTITY(1,1) CONSTRAINT PK_FormSetting PRIMARY KEY
 ,FormId INT NOT NULL CONSTRAINT FK_Form_FromSetting FOREIGN KEY (FormId) REFERENCES Form(Id)
 ,RoleId INT NOT NULL CONSTRAINT FK_From_Role FOREIGN KEY (RoleId) REFERENCES Role(Id)
 ,IsAdd  bit not null
 ,IsUpdate bit not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
)

Create table Farm
(
  Id int IDENTITY(1,1) CONSTRAINT PK_Farm PRIMARY KEY
 ,FarmName varchar(200) not null
 ,Description varchar(500)
 ,Address varchar(500) not null 
 ,TotalArea varchar(200) not null
 ,ActualPrice decimal(18,2) not null
 ,TaxAmount decimal(18,2) not null
 ,TotalValue decimal(18,2) not null
 ,PaperValue decimal(18,2) not null 
 ,Rent decimal(18,2) not null
 ,Owner  varchar(200) not null 
 ,Status varchar(50) not null  
 ,PurchasedDate datetime not null
 ,StartDate datetime not null
 ,EndDate datetime not null 
 ,InsertDate datetime not null
 ,UpdateDate datetime
)

Create table FarmArea
(
  Id int IDENTITY(1,1) CONSTRAINT PK_FarmArea PRIMARY KEY
  ,FarmId INT NOT NULL CONSTRAINT FK_Farm_FarmArea FOREIGN KEY (FarmId) REFERENCES Farm(Id)
  ,AreaName  varchar(200) not null
  ,AreaInAcars  varchar(200) not null
  ,StartingPoint  varchar(200) not null
  ,EndingPoint  varchar(200) not null
  ,Date datetime not null 
  ,InsertDate datetime not null
  ,UpdateDate datetime
)

Create table VendorManagement
(
  Id int IDENTITY(1,1) CONSTRAINT PK_VendorManagement PRIMARY KEY
  ,Name  varchar(200) not null
  ,CompanyName  varchar(200) not null
  ,PhoneNo  varchar(200) not null
  ,PersonalPhoneNo  varchar(200) not null
  ,AddressInfo  varchar(200) not null
  ,OpeningBalance decimal(18,2) not null
  ,OtherDescription  varchar(500)
  ,Type  varchar(200) not null
  ,Date datetime not null
  ,InsertDate datetime not null
  ,UpdateDate datetime
)

Create table CropManagement
(
   Id int IDENTITY(1,1) CONSTRAINT PK_CropManagement PRIMARY KEY  
  ,FarmId INT NOT NULL CONSTRAINT FK_Farm_CropManagement FOREIGN KEY (FarmId) REFERENCES Farm(Id)
  ,AreaId INT NOT NULL CONSTRAINT FK_FarmArea_CropManagement FOREIGN KEY (AreaId) REFERENCES FarmArea(Id)
  ,CropName  varchar(200) not null
  ,CropDuration  varchar(200) not null
  ,CropStartDate datetime not null
  ,CropEndDate datetime not null
  ,InsertDate datetime not null
  ,UpdateDate datetime
)

Create table Account
(
  Id int IDENTITY(1,1) CONSTRAINT PK_Account PRIMARY KEY
 ,Name varchar(200) not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
)

Create table AttendanceManagement
(
  Id int IDENTITY(1,1) CONSTRAINT PK_AttendanceMgmt PRIMARY KEY
 ,Year int not null
 ,Month  int not null
 ,InsertDate datetime not null 
)
Create table AttendanceManagementDetail
(
  Id int IDENTITY(1,1) CONSTRAINT PK_AttendanceMgmtDetail PRIMARY KEY
 ,AttendanceManagementId INT NOT NULL CONSTRAINT FK_AttendanceMgmt_AttendanceMgmtDetail FOREIGN KEY (AttendanceManagementId) REFERENCES AttendanceManagement(Id)
 ,Date datetime not null
 ,IsPresentMorning bit
 ,IsAbsentMorning  bit
 ,IsLeaveMorning  bit
 ,IsPresentNight bit
 ,IsAbsentNight  bit
 ,IsLeaveNight   bit
 ,UserId INT NOT NULL CONSTRAINT FK_AttendanceMgmtDetail_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,InsertDate datetime not null
)

Create table TaskAssignment
(
 Id int IDENTITY(1,1) CONSTRAINT PK_TaskAssignment PRIMARY KEY
,TaskName varchar(Max) not null
,StaffNameId INT NOT NULL CONSTRAINT FK_User_TaskAssignment FOREIGN KEY (StaffNameId) REFERENCES [User](Id)
,Description varchar(Max)
,DeadlineStartDate datetime not null
,DeadlineEndDate datetime not null
,Status varchar(50)
,Remarks varchar(50)
,InsertDate datetime not null
,UpdateDate datetime
)

Create table Seed 
(
Id int IDENTITY(1,1) CONSTRAINT PK_Seed PRIMARY KEY
,FarmId INT NOT NULL CONSTRAINT FK_Farm_Seed FOREIGN KEY (FarmId) REFERENCES Farm(Id)
,Quality varchar(50) not null
,Quantity  varchar(50) not null
,QuantityInNumber int not null
,VendorId INT NOT NULL CONSTRAINT FK_Vendor_Seed FOREIGN KEY (VendorId) REFERENCES VendorManagement(Id)
,TotalPricePer decimal(18,2) not null
,PricePerItem decimal(18,2) not null
,DatePurchase datetime not null
,CropId INT NOT NULL CONSTRAINT FK_CropManagement_SeedExpense FOREIGN KEY (CropId) REFERENCES CropManagement(Id)
,PaymentBill varchar(500) not null
,OtherDescription varchar(500)
,InsertDate datetime not null
,UpdateDate datetime
,AccountId INT NOT NULL CONSTRAINT FK_Account_Seed FOREIGN KEY (AccountId) REFERENCES Account(Id)
,UserId INT NOT NULL CONSTRAINT FK_Seed_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

Create table SeedExpense
(
  Id INT IDENTITY(1,1) CONSTRAINT PK_SeedExpense PRIMARY KEY
 ,FarmId INT NOT NULL CONSTRAINT FK_Farm_SeedExpense FOREIGN KEY (FarmId) REFERENCES Farm(Id)
 ,AreaId INT NOT NULL CONSTRAINT FK_FarmArea_SeedExpense FOREIGN KEY (AreaId) REFERENCES FarmArea(Id) 
 ,SeedId INT NOT NULL CONSTRAINT FK_Seed_SeedExpense FOREIGN KEY (SeedId) REFERENCES [Seed](Id)
 ,QuantityInNumber int not null
 ,Date  datetime not null
 ,Description varchar(500)
 ,AccountId INT NOT NULL CONSTRAINT FK_Account_SeedExpense FOREIGN KEY (AccountId) REFERENCES Account(Id)
 ,UserId INT NOT NULL CONSTRAINT FK_SeedExpense_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

Create table Fertilizer 
(
Id int IDENTITY(1,1) CONSTRAINT PK_Fertilizer PRIMARY KEY
,FarmId INT NOT NULL CONSTRAINT FK_Farm_Fertilizer FOREIGN KEY (FarmId) REFERENCES Farm(Id)
,Brand varchar(200)  not null
,FertilizerName varchar(200) not null
,Type  varchar(50) not null
,Quality  varchar(50) not null
,Quantity  varchar(50) not null
,QuantityInNumber int not null
,VendorId INT NOT NULL CONSTRAINT FK_Vendor_Fertilizer FOREIGN KEY (VendorId) REFERENCES VendorManagement(Id)
,Price decimal (18,2)  not null
,PricePerItem decimal(18,2) not null
,DatePurchase  datetime not null
,PaymentBill varchar(500) not null
,OtherDescription  varchar(500)
,InsertDate datetime not null
,UpdateDate datetime
,AccountId INT NOT NULL CONSTRAINT FK_Account_Fertilizer FOREIGN KEY (AccountId) REFERENCES Account(Id)
,UserId INT NOT NULL CONSTRAINT FK_Fertilizer_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

Create table FertilizerExpense
(
  Id INT IDENTITY(1,1) CONSTRAINT PK_FertilizerExpense PRIMARY KEY     
 ,FarmId INT NOT NULL CONSTRAINT FK_Farm_FertilizerExpense FOREIGN KEY (FarmId) REFERENCES Farm(Id)
 ,AreaId INT NOT NULL CONSTRAINT FK_FarmArea_FertilizerExpense FOREIGN KEY (AreaId) REFERENCES FarmArea(Id)
 ,CropId INT NOT NULL CONSTRAINT FK_CropManagement_FertilizerExpense FOREIGN KEY (CropId) REFERENCES CropManagement(Id)
 ,FertilizerId  INT NOT NULL CONSTRAINT FK_Fertilizer_FertilizerExpense FOREIGN KEY (FertilizerId) REFERENCES [Fertilizer](Id) 
 ,QuantityInNumber int not null
 ,Date  datetime not null
 ,Description varchar(500)
 ,AccountId INT NOT NULL CONSTRAINT FK_Account_FertilizerExpense FOREIGN KEY (AccountId) REFERENCES Account(Id)
 ,UserId INT NOT NULL CONSTRAINT FK_FertilizerExpense_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)


Create table Fuel 
(
Id int IDENTITY(1,1) CONSTRAINT PK_Fuel PRIMARY KEY
,FarmId INT NOT NULL CONSTRAINT FK_Farm_Fuel FOREIGN KEY (FarmId) REFERENCES Farm(Id)
,FuelName varchar(50) not null
,DatePurchase datetime not null
,QuantityInLiter decimal (18,2) not null
,TotalPrice decimal (18,2) not null
,PricePerLiter  decimal (18,2) not null
,PaymentBill varchar(500) not null
,VendorId INT NOT NULL CONSTRAINT FK_Vendor_Fuel FOREIGN KEY (VendorId) REFERENCES VendorManagement(Id)
,OtherDescription varchar(500)
,InsertDate datetime not null
,UpdateDate datetime
,AccountId INT NOT NULL CONSTRAINT FK_Account_Fuel FOREIGN KEY (AccountId) REFERENCES Account(Id)
,UserId INT NOT NULL CONSTRAINT FK_Fuel_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

Create table FuelExpense
(
  Id INT IDENTITY(1,1) CONSTRAINT PK_FuelExpense PRIMARY KEY
 ,FarmId INT NOT NULL CONSTRAINT FK_Farm_FuelExpense FOREIGN KEY (FarmId) REFERENCES Farm(Id)
 ,AreaId INT NOT NULL CONSTRAINT FK_FarmArea_FuelExpense FOREIGN KEY (AreaId) REFERENCES FarmArea(Id)
 ,CropId INT NOT NULL CONSTRAINT FK_CropManagement_FuelExpense FOREIGN KEY (CropId) REFERENCES CropManagement(Id)
 ,FuelId  INT NOT NULL CONSTRAINT FK_Fuel_FuelExpense FOREIGN KEY (FuelId) REFERENCES [Fuel](Id)   
 ,PersonName varchar(200)  not null
 ,Quantity decimal(18,2) not null
 ,Date datetime not null 
 ,Description varchar(500)
 ,AccountId INT NOT NULL CONSTRAINT FK_Account_FuelExpense FOREIGN KEY (AccountId) REFERENCES Account(Id)
 ,UserId INT NOT NULL CONSTRAINT FK_FuelExpense_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

Create table Animal  
(
Id int IDENTITY(1,1) CONSTRAINT PK_Animal PRIMARY KEY
,FarmId INT NOT NULL CONSTRAINT FK_Farm_Animal FOREIGN KEY (FarmId) REFERENCES Farm(Id)
,AnimalName varchar(200) not null
,Category varchar(50) not null
,Price decimal (18,2) not null
,PurchaseDate datetime not null
,Age int  not null
,Color  varchar(200) not null
,Description varchar(500)
,Photo varchar(500) not null
,VendorId INT NOT NULL CONSTRAINT FK_Vendor_Animal FOREIGN KEY (VendorId) REFERENCES VendorManagement(Id)
,FamilyName  varchar(200) not null
,Sex varchar(50)
,InsertDate datetime not null
,UpdateDate datetime
,AccountId INT NOT NULL CONSTRAINT FK_Account_Animal FOREIGN KEY (AccountId) REFERENCES Account(Id)
,UserId INT NOT NULL CONSTRAINT FK_Animal_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

Create table Vehicle
(
Id INT IDENTITY(1,1) CONSTRAINT PK_Vehicle PRIMARY KEY
,FarmId INT NOT NULL CONSTRAINT FK_Farm_Vehicle FOREIGN KEY (FarmId) REFERENCES Farm(Id)
,Name varchar(200) not null
,Color  varchar(200) not null
,Type  varchar(50)  not null
,Power varchar(200) not null
,RegistrationNo  varchar(200) not null
,Picture  varchar(500) not null
,LicenseFile  varchar(500)  not null
,CompanyName  varchar(200)
,Price decimal(18,2)  not null
,ModelNo  varchar(200)  not null
,PurchaseDate datetime not null
,VendorId INT NOT NULL CONSTRAINT FK_Vendor_Vehicle FOREIGN KEY (VendorId) REFERENCES VendorManagement(Id)
,OtherDescription  varchar(500)
,Condition  varchar(50) not null
,InsertDate datetime not null
,UpdateDate datetime
,AccountId INT NOT NULL CONSTRAINT FK_Account_Vehicle FOREIGN KEY (AccountId) REFERENCES Account(Id)
,UserId INT NOT NULL CONSTRAINT FK_Vehicle_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

Create table Machinery 
(
Id INT IDENTITY(1,1) CONSTRAINT PK_Machinery PRIMARY KEY
,FarmId INT NOT NULL CONSTRAINT FK_Farm_Machinery FOREIGN KEY (FarmId) REFERENCES Farm(Id)
,Name  varchar(200)  not null
,Color  varchar(200)  not null
,Type varchar(200)  not null
,RegistrationNo  varchar(200)  not null
,ModelNo varchar(200)  not null
,CompanyName varchar(200)
,Price decimal(18,2)  not null
,PurchaseDate  datetime not null
,OtherDescription varchar(500)
,Condition  varchar(50)  not null
,VendorId INT NOT NULL CONSTRAINT FK_Vendor_Machinery FOREIGN KEY (VendorId) REFERENCES VendorManagement(Id)
,Picture  varchar(500) not null
,LicenseFile  varchar(500)  not null
,InsertDate datetime not null
,UpdateDate datetime
,AccountId INT NOT NULL CONSTRAINT FK_Account_Machinery FOREIGN KEY (AccountId) REFERENCES Account(Id)
,UserId INT NOT NULL CONSTRAINT FK_Machinery_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

Create table OtherItem
(
  ItemId INT IDENTITY(1,1) CONSTRAINT PK_OtherItem PRIMARY KEY    
 ,FarmId INT NOT NULL CONSTRAINT FK_Farm_OtherItem FOREIGN KEY (FarmId) REFERENCES Farm(Id)
 ,TitleExpense  varchar(200)  not null
 ,Description  varchar(200)
 ,Price decimal(18,2)  not null 
 ,DatePurchase  datetime not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
 ,AccountId INT NOT NULL CONSTRAINT FK_Account_OtherItem FOREIGN KEY (AccountId) REFERENCES Account(Id)
 ,UserId INT NOT NULL CONSTRAINT FK_OtherItem_UserId FOREIGN KEY (UserId) REFERENCES [User](Id)
)

Create table OtherExpense
(
  Id INT IDENTITY(1,1) CONSTRAINT PK_OtherExpense PRIMARY KEY     
 ,FarmId INT NOT NULL CONSTRAINT FK_Farm_OtherExpense FOREIGN KEY (FarmId) REFERENCES Farm(Id)
 ,OtherItemId INT NOT NULL CONSTRAINT FK_OtherItem_OtherItemExpense FOREIGN KEY (OtherItemId) REFERENCES [OtherItem](ItemId) 
 ,Amount decimal(18,2) not null
 ,Description varchar(500) 
 ,Date  datetime not null
 ,AccountId INT NOT NULL CONSTRAINT FK_Account_OtherExpense FOREIGN KEY (AccountId) REFERENCES Account(Id)
 ,UserId INT NOT NULL CONSTRAINT FK_OtherExpense_UserId FOREIGN KEY (UserId) REFERENCES [User](Id) 
 )
 
 
 Create table PermanentEmployeeSalary
(
 Id INT IDENTITY(1,1) CONSTRAINT PK_PermanentEmployeeSalary PRIMARY KEY    
 ,Year int not null
 ,Month int not null
 ,UserId INT NOT NULL CONSTRAINT FK_PermanentEmployeeSalary_User FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,IsAllowFund bit not null
 ,BasicSalary decimal(18,2) not null
 ,Bonus decimal(18,2) not null
 ,MedicalAllowance decimal(18,2) not null
 ,TA_DA decimal(18,2) not null
 ,HouseRent decimal(18,2) not null
 ,UtilityAllowance decimal(18,2) not null
 ,ProvidentFund decimal(18,2) not null
 ,Penality decimal(18,2)
 ,Date datetime not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
)

Create table TemporaryEmployeeSalary
(
 Id INT IDENTITY(1,1) CONSTRAINT PK_TemporaryEmployeeSalary PRIMARY KEY    
 ,Year int not null
 ,Month int not null
 ,MorningWages decimal(18,2)
 ,NightWages decimal(18,2)
 ,InsertDate datetime not null
 ,UpdateDate datetime
)

Create table LoanManagement 
(
  Id INT IDENTITY(1,1) CONSTRAINT PK_LoanManagement PRIMARY KEY
 ,UserId INT NOT NULL CONSTRAINT FK_LoanManagement_User FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,LoanAmount decimal(18,2) not null
 ,NoOfInstallment int not null
 ,PerMonthOrYear bit not null
 ,LoanStartDate datetime not null
 ,LoanEndDate datetime not null 
 ,Description varchar(500)
 ,IntersetRate decimal(18,2) not null
 ,Status varchar(200) not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
 
)

Create table PersonalAccount 
(
  Id INT IDENTITY(1,1) CONSTRAINT PK_PersonalAccount PRIMARY KEY
 ,UserId INT NOT NULL CONSTRAINT FK_PersonalAccount_User FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,OpeningBalance decimal(18,2) not null
 ,CreatedDate datetime not null
 ,ClosingDate datetime
 ,InsertDate datetime not null
 ,UpdateDate datetime
 
)

Create table PaidLoan 
(
  Id INT IDENTITY(1,1) CONSTRAINT PK_PaidLoan PRIMARY KEY     
 ,LoanId INT NOT NULL CONSTRAINT FK_LoanManagement_PaidLoan FOREIGN KEY (LoanId) REFERENCES [LoanManagement](Id)
 ,UserId INT NOT NULL CONSTRAINT FK_PaidLoan_User FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,AccountId INT NOT NULL CONSTRAINT FK_Account_PaidLoan FOREIGN KEY (AccountId) REFERENCES Account(Id)
 ,Date datetime not null
 ,Year int not null
 ,Month  int not null
 ,Amount decimal(18,2) not null  
)

Create table TransectionPersonalAccount 
(
  Id INT IDENTITY(1,1) CONSTRAINT PK_TransectionPersonalAccount PRIMARY KEY
 ,UserId INT NOT NULL CONSTRAINT FK_TransectionPersonalAccount_User FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,Balance decimal(18,2) not null 
 ,Credit decimal(18,2)
 ,Debit decimal(18,2)
 ,Date datetime not null 
 ,IsAddedByAdmin bit not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
 )
 
 Create table TransectionAccount 
(
  Id INT IDENTITY(1,1) CONSTRAINT PK_TransectionAccount PRIMARY KEY
 ,PayByUserId INT NOT NULL CONSTRAINT FK_TransectionAccount_User_PayBy FOREIGN KEY (PayByUserId) REFERENCES [User](Id)
 ,UserId INT NOT NULL CONSTRAINT FK_TransectionAccount_User FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,Balance decimal(18,2) not null 
 ,Date datetime not null
 ,InsertDate datetime not null
 )

 Create table LoanReceivableFromEmployee
(
  Id int IDENTITY(1,1) CONSTRAINT PK_EmployeeLoan PRIMARY KEY
 ,LoanId INT NOT NULL CONSTRAINT FK_LoanManagement_EmployeeLoan FOREIGN KEY (LoanId) REFERENCES [LoanManagement](Id)
 ,AmountReceive decimal(18,2) not null
 ,AccountId INT NOT NULL CONSTRAINT FK_Account_EmployeeLoan FOREIGN KEY (AccountId) REFERENCES Account(Id)
 ,UserId INT NOT NULL CONSTRAINT FK_User_EmployeeLoan FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,Date datetime not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
)

Create table LoanReceivableFromBank
(
  Id int IDENTITY(1,1) CONSTRAINT PK_BankLoan PRIMARY KEY
 ,LoanId INT NOT NULL CONSTRAINT FK_LoanManagement_BankLoan FOREIGN KEY (LoanId) REFERENCES [LoanManagement](Id)
 ,Amount decimal(18,2) not null
 ,NoOfInstallment decimal(18,2) not null
 ,InterestRate decimal(18,2) not null
 ,TotalAmountTobePaid decimal(18,2) not null
 ,AccountId INT NOT NULL CONSTRAINT FK_Account_BankLoan FOREIGN KEY (AccountId) REFERENCES Account(Id)
 ,UserId INT NOT NULL CONSTRAINT FK_User_BankLoan FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,LoanReceiveDate datetime not null
 ,LoanEndDate datetime not null
 ,Description varchar(500)
 ,InsertDate datetime not null
 ,UpdateDate datetime
)

Create table ReturnLoan
(
  Id int IDENTITY(1,1) CONSTRAINT PK_ReturnLoan PRIMARY KEY
 ,LoanId INT NOT NULL CONSTRAINT FK_LoanManagement_ReturnLoan FOREIGN KEY (LoanId) REFERENCES [LoanManagement](Id)
 ,InstallmentAmount decimal(18,2) not null
 ,AccountId INT NOT NULL CONSTRAINT FK_Account_ReturnLoan FOREIGN KEY (AccountId) REFERENCES Account(Id)
 ,UserId INT NOT NULL CONSTRAINT FK_User_ReturnLoan FOREIGN KEY (UserId) REFERENCES [User](Id)
 ,PaidDate datetime not null
 ,InsertDate datetime not null
 ,UpdateDate datetime
)



