using System.Diagnostics;
using System.Reflection;
using PaperUpdater.PaperData;
using PaperUpdater.Data;
using PaperUpdater.Functions;
using Pastel;

namespace PaperUpdater; 

public static class Program {
    internal static bool PickingLatest, IsRunningDebug; // if false, we're picking the remembered version of the paper
    
    public static void Main(string[] args) {
        Logger.OriginalColor = Console.ForegroundColor;
        IsRunningDebug = Environment.CommandLine.Contains("--debug");
        Console.Title = $"PaperUpdater v{Assembly.GetExecutingAssembly().GetName().Version} by Minty Labs (MintLily){(IsRunningDebug ? " - Running in debug mode" : "")}";
        
        ApplicationUpdateChecker.CheckForUpdates();
        PaperProjectApi.LoadProjectData();
        PaperProjectApi.GetLatestPaperProjectVersion();
        Self.Init();
        reboot:
        Logger.Intro();
        Logger.WriteSeparator('=');
        Logger.Space();
        Start:
        Logger.BasicLog("Selected Paper Type: " + $"{PaperProjectApi.PaperType}".Pastel(PaperProjectApi.PaperType == "paper" ? "FF55D0" : "B53CFF") +
                        " | Server Version: " + $"{Self.ContainedMinecraftVersion ?? "unknown"}".Pastel("00ff00"));
        Logger.Space();
        var disabled = PaperProjectApi.PaperType != "paper";
        Logger.InputOption(1, "Download/Update to latest paper project version", PaperProjectApi.LatestPaperProjectVersion ?? "unknown");
        Logger.InputOption(2, "Download a specific paper minecraft server version", disabled);
        Logger.InputOption(3, "Update to latest Paper version from your remembered minecraft version", Self.ContainedMinecraftVersion ?? "unknown", disabled);
        Logger.InputOption(4, "Create a windows batch script to easily run your server");
        Logger.InputOption(5, "Run the batch file from this application");
        Logger.InputOption(6, "Change Paper Type");
        Logger.Space();
        Logger.Input("Please select an option: ");
        
        var input = Console.ReadLine();
        Logger.ResetColors();
        Logger.Space();
        switch (input) {
            case "1":
                // Update to latest version
                PickingLatest = true;
                PaperProjectApi.LoadProjectData();
                PaperProjectApi.GetLatestPaperProjectVersion();
                Logger.Log($"Latest version is {PaperProjectApi.LatestPaperProjectVersion}");
                PaperBuildApiJson.LoadPaperJson(PaperProjectApi.LatestPaperProjectVersion!);
                PaperBuildApiJson.UpdateJarFile();
                Logger.ResetColors();
                goto Start;
            case "2":
                // Update to specific minecraft version
                if (disabled) {
                    Logger.BasicLog("This option is disabled because you have " + "purpur".Pastel("B53CFF") + " selected as your paper type");
                    Logger.Space();
                    goto Start;
                }
                PickingLatest = false;
                Logger.Input("Please enter the minecraft version you want to update to download: ");
                var inputMcVersion = Console.ReadLine();
                Self.ContainedMinecraftVersion = inputMcVersion;
                Self.Data!.RememberedMinecraftVersion = inputMcVersion;
                Logger.ResetColors();
                Self.Save();
                Logger.Log($"Saved minecraft version {Self.ContainedMinecraftVersion} to file from expected {inputMcVersion}.");
                PaperBuildApiJson.LoadPaperJson(inputMcVersion!);
                PaperBuildApiJson.UpdateJarFile();
                Logger.Space();
                Logger.ResetColors();
                goto Start;
            case "3":
                // Update to latest Paper version from your remembered minecraft version
                if (disabled) {
                    Logger.BasicLog("This option is disabled because you have " + "purpur".Pastel("B53CFF") + " selected as your paper type");
                    Logger.BasicLog("To update " + "purpur".Pastel("B53CFF") + ", please select option " + "#1".Pastel("ffff00"));
                    Logger.Space();
                    goto Start;
                }
                PickingLatest = false;
                Logger.Log("One moment...", false);
                PaperBuildApiJson.LoadPaperJson(Self.ContainedMinecraftVersion!);
                PaperBuildApiJson.UpdateJarFile();
                Logger.Space();
                Logger.ResetColors();
                goto Start;
            case "4":
                // Create a windows batch script to easily run your server
                BatchFuncs.CreateFile(true);
                Logger.Space();
                Logger.ResetColors();
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
                Console.ForegroundColor = Logger.OriginalColor;
                break;
            case "6":
                Logger.Input("Select a paper type (" + "paper".Pastel("FF55D0") + "/" + "purpur".Pastel("B53CFF") + "): ");
                var paperType = Console.ReadLine();
                PaperProjectApi.PaperType = paperType == "purpur" ? "purpur" : "paper";
                Self.Data!.RememberedPaperType = paperType == "purpur" ? "purpur" : "paper";
                Self.Save();
                Console.ForegroundColor = Logger.OriginalColor;
                Console.Clear();
                goto reboot;
            case "c":
            case "e":
            case "q":
            case "x":
            case "cancel":
            case "exit":
            case "quit":
                Logger.Warn("Closing . . .", false);
                Process.GetCurrentProcess().Kill();
                break;
            default:
                Logger.Error("Invalid input, please select an option from the list above (1 - 6).");
                Logger.Space();
                Logger.ResetColors();
                goto Start;
        }
    }
}