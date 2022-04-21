using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using System.Collections.Generic;
using XpressTest;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationFailedEventsRunner
{
    [Fact]
    public void WhenOperationFailedEventsAreRunThenItRunsTheOperationFailedEvents()
    {
        var parameters = new TestParameters();

        var operationFailedEvent1Mock = new Moq.Mock<IOperationFailedEvent<TestParameters>>();
        var operationFailedEvent2Mock = new Moq.Mock<IOperationFailedEvent<TestParameters>>();
        var operationFailedEvent3Mock = new Moq.Mock<IOperationFailedEvent<TestParameters>>();

        var operationFailedEvents = new List<IOperationFailedEvent<TestParameters>>
        {
            operationFailedEvent1Mock.Object,
            operationFailedEvent2Mock.Object,
            operationFailedEvent3Mock.Object
        };

        GivenA<OperationFailedEventsRunner<TestParameters>>
            .With(operationFailedEvents)
            .WhenIt(sut => sut.Run(parameters))
            .Then(assertion =>
            {
                operationFailedEvent1Mock.Verify(operationFailedEvent => operationFailedEvent.Run(parameters),
                    Times.Once);
                operationFailedEvent2Mock.Verify(operationFailedEvent => operationFailedEvent.Run(parameters),
                    Times.Once);
                operationFailedEvent3Mock.Verify(operationFailedEvent => operationFailedEvent.Run(parameters),
                    Times.Once);
            });
    }
}
