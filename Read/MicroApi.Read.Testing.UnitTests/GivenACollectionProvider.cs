using MicroApi.Read.Testing.UnitTests.TestClasses;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MicroApi.Read.Testing.UnitTests;

public class GivenACollectionProvider
{
    public class WhenItGetsACollection
    {
        [Fact]
        public async Task ThenItReadsTheCollectionAndSuccessfullyReturnsIt()
        {
            var query = new TestQuery();

            var collection = new List<TestEntity>();

            var collectionReaderMock = new Mock<ICollectionReader<TestEntity, TestQuery>>();
            collectionReaderMock
                .Setup(x => x.ReadManyAsync(query))
                .ReturnsAsync(collection);

            var sut = new CollectionProvider<TestEntity, TestQuery>(collectionReaderMock.Object);

            var result = await sut.GetAsync(query);

            collectionReaderMock.Verify(collectionReader => collectionReader.ReadManyAsync(query), Times.Once);

            var successResult = result as SuccessResult<IEnumerable<TestEntity>>;
            Assert.Equal(collection, successResult.Entity);
        }
    }
}