CREATE PROCEDURE [dbo].[SPOrder_GetById]
	@Id int
AS
Begin
	set nocount on;
	select [Id], [OrderName], [OrderDate], [FoodId], [Quantity], [Total]
	from dbo.[Order]
	where Id = @Id;
End
