DECLARE @GameTeamStaging TABLE
(
    GameId INT NOT NULL,
    TeamId INT NOT NULL,
    TeamTypeId INT NOT NULL, -- 1 for Home, 2 for Away
    TopOfPossessionSec INT NOT NULL,
    Score INT NOT NULL
);


INSERT INTO @GameTeamStaging (GameId, TeamId, TeamTypeId, TopOfPossessionSec, Score)
VALUES
-- Week 1
(1, 1, 1, 1200, 35), -- Alabama Crimson Tide (Home)
(1, 2, 2, 1100, 28), -- Georgia Bulldogs (Away)
(2, 3, 1, 1300, 21), -- Florida Gators (Home)
(2, 4, 2, 1000, 14), -- LSU Tigers (Away)
(3, 5, 1, 1400, 31), -- Ohio State Buckeyes (Home)
(3, 6, 2, 1100, 24), -- Michigan Wolverines (Away)
(4, 7, 1, 1150, 27), -- Penn State Nittany Lions (Home)
(4, 8, 2, 1250, 30), -- Wisconsin Badgers (Away)
(5, 9, 1, 1050, 20), -- Kansas State Wildcats (Home)
(5, 10, 2, 1350, 34), -- Kansas Jayhawks (Away)
(6, 11, 1, 1300, 24), -- Colorado Buffaloes (Home)
(6, 12, 2, 1100, 27), -- Iowa State Cyclones (Away)
(7, 13, 1, 1400, 38), -- Clemson Tigers (Home)
(7, 14, 2, 1000, 17), -- Florida State Seminoles (Away)
(8, 15, 1, 1250, 28), -- North Carolina Tar Heels (Home)
(8, 16, 2, 1150, 24), -- Duke Blue Devils (Away),

-- Week 2
(9, 2, 1, 1200, 28), -- Georgia Bulldogs (Home)
(9, 3, 2, 1100, 20), -- Florida Gators (Away)
(10, 4, 1, 1350, 24), -- LSU Tigers (Home)
(10, 1, 2, 1050, 17), -- Alabama Crimson Tide (Away)
(11, 6, 1, 1400, 31), -- Michigan Wolverines (Home)
(11, 7, 2, 1000, 24), -- Penn State Nittany Lions (Away)
(12, 8, 1, 1250, 27), -- Wisconsin Badgers (Home)
(12, 5, 2, 1150, 30), -- Ohio State Buckeyes (Away)
(13, 10, 1, 1100, 21), -- Kansas Jayhawks (Home)
(13, 11, 2, 1200, 24), -- Colorado Buffaloes (Away)
(14, 12, 1, 1350, 27), -- Iowa State Cyclones (Home)
(14, 9, 2, 1050, 17), -- Kansas State Wildcats (Away)
(15, 14, 1, 1400, 31), -- Florida State Seminoles (Home)
(15, 15, 2, 1000, 24), -- North Carolina Tar Heels (Away)
(16, 16, 1, 1250, 28), -- Duke Blue Devils (Home)
(16, 13, 2, 1150, 30), -- Clemson Tigers (Away),

-- Week 3
(17, 1, 1, 1300, 31), -- Alabama Crimson Tide (Home)
(17, 3, 2, 1050, 17), -- Florida Gators (Away)
(18, 2, 1, 1200, 28), -- Georgia Bulldogs (Home)
(18, 6, 2, 1100, 21), -- Michigan Wolverines (Away)
(19, 7, 1, 1150, 27), -- Penn State Nittany Lions (Home)
(19, 4, 2, 1250, 24), -- LSU Tigers (Away)
(20, 5, 1, 1400, 34), -- Ohio State Buckeyes (Home)
(20, 8, 2, 1000, 17), -- Wisconsin Badgers (Away)
(21, 9, 1, 1100, 28), -- Kansas State Wildcats (Home)
(21, 12, 2, 1200, 24), -- Iowa State Cyclones (Away)
(22, 10, 1, 1250, 30), -- Kansas Jayhawks (Home)
(22, 15, 2, 1050, 17), -- North Carolina Tar Heels (Away)
(23, 13, 1, 1400, 38), -- Clemson Tigers (Home)
(23, 11, 2, 1000, 14), -- Colorado Buffaloes (Away)
(24, 14, 1, 1250, 31), -- Florida State Seminoles (Home)
(24, 16, 2, 1150, 24), -- Duke Blue Devils (Away),

-- Week 4
(25, 3, 1, 1200, 24), -- Florida Gators (Home)
(25, 4, 2, 1100, 21), -- LSU Tigers (Away)
(26, 6, 1, 1300, 27), -- Michigan Wolverines (Home)
(26, 1, 2, 1000, 14), -- Alabama Crimson Tide (Away)
(27, 2, 1, 1350, 34), -- Georgia Bulldogs (Home)
(27, 7, 2, 1250, 28), -- Penn State Nittany Lions (Away)
(28, 5, 1, 1150, 31), -- Ohio State Buckeyes (Home)
(28, 8, 2, 1050, 17), -- Wisconsin Badgers (Away)
(29, 10, 1, 1250, 30), -- Kansas Jayhawks (Home)
(29, 12, 2, 1100, 17), -- Iowa State Cyclones (Away)
(30, 9, 1, 1400, 38), -- Kansas State Wildcats (Home)
(30, 15, 2, 1000, 21), -- North Carolina Tar Heels (Away)
(31, 13, 1, 1250, 31), -- Clemson Tigers (Home)
(31, 14, 2, 1150, 24), -- Florida State Seminoles (Away)
(32, 11, 1, 1350, 27), -- Colorado Buffaloes (Home)
(32, 16, 2, 1050, 14), -- Duke Blue Devils (Away)

