using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

internal static class Program
{
    private const string DefaultKlid = "00000409";
    private const uint KLF_ACTIVATE = 0x00000001;
    private const uint WM_INPUTLANGCHANGEREQUEST = 0x0050;

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint flags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr ActivateKeyboardLayout(IntPtr hkl, uint flags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [STAThread]
    private static int Main(string[] args)
    {
        string klid = DefaultKlid;

        if (args != null && args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            klid = NormalizeKlid(args[0]);

        if (!IsValidKlid(klid))
            return 1;

        IntPtr hkl = LoadKeyboardLayout(klid, KLF_ACTIVATE);
        if (hkl == IntPtr.Zero)
            return 2;

        ActivateKeyboardLayout(hkl, KLF_ACTIVATE);

        IntPtr hwnd = GetForegroundWindow();
        if (hwnd != IntPtr.Zero)
            PostMessage(hwnd, WM_INPUTLANGCHANGEREQUEST, IntPtr.Zero, hkl);

        Thread.Sleep(100);
        return 0;
    }

    private static string NormalizeKlid(string value)
    {
        value = value.Trim();

        if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            value = value.Substring(2);

        // "409" or "0409" -> "00000409"
        if (Regex.IsMatch(value, @"^[0-9A-Fa-f]{3,8}$"))
            return value.ToUpperInvariant().PadLeft(8, '0');

        return value;
    }

    private static bool IsValidKlid(string klid)
    {
        return Regex.IsMatch(klid, @"^[0-9A-F]{8}$");
    }
}
