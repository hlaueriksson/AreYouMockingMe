using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using Should;

namespace AreYouMockingMe.Tests.NUnit.Given_HelloWorld_with_AutoMocker
{
    public class When_GetMessage
    {
        private AutoMocker _mocker;
        private HelloWorld _subject;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMocker();
            _subject = _mocker.CreateInstance<HelloWorld>();
        }

        [Test]
        public void Should_invoke_IFoo_GetMessage()
        {
            _subject.GetMessage();

            _mocker.GetMock<IFoo>().Verify(x => x.GetFoo());
        }

        [Test]
        public void Should_invoke_IBar_GetMessage()
        {
            _subject.GetMessage();

            _mocker.GetMock<IBar>().Verify(x => x.GetBar(), Times.Once);
        }

        [Test]
        public void Should_return_a_concatenated_string_with_messages_from_IFoo_and_IBar()
        {
            _mocker.GetMock<IFoo>().Setup(x => x.GetFoo()).Returns("Hello");
            _mocker.GetMock<IBar>().Setup(x => x.GetBar()).Returns(", World!");

            _subject.GetMessage().ShouldEqual("Hello, World!");
        }
    }
}