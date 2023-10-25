using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Framework.Utilities
{
    public static class UniqueUserNameGenerator
    {
        public static string NamePrefix { get; set; } = "Diego";
        public static string LastNamePrefix { get; set; } = "Corredor";

        public static string GenerateUniqueName (string prefix)
        {
        var result = string.Concat(prefix, "_", TimestampBuilder.GenerateUniqueText());
        return result;
        }

        public static string GenerateUniqueNameTimestamp()
        {
            var result = $"{NamePrefix}-{TimestampBuilder.GenerateUniqueText()}";
            return result;
        }

        public static string GenerateUniqueLastNameTimestamp()
        {
            var result = $"{LastNamePrefix}-{TimestampBuilder.GenerateUniqueText()}";
            return result;
        }
    }
}
