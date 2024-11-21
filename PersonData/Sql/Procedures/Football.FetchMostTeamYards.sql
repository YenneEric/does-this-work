CREATE OR ALTER PROCEDURE Football.FetchMostTeamYards
    @Year INT
AS
BEGIN
   SELECT 
    T.TeamName,
    S.Year,
    SUM(PS.RushingYards) AS TotalRushingYards,
    SUM(PS.ReceivingYards) AS TotalReceivingYards,
    SUM(PS.ThrowingYards) AS TotalThrowingYards,
    SUM(PS.RushingYards + PS.ReceivingYards + PS.ThrowingYards) AS Yards,
    RANK() OVER (PARTITION BY S.Year ORDER BY SUM(PS.RushingYards + PS.ReceivingYards + PS.ThrowingYards) DESC) AS Rank
    FROM 
        Team T
    JOIN 
        TeamPlayer TP ON T.TeamId = TP.TeamId
    JOIN 
        Season S ON TP.SeasonId = S.SeasonId
    JOIN 
        PlayerStats PS ON TP.TeamPlayerId = PS.TeamPlayerId
    WHERE 
        S.Year = @Year -- Replace @Year with the desired year, e.g., 2024
    GROUP BY 
        T.TeamName, S.Year
    ORDER BY 
        Rank DESC;


END;
GO