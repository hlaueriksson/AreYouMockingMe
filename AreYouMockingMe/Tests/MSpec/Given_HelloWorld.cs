using Machine.Fakes;
using Machine.Specifications;

namespace AreYouMockingMe.Tests.MSpec
{
    [Subject(typeof(HelloWorld))]
    public class Given_HelloWorld : WithSubject<HelloWorld>
    {
        Establish context = () =>
        {
            The<IFoo>().WhenToldTo(x => x.GetFoo()).Return("Hello");
            The<IBar>().WhenToldTo(x => x.GetBar()).Return(", World!");
        };

        public class GetMessage
        {
            static string _result;

            Because of = () => _result = Subject.GetMessage();

            It should_invoke_IFoo_GetMessage = () => The<IFoo>().WasToldTo(x => x.GetFoo());
            It should_invoke_IBar_GetMessage = () => The<IBar>().WasToldTo(x => x.GetBar());
            It should_return_a_concatenated_string_with_messages_from_IFoo_and_IBar = () => _result.ShouldEqual("Hello, World!");
        }
    }
}