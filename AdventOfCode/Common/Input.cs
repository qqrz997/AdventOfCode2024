namespace AdventOfCode.Common;

public class Input
{
    public Input(string text) => this.Text = text;
    
    public string Text { get; }

    public string[] GetLines() => Text.Split('\n');
}