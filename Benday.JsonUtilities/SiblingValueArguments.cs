using System;

namespace Benday.JsonUtilities
{
    public class SiblingValueArguments
    {
        public string SiblingSearchValue { get; set; } = string.Empty;
        public string SiblingSearchKey { get; set; } = string.Empty;
        public string[] PathArguments { get; set; } = Array.Empty<string>();
        public string DesiredNodeKey { get; set; } = string.Empty;
        public string DesiredNodeValue { get; set; } = string.Empty;
    }
}
