CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllPatients]
    @filter nvarchar(max) = '',
    @id int = null
AS
BEGIN
	SELECT [Id],[Name],[Address],[Email],[DateOfBirth],[Gender] FROM [Patients]
    WHERE (@filter = '' OR NAME LIKE '%' + @filter + '%')
      AND (@id is null OR [Id] = @id)
END 

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_InsertPatient]
    @Name nvarchar(max),
    @Address nvarchar(max),
    @Email nvarchar(max),
    @DateOfBirth datetime,
    @Gender int
AS
BEGIN
    SET NOCOUNT ON;

        INSERT INTO [Patients] ([Name],[Address],[Email],[DateOfBirth],[Gender])
        VALUES (@Name,@Address,@Email,@DateOfBirth,@Gender)

    SELECT SCOPE_IDENTITY() AS Id

END

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_UpdatePatient]
    @Id int,
    @Name nvarchar(max),
    @Address nvarchar(max),
    @Email nvarchar(max),
    @DateOfBirth datetime,
    @Gender int
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Patients]
    SET [Name] = @Name, [Address] = @Address, [Email] = @Email, [DateOfBirth] = @DateOfBirth, [Gender] = @Gender
    WHERE Id = @Id;

    SELECT @@ROWCOUNT;
END

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_DeletePatient]
    -- Add the parameters for the stored procedure here
    @Id int
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [Patients]
    WHERE Id = @Id;

    SELECT @@ROWCOUNT;
END