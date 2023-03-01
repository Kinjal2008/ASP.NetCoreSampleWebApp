CREATE PROCEDURE [dbo].[SPOrder_Insert]
	@OrderName nvarchar(50),
	@OrderDate DateTime2(7),
	@FoodId int,
	@Quantity int,
	@Total money,
	@Id int output
AS
Begin
	set nocount on;

	insert into dbo.[Order] (OrderName, OrderDate, FoodId, Quantity, Total)
	values (@OrderName, @OrderDate, @FoodId, @Quantity, @Total);

	set @Id = SCOPE_IDENTITY();
End