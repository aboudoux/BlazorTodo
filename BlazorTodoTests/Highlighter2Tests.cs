using BlazorTodo.Pages;
using Bunit;

namespace BlazorTodoTests {
    public class Highlighter2Tests
    {
        [Fact(DisplayName = "pas de surbrillance")]
        public void Test11()
        {
            var context = new TestContext();
            var component = context.RenderComponent<HighlightText>(param
                => param.Add(b => b.Text, "bonjour les amis")
                    .Add(b => b.HighlightWords, Array.Empty<string>())
            );

            component.MarkupMatches("bonjour les amis");
        }


        [Fact(DisplayName = "surbrillance un mot")]
        public void Test21()
        {
            var context = new TestContext();
            var component = context.RenderComponent<HighlightText>(param
                => param.Add(b => b.Text, "bonjour les amis")
                    .Add(b => b.HighlightWords, new []{"jour"})
            );

            component.MarkupMatches($"bon{Span("jour")} les amis");
        }

        [Fact(DisplayName = "surbrillance sur 2 mots")]
        public void Test2() {
            var context = new TestContext();
            var component = context.RenderComponent<HighlightText>(param
                => param.Add(b => b.Text, "bonjour les amis")
                    .Add(b => b.HighlightWords, new[] { "jour", "mis" })
            );

            component.MarkupMatches($"bon{Span("jour")} les a{Span("mis")}");
        }

        [Fact(DisplayName = "surbrillance indesirable")]
        public void Test48() {
            var context = new TestContext();
            var component = context.RenderComponent<HighlightText>(param
                => param.Add(b => b.Text, "bonjour les amis")
                    .Add(b => b.HighlightWords, new[] { "jour", "mis", "span" })
            );

            component.MarkupMatches($"bon{Span("jour")} les a{Span("mis")}");
        }

        [Fact(DisplayName = "plusieurs fois le même mot")]
        public void Test55()
        {
            var context = new TestContext();
            var component = context.RenderComponent<HighlightText>(param
                => param.Add(b => b.Text, "bonjour les amis")
                    .Add(b => b.HighlightWords, new[] { "jour", "jour", "mi", "mis" })
            );

            component.MarkupMatches($"bon{Span("jour")} les a{Span("mis")}");
        }

        [Fact(DisplayName = "bug une lettre")]
        public void Test67()
        {
            var context = new TestContext();
            var component = context.RenderComponent<HighlightText>(param
                => param.Add(b => b.Text, "bonjour les amis")
                    .Add(b => b.HighlightWords, new[] { "jou", "am", "s"})
            );

            component.MarkupMatches($"bon{Span("jou")}r le{Span("s")} {Span("am")}i{Span("s")}");
        }

        private static string Span(string label)
            => $"<span style=\"background-color: yellow\">{label}</span>";
    }

    
}
