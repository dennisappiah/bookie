CREATE PROCEDURE [dbo].[spBookings_Search]
	@lastName nvarchar(50),
	@startDate date
AS
begin
		set nocount on;

		select b.Id, b.RoomId, b.GuestId, b.StartDate, b.EndDate, b.CheckedIn, b.TotalCost,
		g.FirstName, g.LastName, r.RoomNumber, r.RoomTypeId, rt.Title as RoomTypeTitle, rt.Description, rt.Price
		from dbo.Bookings b 
		inner join dbo.Guests g on b.guestId = g.Id
		inner join dbo.Rooms r on b.roomId = r.Id
		inner join dbo.RoomTypes rt on r.roomTypeId = rt.Id
		where b.StartDate = @startDate and g.LastName = @lastName

end