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
    FechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
	Total DECIMAL(18,2) NOT NULL
);

CREATE TABLE PedidoProductos (
    PedidoId INT NOT NULL FOREIGN KEY REFERENCES Pedidos(Id),
    ProductoId INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PRIMARY KEY (PedidoId, ProductoId)
);
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(256) NOT NULL,
    Rol NVARCHAR(20) NOT NULL
);
GO


INSERT INTO Clientes (Nombre, Email)
VALUES 
    ('Juan Pérez', 'juan@email.com'),
    ('María García', 'maria@email.com'),
    ('Carlos López', 'carlos@email.com');
GO

INSERT INTO Productos (Nombre, Precio, Stock)
VALUES 
    ('Laptop HP', 1200.00, 10),
    ('Mouse Gaming', 50.00, 20),
    ('Teclado Mecánico', 80.00, 15),
    ('Monitor 24"', 200.00, 8),
    ('Webcam HD', 40.00, 25),
	('Silla Gamer', 20.00, 5);

GO

CREATE TRIGGER trg_UpdateTotalAfterInsert
ON PedidoProductos
AFTER INSERT
AS
BEGIN
    UPDATE p
    SET p.Total = (
        SELECT SUM(pp.Cantidad * pr.Precio)
        FROM PedidoProductos pp
        INNER JOIN Productos pr ON pp.ProductoId = pr.Id
        WHERE pp.PedidoId = p.Id
    )
    FROM Pedidos p
    INNER JOIN inserted i ON p.Id = i.PedidoId;
END;


go
CREATE TRIGGER trg_UpdateTotalAfterUpdate
ON PedidoProductos
AFTER UPDATE
AS
BEGIN
    UPDATE p
    SET p.Total = (
        SELECT SUM(pp.Cantidad * pr.Precio)
        FROM PedidoProductos pp
        INNER JOIN Productos pr ON pp.ProductoId = pr.Id
        WHERE pp.PedidoId = p.Id
    )
    FROM Pedidos p
    INNER JOIN inserted i ON p.Id = i.PedidoId;
END;

go
CREATE TRIGGER trg_UpdateTotalAfterDelete
ON PedidoProductos
AFTER DELETE
AS
BEGIN
    UPDATE p
    SET p.Total = (
        SELECT SUM(pp.Cantidad * pr.Precio)
        FROM PedidoProductos pp
        INNER JOIN Productos pr ON pp.ProductoId = pr.Id
        WHERE pp.PedidoId = p.Id
    )
    FROM Pedidos p
    INNER JOIN deleted d ON p.Id = d.PedidoId;
END;


go


CREATE TRIGGER trg_RestarStock
ON PedidoProductos
AFTER INSERT
AS
BEGIN
    UPDATE Productos
    SET Stock = Stock - i.Cantidad
    FROM Productos p
    INNER JOIN inserted i ON p.Id = i.ProductoId
    WHERE p.Id = i.ProductoId;
END;
go


CREATE VIEW VistaDetalleTotalPedidos AS
SELECT 
    p.Id AS PedidoId,
    p.FechaPedido,
    p.Total,
    c.Nombre AS NombreCliente,
    c.Email AS EmailCliente,
    pr.Nombre AS NombreProducto,
    pp.Cantidad,
    (pr.Precio * pp.Cantidad) AS SubTotal
FROM Pedidos p
INNER JOIN Clientes c ON p.ClienteId = c.Id
INNER JOIN PedidoProductos pp ON p.Id = pp.PedidoId
INNER JOIN Productos pr ON pp.ProductoId = pr.Id;
GO

