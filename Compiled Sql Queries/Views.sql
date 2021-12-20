CREATE VIEW Leaderboard_Details AS 
SELECT L.GameRank AS "Rank", A.Username AS "Gamer Tag", C.CustName AS "Name", G.GameName AS "Game"
FROM Leaderboard AS L	JOIN Customers AS C 
							ON L.CustomerID = C.CustomerID
						JOIN Accounts AS A 
							ON C.AccountNo = A.AccountNo
						JOIN Games AS G 
							ON L.GameID = G.GameID;

CREATE VIEW Customer_Profile AS
SELECT CustomerID, CustName, PhoneNo, Username, AccPassword 
FROM Accounts AS A JOIN Customers AS C 
ON A.AccountNo = C.AccountNo;