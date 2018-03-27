using DoceriaLima.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace DoceriaLima
{
    public class Servicos
    {
        private readonly string _emotionApiKey = ConfigurationManager.AppSettings["EmotionApiKey"];
        private readonly string _emotionUri = ConfigurationManager.AppSettings["EmotionApiUri"];

        private static readonly Dictionary<string, string> Adjetivos = new Dictionary<string, string>()
        {
            { "Anger", "brava" },
            { "Contempt", "desprezo" },
            { "Disgust", "desgosto" },
            { "Fear", "medo" },
            { "Happiness", "feliz" },
            { "Neutral", "neutra" },
            { "Sadness", "triste" },
            { "Surprise", "surpresa" }
        };

        public async Task<string> DeteccaoDeEmocoesAsync(Uri query)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _emotionApiKey);

            HttpResponseMessage response = null;

            var byteData = Encoding.UTF8.GetBytes("{ 'url': '" + query + "' }");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(_emotionUri, content).ConfigureAwait(false);
            }

            var responseString = await response.Content.ReadAsStringAsync();

            var emotions = JsonConvert.DeserializeObject<EmotionResult[]>(responseString)
                .Select(e => Adjetivos[e.scores.ToRankedList().First().Key]).ToList();

            if (!emotions.Any()) return "Nenhuma face detectada :( Eu não consegui encontrar " +
                                        "nenhuma pessoa nessa imagem.";

            var count = emotions.Count;

            string retorno;

            switch (count)
            {
                case 1:
                    retorno = $"Eu identifiquei uma pessoa nessa imagem, e ela parece estar **{emotions.First()}**";
                    break;
                default:
                    var builder = new StringBuilder($"E identifiquei **{count}** pessoas nessa imagem, e elas estão: ");
                    for (var i = 0; i < count; i++)
                    {
                        if (i == count - 1) builder.Append(" & ");
                        else if (i != 0) builder.Append(", ");

                        builder.Append($"**{emotions.ElementAt(i)}**");
                    }
                    retorno = builder.ToString();
                    break;
            }

            var dicionarioDeEmocoes = new Dictionary<string, string>();

            foreach (var item in Adjetivos)
            {
                var i = emotions.Count(c => c == item.Value);

                if (i > 0)
                    dicionarioDeEmocoes.Add(item.Value, i.ToString());
            }

            return dicionarioDeEmocoes.Aggregate(retorno, (current, item) =>
                current + $"\n. Gostou da compra? Agradecemos desde já");
        }
    }
}