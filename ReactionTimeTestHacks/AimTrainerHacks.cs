public static class AimTrainerHacks
{
    public static void Run()
    {
#pragma warning disable CA1416 // Validate platform compatibility

        Thread.Sleep(5000);

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


        Color blue = Color.FromArgb(255, 149, 195, 232);

        //468, 172
        //1437, 671
        // 100px wide

        IntPtr desk = Utils.GetDesktopWindow();
        IntPtr dc = Utils.GetWindowDC(desk);
        for (int i = 0; i < 30; i++)
        {
            Bitmap b = ScreenCapture.CaptureActiveWindow();
            Point click = new(0, 0);
            for (int y = 172; y < 671; y += 33)
            {
                //Console.WriteLine("y " + y);
                for (int x = 468; x < 1437; x += 33)
                {
                    //Console.WriteLine("x " + x);
                    //int a = (int)Utils.GetPixel(dc, x, y);
                    //Color c = Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
                    Color c = b.GetPixel(x, y);
                    if (c == blue)
                    {
                        click = new Point(x, y);
                        break;
                    }
                }
                if (click.X != 0) break;
            }
            ClickOnPointTool.ClickOnPoint(handle, click);
            Console.WriteLine("click " + i);
            Thread.Sleep(75);
        }
        Utils.ReleaseDC(desk, dc);
    }
}
