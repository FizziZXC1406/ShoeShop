use DatabaseProjectFinalExam
CREATE TABLE Product (
    ProductID VARCHAR(10) PRIMARY KEY,
    Image VARBINARY(MAX) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Price INT NOT NULL CHECK (Price > 0),
	AdminID varchar(10),
	FOREIGN KEY (AdminID) REFERENCES AdminAccount(ID),
);

EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;

-- Insert Value For Table Product
INSERT INTO Product (ProductID, Image, Name, Price, AdminID)
VALUES 
(
    'P0048',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Converse7.jpg', SINGLE_BLOB) AS ImageData),
    N'Converse Run Star Hike - Trắng',
	1320000,
	'AD0101'
),
(
    'P0001',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Converse1.jpg', SINGLE_BLOB) AS ImageData),
    N'Converse Chuck Taylor All Star Move - Đen',
	1700000,
	'AD0101'
),
(
    'P0002',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma1.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Slipstream Prm - Hồng',
	1240000,
	'AD0101'
),
(
    'P0003',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma2.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Slipstream Basketball Mix - Trắng',
	850000,
	'AD0101'
),
(
    'P0004',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila1.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Oakmont Mud Guard - Đỏ',
	1150000,
	'AD0101'
),
(
    'P0005',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila2.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Targa 88/22 Lx - Nâu',
	1250000,
	'AD0101'
),
(
    'P0006',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila3.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Zagato V3 - Cam',
	920000,
	'AD0101'
),
(
    'P0007',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike1.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Gamma Force - Trắng',
	2799000,
	'AD0101'
),
(
    'P0008',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike2.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Air Max Excee - Hồng',
	1679000,
	'AD0101'
),
(
    'P0009',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike3.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Court Legacy Lift - Tím',
	1590000,
	'AD0101'
),
(
    'P0010',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike4.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Air Max Excee - Hồng',
	1860000,
	'AD0101'
),
(
    'P0011',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike5.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Court Vision Alta - Hồng',
	1620000,
	'AD0101'
),
(
    'P0012',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike6.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Air Max 90 - Xám',
	2380000,
	'AD0101'
),
(
    'P0013',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Converse2.jpg', SINGLE_BLOB) AS ImageData),
    N'Converse Chuck 70 Mule Recycled Canvas - Trắng',
	1200000,
	'AD0101'
),
(
    'P0014',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Converse3.jpg', SINGLE_BLOB) AS ImageData),
    N'Converse Chuck Taylor All Star Canvas Platform - Trắng',
	1000000,
	'AD0101'
),
(
    'P0015',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Converse4.jpg', SINGLE_BLOB) AS ImageData),
    N'Converse Chuck Taylor All Star - Trắng',
	870000,
	'AD0101'
),

(
    'P0016',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Adidas1.jpg', SINGLE_BLOB) AS ImageData),
    N'Adidas X_Plrboost - Xám',
	2940000,
	'AD0101'
),
(
    'P0017',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Adidas3.jpg', SINGLE_BLOB) AS ImageData),
    N'Adidas Ultraboost 1.0 - Trắng',
	4000000,
	'AD0101'
),
(
    'P0018',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Adidas4.jpg', SINGLE_BLOB) AS ImageData),
    N'Adidas Avryn - Trắng',
	2660000,
	'AD0101'
),
(
    'P0019',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Adidas5.jpg', SINGLE_BLOB) AS ImageData),
    N'Adidas Vl Court Bold - Be',
	1700000,
	'AD0101'
),
(
    'P0020',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Adidas6.jpg', SINGLE_BLOB) AS ImageData),
    N'Adidas Vl Court 3.0 - Đen',
	1700000,
	'AD0101'
),
(
    'P0021',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila4.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Mgx-100 Blanco Mujer - Trắng',
	798000,
	'AD0101'
),
(
    'P0022',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila5.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Classic 1998 V2 - Be',
	2295000,
	'AD0101'
),
(
    'P0023',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila6.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Court Deluxe - Trắng',
	1795000,
	'AD0101'
),
(
    'P0024',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila7.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Echappe Lace - Xám',
	2295000,
	'AD0101'
),
(
    'P0025',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila8.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Echappe Ms - Đen',
	2495000,
	'AD0101'
),
(
    'P0026',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila9.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Elite Court V3 - Be',
	798000,
	'AD0101'
),

