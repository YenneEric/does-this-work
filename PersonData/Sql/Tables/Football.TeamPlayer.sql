IF OBJECT_ID(N'Football.TeamPlayer') IS NULL
BEGIN
   CREATE TABLE Football.TeamPlayer
   (
      TeamPlayerId INT NOT NULL IDENTITY(1, 1), 
      PlayerId INT NOT NULL,                    
      SeasonId INT NOT NULL,                    
      TeamId INT NOT NULL,                      
      JerseyNumber INT NOT NULL,                

      CONSTRAINT [PK_Football_TeamPlayer_TeamPlayerId] PRIMARY KEY CLUSTERED
      (
         TeamPlayerId ASC
      )
   );
END;

/****************************
 * Unique Constraints
 ****************************/


IF NOT EXISTS
   (
      SELECT *
      FROM sys.key_constraints kc
      WHERE kc.parent_object_id = OBJECT_ID(N'Football.TeamPlayer')
         AND kc.[name] = N'UK_Football_TeamPlayer_U1'
   )
BEGIN
   ALTER TABLE Football.TeamPlayer
   ADD CONSTRAINT [UK_Football_TeamPlayer_U1] UNIQUE NONCLUSTERED
   (
      PlayerId ASC,
      SeasonId ASC
   )
END;


IF NOT EXISTS
   (
      SELECT *
      FROM sys.key_constraints kc
      WHERE kc.parent_object_id = OBJECT_ID(N'Football.TeamPlayer')
         AND kc.[name] = N'UK_Football_TeamPlayer_U2'
   )
BEGIN
   ALTER TABLE Football.TeamPlayer
   ADD CONSTRAINT [UK_Football_TeamPlayer_U2] UNIQUE NONCLUSTERED
   (
      PlayerId ASC,
      SeasonId ASC
   )
END;

/****************************
 * Foreign Keys Constraints
 ****************************/


IF NOT EXISTS
   (
      SELECT *
      FROM sys.foreign_keys fk
      WHERE fk.parent_object_id = OBJECT_ID(N'Football.TeamPlayer')
         AND fk.referenced_object_id = OBJECT_ID(N'Football.Player')
         AND fk.[name] = N'FK_Football_TeamPlayer_Player'
   )
BEGIN
   ALTER TABLE Football.TeamPlayer
   ADD CONSTRAINT [FK_Football_TeamPlayer_Player] FOREIGN KEY
   (
      PlayerId
   )
   REFERENCES Football.Player
   (
      PlayerId
   );
END;


IF NOT EXISTS
   (
      SELECT *
      FROM sys.foreign_keys fk
      WHERE fk.parent_object_id = OBJECT_ID(N'Football.TeamPlayer')
         AND fk.referenced_object_id = OBJECT_ID(N'Football.Season')
         AND fk.[name] = N'FK_Football_TeamPlayer_Season'
   )
BEGIN
   ALTER TABLE Football.TeamPlayer
   ADD CONSTRAINT [FK_Football_TeamPlayer_Season] FOREIGN KEY
   (
      SeasonId
   )
   REFERENCES Football.Season
   (
      SeasonId
   );
END;


IF NOT EXISTS
   (
      SELECT *
      FROM sys.foreign_keys fk
      WHERE fk.parent_object_id = OBJECT_ID(N'Football.TeamPlayer')
         AND fk.referenced_object_id = OBJECT_ID(N'Football.Team')
         AND fk.[name] = N'FK_Football_TeamPlayer_Team'
   )
BEGIN
   ALTER TABLE Football.TeamPlayer
   ADD CONSTRAINT [FK_Football_TeamPlayer_Team] FOREIGN KEY
   (
      TeamId
   )
   REFERENCES Football.Team
   (
      TeamId
   );
END;
