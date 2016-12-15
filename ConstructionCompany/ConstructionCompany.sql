/*-- Данный скрипт создает базу данных ConstructionCompany с пятью таблицами и генерирует тестовые записи:
-- 1. Виды работ (Jobs) - 1000 записей
-- 2. Материалы (Materials)- 100 записей
-- 3. Заказчики (Customers) -100 записей
-- 4. Бригады (Brigades) - 100 записей
-- 5. Заказы (Orders) - 1000 записей
--Создание базы данных
CREATE DATABASE Company1

go
ALTER DATABASE Company1 SET RECOVERY SIMPLE
go*/

use ConstructionCompany
go
/*drop table Orders, Jobs, Brigades, Customers, Materials
--Создание таблиц
CREATE TABLE dbo.Materials (MaterialID int IDENTITY(1,1) NOT NULL PRIMARY KEY, NameMaterial nvarchar(20), Packaging nvarchar(20), DescriptionMaterial nvarchar(20), PriceMaterial int) 
CREATE TABLE dbo.Brigades (BrigadeID int IDENTITY(1,1) NOT NULL PRIMARY KEY, NameBrigade nvarchar(20), Brigadier nvarchar(20), Composition nvarchar(20)) 
CREATE TABLE dbo.Customers (CustomerID int IDENTITY(1,1) NOT NULL PRIMARY KEY, FullName nvarchar(20), AddressCustomer nvarchar(20), Telephone int, PassportData nvarchar(20)) 
CREATE TABLE dbo.Jobs (JobID int IDENTITY(1,1) NOT NULL PRIMARY KEY, NameJob nvarchar(20), DescriptionJob nvarchar(20), PriceJob int, MaterialID int) 
CREATE TABLE dbo.Orders (OrderID int IDENTITY(1,1) NOT NULL PRIMARY KEY, CustomerID int, JobID int, BrigadeID int, PriceOrder int, DateStart date, DateEnd date, Completion bit, Payment bit, Senior nvarchar(20)) 

--Добавление связей между таблицами 
ALTER TABLE dbo.Jobs  WITH CHECK ADD  CONSTRAINT FK_Materials_Jobs FOREIGN KEY(MaterialID)
REFERENCES dbo.Materials (MaterialID) ON DELETE CASCADE
GO
ALTER TABLE dbo.Jobs CHECK CONSTRAINT FK_Materials_Jobs
GO
--Добавление связей между таблицами
ALTER TABLE dbo.Orders  WITH CHECK ADD  CONSTRAINT FK_Jobs_Orders FOREIGN KEY(JobID)
REFERENCES dbo.Jobs (JobID) ON DELETE CASCADE
GO
ALTER TABLE dbo.Orders CHECK CONSTRAINT FK_Jobs_Orders
GO
--Добавление связей между таблицами
ALTER TABLE dbo.Orders  WITH CHECK ADD  CONSTRAINT FK_Brigades_Orders FOREIGN KEY(BrigadeID)
REFERENCES dbo.Brigades (BrigadeID) ON DELETE CASCADE
GO
ALTER TABLE dbo.Orders CHECK CONSTRAINT FK_Brigades_Orders
GO
--Добавление связей между таблицами
ALTER TABLE dbo.Orders  WITH CHECK ADD  CONSTRAINT FK_Customers_Orders FOREIGN KEY(CustomerID)
REFERENCES dbo.Customers (CustomerID) ON DELETE CASCADE
GO
ALTER TABLE dbo.Orders CHECK CONSTRAINT FK_Customers_Orders
GO*/

SET NOCOUNT ON
DECLARE @Symbol CHAR(52)= 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz',
		@Position int,
		@i int,
		@NameLimit int,
		@NameJob nvarchar(20), @DescriptionJob nvarchar(20),@NameMaterial nvarchar(20),
		@Packaging nvarchar(20), @DescriptionMaterial nvarchar(20), @NameBrigade nvarchar(20),
		@Brigadier nvarchar(20), @Composition nvarchar(20), @FullName nvarchar(20), @Address nvarchar(20),
		@PassportData nvarchar(20), @Senior nvarchar(20), 
		@bdate date, @bdate1 date,
		@RowCount int,
		@NumberJobs int,
		@NumberMaterials int,
		@NumberBrigades int,
		@NumberCustomers int,
		@NumberOrders int


SET @NumberMaterials = 1000
SET @NumberBrigades = 1000
SET @NumberCustomers = 1000
SET @NumberOrders = 10000
SET @NumberJobs = 10000

BEGIN TRAN


