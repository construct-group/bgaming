using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construct.Bgaming.Types;

public record ResponseTransactions
{
    public string ActionId { get; set; } = null!;
    public string TxId { get; set; } = null!;
    public string ProcessedAt { get; set; } = null!;
}
