USE [EmployeeDB]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [varchar](500) NULL,
	[Department] [int] NULL,
	[DOJ] [datetime] NULL,
	[ProfilePicture] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[DeleteDepartment]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sayantan Das
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDepartment]
	-- Add the parameters for the stored procedure here
	@DepartmentId int,
	@errmsg varchar(500) output
AS
BEGIN
begin tran
begin try
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    delete [dbo].[Department]
		where [DepartmentId] = @DepartmentId
		set @errmsg = 'Success'
		commit
end try
begin catch
	set @errmsg = ERROR_MESSAGE()
	ROLLBACK
end catch
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmployee]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sayantan Das
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteEmployee]
	-- Add the parameters for the stored procedure here
	@EmployeeId int,
	@errmsg varchar(500) output
AS
BEGIN
begin tran
begin try
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    delete [dbo].[Employee]
		where [EmployeeId] = @EmployeeId
		set @errmsg = 'Success'
		commit
end try
begin catch
	set @errmsg = ERROR_MESSAGE()
	ROLLBACK
end catch
END
GO
/****** Object:  StoredProcedure [dbo].[GetDepartmentList]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sayantan Das
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDepartmentList](
	@errmsg varcHAR(500) output
)
AS
BEGIN
BEGIN TRAN
	begin try
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		SELECT * FROM Department
		set @errmsg = 'Success'
		commit
	end try
	begin catch
		set @errmsg = ERROR_MESSAGE()
		rollback
	end catch

END
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeeList]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sayantan Das
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeeList](
	@errmsg varcHAR(500) output
)
AS
BEGIN
BEGIN TRAN
	begin try
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		SELECT Employee.*, Department.DepartmentId,Department.DepartmentName FROM Employee inner join Department on Employee.Department = Department.DepartmentId
		set @errmsg = 'Success'
		commit
	end try
	begin catch
		set @errmsg = ERROR_MESSAGE()
		rollback
	end catch

END
GO
/****** Object:  StoredProcedure [dbo].[InsertDepartment]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sayantan Das
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertDepartment]
	-- Add the parameters for the stored procedure here
	@DepartmentName varchar(500),
	@errmsg varchar(500) output
AS
BEGIN
begin tran
begin try
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [dbo].[Department](
		[DepartmentName]
	)
	values(
		@DepartmentName
		)
		set @errmsg = 'Success'
		commit
end try
begin catch
	set @errmsg = ERROR_MESSAGE()
	ROLLBACK
end catch
END
GO
/****** Object:  StoredProcedure [dbo].[InsertEmployee]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sayantan Das
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertEmployee]
	-- Add the parameters for the stored procedure here
	@EmployeeName varchar(500),
	@Department int,
	@DOJ datetime,
	@ProfilePicture nvarchar(100),
	@errmsg varchar(500) output
AS
BEGIN
begin tran
begin try
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [dbo].[Employee](
		[EmployeeName],
		[Department],
		[DOJ],
		[ProfilePicture]
	)
	values(
		@EmployeeName,
		@Department,
		@DOJ,
		@ProfilePicture
		)
		set @errmsg = 'Success'
		commit
end try
begin catch
	set @errmsg = ERROR_MESSAGE()
	ROLLBACK
end catch
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDepartment]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sayantan Das
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateDepartment]
	-- Add the parameters for the stored procedure here
	@DepartmentId int,
	@DepartmentName varchar(500),
	@errmsg varchar(500) output
AS
BEGIN
begin tran
begin try
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update [dbo].[Department] set
		[DepartmentName] = @DepartmentName
		where [DepartmentId] = @DepartmentId
		set @errmsg = 'Success'
		commit
end try
begin catch
	set @errmsg = ERROR_MESSAGE()
	ROLLBACK
end catch
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmployee]    Script Date: 08-03-2023 17:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sayantan Das
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEmployee]
	-- Add the parameters for the stored procedure here
	@EmployeeId int,
	@EmployeeName varchar(500),
	@Department int,
	@DOJ datetime,
	@ProfilePicture varchar(100),
	@errmsg varchar(500) output
AS
BEGIN
begin tran
begin try
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update [dbo].[Employee] set
		[EmployeeName] = @EmployeeName,
		[Department] = @Department,
		[DOJ] = @DOJ,
		[ProfilePicture] = @ProfilePicture
		where [EmployeeId] = @EmployeeId
		set @errmsg = 'Success'
		commit
end try
begin catch
	set @errmsg = ERROR_MESSAGE()
	ROLLBACK
end catch
END
GO
