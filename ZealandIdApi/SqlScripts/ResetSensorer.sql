﻿USE [ZealandId];

-- Delete all data from the table 'Sensors'
DELETE FROM dbo.Lokaler;

-- Reset the identity seed for the table 'Sensors'
DBCC CHECKIDENT ('Lokaler', RESEED, 0);

