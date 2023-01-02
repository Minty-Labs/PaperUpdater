namespace PaperUpdater; 

public static class Logger {
    public static ConsoleColor OriginalColor;
    
    private static void WriteLineCentered(string line, int referenceLength = -1) {
        if (referenceLength < 0)
            referenceLength = line.Length;

        Console.WriteLine(line.PadLeft(line.Length + Console.WindowWidth / 2 - referenceLength / 2));
    }
    
    private static void WriteLinesCentered(IList<string> lines) {
        var longestLine = lines.Max(a => a.Length);
        foreach (var line in lines)
            WriteLineCentered(line, longestLine);
    }
    
    public static void WriteSeparator(char character, ConsoleColor color = ConsoleColor.White) {
        var foreColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine("".PadLeft(Console.WindowWidth, character));
        Console.ForegroundColor = foreColor;
    }
    
    private static string GetTimestamp() => DateTime.Now.ToString("HH:mm:ss.fff");
    
    public static void ResetColors() => Console.ForegroundColor = ConsoleColor.White;
    
    public static void Space() => Console.WriteLine("");
    
    public static void BasicLog(object s) => Console.WriteLine($"{s}");

    public static void Log(object s, bool withTimestamp = true) {
        var foregroundColor = Console.ForegroundColor;
        var timestamp = GetTimestamp();
        if (withTimestamp) {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(timestamp);
            Console.ForegroundColor = foregroundColor;
            Console.Write("] ");
        }
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("PaperUpdater");
        Console.ForegroundColor = foregroundColor;
        Console.WriteLine($"] {s}");
        Console.ForegroundColor = foregroundColor;
    }
    
    public static void Warn(object s, bool withTimestamp = true) {
        var foregroundColor = Console.ForegroundColor;
        var timestamp = GetTimestamp();
        if (withTimestamp) {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(timestamp);
            Console.ForegroundColor = foregroundColor;
            Console.Write("] ");
        }
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("PaperUpdater");
        Console.ForegroundColor = foregroundColor;
        Console.Write("] [");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("WARN");
        Console.ForegroundColor = foregroundColor;
        Console.Write("] ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(s);
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

    public static void InputOption(int number, string text, bool optionDisabled = false) {
        var foregroundColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{number}. ");
        Console.ForegroundColor = optionDisabled ? ConsoleColor.DarkGray : foregroundColor;
        Console.WriteLine(text);
        Console.ForegroundColor = foregroundColor;
    }
    
    public static void InputOption(int number, string text, string text2, bool optionDisabled = false) {
        var foregroundColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{number}. ");
        Console.ForegroundColor = optionDisabled ? ConsoleColor.DarkGray : foregroundColor;
        Console.Write(text);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(" -> ");
        Console.ForegroundColor = optionDisabled ? ConsoleColor.DarkGray : ConsoleColor.Green;
        Console.WriteLine(text2);
        Console.ForegroundColor = foregroundColor;
    }

    public static void Input(string text) {
        var foregroundColor = Console.ForegroundColor;
        Console.ForegroundColor = foregroundColor;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("INPUT");
        Console.ForegroundColor = foregroundColor;
        Console.Write($"] {text}");
        Console.ForegroundColor = ConsoleColor.Yellow;
    }

    public static void Intro() {
        ResetColors();
        // Console.Write("Welcome, thanks for using ");
        // Console.ForegroundColor = ConsoleColor.Cyan;
        // Console.Write("PaperUpdater");
        WriteLineCentered("Welcome, thanks for using PaperUpdater!");
        WriteLineCentered("This program is brought to you by Minty Labs, developed by MintLily.");
        ResetColors();
        // Console.WriteLine("!");
    }
    
    public static void SendUpdateNotice() {
        var foreColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        WriteSeparator('-', ConsoleColor.Red);
        WriteLineCentered("A newer version of PaperUpdater is available!");
        WriteSeparator('-', ConsoleColor.Red);
        Console.ForegroundColor = foreColor;
        Space();
    }
}