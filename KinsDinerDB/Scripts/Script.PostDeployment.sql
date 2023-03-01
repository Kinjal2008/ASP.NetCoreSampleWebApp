/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
if not exists (select * from dbo.Food)
    begin
        insert into dbo.Food(Title, [Description], price) 
        values
        ('Cheeseburger Meal', 'Cheese burger, fries, and a drink', 5.95),
         ('Chili Paneer Meal', 'Chili paneer, fries, and a drink', 4.15),
          ('Vegan Meal', 'A large Salad and a juice', 1.95);
    end