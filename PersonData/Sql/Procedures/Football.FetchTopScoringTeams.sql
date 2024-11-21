
--another aggregate to rank teams based on points for a given year

CREATE OR ALTER PROCEDURE Football.FetchTopScoringTeams
    @Year INT
AS
BEGIN
    SELECT 
        t.TeamName,
        SUM(CAST(gt.Score AS INT)) AS TotalPoints,
        COUNT(gt.GameId) AS GamesPlayed,
        AVG(CAST(gt.Score AS DECIMAL(10, 2))) AS AveragePoints,
        RANK() OVER (ORDER BY SUM(CAST(gt.Score AS INT)) DESC) AS TeamRank
    FROM 
        Football.GameTeam gt
    INNER JOIN 
        Football.Game g ON gt.GameId = g.GameId
    INNER JOIN 
        Football.Team t ON gt.TeamId = t.TeamId
    WHERE 
        g.[Date] BETWEEN DATEFROMPARTS(@Year, 1, 1) AND DATEFROMPARTS(@Year, 12, 31)
    GROUP BY 
        t.TeamName
    ORDER BY 
        TotalPoints DESC;
END;
GO