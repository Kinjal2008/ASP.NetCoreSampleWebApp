CREATE PROCEDURE [dbo].[SPOrder_Delete]
	@Id int
AS
Begin
	set nocount on;

	delete from dbo.[Order] 
	where Id = @Id;

End