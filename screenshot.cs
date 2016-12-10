#r "System.Drawing.dll"
using System.Runtime.InteropServices;
using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Forms;

public class Startup
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern IntPtr GetDesktopWindow();

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

    [DllImport("user32.dll")]
    private static extern IntPtr GetClientRect(IntPtr hWnd, ref Rect rect);

    [DllImport("user32.dll")]
    private static extern IntPtr ClientToScreen(IntPtr hWnd, ref Point point);

    public static Image CaptureDesktop()
    {
        return CaptureWindow(GetDesktopWindow());
    }

    public static Bitmap CaptureActiveWindow()
    {
        return CaptureWindow(GetForegroundWindow());
    }

    public static Bitmap CaptureWindow(IntPtr handle)
    {
        var rect = new Rect();
        GetClientRect(handle, ref rect);

        var point = new Point(0,0);
        ClientToScreen(handle, ref point);

        var bounds = new Rectangle(point.X, point.Y, rect.Right, rect.Bottom);
        var result = new Bitmap(bounds.Width, bounds.Height);

        using (var graphics = Graphics.FromImage(result))
        {
            graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
        }

        return result;
    }
    public async Task<object> Invoke(Int32 input)
    {
        IntPtr hWnd = (IntPtr)input;
        if (hWnd == (IntPtr)0) {
            hWnd = GetForegroundWindow();
        }
        return ImageToByte(CaptureWindow(hWnd));
    }
    public static byte[] ImageToByte(Image img)
    {
        ImageConverter converter = new ImageConverter();
        return (byte[])converter.ConvertTo(img, typeof(byte[]));
    }
}
