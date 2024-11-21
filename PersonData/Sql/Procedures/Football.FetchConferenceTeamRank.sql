--one of the aggregate querires that ranks team win in conference based on year and conference names

CREATE OR ALTER PROCEDURE Football.FetchConferenceTeamRank
    @Year INT,
    @ConfName NVARCHAR(255)
AS
BEGIN
    
    CREATE TABLE #TeamWins (
        TeamName NVARCHAR(255),
        Wins INT,
        ConferenceRank INT
    );

    
    INSERT INTO #TeamWins (TeamName, Wins, ConferenceRank)
    SELECT 
        T.TeamName,
        COUNT(CASE WHEN GT.Score > Opp.Score THEN 1 ELSE NULL END) AS Wins,
        RANK() OVER (ORDER BY COUNT(CASE WHEN GT.Score > Opp.Score THEN 1 ELSE NULL END) DESC) AS ConferenceRank
    FROM 
        Football.Game G
        INNER JOIN Football.GameTeam GT ON G.GameId = GT.GameId
        INNER JOIN Football.Team T ON GT.TeamId = T.TeamId
        INNER JOIN Football.Conference C ON T.ConfId = C.ConfId
        LEFT JOIN (
            SELECT GameId, 
                   TeamId, 
                   Score
            FROM Football.GameTeam
        ) Opp ON GT.GameId = Opp.GameId AND GT.TeamId <> Opp.TeamId
    WHERE 
        C.ConfName = @ConfName 
        AND YEAR(G.[Date]) = @Year 
    GROUP BY 
        T.TeamName;

    
    SELECT 
        TeamName,
        Wins,
        ConferenceRank
    FROM 
        #TeamWins
    ORDER BY 
        ConferenceRank;

    
    DROP TABLE #TeamWins;
END;
