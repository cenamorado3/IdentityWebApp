using System;
using System.Collections.Generic;

namespace WebApplication1.Identity
{
    public partial class Aspnetuserlogin
    {
        public int Userid { get; set; }
        public string Loginprovider { get; set; } = null!;
        public string Providerkey { get; set; } = null!;

        public virtual Aspnetuser User { get; set; } = null!;
    }
}
