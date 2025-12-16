CREATE DATABASE IF NOT EXISTS carstore;
USE carstore;

DROP TABLE IF EXISTS cars;

CREATE TABLE cars (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Brand VARCHAR(50) NOT NULL,
    Model VARCHAR(50) NOT NULL,
    Year INT NOT NULL,
    Color VARCHAR(30),
    Price DECIMAL(10,2),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Пример вставки данных
INSERT INTO cars (Brand, Model, Year, Color, Price) VALUES
('Toyota', 'Camry', 2020, 'White', 25000.00),
('Honda', 'Civic', 2019, 'Black', 20000.00),
('Ford', 'Mustang', 2021, 'Red', 35000.00),
('BMW', 'X5', 2019, 'Black', 45000.00),
('Audi', 'A6', 2021, 'Gray', 38000.00);
