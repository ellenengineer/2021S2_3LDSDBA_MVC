using System;
using System.Collections.Generic;

#nullable disable

namespace MVC.Models
{
    public partial class TipoContum
    {
        public TipoContum()
        {
            Conta = new HashSet<Contum>();
        }

        public int CodTipoCta { get; set; }
        public string NomeTipoCta { get; set; }

        public virtual ICollection<Contum> Conta { get; set; }
    }
}
