---------------------------------------------------------------------USER------------------------------------------------------------------------------------
Create table UserRegister(
UserId int primary key identity(1,1),
FullName varchar(30),
EmailId varchar(MAX),
Password varchar(20),
MobileNumber varchar(20)
);

select * from UserRegister
UPDATE UserRegister SET IsAdmin = 'Admin' WHERE UserId = 2;
UPDATE UserRegister SET IsAdmin = 'Admin' WHERE UserId = 3;
UPDATE UserRegister SET IsAdmin = 'User' WHERE UserId = 1;

Alter PROCEDURE spUserRegistration
(
    @FullName varchar(50),
    @EmailId varchar(50),
    @Password varchar(50),
    @MobileNumber varchar(20),
	@IsAdmin bit 
)
AS
BEGIN
begin try  
    INSERT INTO UserRegister (FullName, EmailId, Password, MobileNumber,IsAdmin)
    VALUES (@FullName, @EmailId, @Password, @MobileNumber,@IsAdmin)
end try
begin catch
Select ERROR_MESSAGE() As ErrorMessage;
end catch
END

-- Create the stored procedure named 'GetUserByEmail'
Alter PROCEDURE GetUser(
@EmailId varchar(Max)
)
As
Begin Select * from UserRegister where EmailId=@EmailId
End




Alter Procedure spResetPassword(
@FullName varchar(50),
@EmailId varchar(MAX),
@Password varchar(50),
@MobileNumber varchar(50),
@IsAdmin bit
)
As
BEGIN
begin try
	update UserRegister set FullName=@FullName,MobileNumber=@MobileNumber,EmailId=@EmailId,Password=@Password ,IsAdmin=@IsAdmin where EmailId=@EmailId
end try
begin catch
Select ERROR_MESSAGE() As ErrorMessage;
end catch
END

----------------------------------------------------------------------BOOK------------------------------------------------------------------------------------
Create table Book(
BookId int primary key identity(1,1),
BookName varchar(50),
BookDescription varchar(MAX),
BookAuthor varchar(50),
BookImage varchar(50),
BookCount int,
BookPrice int,
Rating int,
);

select * from book;

drop table ;

Alter table Book 
Add UserId int foreign key references UserRegister(UserId);

Update Book set UserId=1 Where BookId=7;
select * from Book

Alter procedure spAddBook
(

@BookName varchar(50),
@BookDescription varchar(MAX),
@BookAuthor varchar(50),
@BookImage varchar(50),
@BookCount int,
@BookPrice int,
@Rating int
)
As
BEGIN
begin try
	Insert into Book(BookName,BookDescription,BookAuthor,BookImage,BookCount,BookPrice,Rating) values( @BookName,@BookDescription,@BookAuthor,@BookImage,@BookCount,@BookPrice,@Rating)
end try
begin catch
Select ERROR_MESSAGE() AS ErrorMessage;
end catch
End



Create procedure spGetAllBooks
As
BEGIN
begin try
	Select * from Book
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End

Create procedure spUpdateBook
(
@BookId int,
@BookName varchar(50),
@BookDescription varchar(MAX),
@BookAuthor varchar(50),
@BookImage varchar(50),
@BookCount int,
@BookPrice int,
@Rating int
)
As 
BEGIN
begin try
	Update Book set BookName=@BookName, BookDescription=@BookDescription, BookAuthor=@BookAuthor, BookImage=@BookImage, BookCount=@BookCount, BookPrice=@BookPrice, Rating=@Rating
    where BookId=@BookId
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End


Create procedure spDeleteBook
(
	@BookId int
)
As
BEGIN
begin try
	Delete from Book where BookId=@BookId;
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End

Create procedure spUploadImage
(
	@BookId int,
	@FileLink varchar(max)
)
As
BEGIN
begin try
	update Book set BookImage = @FileLink where BookId=@BookId
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End

Create procedure spGetBookById
(
	@BookId int
)
As
BEGIN
begin try
	Select * from Book where BookId=@BookId;
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End

---------------------------------------------------------------------WISHLIST------------------------------------------------------------------------------------
CREATE TABLE WishList
(
    WishListId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT foreign key references UserRegister(UserId) ,
    BookId INT foreign key references Book(BookId),
    
);

