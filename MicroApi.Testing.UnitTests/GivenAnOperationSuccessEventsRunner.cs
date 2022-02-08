using MicroApi.Testing.UnitTests.TestClasses;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MicroApi.Testing.UnitTests;

public class GivenAnOperationSuccessEventsRunner
{
    public class WhenOperationSuccessEventsAreRun
    {
        [Fact]
        public void ThenItRunsTheOperationSuccessEvents()
        {
            var entity = new TestEntity(1);

            var parameters = new TestParameters();

            var operationSuccessEvent1Mock = new Mock<IOperationSuccessEvent<TestEntity, TestParameters>>();
            var operationSuccessEvent2Mock = new Mock<IOperationSuccessEvent<TestEntity, TestParameters>>();
            var operationSuccessEvent3Mock = new Mock<IOperationSuccessEvent<TestEntity, TestParameters>>();

            var operationSuccessEvents = new List<IOperationSuccessEvent<TestEntity, TestParameters>>
            {
                operationSuccessEvent1Mock.Object,
                operationSuccessEvent2Mock.Object,
                operationSuccessEvent3Mock.Object
            };

            var sut = new OperationSuccessEventsRunner<TestEntity, TestParameters>(operationSuccessEvents);

            sut.Run(entity, parameters);

            operationSuccessEvent1Mock.Verify(operationSuccessEvent => operationSuccessEvent.Run(entity, parameters), Times.Once);
            operationSuccessEvent2Mock.Verify(operationSuccessEvent => operationSuccessEvent.Run(entity, parameters), Times.Once);
            operationSuccessEvent3Mock.Verify(operationSuccessEvent => operationSuccessEvent.Run(entity, parameters), Times.Once);
        }
    }
}
