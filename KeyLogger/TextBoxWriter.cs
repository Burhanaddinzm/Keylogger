using System.Text;

namespace KeyLogger;

public class TextBoxWriter : TextWriter
{
    private readonly TextBox _textBox;

    public TextBoxWriter(TextBox textBox)
    {
        _textBox = textBox;
    }

    public override void Write(char value)
    {
        _textBox.Invoke((Action)(() => _textBox.AppendText(value.ToString())));
    }

    public override void Write(string? value)
    {
        _textBox.Invoke((Action)(() => _textBox.AppendText(value)));
    }

    public override Encoding Encoding => Encoding.UTF8;
}
