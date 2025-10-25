CREATE DATABASE BancoDB;
GO

USE BancoDB;
GO

CREATE TABLE Clientes (
    ClienteId UNIQUEIDENTIFIER PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Genero NVARCHAR(20),
    Edad INT,
    Identificacion NVARCHAR(50) UNIQUE NOT NULL,
    Direccion NVARCHAR(200),
    Telefono NVARCHAR(50),
    ContrasenaHash NVARCHAR(200),
    Estado BIT NOT NULL
);

CREATE TABLE Cuentas (
    CuentaId UNIQUEIDENTIFIER PRIMARY KEY,
    NumeroCuenta NVARCHAR(50) UNIQUE NOT NULL,
    TipoCuenta NVARCHAR(50),
    SaldoInicial DECIMAL(18,2),
    Estado BIT NOT NULL,
    ClienteId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
);

CREATE TABLE Movimientos (
    MovimientoId UNIQUEIDENTIFIER PRIMARY KEY,
    Fecha DATETIME NOT NULL,
    TipoMovimiento NVARCHAR(20) NOT NULL,
    Valor DECIMAL(18,2) NOT NULL,
    Saldo DECIMAL(18,2) NOT NULL,
    CuentaId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (CuentaId) REFERENCES Cuentas(CuentaId)
);

CREATE TABLE Persona (
    PersonaId UNIQUEIDENTIFIER PRIMARY KEY,
    Nombre NVARCHAR(50)  NOT NULL,
    Genero NVARCHAR(50),
    Edad INT,
    Identificaion int NOT NULL,
    Direccion NVARCHAR(50),
    Telefono NVARCHAR(20)
);
