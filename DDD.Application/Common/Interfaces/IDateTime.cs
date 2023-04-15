﻿namespace DDD.Application.Common.Interfaces;

public interface IDateTime
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}