alter table WishList add IsAvailable bit default 1

ALTER TABLE WishList
ADD BookCount INT NOT NULL ; -- Assuming BookCount cannot be NULL and setting a default value of 1



Create procedure spAddToWishList
(
	@UserId int,
	@BookId int
)
AS
BEGIN
begin try
	Insert into WishList (UserId, BookId) values (@UserId, @BookId)
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
END

select * from wishlist
delete from wishList where WishListId=6

alter procedure spGetAllWishList
(
	@UserId int
)
as begin
Begin try
if not  exists (select 1 from Wishlist where UserId = @UserId and (IsAvailable = 0))
begin
	select * from 
		Wishlist INNER JOIN
		 Book on Book.BookId = Wishlist.BookId 
		 where Wishlist.UserId = @UserId and WishList.IsAvailable=1
		 end
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
END

exec spGetAllWishList 1

delete from wishlist where WishListId=4;



CREATE PROCEDURE spUpdateWishList
    @WishListId INT,
    @UserId INT,
    @BookId INT,
    @BookCount INT
AS
BEGIN
begin try
   
    UPDATE WishList
    SET UserId = @UserId,
        BookId = @BookId,
        BookCount = @BookCount
    WHERE WishListId = @WishListId
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
END



Create procedure spDeleteWishList
(
	@WishListId int
)
As
Begin
begin try
	Delete from WishList where WishListId=@WishListId;
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End

------------------------------------------------------------------------CART------------------------------------------------------------------------------------
CREATE TABLE Cart
(
    CartId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT foreign key references UserRegister(UserId),
    BookId INT  foreign key references Book(BookId),
	BookCount int NOT NULL,
	IsAvailable int,
);

select * from Cart

alter table Cart add IsAvailable bit default 1

Alter table Cart Add BookCount INT NOT NULL DEFAULT 1;

drop table cart

UPDATE WishList
SET IsAvailable = 1
WHERE IsAvailable IS NULL

select * from Cart


Create procedure spAddToCart
(

	@UserId int,
	@BookId int,
	@BookCount int
	
	
)
AS
BEGIN
begin try
	Insert into Cart ( UserId, BookId,BookCount) values ( @UserId, @BookId,@BookCount)
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
END

Drop Procedure spAddToCart;


alter procedure spGetAllCart
(
	@UserId int
)
as begin
Begin try
if   exists (select 1 from Cart where UserId = @UserId and (IsAvailable = 0))
begin
	select * from 
		Cart INNER JOIN Book on Book.BookId = Cart.BookId 
		 where Cart.UserId = @UserId and Cart.IsAvailable=1
		 end
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
END
 select *from cart

exec spGetAllCart 1

alter procedure spUpdateCart(
    @UserId int,
	@BookId int,
	@BookCount int
)
As 
Begin
Begin try
Update Cart set BookCount = @BookCount
where BookId=@BookId and UserId = @UserId;
End try
Begin catch
Select Error_Message() As ErrorMessage;
End Catch
End



create procedure spDeleteCart
(

	@BookId int
)
As
Begin
begin try
	Delete from Cart where BookId=@BookId;
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End

-----------------------------------------------------------------------TYPE------------------------------------------------------------------------------------

Create table Type(
TypeId INT PRIMARY KEY IDENTITY(1,1),
TypeName VARCHAR(50)
);

INSERT INTO Type (TypeName) VALUES ('home'),
       ('office');

----------------------------------------------------------------CUSTOMER DETAILS------------------------------------------------------------------------------------
CREATE TABLE CustomerDetails (
	CustomerId int primary key identity(1,1),
    FullName VARCHAR(50),
    MobileNumber VARCHAR(50),
    Address VARCHAR(MAX),
    CityOrTown VARCHAR(50),
    State VARCHAR(50),
    TypeId INT foreign key references Type(TypeId), 
    UserId INT foreign key references UserRegister(UserId),
);





CREATE PROCEDURE spAddDetails 
    @FullName VARCHAR(50),
    @MobileNumber VARCHAR(50),
    @Address VARCHAR(MAX),
    @CityOrTown VARCHAR(50),
    @State VARCHAR(50),
    @TypeId INT, 
    @UserId INT
