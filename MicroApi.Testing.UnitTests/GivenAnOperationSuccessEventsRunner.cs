using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using System.Collections.Generic;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationSuccessEventsRunner
{
    [Fact]
    public void WhenOperationSuccessEventsAreRunThenItRunsTheOperationSuccessEvents()
    {
        var entity = new TestEntity(1);

        var parameters = new TestParameters();

        var operationSuccessEvent1Mock = new Moq.Mock<IOperationSuccessEvent<TestEntity, TestParameters>>();
        var operationSuccessEvent2Mock = new Moq.Mock<IOperationSuccessEvent<TestEntity, TestParameters>>();
        var operationSuccessEvent3Mock = new Moq.Mock<IOperationSuccessEvent<TestEntity, TestParameters>>();

        var operationSuccessEvents = new List<IOperationSuccessEvent<TestEntity, TestParameters>>
        {
            operationSuccessEvent1Mock.Object,
            operationSuccessEvent2Mock.Object,
            operationSuccessEvent3Mock.Object
        };

        GivenA<OperationSuccessEventsRunner<TestEntity, TestParameters>>
            .With(operationSuccessEvents)
            .WhenIt(sut => sut.Run(entity, parameters))
            .Then(assertion =>
            {
                operationSuccessEvent1Mock.Verify(
                    operationSuccessEvent => operationSuccessEvent.Run(entity, parameters), Times.Once);
                operationSuccessEvent2Mock.Verify(
                    operationSuccessEvent => operationSuccessEvent.Run(entity, parameters), Times.Once);
                operationSuccessEvent3Mock.Verify(
                    operationSuccessEvent => operationSuccessEvent.Run(entity, parameters), Times.Once);
            });
    }
}
