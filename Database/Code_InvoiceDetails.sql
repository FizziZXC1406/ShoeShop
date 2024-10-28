use DatabaseProjectFinalExam
CREATE TABLE InvoiceDetails (
    InvoiceDetailsID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID int NOT NULL,
    ProductID VARCHAR(10) NOT NULL,
    SizeID VARCHAR(10) NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
	Size int not null,
    Quantity INT NOT NULL CHECK (Quantity > 0),
	Price int not null check (Price >= 0),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(ID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    FOREIGN KEY (SizeID, ProductID) REFERENCES ProductSize(SizeID, ProductID)
);
