﻿namespace MicroApi.Core;

public class ValidationResult
{
    public ValidationResult(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }

    public bool IsSuccessful { get; }
}
