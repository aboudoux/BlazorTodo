using BlazorTodo.Stores;
using FluentAssertions;

namespace BlazorTodoTests {
    public class UnitTest1
    {
        private Application _app;
        public UnitTest1()
        {
            _app = new Application();
        }

        [Fact(DisplayName = "Quand je clique sur + la t�che est ajout�e")]
        public async Task Test1()
        {
            _app.State.TaskInput = "premi�re t�che";
            await _app.Send(new TodoState.AddTask());

            _app.State.TodoList.Should().HaveCount(1);
            _app.State.TodoList.First().Label.Should().Be("premi�re t�che");
        }

        [Fact(DisplayName = "Si le champ est vide il ne se passe rien quand je clique sur +")]
        public async Task Test24()
        {
            _app.State.TaskInput = string.Empty;
            await _app.Send(new TodoState.AddTask());
            _app.State.TodoList.Should().BeEmpty();
        }

        [Fact(DisplayName = "apr�s avoir cliqu� sur +, le champ texte est r�initialis�")]
        public async Task Test32()
        {
            _app.State.TaskInput = "essai";
            await _app.Send(new TodoState.AddTask());

            _app.State.TodoList.Should().NotBeEmpty();
            _app.State.TaskInput.Should().BeEmpty();
        }

        [Fact(DisplayName = "Quand je clique sur clear, les t�ches coch�es sont retir�es")]
        public async Task Test42()
        {
            _app.State.TaskInput = "test 1";
            await _app.Send(new TodoState.AddTask());
            _app.State.TaskInput = "test 2";
            await _app.Send(new TodoState.AddTask());

            await _app.Send(new TodoState.CheckTask(_app.State.TodoList.ElementAt(1).Id, true));
            await _app.Send(new TodoState.ClearTask());

            _app.State.TodoList.Should().HaveCount(1)
                .And.AllSatisfy(task => task.Checked.Should().BeFalse());
        }
    }
}