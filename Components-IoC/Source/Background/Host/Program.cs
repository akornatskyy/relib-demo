using ReusableLibrary.Host;
using ReusableLibrary.Unity;

namespace Tickets.Background
{
    public static class Program
    {
        internal static void Main()
        {
            UnityBootstrapLoader.Initialize(UnityBootstrapLoader.LoadConfigFilesFromAppSettings());
            ConsoleLauncher.Run();
        }
    }
}
