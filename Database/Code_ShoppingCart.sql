use DatabaseProjectFinalExam
CREATE TABLE ShoppingCart (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID VARCHAR(10) NOT NULL,
    SizeID VARCHAR(10) NOT NULL,
    Image VARBINARY(MAX) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Price INT NOT NULL,
    Size INT NOT NULL,
    Quantity INT NOT NULL,
	UserID int not null,
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
	FOREIGN KEY (UserID) REFERENCES UserAccount(ID),
    FOREIGN KEY (SizeID, ProductID) REFERENCES ProductSize(SizeID, ProductID)
);

SELECT ProductSize.ProductID, Image, Name, Price, ProductSize.Quantity, ProductSize.Size
FROM Product, ProductSize
where Product.ProductID = ProductSize.ProductID

SELECT *
From ShoppingCart sc
join Product p
on sc.ProductID = p.ProductID
where p.AdminID = 'AD0101';