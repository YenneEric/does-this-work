
--this is a aggreagete function to create a scedule for a team given name and year

CREATE PROCEDURE Football.GetTeamSchedule
    @TeamName NVARCHAR(100),
    @Year INT
AS
BEGIN
    SELECT DISTINCT
        g.GameId, 
        g.Date AS GameDate,
        g.Location AS GameLocation,
        t1.TeamName AS Team,
        gt1.Score AS TeamScore,
        gt1.TopOfPossessionSec AS TeamTimeOfPossession,
        t2.TeamName AS Opponent,
        gt2.Score AS OpponentScore,
        gt2.TopOfPossessionSec AS OpponentTimeOfPossession,
        CASE 
            WHEN gt1.Score > gt2.Score THEN t1.TeamName
            WHEN gt1.Score < gt2.Score THEN t2.TeamName
            ELSE 'Draw'
        END AS Winner
    FROM Game g
    INNER JOIN GameTeam gt1 ON g.GameId = gt1.GameId
    INNER JOIN Team t1 ON gt1.TeamId = t1.TeamId
    INNER JOIN GameTeam gt2 ON g.GameId = gt2.GameId AND gt1.TeamId != gt2.TeamId
    INNER JOIN Team t2 ON gt2.TeamId = t2.TeamId
    INNER JOIN TeamPlayer tp ON tp.TeamId = t1.TeamId
    INNER JOIN Season s ON tp.SeasonId = s.SeasonId
    WHERE t1.TeamName = @TeamName
      AND s.Year = @Year
      AND g.Date BETWEEN CONCAT(@Year, '-01-01') AND CONCAT(@Year, '-12-31')
    ORDER BY g.Date;
END;
