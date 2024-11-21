/****************************
 * Game Table
 ****************************/
IF OBJECT_ID(N'Football.Game') IS NULL
BEGIN
   CREATE TABLE Football.Game
   (
      GameId INT NOT NULL IDENTITY(1, 1), 
      [Date] DATE NOT NULL,
      --[Time]DATETIMEOFFSET NOT NULL DEFAULT(SYSDATETIMEOFFSET()) 
      [Location] NVARCHAR(255) NOT NULL,
      Canceled INT NOT NULL DEFAULT(0),   -- 0 = Not Canceled, 1 = Canceled

      CONSTRAINT [PK_Football_Game_GameId] PRIMARY KEY CLUSTERED
      (
         GameId ASC
      )
   );
END;

