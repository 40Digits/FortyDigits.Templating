namespace FortyDigits.Templating
{
    public interface ITemplateParser<out T> where T : ITemplateBase
    {
        T GetTemplate(string template);
    }
}