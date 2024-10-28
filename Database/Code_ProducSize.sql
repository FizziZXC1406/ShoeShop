use DatabaseProjectFinalExam
CREATE TABLE ProductSize (
    SizeID VARCHAR(10) NOT NULL,
    ProductID VARCHAR(10) NOT NULL,
    Size INT NOT NULL,
    Quantity INT NOT NULL,
    PRIMARY KEY (SizeID, ProductID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0001', 'P0001', 37, 40),
    ('S0002', 'P0001', 38, 40),
    ('S0003', 'P0001', 39, 40),
	('S0004', 'P0001', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0005', 'P0002', 37, 40),
    ('S0006', 'P0002', 38, 40),
    ('S0007', 'P0002', 39, 40),
	('S0008', 'P0002', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0009', 'P0003', 37, 40),
    ('S0010', 'P0003', 38, 40),
    ('S0011', 'P0003', 39, 40),
	('S0012', 'P0003', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0013', 'P0004', 37, 40),
    ('S0014', 'P0004', 38, 40),
    ('S0015', 'P0004', 39, 40),
	('S0016', 'P0004', 40, 40);

INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0017', 'P0005', 37, 40),
    ('S0018', 'P0005', 38, 40),
    ('S0019', 'P0005', 39, 40),
	('S0040', 'P0005', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0021', 'P0006', 37, 40),
    ('S0022', 'P0006', 38, 40),
    ('S0023', 'P0006', 39, 40),
	('S0024', 'P0006', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0025', 'P0007', 37, 40),
    ('S0026', 'P0007', 38, 40),
    ('S0027', 'P0007', 39, 40),
	('S0028', 'P0007', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0029', 'P0008', 37, 40),
    ('S0030', 'P0008', 38, 40),
    ('S0031', 'P0008', 39, 40),
	('S0032', 'P0008', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0033', 'P0009', 37, 40),
    ('S0034', 'P0009', 38, 40),
    ('S0035', 'P0009', 39, 40),
	('S0036', 'P0009', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0037', 'P0010', 37, 40),
    ('S0038', 'P0010', 38, 40),
    ('S0039', 'P0010', 39, 40),
	('S0040', 'P0010', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0041', 'P0011', 37, 40),
    ('S0042', 'P0011', 38, 40),
    ('S0043', 'P0011', 39, 40),
	('S0044', 'P0011', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0045', 'P0012', 37, 40),
    ('S0046', 'P0012', 38, 40),
    ('S0047', 'P0012', 39, 40),
	('S0048', 'P0012', 40, 40);
--------------------------------
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0053', 'P0013', 37, 40),
    ('S0054', 'P0013', 38, 40),
    ('S0055', 'P0013', 39, 40),
    ('S0056', 'P0013', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0057', 'P0014', 37, 40),
    ('S0058', 'P0014', 38, 40),
    ('S0059', 'P0014', 39, 40),
    ('S0060', 'P0014', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0061', 'P0015', 37, 40),
    ('S0062', 'P0015', 38, 40),
    ('S0063', 'P0015', 39, 40),
    ('S0064', 'P0015', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0065', 'P0016', 37, 40),
    ('S0066', 'P0016', 38, 40),
    ('S0067', 'P0016', 39, 40),
    ('S0068', 'P0016', 40, 40);
---------------------
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0069', 'P0017', 37, 40),
    ('S0070', 'P0017', 38, 40),
    ('S0071', 'P0017', 39, 40),
    ('S0072', 'P0017', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0073', 'P0018', 37, 40),
    ('S0074', 'P0018', 38, 40),
    ('S0075', 'P0018', 39, 40),
    ('S0076', 'P0018', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0077', 'P0019', 37, 40),
    ('S0078', 'P0019', 38, 40),
    ('S0079', 'P0019', 39, 40),
    ('S0080', 'P0019', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0081', 'P0020', 37, 40),
    ('S0082', 'P0020', 38, 40),
    ('S0083', 'P0020', 39, 40),
    ('S0084', 'P0020', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0085', 'P0021', 37, 40),
    ('S0086', 'P0021', 38, 40),
    ('S0087', 'P0021', 39, 40),
    ('S0088', 'P0021', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0089', 'P0022', 37, 40),
    ('S0090', 'P0022', 38, 40),
    ('S0091', 'P0022', 39, 40),
    ('S0092', 'P0022', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0093', 'P0023', 37, 40),
    ('S0094', 'P0023', 38, 40),
    ('S0095', 'P0023', 39, 40),
    ('S0096', 'P0023', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0097', 'P0024', 37, 40),
    ('S0098', 'P0024', 38, 40),
    ('S0099', 'P0024', 39, 40),
    ('S0100', 'P0024', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0101', 'P0025', 37, 40),
    ('S0102', 'P0025', 38, 40),
    ('S0103', 'P0025', 39, 40),
    ('S0104', 'P0025', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0105', 'P0026', 37, 40),
    ('S0106', 'P0026', 38, 40),
    ('S0107', 'P0026', 39, 40),
    ('S0108', 'P0026', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0109', 'P0027', 37, 40),
    ('S0110', 'P0027', 38, 40),
    ('S0111', 'P0027', 39, 40),
    ('S0112', 'P0027', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0113', 'P0028', 37, 40),
    ('S0114', 'P0028', 38, 40),
    ('S0115', 'P0028', 39, 40),
    ('S0116', 'P0028', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0117', 'P0029', 37, 40),
    ('S0118', 'P0029', 38, 40),
    ('S0119', 'P0029', 39, 40),
    ('S0140', 'P0029', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0121', 'P0030', 37, 40),
    ('S0122', 'P0030', 38, 40),
    ('S0123', 'P0030', 39, 40),
    ('S0124', 'P0030', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0125', 'P0031', 37, 40),
    ('S0126', 'P0031', 38, 40),
    ('S0127', 'P0031', 39, 40),
    ('S0128', 'P0031', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0129', 'P0032', 37, 40),
    ('S0130', 'P0032', 38, 40),
    ('S0131', 'P0032', 39, 40),
    ('S0132', 'P0032', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0133', 'P0033', 37, 40),
    ('S0134', 'P0033', 38, 40),
    ('S0135', 'P0033', 39, 40),
    ('S0136', 'P0033', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0137', 'P0034', 37, 40),
    ('S0138', 'P0034', 38, 40),
    ('S0139', 'P0034', 39, 40),
    ('S0140', 'P0034', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0141', 'P0035', 37, 40),
    ('S0142', 'P0035', 38, 40),
    ('S0143', 'P0035', 39, 40),
    ('S0144', 'P0035', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0145', 'P0036', 37, 40),
    ('S0146', 'P0036', 38, 40),
    ('S0147', 'P0036', 39, 40),
    ('S0148', 'P0036', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0149', 'P0037', 37, 40),
    ('S0150', 'P0037', 38, 40),
    ('S0151', 'P0037', 39, 40),
    ('S0152', 'P0037', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0153', 'P0038', 37, 40),
    ('S0154', 'P0038', 38, 40),
    ('S0155', 'P0038', 39, 40),
    ('S0156', 'P0038', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0157', 'P0039', 37, 40),
    ('S0158', 'P0039', 38, 40),
    ('S0159', 'P0039', 39, 40),
    ('S0160', 'P0039', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0161', 'P0040', 37, 40),
    ('S0162', 'P0040', 38, 40),
    ('S0163', 'P0040', 39, 40),
    ('S0164', 'P0040', 40, 40);

	-------------------------------

INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0165', 'P0041', 37, 40),
    ('S0166', 'P0041', 38, 40),
    ('S0167', 'P0041', 39, 40),
    ('S0168', 'P0041', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0169', 'P0042', 37, 40),
    ('S0170', 'P0042', 38, 40),
    ('S0171', 'P0042', 39, 40),
    ('S0172', 'P0042', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0173', 'P0043', 37, 40),
    ('S0174', 'P0043', 38, 40),
    ('S0175', 'P0043', 39, 40),
    ('S0176', 'P0043', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0177', 'P0044', 37, 40),
    ('S0178', 'P0044', 38, 40),
    ('S0179', 'P0044', 39, 40),
    ('S0180', 'P0044', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0181', 'P0045', 37, 40),
    ('S0182', 'P0045', 38, 40),
    ('S0183', 'P0045', 39, 40),
    ('S0184', 'P0045', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0185', 'P0046', 37, 40),
    ('S0186', 'P0046', 38, 40),
    ('S0187', 'P0046', 39, 40),
    ('S0188', 'P0046', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0189', 'P0047', 37, 40),
    ('S0190', 'P0047', 38, 40),
    ('S0191', 'P0047', 39, 40),
    ('S0192', 'P0047', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0193', 'P0048', 37, 40),
    ('S0194', 'P0048', 38, 40),
    ('S0195', 'P0048', 39, 40),
    ('S0196', 'P0048', 40, 40);

---------------------------------------------------------------

INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0197', 'P0049', 37, 40),
    ('S0198', 'P0049', 38, 40),
    ('S0199', 'P0049', 39, 40),
    ('S0400', 'P0049', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0401', 'P0050', 37, 40),
    ('S0402', 'P0050', 38, 40),
    ('S0403', 'P0050', 39, 40),
    ('S0404', 'P0050', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0405', 'P0051', 37, 40),
    ('S0406', 'P0051', 38, 40),
    ('S0407', 'P0051', 39, 40),
    ('S0408', 'P0051', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0409', 'P0052', 37, 40),
    ('S0210', 'P0052', 38, 40),
    ('S0211', 'P0052', 39, 40),
    ('S0212', 'P0052', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0213', 'P0053', 37, 40),
    ('S0214', 'P0053', 38, 40),
    ('S0215', 'P0053', 39, 40),
    ('S0216', 'P0053', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0217', 'P0054', 37, 40),
    ('S0218', 'P0054', 38, 40),
    ('S0219', 'P0054', 39, 40),
    ('S0240', 'P0054', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0221', 'P0055', 37, 40),
    ('S0222', 'P0055', 38, 40),
    ('S0223', 'P0055', 39, 40),
    ('S0224', 'P0055', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
    ('S0225', 'P0056', 37, 40),
    ('S0226', 'P0056', 38, 40),
    ('S0227', 'P0056', 39, 40),
    ('S0228', 'P0056', 40, 40);

	-------------------------------------------------------------

INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0229', 'P0057', 37, 40),
    ('S0230', 'P0057', 38, 40),
    ('S0231', 'P0057', 39, 40),
    ('S0232', 'P0057', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0233', 'P0058', 37, 40),
    ('S0234', 'P0058', 38, 40),
    ('S0235', 'P0058', 39, 40),
    ('S0236', 'P0058', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0237', 'P0059', 37, 40),
    ('S0238', 'P0059', 38, 40),
    ('S0239', 'P0059', 39, 40),
    ('S0240', 'P0059', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0241', 'P0060', 37, 40),
    ('S0242', 'P0060', 38, 40),
    ('S0243', 'P0060', 39, 40),
    ('S0244', 'P0060', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0245', 'P0061', 37, 40),
    ('S0246', 'P0061', 38, 40),
    ('S0247', 'P0061', 39, 40),
    ('S0248', 'P0061', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0249', 'P0062', 37, 40),
    ('S0250', 'P0062', 38, 40),
    ('S0251', 'P0062', 39, 40),
    ('S0252', 'P0062', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0253', 'P0063', 37, 40),
    ('S0254', 'P0063', 38, 40),
    ('S0255', 'P0063', 39, 40),
    ('S0256', 'P0063', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0257', 'P0064', 37, 40),
    ('S0258', 'P0064', 38, 40),
    ('S0259', 'P0064', 39, 40),
    ('S0260', 'P0064', 40, 40);
INSERT INTO ProductSize (SizeID, ProductID, Size, Quantity)
VALUES 
	('S0261', 'P0065', 37, 40),
    ('S0262', 'P0065', 38, 40),
    ('S0263', 'P0065', 39, 40),
    ('S0264', 'P0065', 40, 40);