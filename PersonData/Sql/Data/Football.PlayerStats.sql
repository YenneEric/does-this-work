DECLARE @PlayerStatsStaging TABLE
(
    TeamPlayerId INT NOT NULL,
    GameId INT NOT NULL,
    TeamId INT NOT NULL,
    RushingYards INT NOT NULL,
    ReceivingYards INT NOT NULL,
    ThrowingYards INT NOT NULL,
    Tackles INT NOT NULL,
    Sacks INT NOT NULL,
    Turnovers INT NOT NULL,
    InterceptionsCaught INT NOT NULL,
    Touchdowns INT NOT NULL,
    Punts INT NOT NULL,
    FieldGoalsMade INT NOT NULL
);



INSERT INTO @PlayerStatsStaging (TeamPlayerId, GameId, TeamId, RushingYards, ReceivingYards, ThrowingYards, Tackles, Sacks, Turnovers, InterceptionsCaught, Touchdowns, Punts, FieldGoalsMade)
VALUES
-- Game 1: Alabama (35) vs Georgia (28)
-- Alabama Players
(1, 1, 1, 40, 50, 250, 8, 1, 0, 0, 2, 2, 1), -- QB
(2, 1, 1, 120, 0, 0, 7, 1, 0, 0, 2, 0, 0), -- RB
(3, 1, 1, 100, 0, 0, 6, 0, 0, 0, 1, 0, 0), -- RB
(4, 1, 1, 0, 100, 0, 5, 0, 0, 0, 1, 0, 0), -- WR
(5, 1, 1, 0, 70, 0, 4, 0, 0, 0, 1, 0, 0), -- WR
(6, 1, 1, 0, 60, 0, 4, 0, 0, 0, 0, 0, 0), -- WR
(7, 1, 1, 0, 0, 0, 5, 1, 1, 1, 0, 0, 0), -- Defensive Player

-- Georgia Players
(18, 1, 2, 50, 40, 200, 7, 1, 1, 0, 2, 1, 0), -- QB
(19, 1, 2, 80, 0, 0, 6, 0, 0, 0, 2, 0, 0), -- RB
(20, 1, 2, 60, 20, 0, 6, 0, 0, 0, 1, 0, 0), -- RB
(21, 1, 2, 0, 90, 0, 5, 0, 0, 0, 1, 0, 0), -- WR
(22, 1, 2, 0, 70, 0, 4, 0, 0, 0, 1, 0, 0), -- WR
(23, 1, 2, 0, 50, 0, 5, 1, 0, 1, 0, 0, 0), -- Defensive Player

-- Game 2: Florida (21) vs LSU (14)
-- Florida Players
(35, 2, 3, 30, 50, 250, 7, 0, 0, 0, 2, 0, 0), -- QB
(36, 2, 3, 100, 0, 0, 6, 1, 0, 0, 2, 0, 0), -- RB
(37, 2, 3, 60, 40, 0, 5, 0, 0, 0, 1, 0, 0), -- WR
(38, 2, 3, 0, 60, 0, 5, 0, 0, 0, 1, 0, 0), -- WR
(39, 2, 3, 0, 30, 0, 6, 0, 0, 0, 0, 0, 0), -- WR

-- LSU Players
(52, 2, 4, 40, 20, 200, 6, 1, 0, 1, 2, 0, 0), -- QB
(53, 2, 4, 90, 0, 0, 5, 0, 1, 0, 2, 0, 0), -- RB
(54, 2, 4, 50, 30, 0, 6, 0, 0, 0, 0, 0, 0), -- WR
(55, 2, 4, 0, 50, 0, 6, 0, 0, 0, 0, 0, 0), -- WR
(56, 2, 4, 0, 0, 0, 5, 1, 1, 0, 0, 0, 0), -- Defensive Player

-- Game 3: Ohio State (31) vs Michigan (24)
-- Ohio State Players
(69, 3, 5, 20, 30, 280, 8, 1, 0, 0, 3, 2, 1), -- QB
(70, 3, 5, 120, 0, 0, 7, 1, 0, 0, 2, 0, 0), -- RB
(71, 3, 5, 90, 20, 0, 6, 0, 0, 0, 1, 0, 0), -- WR
(72, 3, 5, 0, 80, 0, 5, 0, 0, 0, 1, 0, 0), -- WR

-- Michigan Players
(86, 3, 6, 30, 40, 240, 6, 0, 1, 0, 2, 0, 0), -- QB
(87, 3, 6, 100, 0, 0, 6, 0, 0, 0, 2, 0, 0), -- RB
(88, 3, 6, 80, 30, 0, 6, 1, 0, 0, 1, 0, 0), -- WR
(89, 3, 6, 0, 70, 0, 5, 0, 0, 0, 1, 0, 0), -- WR


