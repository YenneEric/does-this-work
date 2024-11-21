﻿

CREATE OR ALTER PROCEDURE Football.UpdateGameDetails
    @GameId INT,                       
    @HomeTeamName NVARCHAR(255) = NULL, 
    @HomeScore INT = NULL,              
    @HomeTimeOfPossession INT = NULL,  
    @AwayTeamName NVARCHAR(255) = NULL, 
    @AwayScore INT = NULL,             
    @AwayTimeOfPossession INT = NULL,   
    @GameLocation NVARCHAR(255) = NULL, 
    @GameDate DATE = NULL               
AS
BEGIN
    -- Update the home team details
    UPDATE gt
    SET
        Score = COALESCE(@HomeScore, Score),
        TopOfPossessionSec = COALESCE(@HomeTimeOfPossession, TopOfPossessionSec)
    FROM Football.GameTeam gt
    INNER JOIN Football.Team t ON gt.TeamId = t.TeamId
    WHERE gt.GameId = @GameId AND gt.TeamTypeId = 1 
      AND (t.TeamName = @HomeTeamName OR @HomeTeamName IS NULL);

    -- Update the away team details
    UPDATE gt
    SET
        Score = COALESCE(@AwayScore, Score),
        TopOfPossessionSec = COALESCE(@AwayTimeOfPossession, TopOfPossessionSec)
    FROM Football.GameTeam gt
    INNER JOIN Football.Team t ON gt.TeamId = t.TeamId
    WHERE gt.GameId = @GameId AND gt.TeamTypeId = 2 -- Away team
      AND (t.TeamName = @AwayTeamName OR @AwayTeamName IS NULL);

    -- Update the game location and date
    UPDATE Football.Game
    SET
        Location = COALESCE(@GameLocation, Location),
        Date = COALESCE(@GameDate, Date)
    WHERE GameId = @GameId;
END;
GO
