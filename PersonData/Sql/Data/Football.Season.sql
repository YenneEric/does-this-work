DECLARE @SeasonStaging TABLE
(
   [Year] INT NOT NULL UNIQUE
);


INSERT @SeasonStaging([Year])
VALUES
   (2019),
   (2020),
   (2021),
   (2022),
   (2023),
   (2024);


MERGE Football.Season T
USING @SeasonStaging S ON S.[Year] = T.[Year]
WHEN NOT MATCHED THEN
   INSERT([Year])
   VALUES(S.[Year]);
