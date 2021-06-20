using System.Collections.Generic;
using Data.Models;

namespace MercadoClientes.ViewModels
{
    public class ProdutoIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<ProdutoModel> Produtos { get; set; }
    }
}