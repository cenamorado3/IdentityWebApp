using System;
using System.Collections.Generic;

namespace WebApplication1.Identity
{
    public partial class Aspnetuserrole
    {
        public int Userid { get; set; }
        public int Roleid { get; set; }

        public virtual Aspnetrole Role { get; set; } = null!;
        public virtual Aspnetuser User { get; set; } = null!;
    }
}
