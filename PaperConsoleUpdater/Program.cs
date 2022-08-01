namespace PaperUpdater; 

public static class Program {
    public static void Main(string[] args) {
        PaperApiJson.LoadPaperJson();
        PaperApiJson.UpdateJarFile();
    }
}