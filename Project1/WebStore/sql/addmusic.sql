USE ProductDb
GO



-- Add new music category
declare @musicCategoryId int

insert into Category (CategoryName)
		values ('Music')
SELECT @musicCategoryId=SCOPE_IDENTITY()

declare @productId int

-- Add new music products
insert into Product (ProductName, ProductPrice, ProductQuantity)
		values ('Kate Bush: Kick Inside', 19.99, 30)
SELECT @productId=SCOPE_IDENTITY()
insert into ProductCategory (CategoryId, ProductId)
		values(@musicCategoryId, @productId)

insert into Product (ProductName, ProductPrice, ProductQuantity)
		values ('Steve Winwood: High Life', 17.99, 22)
SELECT @productId=SCOPE_IDENTITY()
insert into ProductCategory (CategoryId, ProductId)
		values(@musicCategoryId, @productId)
GO