

CREATE OR ALTER PROCEDURE Football.FetchConferenceTeamRank
    @Year INT,
    @ConfName NVARCHAR(255)
AS
BEGIN
    -- Temporary table to store results
    CREATE TABLE #TeamWins (
        TeamName NVARCHAR(255),
        Wins INT,
        ConferenceRank INT
    );

    -- Calculate team wins within the specified conference and year
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
        AND YEAR(G.[Date]) = @Year -- Filter by the year from the Game table
    GROUP BY 
        T.TeamName;

    -- Select results
    SELECT 
        TeamName,
        Wins,
        ConferenceRank
    FROM 
        #TeamWins
    ORDER BY 
        ConferenceRank;

    -- Cleanup
    DROP TABLE #TeamWins;
END;
