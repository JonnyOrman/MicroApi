﻿using MicroApi.Core;

namespace MicroApi.Read.Testing.UnitTests.TestClasses;

public class TestEntity : Entity<string>
{
    public TestEntity(string key) : base(key)
    {
    }
}
