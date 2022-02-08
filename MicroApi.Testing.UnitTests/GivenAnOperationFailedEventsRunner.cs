using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationFailedEventsRunner
{
    public class WhenOperationFailedEventsAreRun
    {
        [Fact]
        public void ThenItRunsTheOperationFailedEvents()
        {
            var parameters = new TestParameters();

            var operationFailedEvent1Mock = new Mock<IOperationFailedEvent<TestParameters>>();
            var operationFailedEvent2Mock = new Mock<IOperationFailedEvent<TestParameters>>();
            var operationFailedEvent3Mock = new Mock<IOperationFailedEvent<TestParameters>>();

            var operationFailedEvents = new List<IOperationFailedEvent<TestParameters>>
            {
                operationFailedEvent1Mock.Object,
                operationFailedEvent2Mock.Object,
                operationFailedEvent3Mock.Object
            };

            var sut = new OperationFailedEventsRunner<TestParameters>(operationFailedEvents);

            sut.Run(parameters);

            operationFailedEvent1Mock.Verify(operationFailedEvent => operationFailedEvent.Run(parameters), Times.Once);
            operationFailedEvent2Mock.Verify(operationFailedEvent => operationFailedEvent.Run(parameters), Times.Once);
            operationFailedEvent3Mock.Verify(operationFailedEvent => operationFailedEvent.Run(parameters), Times.Once);
        }
    }
}
