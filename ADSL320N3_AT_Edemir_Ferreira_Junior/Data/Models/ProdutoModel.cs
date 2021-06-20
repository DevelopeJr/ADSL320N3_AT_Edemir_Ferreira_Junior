using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProdutoModel
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string CaracteristicasProduto { get; set; }
        public int QuantidadeProduto { get; set; }
        public DateTime DataFabricacao { get; set; }
    }
}
