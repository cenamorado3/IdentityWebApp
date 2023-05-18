using System;
using System.Collections.Generic;

namespace WebApplication1.Identity
{
    public partial class Aspnetuser
    {
        public Aspnetuser()
        {
            Aspnetuserclaims = new HashSet<Aspnetuserclaim>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Passwordhash { get; set; }
        public string? Securitystamp { get; set; }
        public string Discriminator { get; set; } = null!;

        public virtual Aspnetuserlogin? Aspnetuserlogin { get; set; }
        public virtual Aspnetuserrole? Aspnetuserrole { get; set; }
        public virtual ICollection<Aspnetuserclaim> Aspnetuserclaims { get; set; }
    }
}
