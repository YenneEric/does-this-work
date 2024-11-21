IF OBJECT_ID(N'Football.Team') IS NULL
BEGIN
   CREATE TABLE Football.Team
   (
      TeamId INT NOT NULL IDENTITY(1, 1), 
      TeamName NVARCHAR(255) NOT NULL,   
      [Location] NVARCHAR(255) NOT NULL,
      Mascot NVARCHAR(255),
      ConfId INT NOT NULL,

      CONSTRAINT [PK_Football_Team_TeamId] PRIMARY KEY CLUSTERED
      (
         TeamId ASC
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
      WHERE kc.parent_object_id = OBJECT_ID(N'Football.Team')
         AND kc.[name] = N'UK_Football_Team_TeamName'
   )
BEGIN
   ALTER TABLE Football.Team
   ADD CONSTRAINT [UK_Football_Team_TeamName] UNIQUE NONCLUSTERED
   (
      TeamName ASC
   )
END;

/****************************
 * Foreign Keys Constraints
 ****************************/

IF NOT EXISTS
   (
      SELECT *
      FROM sys.foreign_keys fk
      WHERE fk.parent_object_id = OBJECT_ID(N'Football.Team')
         AND fk.referenced_object_id = OBJECT_ID(N'Football.Conference')
         AND fk.[name] = N'FK_Football_Team_Conference'
   )
BEGIN
   ALTER TABLE Football.Team
   ADD CONSTRAINT [FK_Football_Team_Conference] FOREIGN KEY
   (
      ConfId
   )
   REFERENCES Football.Conference
   (
      ConfId
   );
END;
