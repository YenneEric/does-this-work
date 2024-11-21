DECLARE @TeamStaging TABLE
(
    TeamName NVARCHAR(255) NOT NULL,
    [Location] NVARCHAR(255) NOT NULL,
    Mascot NVARCHAR(255),
    ConfId INT NOT NULL
);

-- SEC Teams
INSERT INTO @TeamStaging (TeamName, [Location], Mascot, ConfId)
VALUES
    ('Alabama Crimson Tide', 'Tuscaloosa, AL', 'Big Al', 4),
    ('Georgia Bulldogs', 'Athens, GA', 'Uga', 4),
    ('Florida Gators', 'Gainesville, FL', 'Albert Gator', 4),
    ('LSU Tigers', 'Baton Rouge, LA', 'Mike the Tiger', 4);

-- Big Ten Teams
INSERT INTO @TeamStaging (TeamName, [Location], Mascot, ConfId)
VALUES
    ('Ohio State Buckeyes', 'Columbus, OH', 'Brutus Buckeye', 2),
    ('Michigan Wolverines', 'Ann Arbor, MI', 'Wolverine', 2),
    ('Penn State Nittany Lions', 'University Park, PA', 'Nittany Lion', 2),
    ('Wisconsin Badgers', 'Madison, WI', 'Bucky Badger', 2);

-- Big-12 Teams (Kansas State, Kansas University, Colorado, Iowa State)
INSERT INTO @TeamStaging (TeamName, [Location], Mascot, ConfId)
VALUES
    ('Kansas State Wildcats', 'Manhattan, KS', 'Willie the Wildcat', 3),
    ('Kansas Jayhawks', 'Lawrence, KS', 'Big Jay', 3),
    ('Colorado Buffaloes', 'Boulder, CO', 'Ralphie', 3),
    ('Iowa State Cyclones', 'Ames, IA', 'Cy the Cardinal', 3);

-- ACC Teams
INSERT INTO @TeamStaging (TeamName, [Location], Mascot, ConfId)
VALUES
    ('Clemson Tigers', 'Clemson, SC', 'The Tiger', 1),
    ('Florida State Seminoles', 'Tallahassee, FL', 'Osceola and Renegade', 1),
    ('North Carolina Tar Heels', 'Chapel Hill, NC', 'Rameses', 1),
    ('Duke Blue Devils', 'Durham, NC', 'Blue Devil', 1);

-- Insert Staged Data into Football.Team
INSERT INTO Football.Team (TeamName, [Location], Mascot, ConfId)
SELECT TeamName, [Location], Mascot, ConfId
FROM @TeamStaging;
