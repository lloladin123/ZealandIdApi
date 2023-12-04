USE [ZealandId];

-- Delete all data from the table 'Sensors'
DELETE FROM dbo.Sensorer;

-- Reset the identity seed for the table 'Sensors'
DBCC CHECKIDENT ('Sensorer', RESEED, 0);

