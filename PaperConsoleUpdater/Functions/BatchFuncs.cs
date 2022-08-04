using System.Diagnostics;

namespace PaperUpdater.Functions; 

public static class BatchFuncs {
    private const string BatchFileContents =
        @"@ECHO OFF
title Personal Minecraft Server
:loop
java -Xmx2048M -Xms2048M -jar paper.jar -d64 -nogui
goto loop
";

    public static void CreateFile(bool onlyCreating) {
        if (File.Exists(Path.Combine(Environment.CurrentDirectory, "run.bat"))) {
            Logger.Warn("run.bat already exists, skipping creation", false);
            return;
        }
        
        if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "run.bat"))) 
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "run.bat"), BatchFileContents);
        if (onlyCreating)
            Logger.Log("Successfully created your batch file.");
    }

    public static void RunServerFromFile() {
        if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "run.bat"))) 
            CreateFile(false);
        
        Process.Start(Path.Combine(Environment.CurrentDirectory, "run.bat"));
        Process.GetCurrentProcess().Kill();
    }
}