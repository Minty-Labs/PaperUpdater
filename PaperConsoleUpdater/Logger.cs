namespace PaperUpdater; 

public static class Logger {
    public static void WriteLineCentered(string line, int referenceLength = -1) {
        if (referenceLength < 0)
            referenceLength = line.Length;

        Console.WriteLine(line.PadLeft(line.Length + Console.WindowWidth / 2 - referenceLength / 2));
    }
    
    private static string GetTimestamp() => DateTime.Now.ToString("HH:mm:ss.fff");
    
    private static void ResetColors() => Console.ForegroundColor = ConsoleColor.White;
    
    public static void Space() => Console.WriteLine("");

    public static void Log(object s) {
        ResetColors();
        var foregroundColor = Console.ForegroundColor;
        var timestamp = GetTimestamp();
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(timestamp);
        Console.ForegroundColor = foregroundColor;
        Console.Write("] [");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("PaperUpdater");
        Console.ForegroundColor = foregroundColor;
        Console.WriteLine("] " + s);
        Console.ForegroundColor = foregroundColor;
    }
    
    private static int _errorCount;
    
    public static void Error(object s) {
        ResetColors();
        var foregroundColor = Console.ForegroundColor;
        if (_errorCount < 255) {
            var timestamp = GetTimestamp();
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(timestamp);
            Console.ForegroundColor = foregroundColor;
            Console.Write("] [");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("PaperUpdater");
            Console.ForegroundColor = foregroundColor;
            Console.Write("] [");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error");
            Console.ForegroundColor = foregroundColor;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ForegroundColor = foregroundColor;
            _errorCount++;
        }

        if (_errorCount != 255) return;
        var timestamp2 = GetTimestamp();
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(timestamp2);
        Console.ForegroundColor = foregroundColor;
        Console.Write("] [");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("PaperUpdater");
        Console.ForegroundColor = foregroundColor;
        Console.Write("] [");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Error");
        Console.ForegroundColor = foregroundColor;
        Console.Write("] ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The console error limit has been reached.");
        Console.ForegroundColor = foregroundColor;
        _errorCount++;
    }
}