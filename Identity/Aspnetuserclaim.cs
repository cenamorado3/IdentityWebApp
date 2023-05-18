using System;
using System.Collections.Generic;

namespace WebApplication1.Identity
{
    public partial class Aspnetuserclaim
    {
        public int Id { get; set; }
        public int Claimtype { get; set; }
        public string Claimvalue { get; set; }
        public int UserId { get; set; }

        public virtual Aspnetuser User { get; set; } = null!;
    }
}
