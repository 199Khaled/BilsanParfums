-- Stored Procedures for Table: Flakons

Use [BilsanDb];
Go


CREATE OR ALTER PROCEDURE SP_Get_Flakons_ByID
(
    @FlakonID int
)
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve data
        SELECT *
        FROM Flakons
        WHERE FlakonID = @FlakonID;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE SP_Get_All_Flakons
AS
BEGIN
    BEGIN TRY
        -- Attempt to retrieve all data from the table
        SELECT *
        FROM Flakons;
    END TRY
    BEGIN CATCH
        -- Call the centralized error handling procedure
        EXEC SP_HandleError;
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Add_Flakons
(
    @Form nvarchar(50) = NULL,
    @Verschlussart nvarchar(50) = NULL,
    @Farbe nvarchar(50) = NULL,
    @FlakonsMengeInMl nvarchar(10),
    @Flakons_pro_Karton int,
    @Kartons_Lager int,
    @Verbleibende_Flakons int = NULL,
    @Erstellungsdatum date = NULL,
    @NewID INT OUTPUT

)
AS
BEGIN
    BEGIN TRY
        -- Check if any required parameters are NULL
            IF @FlakonsMengeInMl IS NULL OR @Flakons_pro_Karton IS NULL OR @Kartons_Lager IS NULL
        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Insert the data into the table
        INSERT INTO Flakons ([Form],[Verschlussart],[Farbe],[FlakonsMengeInMl],[Flakons_pro_Karton],[Kartons_Lager],[Verbleibende_Flakons],[Erstellungsdatum])
        VALUES (    LTRIM(RTRIM(@Form)),
    LTRIM(RTRIM(@Verschlussart)),
    LTRIM(RTRIM(@Farbe)),
    @FlakonsMengeInMl,
    @Flakons_pro_Karton,
    @Kartons_Lager,
    @Verbleibende_Flakons,
    @Erstellungsdatum
);

        -- Set the new ID
        SET @NewID = SCOPE_IDENTITY();  -- Get the last inserted ID
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Error handling
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Update_Flakons_ByID
(
    @FlakonID int,
    @Form nvarchar(50) = NULL,
    @Verschlussart nvarchar(50) = NULL,
    @Farbe nvarchar(50) = NULL,
    @FlakonsMengeInMl nvarchar(10),
    @Flakons_pro_Karton int,
    @Kartons_Lager int,
    @Verbleibende_Flakons int = NULL,
    @Erstellungsdatum date = NULL

)
AS
BEGIN
    BEGIN TRY
        -- Check if required parameters are NULL or contain only whitespace after trimming

        IF (@FlakonsMengeInMl IS NULL OR @FlakonsMengeInMl = '') OR (@Flakons_pro_Karton IS NULL OR @Flakons_pro_Karton = '') OR (@Kartons_Lager IS NULL OR @Kartons_Lager = '')

        BEGIN
            RAISERROR('One or more required parameters are NULL or have only whitespace.', 16, 1);
            RETURN;
        END

        -- Update the record in the table
        UPDATE Flakons
        SET     [Form] = LTRIM(RTRIM(@Form)),
    [Verschlussart] = LTRIM(RTRIM(@Verschlussart)),
    [Farbe] = LTRIM(RTRIM(@Farbe)),
    [FlakonsMengeInMl] = @FlakonsMengeInMl,
    [Flakons_pro_Karton] = @Flakons_pro_Karton,
    [Kartons_Lager] = @Kartons_Lager,
    [Verbleibende_Flakons] = @Verbleibende_Flakons,
    [Erstellungsdatum] = @Erstellungsdatum

        WHERE FlakonID = @FlakonID;
        
        -- Optionally, you can check if the update was successful and raise an error if no rows were updated
        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('No rows were updated. Please check the PersonID or other parameters.', 16, 1);
            RETURN;
        END
    END TRY
    BEGIN CATCH
        EXEC SP_HandleError; -- Handle errors
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Delete_Flakons_ByID
(
    @FlakonID int
)
AS
BEGIN

    BEGIN TRY
        -- Check if the record exists before attempting to delete
        IF NOT EXISTS (SELECT 1 FROM Flakons WHERE FlakonID = @FlakonID)
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END

        -- Attempt to delete the record
        DELETE FROM Flakons WHERE FlakonID = @FlakonID;

        -- Ensure at least one row was deleted
        IF @@ROWCOUNT = 0
        BEGIN
            EXEC SP_HandleError;
            RETURN;
        END
    END TRY
    BEGIN CATCH
        -- Handle all errors (including FK constraint violations)
        EXEC SP_HandleError;
    END CATCH
END;
GO


CREATE OR ALTER PROCEDURE SP_Search_Flakons_ByColumn
(
    @ColumnName NVARCHAR(128),  -- Column name without spaces
    @SearchValue NVARCHAR(255), -- Value to search for
    @Mode NVARCHAR(20) = 'Anywhere' -- Search mode (default: Anywhere)
)
AS
BEGIN
    BEGIN TRY
        DECLARE @ActualColumn NVARCHAR(128);
        DECLARE @SQL NVARCHAR(MAX);
        DECLARE @SearchPattern NVARCHAR(255);

        -- Map input column name to actual database column name
        SET @ActualColumn = 
            CASE @ColumnName
                WHEN 'FlakonID' THEN 'FlakonID'
        WHEN 'Form' THEN 'Form'
        WHEN 'Verschlussart' THEN 'Verschlussart'
        WHEN 'Farbe' THEN 'Farbe'
        WHEN 'FlakonsMengeInMl' THEN 'FlakonsMengeInMl'
        WHEN 'Flakons_pro_Karton' THEN 'Flakons_pro_Karton'
        WHEN 'Kartons_Lager' THEN 'Kartons_Lager'
        WHEN 'Verbleibende_Flakons' THEN 'Verbleibende_Flakons'
        WHEN 'Erstellungsdatum' THEN 'Erstellungsdatum'
                ELSE NULL
            END;

        -- Validate the column name
        IF @ActualColumn IS NULL
        BEGIN
            RAISERROR('Invalid Column Name provided.', 16, 1);
            RETURN;
        END

        -- Validate the search value (ensure it's not empty or NULL)
        IF ISNULL(LTRIM(RTRIM(@SearchValue)), '') = ''
        BEGIN
            RAISERROR('Search value cannot be empty.', 16, 1);
            RETURN;
        END

        -- Prepare the search pattern based on the mode
        SET @SearchPattern =
            CASE 
                WHEN @Mode = 'Anywhere' THEN '%' + LTRIM(RTRIM(@SearchValue)) + '%'
                WHEN @Mode = 'StartsWith' THEN LTRIM(RTRIM(@SearchValue)) + '%'
                WHEN @Mode = 'EndsWith' THEN '%' + LTRIM(RTRIM(@SearchValue))
                WHEN @Mode = 'ExactMatch' THEN LTRIM(RTRIM(@SearchValue))
                ELSE '%' + LTRIM(RTRIM(@SearchValue)) + '%'
            END;

        -- Build the dynamic SQL query safely
        SET @SQL = N'SELECT * FROM ' + QUOTENAME('Flakons') + 
                   N' WHERE ' + QUOTENAME(@ActualColumn) + N' LIKE @SearchPattern OPTION (RECOMPILE)';

        -- Execute the dynamic SQL with parameterized search pattern
        EXEC sp_executesql @SQL, N'@SearchPattern NVARCHAR(255)', @SearchPattern;
    END TRY
    BEGIN CATCH
        -- Handle errors
        EXEC SP_HandleError;
    END CATCH
END;
GO