-- Game 4: Penn State (27) vs Wisconsin (30)
-- Penn State Players
(103, 4, 7, 40, 20, 200, 8, 1, 0, 1, 2, 1, 0), -- QB
(104, 4, 7, 120, 0, 0, 6, 1, 0, 0, 2, 0, 0), -- RB
(105, 4, 7, 80, 30, 0, 5, 0, 0, 0, 1, 0, 0), -- WR
(106, 4, 7, 0, 100, 0, 6, 0, 0, 0, 1, 0, 0), -- WR

-- Wisconsin Players
(120, 4, 8, 30, 40, 240, 7, 1, 0, 0, 3, 1, 0), -- QB
(121, 4, 8, 90, 0, 0, 6, 1, 0, 0, 2, 0, 0), -- RB
(122, 4, 8, 60, 50, 0, 6, 0, 0, 0, 1, 0, 0), -- WR
(123, 4, 8, 0, 70, 0, 5, 0, 0, 0, 1, 0, 0), -- WR

-- Game 5: Kansas State (20) vs Kansas Jayhawks (34)
-- Kansas State Players
-- Quarterback
    (137, 5, 9, 50, 10, 180, 7, 1, 1, 1, 2, 1, 0), -- Clark Griswold

    -- Running Backs
    (138, 5, 9, 100, 0, 0, 6, 0, 0, 0, 2, 0, 0), -- Ellen Griswold
    (139, 5, 9, 120, 0, 0, 5, 0, 0, 0, 3, 0, 0), -- Rusty Griswold

    -- Wide Receivers
    (140, 5, 9, 0, 90, 0, 5, 0, 0, 0, 1, 0, 0), -- Audrey Griswold
    (141, 5, 9, 0, 100, 0, 4, 0, 0, 0, 1, 0, 0), -- Cousin Eddie
    (142, 5, 9, 0, 80, 0, 4, 0, 0, 0, 1, 0, 0), -- Catherine Johnson

    -- Tight Ends
    (143, 5, 9, 0, 50, 0, 3, 0, 0, 0, 1, 0, 0), -- Uncle Lewis
    (144, 5, 9, 0, 40, 0, 3, 0, 0, 0, 1, 0, 0), -- Aunt Bethany
    (145, 5, 9, 0, 60, 0, 3, 0, 0, 0, 1, 0, 0), -- Ruby Sue

    -- Linebackers
    (146, 5, 9, 0, 0, 0, 15, 5, 2, 0, 0, 0, 0), -- Todd Chester
    (147, 5, 9, 0, 0, 0, 14, 4, 1, 0, 0, 0, 0), -- Margo Chester

    -- Cornerbacks
    (148, 5, 9, 0, 0, 0, 10, 3, 1, 1, 0, 0, 0), -- Frank Shirley
    (149, 5, 9, 0, 0, 0, 10, 2, 0, 1, 0, 0, 0), -- Mary

    -- Safeties
    (150, 5, 9, 0, 0, 0, 8, 1, 1, 2, 0, 0, 0), -- Rocky
    (151, 5, 9, 0, 0, 0, 8, 1, 0, 1, 0, 0, 0), -- Snots

    -- Kicker
    (152, 5, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3), -- Art

    -- Punter
    (153, 5, 9, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0), -- Eddie Jr.

-- Kansas Jayhawks Players
(154, 5, 10, 20, 30, 260, 8, 2, 0, 0, 3, 1, 1), -- QB
(155, 5, 10, 110, 0, 0, 7, 1, 0, 0, 2, 0, 0), -- RB
(156, 5, 10, 80, 50, 0, 6, 0, 0, 0, 1, 0, 0), -- WR
(157, 5, 10, 0, 60, 0, 5, 0, 0, 0, 1, 0, 0), -- WR

-- Game 6: Colorado (24) vs Iowa State (27)
-- Colorado Players
(171, 6, 11, 40, 20, 220, 7, 1, 0, 1, 2, 1, 0), -- QB
(172, 6, 11, 90, 0, 0, 6, 0, 1, 0, 2, 0, 0), -- RB
(173, 6, 11, 50, 50, 0, 6, 0, 0, 0, 1, 0, 0), -- WR
(174, 6, 11, 0, 70, 0, 5, 0, 0, 0, 1, 0, 0), -- WR

