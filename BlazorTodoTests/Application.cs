using System.Reflection;
using BlazorState;
using BlazorTodo.Stores;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTodoTests;

public class Application
{
    private readonly IMediator _mediator;
    private readonly IStore _store;

    public Application()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddBlazorState(a =>
        {
            a.UseCloneStateBehavior = false;
            a.Assemblies = new[] { typeof(BlazorTodo.App).Assembly };
        });

        var provider = builder.Services.BuildServiceProvider();
        _mediator = provider.GetService<IMediator>() ?? throw new ArgumentNullException("Mediator");
        _store = provider.GetService<IStore>() ?? throw new ArgumentNullException("Store");
    }

    public async Task Send(IAction action) => await _mediator.Send(action);
    public TodoState State => _store.GetState<TodoState>();
}