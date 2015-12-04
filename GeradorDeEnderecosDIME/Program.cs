using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeradorDeEnderecosDIME
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configuração
            const string fileName = @"D:\EnderecosDIME.txt";

            const int quantidadeDeGalpoes = 1;
            const int ultimaLetraDaRua = 'M';
            const int quantidadeDeBlocos = 45;
            const int quantidadeDePrateleiras = 22;

            var galpoes = GerarGalpoes(quantidadeDeGalpoes, ultimaLetraDaRua, quantidadeDeBlocos, quantidadeDePrateleiras);

            // Geração do arquivo
            try
            {
                if (File.Exists(fileName)) File.Delete(fileName);
                using (var fs = File.Create(fileName))
                {
                    var idArea = Guid.NewGuid();
                    galpoes.ForEach(galpao => galpao.Ruas.ForEach(rua => rua.Blocos.ForEach(bloco => bloco.Pratileiras.ForEach(prateleira =>
                    {
                        var endereco = String.Format("{0}{1}{2}{3}", galpao.Nome, rua.Nome, bloco.Nome,prateleira.Nome);
                        var codigo = String.Format("{0}.{1}.{2}.{3}", galpao.Nome, rua.Nome, bloco.Nome, prateleira.Nome);
                        var query = String.Format("INSERT INTO ENDRECO (Id, Codigo, CodigoBarras, IdAreaArmazem) VALUES ('{0}', '{1}', '{2}', '{3}');\r\n", 
                            Guid.NewGuid(), codigo, endereco, idArea);
                        var texto = new UTF8Encoding(true).GetBytes(query);
                        fs.Write(texto, 0, texto.Length);
                    }))));                       
                }
                Console.WriteLine("Documento gerado com sucesso no path {0}", fileName);
                Console.WriteLine();
                Console.WriteLine("Galpões: {0};", quantidadeDeGalpoes);
                Console.WriteLine("Ruas: {0};", galpoes.Sum(g => g.Ruas.Count));
                Console.WriteLine("Blocos: {0};", galpoes.Sum(g => g.Ruas.Sum(r => r.Blocos.Count)));
                Console.WriteLine("Prateleiras: {0};", galpoes.Sum(g => g.Ruas.Sum(r => r.Blocos.Sum(b => b.Pratileiras.Count))));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }                

            Console.ReadKey();
        }

        static List<Galpao> GerarGalpoes(int quantidadeDeGalpoes, int ultimaLetraDaRua, 
            int quantidadeDeBlocos, int quantidadeDePrateleiras)
        {    
            // Criação da estrutura
            var galpoes = new List<Galpao>();
            for (var y = 1; y <= quantidadeDeGalpoes; y++)
            {
                var galpao = new Galpao(String.Format("G{0}", y));

                for (var x = 'A'; x <= ultimaLetraDaRua; x++)
                {
                    var rua = new Rua(String.Format("R{0}", x));

                    for (var i = 1; i <= quantidadeDeBlocos; i++)
                    {
                        var bloco = new Bloco(String.Format("B{0}", i.ToString("00")));
                        for (var j = 1; j <= quantidadeDePrateleiras; j++)
                        {
                            bloco.Pratileiras.Add(new Pratileira(String.Format("P{0}", j.ToString("00"))));
                        }
                        rua.Blocos.Add(bloco);
                    }
                    galpao.Ruas.Add(rua);
                }
                galpoes.Add(galpao);
            }

            return galpoes;
        }
    }
}
