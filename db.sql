CREATE DATABASE TestDb
GO

USE TestDb
GO

CREATE TABLE Nodes
(
	NodeId INT NOT NULL IDENTITY,
	Nr INT NOT NULL

	PRIMARY KEY (NodeId)
)
GO
CREATE UNIQUE NONCLUSTERED INDEX ix_nodes_nr ON Nodes (Nr)
GO

CREATE TABLE NodesData
(
	DataId INT NOT NULL IDENTITY,
	NodeId INT NOT NULL,
	Created DATETIME2 NOT NULL

	PRIMARY KEY (DataId),
	FOREIGN KEY (NodeId) REFERENCES Nodes (NodeId)
)
GO


-- Test data --
DECLARE @NewNodeId INT 

INSERT Nodes (Nr) 
SELECT 1

SET @NewNodeId = SCOPE_IDENTITY()

INSERT NodesData (NodeId, Created) 
SELECT @NewNodeId, GETUTCDATE() UNION ALL
SELECT @NewNodeId, GETUTCDATE()



---
--- Scaffold database --

/*
dotnet ef dbcontext scaffold "Server=192.168.0.15;Database=TestDb;User Id=TestUser;Password=TestUser" Microsoft.EntityFrameworkCore.SqlServer --context TestDbContext --output-dir "Db/EF" --force
*/