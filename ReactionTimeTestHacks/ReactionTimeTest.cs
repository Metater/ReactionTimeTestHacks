public static class ReactionTimeTest
{
    public static void Run()
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");

        Thread.Sleep(1000);
        Point point = new();
        Utils.GetCursorPos(out point);

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
                //if (process.MainWindowTitle == "Minecraft 1.12.2") handle = process.MainWindowHandle;
                //if (process.Id == 17328) handle = process.MainWindowHandle;
            }
        }

        Color cc = Color.FromArgb(255, 75, 219, 106);
        while (true)
        {
            Color c = Utils.GetColorAt(point.X, point.Y);
            if (c == cc)
            {
                break;
            }
            //Thread.Sleep(0);
        }
        ClickOnPointTool.ClickOnPoint(handle, point);
    }
}