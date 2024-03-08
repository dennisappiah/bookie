CREATE PROCEDURE [dbo].[spGuests_Insert]
	@firstName nvarchar(50),
	@lastName nvarchar(50)
AS
begin
	set nocount on;

	if not exists (select 1 from dbo.Guests where FirstName = @firstName and LastName = @lastName)
	begin
	-- if guest not exist then, add a new guest record
		insert into dbo.Guests(FirstName, LastName)
		values (@firstName, @lastName);
	end


end
