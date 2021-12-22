﻿using Customer.Domain.Exceptions;

namespace Customer.Domain.ValueObjects;

public record CustomerAge
{
    public uint Value { get; }
    public CustomerAge(uint value)
    {
        if (value <= 12 )
        {
            throw new InvalidCustomerAgeException(value);
        }
        Value = value;
    }
    public static implicit operator uint(CustomerAge customerAge) => customerAge.Value;
    public static implicit operator CustomerAge(uint value) => new(value);
}