-- Iowa State Players
(188, 6, 12, 30, 40, 240, 8, 1, 0, 0, 3, 1, 1), -- QB
(189, 6, 12, 100, 0, 0, 7, 1, 0, 0, 2, 0, 0), -- RB
(190, 6, 12, 80, 30, 0, 6, 0, 0, 0, 1, 0, 0), -- WR
(191, 6, 12, 0, 60, 0, 5, 0, 0, 0, 1, 0, 0), -- WR

-- Game 7: Clemson (38) vs Florida State (17)
-- Clemson Players
(205, 7, 13, 50, 40, 280, 9, 1, 0, 0, 4, 2, 1), -- QB
(206, 7, 13, 120, 0, 0, 8, 1, 0, 0, 3, 0, 0), -- RB
(207, 7, 13, 90, 60, 0, 7, 0, 0, 0, 2, 0, 0), -- WR
(208, 7, 13, 0, 80, 0, 6, 0, 0, 0, 1, 0, 0), -- WR

-- Florida State Players
(222, 7, 14, 30, 40, 200, 7, 0, 1, 1, 2, 1, 0), -- QB
(223, 7, 14, 90, 0, 0, 6, 0, 0, 0, 2, 0, 0), -- RB
(224, 7, 14, 60, 20, 0, 5, 0, 0, 0, 1, 0, 0), -- WR
(225, 7, 14, 0, 60, 0, 5, 0, 0, 0, 1, 0, 0), -- WR

-- Game 8: North Carolina (28) vs Duke (24)
-- North Carolina Players
(239, 8, 15, 30, 50, 240, 8, 1, 0, 0, 3, 1, 0), -- QB
(240, 8, 15, 100, 0, 0, 7, 0, 0, 0, 2, 0, 0), -- RB
(241, 8, 15, 70, 30, 0, 6, 0, 0, 0, 1, 0, 0), -- WR
(242, 8, 15, 0, 70, 0, 5, 0, 0, 0, 1, 0, 0), -- WR

-- Duke Players
(256, 8, 16, 20, 40, 200, 7, 0, 1, 0, 2, 1, 0), -- QB
(257, 8, 16, 90, 0, 0, 6, 0, 0, 0, 2, 0, 0), -- RB
(258, 8, 16, 60, 30, 0, 5, 0, 0, 0, 1, 0, 0), -- WR
(259, 8, 16, 0, 60, 0, 5, 0, 0, 0, 1, 0, 0), -- WR





--Game 9

--(9, 2, 1, 1200, 28), -- Georgia Bulldogs (Home)
--(9, 3, 2, 1100, 20), -- Florida Gators (Away)

(18, 9, 2, 50, 40, 200, 7, 1, 1, 0, 20, 1, 0), -- QB
(19, 9, 2, 80, 0, 0, 6, 0, 0, 0, 2, 0, 0), -- RB
(20, 9, 2, 60, 20, 0, 6, 0, 0, 0, 1, 0, 0), -- RB
(21, 9, 2, 0, 90, 0, 5, 0, 0, 0, 1, 0, 0), -- WR
(22, 9, 2, 0, 70, 0, 4, 0, 0, 0, 1, 0, 0), -- WR
(23, 9, 2, 0, 50, 0, 5, 1, 0, 1, 0, 0, 0); -- Defensive Player





MERGE INTO Football.PlayerStats AS PS
USING @PlayerStatsStaging AS S
   ON PS.TeamPlayerId = S.TeamPlayerId AND PS.GameId = S.GameId AND PS.TeamId = S.TeamId
WHEN MATCHED THEN
   UPDATE 
   SET 
      RushingYards = S.RushingYards,
      ReceivingYards = S.ReceivingYards,
      ThrowingYards = S.ThrowingYards,
      Tackles = S.Tackles,
      Sacks = S.Sacks,
      Turnovers = S.Turnovers,
      InterceptionsCaught = S.InterceptionsCaught,
      Touchdowns = S.Touchdowns,
      Punts = S.Punts,
      FieldGoalsMade = S.FieldGoalsMade
WHEN NOT MATCHED THEN
   INSERT (TeamPlayerId, GameId, TeamId, RushingYards, ReceivingYards, ThrowingYards, Tackles, Sacks, Turnovers, InterceptionsCaught, Touchdowns, Punts, FieldGoalsMade)
   VALUES (S.TeamPlayerId, S.GameId, S.TeamId, S.RushingYards, S.ReceivingYards, S.ThrowingYards, S.Tackles, S.Sacks, S.Turnovers, S.InterceptionsCaught, S.Touchdowns, S.Punts, S.FieldGoalsMade);