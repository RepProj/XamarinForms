using System.Net;
using App01_ConsultarCEP.Servico.Modelo;
using Newtonsoft.Json;
//Datavale
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace App01_ConsultarCEP.Servico
{
   public class ViaCEPServico
   {
      private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json/";

      public static Endereco BuscarEnderecoViaCEP(string cep)
      {
         string NovoEnderecoURL = string.Format(EnderecoURL, cep);
        
         WebClient wc = new WebClient();
         string Conteudo = wc.DownloadString(NovoEnderecoURL);

         //Método Datavale
         object result;
         DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Endereco));
         using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(Conteudo), true))
         {
            result = serializer.ReadObject(ms);
         }

         Endereco endereco = JsonConvert.DeserializeObject<Endereco>(Conteudo);


         if (endereco.cep == null)
         {
            return null;
         }

         return endereco;
      }
   }
}
