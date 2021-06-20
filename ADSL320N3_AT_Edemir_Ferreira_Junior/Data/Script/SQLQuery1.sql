USE Mercado_Clientes


CREATE TABLE Produto(
	IdProduto INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	NomeProduto NVARCHAR(50),
	CaracteristicasProduto NVARCHAR(300),
	QuantidadeProduto INT,
	DataFabricacao DATETIME2,
);

DROP TABLE Produto;

SELECT * FROM Produto;

SELECT IdProduto, NomeProduto, CaracteristicasProduto, QuantidadeProduto, DataFabricacao FROM Produto;

INSERT INTO Produto
	(NomeProduto, CaracteristicasProduto, QuantidadeProduto, DataFabricacao)
	VALUES	('iPod Classic', '160gb', 15, '2009-07-05'),
			('iPhone 12 Pro Max', '512gb', 97, '2021-02-05'),
			('MacBook Pro Retina', 'Tela 16" 1TB 32gb Ram', 1300, '2021-06-03'),
			('iMac', 'Tela 24 1TB 64gb Ram', 1350, '2021-06-03'),
			('iPad Pro', 'Tela 12.9 256gb', 1700, '2021-06-03'),
			('iPad Air', 'Wi-Fi + Cellular', 1100, '2021-06-03'),
			('Apple Watch Series 6', '(Product)red Case 44mm', 1500, '2021-06-03'),
			('Teclado Keychron k3', 'Mecânico Optico', 350, '2021-05-07'),
			('Teclado Keychron k2', 'Mecânico', 50, '2020-05-07'),
			('Apple TV 4k', '64gb', 750, '2021-01-09');

UPDATE Produto
	SET QuantidadeProduto = '1300'
	WHERE IdProduto = 0;

DELETE FROM Produto WHERE IdProduto = 10;