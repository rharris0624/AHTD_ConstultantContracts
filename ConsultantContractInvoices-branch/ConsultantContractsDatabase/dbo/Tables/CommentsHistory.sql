CREATE TABLE [dbo].[CommentsHistory] (
    [CommentId]   INT           IDENTITY (1, 1) NOT NULL,
    [CommentNo]   CHAR (15)     NOT NULL,
    [Type]        CHAR (15)     NOT NULL,
    [Comment]     VARCHAR (300) NOT NULL,
    [UserId]      CHAR (7)      NOT NULL,
    [DateEntered] DATETIME      NOT NULL,
    CONSTRAINT [Comments_PK] PRIMARY KEY CLUSTERED ([CommentId] ASC)
);

