using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Framework.Utilities
{
    public static class UniqueApplicationInfoGenerator
    {
        public static string TitlePrefix { get; set; } = "App";
        public static string DescriptionPrefix { get; set; } = "Description";

        public static string GenerateUniqueTitle (string prefix)
        {
        var result = string.Concat(prefix, "_", TimestampBuilder.GenerateUniqueText());
        return result;
        }

        public static string GenerateUniqueTitleTimestamp()
        {
            var result = $"{TitlePrefix}-{TimestampBuilder.GenerateUniqueText()}";
            return result;
        }

        public static string GenerateUniqueDescriptionNameTimestamp()
        {
            var result = $"{DescriptionPrefix}-{TimestampBuilder.GenerateUniqueText()}";
            return result;
        }
    }
}
