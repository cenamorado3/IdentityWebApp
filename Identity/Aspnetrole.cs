using System;
using System.Collections.Generic;

namespace WebApplication1.Identity
{
    public partial class Aspnetrole
    {
        public Aspnetrole()
        {
            Aspnetuserroles = new HashSet<Aspnetuserrole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Aspnetuserrole> Aspnetuserroles { get; set; }
    }
}
