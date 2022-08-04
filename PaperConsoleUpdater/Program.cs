using System.Diagnostics;
using System.Reflection;
using PaperUpdater.PaperData;
using PaperUpdater.Data;
using PaperUpdater.Functions;

namespace PaperUpdater; 

public static class Program {
    internal static bool PickingLatest, IsRunningDebug; // if false, we're picking the remembered version of the paper
    
    public static void Main(string[] args) {
        IsRunningDebug = Environment.CommandLine.Contains("--debug");
        Console.Title = $"PaperUpdater v{Assembly.GetExecutingAssembly().GetName().Version} by Minty Labs (MintLily){(IsRunningDebug ? " - Running in debug mode" : "")}";
        
        ApplicationUpdateChecker.CheckForUpdates();
        Server.Init();
        Self.Init();
        
        Logger.Intro();
        Logger.WriteSeparator('=');
        Logger.Space();
        Start:
        Logger.InputOption(1, "Download/Update to latest both minecraft and paper version", true);
        Logger.InputOption(2, "Download a specific paper minecraft server version");
        Logger.InputOption(3, "Update to latest Paper version from your remembered minecraft version");
        Logger.InputOption(4, "Create a windows batch script to easily run your server");
        Logger.InputOption(5, "Run the batch file and close this application");
        Logger.Space();
        Logger.Input("Please select an option: ");
        
        var input = Console.ReadLine();
        Logger.ResetColors();
        Logger.Space();
        switch (input) {
            case "1":
                // Update to latest version
                // PickingLatest = true;
                Logger.Warn("This option is now yet implemented.");
                goto Start;
                // break;
            case "2":
                // Update to specific minecraft version
                PickingLatest = false;
                Logger.Input("Please enter the minecraft version you want to update to download: ");
                var inputMcVersion = Console.ReadLine();
                Self.ContainedMinecraftVersion = inputMcVersion;
                Self.Save();
                PaperApiJson.LoadPaperJson(inputMcVersion!);
                PaperApiJson.UpdateJarFile();
                // Process.GetCurrentProcess().Kill();
                // break;
                Logger.Space();
                goto Start;
            case "3":
                // Update to latest Paper version from your remembered minecraft version
                PickingLatest = false;
                Logger.Log("This program will automatically close once it is finished.", false);
                Logger.Log("One moment...", false);
                PaperApiJson.LoadPaperJson(Self.ContainedMinecraftVersion!);
                PaperApiJson.UpdateJarFile();
                // Process.GetCurrentProcess().Kill();
                // break;
                Logger.Space();
                goto Start;
            case "4":
                // Create a windows batch script to easily run your server
                BatchFuncs.CreateFile(true);
                Logger.Space();
                goto Start;
            case "5":
                // Run the batch file and close this application
                if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "paper.jar"))) {
                    Logger.Warn("It seems you do not have paper.jar in your current directory. Please download it before running this command.", false);
                    Logger.Space();
                    goto Start;
                }
                BatchFuncs.RunServerFromFile();
                Logger.Space();
                break;
            default:
                Logger.Error("Invalid input, please select an option from the list above (1 - 3).");
                Logger.Space();
                goto Start;
        }
    }
}