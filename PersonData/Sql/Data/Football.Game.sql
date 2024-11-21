DECLARE @GameStaging TABLE
(
    [Date] DATE NOT NULL,
    [Location] NVARCHAR(255) NOT NULL,
    Canceled INT NOT NULL DEFAULT(0)
);

INSERT INTO @GameStaging ([Date], [Location], Canceled)
VALUES
-- Week 1
('2019-09-01', 'Tuscaloosa, AL', 0),
('2019-09-01', 'Gainesville, FL', 0),
('2019-09-01', 'Columbus, OH', 0),
('2019-09-01', 'University Park, PA', 0),
('2019-09-01', 'Manhattan, KS', 0),
('2019-09-01', 'Boulder, CO', 0),
('2019-09-01', 'Clemson, SC', 0),
('2019-09-01', 'Chapel Hill, NC', 0),

-- Week 2
('2019-09-08', 'Athens, GA', 0),
('2019-09-08', 'Baton Rouge, LA', 0),
('2019-09-08', 'Ann Arbor, MI', 0),
('2019-09-08', 'Madison, WI', 0),
('2019-09-08', 'Lawrence, KS', 0),
('2019-09-08', 'Ames, IA', 0),
('2019-09-08', 'Tallahassee, FL', 0),
('2019-09-08', 'Durham, NC', 0),

-- Week 3
('2019-09-15', 'Tuscaloosa, AL', 0),
('2019-09-15', 'Athens, GA', 0),
('2019-09-15', 'University Park, PA', 0),
('2019-09-15', 'Columbus, OH', 0),
('2019-09-15', 'Manhattan, KS', 0),
('2019-09-15', 'Lawrence, KS', 0),
('2019-09-15', 'Clemson, SC', 0),
('2019-09-15', 'Tallahassee, FL', 0),

-- Week 4
('2019-09-22', 'Gainesville, FL', 0),
('2019-09-22', 'Ann Arbor, MI', 0),
('2019-09-22', 'Athens, GA', 0),
('2019-09-22', 'Madison, WI', 0),
('2019-09-22', 'Ames, IA', 0),
('2019-09-22', 'Boulder, CO', 0),
('2019-09-22', 'Durham, NC', 0),
('2019-09-22', 'Clemson, SC', 0),

-- Week 5
('2019-09-29', 'Tuscaloosa, AL', 0),
('2019-09-29', 'Athens, GA', 0),
('2019-09-29', 'Ann Arbor, MI', 0),
('2019-09-29', 'Baton Rouge, LA', 0),
('2019-09-29', 'Manhattan, KS', 0),
('2019-09-29', 'Lawrence, KS', 0),
('2019-09-29', 'Ames, IA', 0),
('2019-09-29', 'Boulder, CO', 0),

-- Week 6
('2019-10-06', 'Gainesville, FL', 0),
('2019-10-06', 'Tuscaloosa, AL', 0),
('2019-10-06', 'Athens, GA', 0),
('2019-10-06', 'Madison, WI', 0),
('2019-10-06', 'Clemson, SC', 0),
('2019-10-06', 'Tallahassee, FL', 0),
('2019-10-06', 'Chapel Hill, NC', 0),
('2019-10-06', 'Durham, NC', 0),

-- Week 7
('2019-10-13', 'Ann Arbor, MI', 0),
('2019-10-13', 'Gainesville, FL', 0),
('2019-10-13', 'Tuscaloosa, AL', 0),
('2019-10-13', 'University Park, PA', 0),
('2019-10-13', 'Lawrence, KS', 0),
('2019-10-13', 'Manhattan, KS', 0),
('2019-10-13', 'Ames, IA', 0),
('2019-10-13', 'Boulder, CO', 0),

-- Week 8
('2019-10-20', 'Tuscaloosa, AL', 0),
('2019-10-20', 'Athens, GA', 0),
('2019-10-20', 'Gainesville, FL', 0),
('2019-10-20', 'Ann Arbor, MI', 0),
('2019-10-20', 'University Park, PA', 0),
('2019-10-20', 'Madison, WI', 0),
('2019-10-20', 'Columbus, OH', 0),
('2019-10-20', 'Chapel Hill, NC', 0);



MERGE Football.Game T
USING @GameStaging S 
ON T.[Date] = S.[Date] AND T.[Location] = S.[Location] AND T.Canceled = S.Canceled
WHEN NOT MATCHED THEN
   INSERT ([Date], [Location], Canceled)
   VALUES (S.[Date], S.[Location], S.Canceled);
