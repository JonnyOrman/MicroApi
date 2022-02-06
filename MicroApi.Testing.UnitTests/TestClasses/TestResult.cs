namespace MicroApi.Testing.UnitTests.TestClasses;

public class TestResult : Result<TestEntity>
{
    public TestResult(bool isSuccessful, string message) : base(isSuccessful, message)
    {
    }
}