AS
BEGIN
begin try
    Insert into CustomerDetails (FullName, MobileNumber, Address, CityOrTown, State, UserId, TypeId) 
    values (@FullName, @MobileNumber, @Address, @CityOrTown, @State, @UserId, @TypeId)
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End


Alter procedure spUpdateAddress(
@CustomerId int,
@UserId int,
@TypeId int,
@FullName varchar(50),
@MobileNumber varchar(15),
@Address varchar(max),
@CityOrTown varchar(max),
@State varchar(50)
)
As 
Begin
Begin try
Update CustomerDetails set UserId = @UserId, TypeId = @TypeId, FullName = @FullName,
MobileNumber = @MobileNumber, Address = @Address, CityOrTown = @CityOrTown, State = @State  
where CustomerId=@CustomerId and UserId=@UserId;
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End


Alter procedure spDeleteAddress
(
	@CustomerId int,
	@UserId int
)
As
Begin
begin try
	Delete from CustomerDetails where CustomerId=@CustomerId and UserId=@UserId;
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End


Alter procedure spGetAddressById
(
	@UserId int
)
As
BEGIN
begin try
	Select * from CustomerDetails INNER JOIN Type on Type.TypeId = CustomerDetails.TypeId 
		 where CustomerDetails.UserId = @UserId;
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End

----------------------------------------------------------------CUSTOMER FEEDBACK------------------------------------------------------------------------------------
Create table CustomerFeedback
(
	FeedbackId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT foreign key references UserRegister(UserId) ,
    BookId INT foreign key references Book(BookId) ,
	CustomerDescription VARCHAR(MAX),
	Ratings int,
);



CREATE PROCEDURE spAddFeedback 

    @UserId INT ,
    @BookId INT ,
	@CustomerDescription VARCHAR(MAX),
	@Ratings int
   
AS
BEGIN
begin try
    Insert into CustomerFeedback ( UserId, BookId, CustomerDescription, Ratings) 
    values ( @UserId, @BookId, @CustomerDescription, @Ratings)
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End

create procedure spGetAllFeedback
(
	@UserId int
)
As
BEGIN
begin try
	Select * from CustomerFeedback INNER JOIN 
	Book on Book.BookId = CustomerFeedback.BookId where CustomerFeedback.UserId=@UserId
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
End

---------------------------------------------------------------------ORDER PLACED------------------------------------------------------------------------------------

create table OrderPlaced
(
OrderId int primary key identity(1,1),
CustomerId int foreign key references CustomerDetails(CustomerId),
CartId int foreign key references Cart(CartId)
);

select * from OrderPlaced

alter procedure spPlaceOrder
(

	@CustomerId int,
	@CartId int
	
)
AS
BEGIN
begin try
	Insert into OrderPlaced ( CustomerId, CartId) values ( @CustomerId, @CartId)
	update Cart set IsAvailable = 0 where CartId = @CartId
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
END

----------------------------------------------------------------------ORDER SUMMARY------------------------------------------------------------------------------------
create table OrderSummary
(
summaryId int primary key identity(1,1),
OrderId int foreign key references OrderPlaced(OrderId)
);





Create procedure spGetSummary
(

	@UserId int,
	@OrderId int
)
AS
BEGIN
begin try
	Insert into OrderSummary(OrderId) values (@OrderId)
	SELECT
		OS.summaryId,
		OS.OrderId,
		OP.CustomerId,
		OP.CartId,
		C.UserId,
		C.BookId,
		C.BookCount,
		Book.BookId,
		Book.BookName,
		Book.BookDescription,
		Book.BookAuthor,
		Book.BookImage,
	    Book.BookCount,
        Book.BookPrice,
        Book.Rating

	FROM
		OrderSummary OS
	JOIN
		OrderPlaced OP ON OS.OrderId = OP.OrderId
	JOIN
		Cart C ON OP.CartId = C.CartId
	JOIN
		Book ON C.BookId=Book.BookId where C.UserId=@UserId;
end try
begin catch
Select Error_Message() As ErrorMessage;
end catch
END

drop table cart
drop table OrderPlaced
drop  OrderSummary
selet * from  Book;
drop table  WishList
drop table CustomerDetails

Select * from CustomerFeedback
select * from cart
select * from customerdetails
select * from orderplaced

	












