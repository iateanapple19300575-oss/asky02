CREATE TABLE dbo.W_Lecture_Actual (
    ID             BIGINT         IDENTITY(1,1) NOT NULL,
    Lecture_Date   DATE           NOT NULL,
    Teacher_Code   CHAR(4)        NOT NULL,
    Site_Code      CHAR(2)        NOT NULL,
    Grade          CHAR(1)        NOT NULL,
    Class_Code     CHAR(3)        NOT NULL,
    Koma_Seq       TINYINT        NOT NULL,
    Subjects       CHAR(1)        NOT NULL,
    Text_Times     VARCHAR(50)    NOT NULL,
    Start_Time     TIME(0)        NULL,
    End_Time       TIME(0)        NULL,
    Pinch_Type     VARCHAR(2)     NULL,
    Create_Date    DATETIME       NOT NULL DEFAULT GETDATE(),
    Create_User    VARCHAR(50)    NOT NULL,
    Update_Date    DATETIME       NULL,
    Update_User    VARCHAR(50)    NULL,
    RowVersion     ROWVERSION     NULL,

    CONSTRAINT PK_W_Lecture_Actual PRIMARY KEY (ID)
);

CREATE NONCLUSTERED INDEX IX_W_Lecture_Actual_IDX01
ON dbo.W_Lecture_Actual (
    Lecture_Date,
    Teacher_Code,
    Site_Code,
    Grade,
    Class_Code,
    Koma_Seq,
    Subjects
);

CREATE TRIGGER TRG_W_Lecture_Actual_IU
ON dbo.W_Lecture_Actual
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- INSERT 行には CreatedAt をセット
    UPDATE t
    SET Create_Date = GETDATE()
    FROM dbo.W_Lecture_Actual t
    INNER JOIN inserted i ON t.ID = i.ID
    LEFT JOIN deleted d ON i.ID = d.ID
    WHERE d.ID IS NULL;   -- INSERT の場合のみ

    -- UPDATE 行には UpdatedAt をセット
    UPDATE t
    SET Update_Date = GETDATE()
    FROM dbo.W_Lecture_Actual t
    INNER JOIN inserted i ON t.ID = i.ID
    INNER JOIN deleted d ON i.ID = d.ID;  -- UPDATE の場合のみ
END;
GO

