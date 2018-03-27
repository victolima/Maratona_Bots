using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Bot.Builder.FormFlow;

namespace DoceriaLima.Dialogs
{
    [Serializable]
    [LuisModel("8c3fc65e-0ee1-40a6-8681-e39d49384fbb", "6101b5aa481b43e8885c1e6931e8df5c")]

    public class DoceDialog : LuisDialog<object>
    {
        private enum TipoDeProcessamento { Emocoes, Descricao, Classificacao }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Desculpe, não consegui entender a frase {result.Query}");
        }

        [LuisIntent("Tudo bem")]
        public async Task TudoBem(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Comigo está tudo ótimo, obrigado :)");
        }

        [LuisIntent("Indicações")]
        public async Task Indicacoes(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Atualmente em nosso estoques temos bolos de: Alpino, Brigadeiro, Prestigio, Leite Ninho, Floreta Negra");
        }

        [LuisIntent("Sobre")]
        public async Task Sobre(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Eu sou um bot e estou sempre aprendendo. Tenha paciência comigo");
        }

        [LuisIntent("Cumprimento")]
        public async Task Cumprimento(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Olá! Eu sou o bot da Doceria Lima.");
        }

        [LuisIntent("Alpino")]
        public async Task Alpino(IDialogContext context, LuisResult result)
        {
            var activity = (Activity)context.Activity;
            var message = activity.CreateReply();

            var thumb = new ThumbnailCard()
            {
                Title = "Alpino",
                Subtitle = "Bolo de Alpino",
                Images = new List<CardImage>
                {
                    new CardImage("https://www.google.com.br/imgres?imgurl=http%3A%2F%2Fs.glbimg.com%2Fpo%2Frc%2Fmedia%2F2012%2F06%2F13%2F15%2F39%2F09%2F186%2FImagem_006.jpg&imgrefurl=http%3A%2F%2Fgshow.globo.com%2Freceitas-gshow%2Freceita%2Fbolo-alpino-4e36e71395b5d036a400888f.html&docid=dKw5jYtgPrb59M&tbnid=W_2Gxo6L-IqqOM%3A&vet=10ahUKEwjbq6edjYbaAhVDz1MKHeEFBdcQMwgzKAUwBQ..i&w=3296&h=2472&bih=637&biw=1366&q=bolo%20de%20alpino&ved=0ahUKEwjbq6edjYbaAhVDz1MKHeEFBdcQMwgzKAUwBQ&iact=mrc&uact=8","Alpino")
                }
            };

            message.Attachments.Add(thumb.ToAttachment());
            await context.PostAsync(message);
        }

        [LuisIntent("Prestigio")]
        public async Task Prestigio(IDialogContext context, LuisResult result)
        {
            var activity = (Activity)context.Activity;
            var message = activity.CreateReply();

            var thumb = new ThumbnailCard()
            {
                Title = "Prestigio",
                Subtitle = "Bolo de Prestigio",
                Images = new List<CardImage>
                {
                    new CardImage("https://www.google.com.br/imgres?imgurl=https%3A%2F%2Fimg.cybercook.uol.com.br%2Fimagens%2Freceitas%2F740%2Fbolo-prestigio.jpg&imgrefurl=https%3A%2F%2Fcybercook.uol.com.br%2Fbolo-prestigio-r-12-7740.html&docid=l98n_16xVr65LM&tbnid=PFNzPp7ReomqNM%3A&vet=10ahUKEwiQ67vZjYbaAhUPvVMKHXSKAccQMwhXKBAwEA..i&w=215&h=160&bih=637&biw=1366&q=bolo%20de%20prestigio&ved=0ahUKEwiQ67vZjYbaAhUPvVMKHXSKAccQMwhXKBAwEA&iact=mrc&uact=8","Prestigio")
                }
            };

            message.Attachments.Add(thumb.ToAttachment());
            await context.PostAsync(message);

        }

        [LuisIntent("Leite Ninho")]
        public async Task Leite_Ninho(IDialogContext context, LuisResult result)
        {
            var activity = (Activity)context.Activity;
            var message = activity.CreateReply();

            var thumb = new ThumbnailCard()
            {
                Title = "Leite Ninho",
                Subtitle = "Bolo Leite Ninho",
                Images = new List<CardImage>
                {
                    new CardImage("https://www.google.com.br/imgres?imgurl=http%3A%2F%2Fwww.tudonopotinho.com.br%2Fwp-content%2Fuploads%2F2015%2F08%2Fbolo-mousse-de-leite-ninho1.jpg&imgrefurl=http%3A%2F%2Fwww.tudonopotinho.com.br%2Fbolo-mousse-de-leite-ninho%2F&docid=QHycVXIbXojxuM&tbnid=RuWsT-EDBRwPtM%3A&vet=10ahUKEwjv7pSsjobaAhUEyVMKHb1ZCQwQMwhuKBUwFQ..i&w=870&h=650&bih=637&biw=1366&q=bolo%20de%20leite%20ninho&ved=0ahUKEwjv7pSsjobaAhUEyVMKHb1ZCQwQMwhuKBUwFQ&iact=mrc&uact=8","Leite Ninho")
                }
            };

            message.Attachments.Add(thumb.ToAttachment());
            await context.PostAsync(message);
        }

        [LuisIntent("Brigadeiro")]
        public async Task Brigadeiro(IDialogContext context, LuisResult result)
        {
            var activity = (Activity)context.Activity;
            var message = activity.CreateReply();

            var thumb = new ThumbnailCard()
            {
                Title = "Brigadeiro",
                Subtitle = "Bolo de Brigadeiro.",
                Images = new List<CardImage>
                {
                    new CardImage("https://www.google.com.br/imgres?imgurl=https%3A%2F%2Fimg.elo7.com.br%2Fproduct%2Foriginal%2F18D55C1%2Fbolo-de-brigadeiro-brigadeiro.jpg&imgrefurl=https%3A%2F%2Fwww.elo7.com.br%2Fbolo-de-brigadeiro%2Fdp%2F474919&docid=eSDwtnHWuX9APM&tbnid=5vAls7Lx8XKBwM%3A&vet=10ahUKEwj_5LvnjobaAhUGuFMKHfOvBBIQMwhkKAYwBg..i&w=5005&h=3321&bih=637&biw=1366&q=bolo%20de%20brigadeiro&ved=0ahUKEwj_5LvnjobaAhUGuFMKHfOvBBIQMwhkKAYwBg&iact=mrc&uact=8","Brigadeiro")
                }
            };

            message.Attachments.Add(thumb.ToAttachment());
            await context.PostAsync(message);
        }

        [LuisIntent("Floresta Negra")]
        public async Task Floresta_Negra(IDialogContext context, LuisResult result)
        {
            var activity = (Activity)context.Activity;
            var message = activity.CreateReply();

            var thumb = new ThumbnailCard()
            {
                Title = "Floresta Negra",
                Subtitle = "Bolo de Floresta Negra",
                Images = new List<CardImage>
                {
                    new CardImage("https://www.google.com.br/imgres?imgurl=https%3A%2F%2Fimg.itdg.com.br%2Ftdg%2Fimages%2Frecipes%2F000%2F081%2F014%2F72081%2F72081_original.jpg%3Fmode%3Dcrop%26width%3D370%26height%3D278&imgrefurl=http%3A%2F%2Fwww.tudogostoso.com.br%2Freceita%2F81014-bolo-floresta-negra.html&docid=jl1mGwdzcXufTM&tbnid=OZVOnrhLkUdfBM%3A&vet=10ahUKEwjk0K-oj4baAhWRqlMKHe6hAsoQMwgzKAEwAQ..i&w=370&h=278&bih=637&biw=1366&q=bolo%20de%20floresta%20negra&ved=0ahUKEwjk0K-oj4baAhWRqlMKHe6hAsoQMwgzKAEwAQ&iact=mrc&uact=8","Floresta Negra")
                }
            };

            message.Attachments.Add(thumb.ToAttachment());
            await context.PostAsync(message);
        }

        [LuisIntent("Cotação")]
        public async Task Cotacao(IDialogContext context, LuisResult result)
        {
            var moedas = result.Entities?.Select(e => e.Entity);
            var filtro = string.Join(",", moedas.ToArray());
            var endpoint = $"http://api-cotacoes20180327043804.azurewebsites.net/api/Cotacoes/{filtro}";
            

            await context.PostAsync("Aguarde um momento enquanto eu obtenho os valores...");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    await context.PostAsync("Ocorreu algum erro... tente mais tarde");
                    return;
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<Models.Cotacao[]>(json);
                    var cotacoes = resultado.Select(c => $"R$50 ficará {c.Nome} {50 / c.Valor}");
                    await context.PostAsync($"{string.Join(",", cotacoes.ToArray())}");
                }
            }
        }

        [LuisIntent("Comprar")]
        public async Task Comprar(IDialogContext context, LuisResult result)
        {
            var myform = new FormDialog<Form.Pedido>(new Form.Pedido(), Form.Pedido.BuildForm, FormOptions.PromptInStart, null);
            context.Call(myform, LivroFormComplete);
        }

        private async Task LivroFormComplete(IDialogContext context, IAwaitable<object> result)
        {
            object order = null;
            try
            {
                order = await result;
            }
            catch (OperationCanceledException)
            {
                await context.PostAsync("Você cancelou a compra!");
                return;
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("Preço")]
        public async Task Preco(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"O preço é R$50,00");
        }

        [LuisIntent("Chegou")]
        public async Task Chegou(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Que legal, espero ter gostado de comprar com a gente. Me envia o link de uma foto sua com o produto para postarmos no site :)");
            context.Wait((c, a) => ProcessarImagemAsync(c, a, TipoDeProcessamento.Emocoes));
        }

        private async Task ProcessarImagemAsync(IDialogContext contexto, IAwaitable<IMessageActivity> argument, TipoDeProcessamento tipoDeProcessamento)
        {
            var activity = await argument;
            Uri uri = null;

            uri = activity.Attachments?.Any() == true ?
                new Uri(activity.Attachments[0].ContentUrl) :
                new Uri(activity.Text);

            try
            {
                string reply;

                switch (tipoDeProcessamento)
                {
                    case TipoDeProcessamento.Emocoes:
                        reply = await new Servicos().DeteccaoDeEmocoesAsync(uri);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(tipoDeProcessamento), tipoDeProcessamento, null);
                }

                await contexto.PostAsync(reply);
            }
            catch (Exception)
            {
                await contexto.PostAsync("Ops! Deu algo errado, favor analisar sua imagem!");
            }

            contexto.Wait(MessageReceived);
        }
    }
}