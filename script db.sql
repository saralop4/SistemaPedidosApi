-- Crear la base de datos
CREATE DATABASE PedidosDB;
GO

USE PedidosDB;
GO

CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL CHECK (Precio > 0),
    Stock INT NOT NULL CHECK (Stock >= 0),
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Pedidos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT NOT NULL FOREIGN KEY REFERENCES Clientes(Id),
    FechaPedido DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE PedidoProductos (
    PedidoId INT NOT NULL FOREIGN KEY REFERENCES Pedidos(Id),
    ProductoId INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PRIMARY KEY (PedidoId, ProductoId)
);
GO


CREATE VIEW VistaTotalPedidos AS
SELECT 
    p.Id AS PedidoId,
    SUM(pp.Cantidad * pr.Precio) AS Total
FROM 
    Pedidos p
INNER JOIN 
    PedidoProductos pp ON p.Id = pp.PedidoId
INNER JOIN 
    Productos pr ON pp.ProductoId = pr.Id
GROUP BY 
    p.Id;
