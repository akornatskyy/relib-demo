using System;

namespace Public.WebMvc.Constants
{
    public static class VaryByCustomNames
    {
        public static readonly string[] VaryByUser = new string[] 
        { 
            Tickets,

            Home
        };

        public static readonly string[] VaryByScheme = new string[] 
        { 
            Tickets
        };

        public const string Tickets = "Tickets";

        public const string Home = "AboutUs";
    }
}
