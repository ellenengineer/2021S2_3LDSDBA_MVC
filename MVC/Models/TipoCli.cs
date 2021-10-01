using System;
using System.Collections.Generic;

#nullable disable

namespace MVC.Models
{
    public partial class TipoCli
    {
        public TipoCli()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int CodTipoCli { get; set; }
        public string NomeTipoCli { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
