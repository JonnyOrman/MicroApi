namespace MicroApi.Testing.UnitTests.TestClasses;

public class TestResultTypeA : TestResult
{
    public TestResultTypeA(bool isSuccessful, string message) : base(isSuccessful, message)
    {
    }
}