(
    'P0027',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila10.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Oakmont Evo - Be',
	2795000,
	'AD0101'
),
(
    'P0028',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila11.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Fieldgage - Trắng',
	1995000,
	'AD0101'
),
(
    'P0029',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila12.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Oakmont Tr Dial V2 - Be',
	2995000,
	'AD0101'
),
(
    'P0030',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila13.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Ranger Lite V2 - Xám',
	2495000,
	'AD0101'
),
(
    'P0031',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila14.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Ray Wave - Đen',
	2295000,
	'AD0101'
),
(
    'P0032',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila15.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Rayflide Canvas - Xanh Navy',
	2295000,
	'AD0101'
),
(
    'P0033',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila16.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Rayflide - Trắng',
	2295000,
	'AD0101'
),
(
    'P0034',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila17.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila Sand Blast Lite - Tím',
	1795000,
	'AD0101'
),
(
    'P0035',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Fila18.jpg', SINGLE_BLOB) AS ImageData),
    N'Fila A-Low - Trắng',
	1595000,
	'AD0101'
),
(
    'P0036',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma3.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Rs-X Playstation - Đen',
	2370000,
	'AD0101'
),
(
    'P0037',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma4.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Slipstream Lo For The Fanbase - Nâu',
	2449000,
	'AD0101'
),
(
    'P0038',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma5.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Spirex Speed Mist - Xám',
	2579000,
	'AD0101'
),
(
    'P0039',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma6.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Cali Court Pure Luxe Wns - Đen',
	1959000,
	'AD0101'
),
(
    'P0040',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma7.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Mayze Stack Fashion - Trắng',
	2159000,
	'AD0101'
),
(
    'P0041',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma8.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Mayze Liberty - Trắng',
	2159000,
	'AD0101'
),
(
    'P0042',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma9.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Mayze Stack Luxe - Trắng',
	2195000,
	'AD0101'
),
(
    'P0043',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma10.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Slipstream Deboss - Xanh Mint',
	1919000,
	'AD0101'
),
(
    'P0044',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma11.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma X The Smurfs Mayze - Trắng',
	2099000,
	'AD0101'
),
(
    'P0045',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma12.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Rs-X Efekt Prm - Đen',
	2219000,
	'AD0101'
),
(
    'P0046',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma13.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Slipstream Expedition - Nâu',
	2169000,
	'AD0101'
),
(
    'P0047',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma14.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Slipstream Lo Retro - Trắng',
	2039000,
	'AD0101'
),
(
    'P0049',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Puma15.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Slipstream Xtreme Earth - Nâu',
	2459000,
	'AD0101'
),

(
    'P0050',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike7.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Air Max 97 Futura - Tím',
	5899000,
	'AD0101'
),
(
    'P0051',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike8.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Al8 - Xanh Lá',
	2799000,
	'AD0101'
),
(
    'P0052',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike9.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Gamma Force - Trắng',
	2799000,
	'AD0101'
),
(
    'P0053',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike10.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Air Huarache Craft - Hồng',
	2423000,
	'AD0101'
),
(
    'P0054',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike11.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Air Max Sc Se - Trắng',
	1487000,
	'AD0101'
),
(
    'P0055',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike12.jpg', SINGLE_BLOB) AS ImageData),
    N'Puma Slipstream Lo Retro - Trắng',
	2057000,
	'AD0101'
),
(
    'P0056',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike13.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Blazer Mid 77 - Trắng',
	1859000,
	'AD0101'
),
(
    'P0057',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike14.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Tech Hera - Hồng',
	2051000,
	'AD0101'
),
(
    'P0058',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Nike15.jpg', SINGLE_BLOB) AS ImageData),
    N'Nike Phoenix Waffle - Trắng',
	2169000,
	'AD0101'
),
(
    'P0059',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Adidas2.jpg', SINGLE_BLOB) AS ImageData),
    N'Adidas X_Plrphase - Xanh Dương',
	1750000,
	'AD0101'
),
-----------------------------------------------------
(
    'P0060',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Skechers1.jpg', SINGLE_BLOB) AS ImageData),
    N'Skechers D Lites 4.0 Pokémon Koga Ninja - Xanh Dương',
	1445000,
	'AD0101'
),
(
    'P0061',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Skechers2.jpg', SINGLE_BLOB) AS ImageData),
    N'Skechers Moonhiker - Xám',
	2290000,
	'AD0101'
),
(
    'P0062',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Skechers3.jpg', SINGLE_BLOB) AS ImageData),
    N'Skechers Sport Court 92 - Hồng',
	1045000,
	'AD0101'
),
(
    'P0063',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Skechers4.jpg', SINGLE_BLOB) AS ImageData),
    N'Skechers Arch Fit - Wave Rush - Hồng',
	1345000,
	'AD0101'
),
(
    'P0064',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Skechers5.jpg', SINGLE_BLOB) AS ImageData),
    N'Skechers D Lites - Nhiều Màu',
	1554000,
	'AD0101'
),
(
    'P0065',
    (SELECT * FROM OPENROWSET(BULK N'D:\STUDY\Nam 2 - HK3 (2023 - 2024)\Lap Trinh Window\ProjectFinalExam\Image\Converse6.jpg', SINGLE_BLOB) AS ImageData),
    N'Converse Chuck Taylor All Star Lift - Đen',
	1554000,
	'AD0101'
);