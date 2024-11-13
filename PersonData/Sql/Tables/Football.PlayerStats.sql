﻿/****************************
 * PlayerStats Table
 ****************************/
IF OBJECT_ID(N'Football.PlayerStats') IS NULL
BEGIN
   CREATE TABLE Football.PlayerStats
   (
      PlayerStatsId INT NOT NULL IDENTITY(1, 1), -- Primary Key (PK)
      TeamPlayerId INT NOT NULL,                -- FK1, U1
      GameId INT NOT NULL,                      -- FK2, U1
      TeamId INT NOT NULL,                      -- FK3, Composite FK with GameId

      RushingYards INT NOT NULL,
      ReceivingYards INT NOT NULL,
      ThrowingYards INT NOT NULL,
      Tackles INT NOT NULL,
      Sacks INT NOT NULL,
      Turnovers INT NOT NULL,
      InterceptionsCaught INT NOT NULL,
      Touchdowns INT NOT NULL,
      Punts INT NOT NULL,
      FieldGoalsMade INT NOT NULL,

      CONSTRAINT [PK_Football_PlayerStats_PlayerStatsId] PRIMARY KEY CLUSTERED
      (
         PlayerStatsId ASC
      )
   );
END;

/****************************
 * Unique Constraints
 ****************************/

-- Unique Constraint U1: TeamPlayerId and GameId
IF NOT EXISTS
   (
      SELECT *
      FROM sys.key_constraints kc
      WHERE kc.parent_object_id = OBJECT_ID(N'Football.PlayerStats')
         AND kc.[name] = N'UK_Football_PlayerStats_U1'
   )
BEGIN
   ALTER TABLE Football.PlayerStats
   ADD CONSTRAINT [UK_Football_PlayerStats_U1] UNIQUE NONCLUSTERED
   (
      TeamPlayerId ASC,
      GameId ASC
   );
END;

/****************************
 * Foreign Keys Constraints
 ****************************/

-- Foreign Key to Football.TeamPlayer
IF NOT EXISTS
   (
      SELECT *
      FROM sys.foreign_keys fk
      WHERE fk.parent_object_id = OBJECT_ID(N'Football.PlayerStats')
         AND fk.referenced_object_id = OBJECT_ID(N'Football.TeamPlayer')
         AND fk.[name] = N'FK_Football_PlayerStats_TeamPlayerId'
   )
BEGIN
   ALTER TABLE Football.PlayerStats
   ADD CONSTRAINT [FK_Football_PlayerStats_TeamPlayerId] FOREIGN KEY
   (
      TeamPlayerId
   )
   REFERENCES Football.TeamPlayer
   (
      TeamPlayerId
   );
END;

-- Composite Foreign Key: GameId and TeamId references GameTeam (GameId, TeamId)
IF NOT EXISTS
   (
      SELECT *
      FROM sys.foreign_keys fk
      WHERE fk.parent_object_id = OBJECT_ID(N'Football.PlayerStats')
         AND fk.referenced_object_id = OBJECT_ID(N'Football.GameTeam')
         AND fk.[name] = N'FK_Football_PlayerStats_GameTeam'
   )
BEGIN
   ALTER TABLE Football.PlayerStats
   ADD CONSTRAINT [FK_Football_PlayerStats_GameTeam] FOREIGN KEY
   (
      GameId,
      TeamId
   )
   REFERENCES Football.GameTeam
   (
      GameId,
      TeamId
   );
END;