-- материалы - 1000
SELECT @i=0 FROM dbo.Materials WITH (TABLOCKX) WHERE 1=0
SET @RowCount=1
	
	WHILE @RowCount<=@NumberMaterials
	BEGIN
		
		SET @NameMaterial=''
		SET @Packaging=''
		SET @DescriptionMaterial=''
		SET @NameLimit=5+RAND()*15 -- им¤ от 5 до 20 символов
			

		SET @i=1

		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @NameMaterial = @NameMaterial + SUBSTRING(@Symbol, @Position, 1)
			SET @Position=RAND()*52
			SET @Packaging=@Packaging + SUBSTRING(@Symbol, @Position, 1)
			SET @Position=RAND()*52
			SET @DescriptionMaterial=@DescriptionMaterial + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		INSERT INTO dbo.Materials (NameMaterial, Packaging, DescriptionMaterial, PriceMaterial) 
		SELECT @NameMaterial, @Packaging, @DescriptionMaterial, RAND()*500 
		

		SET @RowCount +=1
	END

	-- бриагады - 1000
SELECT @i=0 FROM dbo.Brigades WITH (TABLOCKX) WHERE 1=0
SET @RowCount=1
	
	WHILE @RowCount<=@NumberBrigades
	BEGIN
		
		SET @NameBrigade=''
		SET @Brigadier=''
		SET @Composition=''
		SET @NameLimit=5+RAND()*15 -- им¤ от 5 до 20 символов
			

		SET @i=1

		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @NameBrigade = @NameBrigade + SUBSTRING(@Symbol, @Position, 1)
			SET @Position=RAND()*52
			SET @Brigadier=@Brigadier + SUBSTRING(@Symbol, @Position, 1)
			SET @Position=RAND()*52
			SET @Composition=@Composition + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		INSERT INTO dbo.Brigades(NameBrigade, Brigadier, Composition) 
		SELECT @NameBrigade, @Brigadier, @Composition
		

		SET @RowCount +=1
	END

-- заказчики - 1000
SELECT @i=0 FROM dbo.Customers WITH (TABLOCKX) WHERE 1=0
SET @RowCount=1
	
	WHILE @RowCount<=@NumberCustomers
	BEGIN
		
		SET @FullName=''
		SET @Address=''
		SET @PassportData=''
		SET @NameLimit=5+RAND()*15 -- им¤ от 5 до 20 символов
			

		SET @i=1

		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @FullName = @FullName + SUBSTRING(@Symbol, @Position, 1)
			SET @Position=RAND()*52
			SET @Address=@Address + SUBSTRING(@Symbol, @Position, 1)
			SET @Position=RAND()*52
			SET @PassportData=@PassportData + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		INSERT INTO dbo.Customers(FullName, [Address], Telephone, PassportData) 
		SELECT @FullName, @Address, RAND()*500 , @PassportData
		

		SET @RowCount +=1
	END
	
-- виды работы - 1000
SELECT @i=0 FROM dbo.Jobs WITH (TABLOCKX) WHERE 1=0
	SET @RowCount=1
	
	WHILE @RowCount<=@NumberJobs
	BEGIN
		
		SET @NameJob=''
		SET @DescriptionJob=''
		SET @NameLimit=5+RAND()*15 -- имя от 5 до 50 символов
		SET @i=1

		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @NameJob = @NameJob + SUBSTRING(@Symbol, @Position, 1)
			SET @Position=RAND()*52
			SET @DescriptionJob = @DescriptionJob + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		INSERT INTO dbo.Jobs (NameJob, DescriptionJob, PriceJob, MaterialID) 
		SELECT @NameJob, @DescriptionJob, RAND()*500, CAST( (1+RAND()*(@NumberMaterials-1)) as int)

		SET @RowCount +=1
	END

-- заказы 10000
SELECT @i=0 FROM dbo.Orders WITH (TABLOCKX) WHERE 1=0
SET @RowCount=1	

	WHILE @RowCount<=@NumberOrders
	BEGIN
		
		SET @Senior=''
		SET @bdate=dateadd(day,-RAND()*15000,GETDATE())
		SET @bdate1=dateadd(day,-RAND()*15000,GETDATE())
		SET @NameLimit=5+RAND()*15 -- имя от 5 до 50 символов
		SET @i=1

		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @Senior = @Senior + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		INSERT INTO dbo.Orders(CustomerID, JobID, BrigadeID, PriceOrder, DateStart, DateEnd, Completion, Payment, Senior)
		SELECT 
		CAST( (1+RAND()*(@NumberCustomers-1)) as int),
		CAST( (1+RAND()*(@NumberJobs-1)) as int),
		CAST( (1+RAND()*(@NumberBrigades-1)) as int),
		RAND()*500, 
		@bdate, @bdate1, ROUND(RAND()*1,0), ROUND(RAND()*1,0), @Senior
		
		SET @RowCount +=1
	END
	
COMMIT TRAN
GO

