using BlazorState;
using MediatR;
using static BlazorTodo.Stores.TodoState;

namespace BlazorTodo.Stores;

public class TodoState : State<TodoState>
{
    public string TaskInput { get; set; } = string.Empty;
    public List<TodoTask> TodoList { get; set; } = new();

    public override void Initialize()
    {
        TaskInput = string.Empty;
        TodoList.Clear();
    }

    public record AddTask : IAction;

    public record CheckTask(Guid TaskId, bool Checked) : IAction;
    public record ClearTask : IAction;
}

public record TodoTask(Guid Id, string Label, bool Checked);

public class TaskReducer : ActionHandler<AddTask>,
    IRequestHandler<CheckTask>,
    IRequestHandler<ClearTask>
{
    private TodoState State => Store.GetState<TodoState>();
    public TaskReducer(IStore store) : base(store)
    {
    }

    public override Task Handle(AddTask action, CancellationToken aCancellationToken)
    {
        if (string.IsNullOrWhiteSpace(State.TaskInput)) return Task.CompletedTask;
        State.TodoList.Add(new TodoTask(Guid.NewGuid(), State.TaskInput, false));
        State.TaskInput = string.Empty;

        return Task.CompletedTask;
    }

    public Task Handle(CheckTask action, CancellationToken cancellationToken)
    {
        var index = State.TodoList.FindIndex(a => a.Id == action.TaskId);
        State.TodoList[index] = State.TodoList[index] with {Checked = action.Checked};
        return Task.CompletedTask;
    }

    public Task Handle(ClearTask action, CancellationToken cancellationToken)
    {
        State.TodoList.RemoveAll(a => a.Checked);
        return Task.CompletedTask;
    }
}