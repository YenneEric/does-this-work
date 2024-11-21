
--edit this stats of a player given a gameid teamname, and playerid all others are optional


CREATE OR ALTER PROCEDURE Football.EditPlayerStats
    @GameId INT,
    @TeamName NVARCHAR(255),
    @PlayerId INT,
    @RushingYards INT = NULL,
    @ReceivingYards INT = NULL,
    @ThrowingYards INT = NULL,
    @Tackles INT = NULL,
    @Sacks INT = NULL,
    @Turnovers INT = NULL,
    @InterceptionsCaught INT = NULL,
    @Touchdowns INT = NULL,
    @Punts INT = NULL,
    @FieldGoalsMade INT = NULL
AS
BEGIN
    
    IF EXISTS (
        SELECT 1
        FROM Football.Game g
        INNER JOIN Football.GameTeam gt ON g.GameId = gt.GameId
        INNER JOIN Football.Team t ON gt.TeamId = t.TeamId
        INNER JOIN Football.TeamPlayer tp ON t.TeamId = tp.TeamId
        WHERE g.GameId = @GameId AND t.TeamName = @TeamName AND tp.PlayerId = @PlayerId
    )
    BEGIN
        
        UPDATE Football.PlayerStats
        SET
            RushingYards = COALESCE(@RushingYards, RushingYards),
            ReceivingYards = COALESCE(@ReceivingYards, ReceivingYards),
            ThrowingYards = COALESCE(@ThrowingYards, ThrowingYards),
            Tackles = COALESCE(@Tackles, Tackles),
            Sacks = COALESCE(@Sacks, Sacks),
            Turnovers = COALESCE(@Turnovers, Turnovers),
            InterceptionsCaught = COALESCE(@InterceptionsCaught, InterceptionsCaught),
            Touchdowns = COALESCE(@Touchdowns, Touchdowns),
            Punts = COALESCE(@Punts, Punts),
            FieldGoalsMade = COALESCE(@FieldGoalsMade, FieldGoalsMade)
        WHERE GameId = @GameId
          AND TeamPlayerId = (
              SELECT tp.TeamPlayerId
              FROM Football.TeamPlayer tp
              INNER JOIN Football.Team t ON tp.TeamId = t.TeamId
              WHERE tp.PlayerId = @PlayerId AND t.TeamName = @TeamName
          );
    END
END;
GO
