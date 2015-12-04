using System.Collections.Generic;

namespace GeradorDeEnderecosDIME
{
    public class Bloco
    {
        public List<Pratileira> Pratileiras { get; set; }
        public string Nome { get; set; }

        public Bloco(string nome)
        {
            this.Pratileiras = new List<Pratileira>();
            this.Nome = nome;
        }
    }
}
