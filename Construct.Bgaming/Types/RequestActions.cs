using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construct.Bgaming.Types
{
    public record RequestActions
    {
        [JsonProperty("action")] public string Action { get; set; } = null!;
        [JsonProperty("amount")] public float Amount { get; set; }
        [JsonProperty("action_id")] public long ActionId { get; set; }
    }
}