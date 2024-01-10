#pragma warning disable CA1416 // Validate platform compatibility

using System.Security.Cryptography;
using System.Text;

public static class VerbalMemoryHacks
{
    public static void Run()
    {

        Thread.Sleep(5000);

        // 420
        // 660
        // 1260

        Process[] processlist = Process.GetProcesses();

        IntPtr handle = IntPtr.Zero;

        foreach (Process process in processlist)
        {
            if (!String.IsNullOrEmpty(process.MainWindowTitle))
            {
                Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);

                if (process.ProcessName == "chrome")
                {
                    handle = process.MainWindowHandle;
                }
            }
        }

        Utils.GetCursorPos(out Point pos);
        ClickOnPointTool.ClickOnPoint(handle, pos);

        static byte[] GetHash(byte[] data)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(data);
        }

        static string GetHashString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(data))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        void Seen() => ClickOnPointTool.ClickOnPoint(handle, new Point(890, 516));
        void New() => ClickOnPointTool.ClickOnPoint(handle, new Point(1020, 516));

        List<string> hashes = new();

        var rect = new ScreenCapture.Rect();
        ScreenCapture.GetWindowRect(handle, ref rect);
        var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        var result = new Bitmap(bounds.Width, bounds.Height);
        using (var graphics = Graphics.FromImage(result))
            for (int i = 0; i < 1000; i++)
            {
                //IntPtr desk = Utils.GetDesktopWindow();
                //IntPtr dc = Utils.GetWindowDC(desk);
                //Bitmap b = ScreenCapture.CaptureActiveWindow();

                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);

                byte[] data = new byte[600];
                int j = 0;
                for (int x = 660; x < 1260; x++)
                {
                    Color c = result.GetPixel(x, 420);
                    data[j] = c.R;
                    j++;
                }
                string hash = GetHashString(data);
                Console.WriteLine(hash);
                if (hashes.Contains(hash))
                {
                    Seen();
                }
                else
                {
                    hashes.Add(hash);
                    New();
                }
                //Utils.ReleaseDC(desk, dc);
                Thread.Sleep(25);
            }
    }
}