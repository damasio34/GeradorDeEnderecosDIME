using System.Collections.Generic;

namespace GeradorDeEnderecosDIME
{
    public class Rua
    {
        public string Nome { get; set; }
        public List<Bloco> Blocos { get; set; }
        
        public Rua(string nome)
        {
            this.Blocos = new List<Bloco>();
            this.Nome = nome;
        }
    }
}
