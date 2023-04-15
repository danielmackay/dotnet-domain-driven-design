﻿namespace DDD.Application.Orders.Queries.GetAllOrders;

public record LineItemDto(Guid Id, ProductDto Product, MoneyDto Price);

public record ProductDto(Guid Id, string Name, string Sku);

public record MoneyDto(string Currency, decimal Amount);