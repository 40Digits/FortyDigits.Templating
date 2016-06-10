namespace FortyDigits.Templating
{
    public interface ITemplate<in T> : ITemplateBase
    {
        string Render(T values);
    }
}