-- Week 5
(33, 1, 1, 1200, 31), -- Alabama Crimson Tide (Home)
(33, 5, 2, 1100, 28), -- Ohio State Buckeyes (Away)
(34, 2, 1, 1300, 34), -- Georgia Bulldogs (Home)
(34, 8, 2, 1050, 21), -- Wisconsin Badgers (Away)
(35, 6, 1, 1400, 27), -- Michigan Wolverines (Home)
(35, 3, 2, 1150, 24), -- Florida Gators (Away)
(36, 4, 1, 1250, 31), -- LSU Tigers (Home)
(36, 7, 2, 1050, 28), -- Penn State Nittany Lions (Away)
(37, 9, 1, 1350, 24), -- Kansas State Wildcats (Home)
(37, 16, 2, 1100, 14), -- Duke Blue Devils (Away)
(38, 10, 1, 1300, 31), -- Kansas Jayhawks (Home)
(38, 13, 2, 1000, 21), -- Clemson Tigers (Away)
(39, 12, 1, 1250, 28), -- Iowa State Cyclones (Home)
(39, 15, 2, 1150, 24), -- North Carolina Tar Heels (Away)
(40, 11, 1, 1400, 27), -- Colorado Buffaloes (Home)
(40, 14, 2, 1000, 17), -- Florida State Seminoles (Away),

-- Week 6
(41, 3, 1, 1200, 31), -- Florida Gators (Home)
(41, 7, 2, 1150, 24), -- Penn State Nittany Lions (Away)
(42, 1, 1, 1300, 28), -- Alabama Crimson Tide (Home)
(42, 6, 2, 1050, 21), -- Michigan Wolverines (Away)
(43, 2, 1, 1400, 34), -- Georgia Bulldogs (Home)
(43, 4, 2, 1000, 17), -- LSU Tigers (Away)
(44, 8, 1, 1250, 31), -- Wisconsin Badgers (Home)
(44, 5, 2, 1100, 24), -- Ohio State Buckeyes (Away)
(45, 13, 1, 1350, 38), -- Clemson Tigers (Home)
(45, 10, 2, 1050, 17), -- Kansas Jayhawks (Away)
(46, 14, 1, 1200, 31), -- Florida State Seminoles (Home)
(46, 9, 2, 1150, 21), -- Kansas State Wildcats (Away)
(47, 15, 1, 1400, 34), -- North Carolina Tar Heels (Home)
(47, 11, 2, 1050, 17), -- Colorado Buffaloes (Away)
(48, 16, 1, 1250, 27), -- Duke Blue Devils (Home)
(48, 12, 2, 1100, 21), -- Iowa State Cyclones (Away),

-- Week 7
(49, 6, 1, 1200, 31), -- Michigan Wolverines (Home)
(49, 4, 2, 1150, 28), -- LSU Tigers (Away)
(50, 3, 1, 1300, 24), -- Florida Gators (Home)
(50, 2, 2, 1050, 21), -- Georgia Bulldogs (Away)
(51, 1, 1, 1400, 34), -- Alabama Crimson Tide (Home)
(51, 8, 2, 1000, 17), -- Wisconsin Badgers (Away)
(52, 7, 1, 1250, 31), -- Penn State Nittany Lions (Home)
(52, 5, 2, 1100, 24), -- Ohio State Buckeyes (Away)
(53, 10, 1, 1350, 28), -- Kansas Jayhawks (Home)
(53, 14, 2, 1150, 21), -- Florida State Seminoles (Away)
(54, 9, 1, 1400, 31), -- Kansas State Wildcats (Home)
(54, 16, 2, 1000, 14), -- Duke Blue Devils (Away)
(55, 12, 1, 1250, 27), -- Iowa State Cyclones (Home)
(55, 13, 2, 1100, 21), -- Clemson Tigers (Away)
(56, 11, 1, 1300, 31), -- Colorado Buffaloes (Home)
(56, 15, 2, 1050, 17), -- North Carolina Tar Heels (Away),

-- Week 8
(57, 1, 1, 1300, 38), -- Alabama Crimson Tide (Home)
(57, 13, 2, 1000, 21), -- Clemson Tigers (Away)
(58, 2, 1, 1400, 31), -- Georgia Bulldogs (Home)
(58, 14, 2, 1100, 17), -- Florida State Seminoles (Away)
(59, 3, 1, 1200, 28), -- Florida Gators (Home)
(59, 16, 2, 1150, 21), -- Duke Blue Devils (Away)
(60, 6, 1, 1350, 34), -- Michigan Wolverines (Home)
(60, 9, 2, 1050, 17), -- Kansas State Wildcats (Away)
(61, 7, 1, 1400, 31), -- Penn State Nittany Lions (Home)
(61, 10, 2, 1000, 21), -- Kansas Jayhawks (Away)
(62, 8, 1, 1250, 27), -- Wisconsin Badgers (Home)
(62, 12, 2, 1100, 21), -- Iowa State Cyclones (Away)
(63, 5, 1, 1300, 31), -- Ohio State Buckeyes (Home)
(63, 11, 2, 1150, 24), -- Colorado Buffaloes (Away)
(64, 15, 1, 1400, 34), -- North Carolina Tar Heels (Home)
(64, 4, 2, 1050, 14); -- LSU Tigers (Away)


MERGE Football.GameTeam T
USING @GameTeamStaging S
ON T.GameId = S.GameId AND T.TeamId = S.TeamId AND T.TeamTypeId = S.TeamTypeId
WHEN NOT MATCHED THEN
   INSERT (GameId, TeamId, TeamTypeId, TopOfPossessionSec, Score)
   VALUES (S.GameId, S.TeamId, S.TeamTypeId, S.TopOfPossessionSec, S.Score);
