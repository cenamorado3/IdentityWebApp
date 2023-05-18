using System;
using System.Collections.Generic;

namespace WebApplication1.Identity
{
    public partial class Aspnetuserclaim
    {
        public int Id { get; set; }
        public string Claimtype { get; set; } = null!;
        public string Claimvalue { get; set; } = null!;
        public int UserId { get; set; }

        public virtual Aspnetuser User { get; set; } = null!;
    }
}
