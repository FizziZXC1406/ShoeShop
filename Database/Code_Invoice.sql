use DatabaseProjectFinalExam
CREATE TABLE Invoice (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    Date DATETIME NOT NULL,
    CustomerName NVARCHAR(100) NOT NULL,
    CustomerPhoneNumber VARCHAR(20) NOT NULL,
    DeliveryAddress NVARCHAR(300) NOT NULL,
	TotalPayment int not null check (TotalPayment >= 0),
	StatusInvoice nvarchar(20) not null,
    FOREIGN KEY (UserID) REFERENCES UserAccount(ID)
);
