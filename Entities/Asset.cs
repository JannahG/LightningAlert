using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class Asset
    {
        [JsonPropertyName("assetName")]
        public string? AssetName { get; set; }
        [JsonPropertyName("quadKey")]
        public string? QuadKey { get; set; }
        [JsonPropertyName("assetOwner")]
        public string? AssetOwner { get; set; }
    }
}