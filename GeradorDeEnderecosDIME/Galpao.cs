using System.Collections.Generic;

namespace GeradorDeEnderecosDIME
{
    public class Galpao
    {
        public List<Rua> Ruas { get; set; }
        public string Nome { get; set; }

        public Galpao(string nome)
        {
            this.Ruas = new List<Rua>();
            this.Nome = nome;
        }
    }
}
