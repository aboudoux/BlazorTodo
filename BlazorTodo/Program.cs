using System.Reflection;
using BlazorState;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorState(a =>
{
    a.UseCloneStateBehavior = false;
    a.Assemblies = new[] {Assembly.GetExecutingAssembly()};
});

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
