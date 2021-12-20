CREATE OR REPLACE PROCEDURE Update_Seat_Status()
IS
BEGIN
		FOR c IN (SELECT SeatNo, Start_Time, End_Time FROM Bookings) LOOP
			IF (c.Start_Time <= SYSDATETIME AND c.End_Time >= SYSDATETIME) THEN
				UPDATE Seats SET CurrentStatus = 'Occupied' WHERE SeatNo = c.SeatNo;
			ELSE
				UPDATE Seats SET CurrentStatus = 'Free' WHERE SeatNo = c.SeatNo;
			END IF;
		END LOOP;
END;