using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAValidationResultBuilderCreator
{
    public class WhenAValidationResultBuilderIsCreated
    {
        [Fact]
        public void ThenItCreatesAValidationResultBuilder()
        {
            var sut = new ValidationResultBuilderCreator();

            var result = sut.Create();

            Assert.NotNull(result);
        }
    }
}
