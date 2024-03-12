using Construct.Bgaming.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construct.Bgaming.Launching.CreateSession
{
    public record CreateSessionResponse
    {
        public decimal Balance { get; init; }
        public Guid GameId { get; init; }
        public ResponseTransactions Transactions { get; init; } = null!;
    }
}