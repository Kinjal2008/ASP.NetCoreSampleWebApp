CREATE PROCEDURE [dbo].[SPOrder_Update]
	@Id int,
	@OrderName nvarchar(50)
AS
Begin
	set nocount on;

	update dbo.[Order] 
	set OrderName = @OrderName
	where Id = @Id;

End