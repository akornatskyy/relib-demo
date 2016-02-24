namespace Public.FunctionalTests.Constants
{
    public static class EnvironmentNames
    {
#if DEBUG
        public const int WebServerPort = 1895;
        public const string PathToWebSite = @"..\..\..\..\Source\Public\WebMvc";
#else
        public const int WebServerPort = 9518;
        public const string PathToWebSite = @"..\..\..\..\..\artifacts\Bin\WebMvc";
#endif
    }
}