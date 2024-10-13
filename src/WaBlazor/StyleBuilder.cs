namespace WaBlazor;

public class StyleBuilder
{
    private string? _stringBuffer;

    public StyleBuilder()
    {
        
    }

    public static StyleBuilder Default(string prop, string value) => new(prop, value);

    public static StyleBuilder Default(string style) => Empty().AddStyle(style);
    public static StyleBuilder Empty() => new();

    public StyleBuilder(string prop, string value) => _stringBuffer = $"{prop}:{value};";

    public StyleBuilder AddStyle(string style) => !string.IsNullOrWhiteSpace(style) ? AddRaw($"{style};") : this;

    private StyleBuilder AddRaw(string? style)
    {
        _stringBuffer += style;
        return this;
    }


    public StyleBuilder AddStyle(string prop, string? value, bool when = true) =>
        when ?  AddRaw($"{prop}:{value};") : this;
 

    public StyleBuilder AddStyle(StyleBuilder builder, bool when) => when ? this.AddRaw(builder.Build()) : this;

    public StyleBuilder AddStyleFromAttributes(IReadOnlyDictionary<string, object>? additionalAttributes) =>
        additionalAttributes == null ? this :
        additionalAttributes.TryGetValue("style", out var c) ? AddRaw(c?.ToString()) : this;


    public string Build()
    {
        return _stringBuffer != null ? _stringBuffer.Trim() : string.Empty;
    }

    public override string ToString() => Build();
}