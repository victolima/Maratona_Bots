using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoceriaLima.Form
{
    [Serializable]
    public class Pedido
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Rua { get; set; }
        public string Número { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }

        public FormaPagamento FormaPagamento { get; set; }
        public TipoEntrega TipoEntrega { get; set; }

        public static IForm<Pedido> BuildForm()
        {
            var form = new FormBuilder<Pedido>();
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Message("Boa compra. Será um prazer atender você.");
            //form.Configuration.Yes = new string[] { "sim", "yes", "s" };
            //form.Configuration.No = new string[] { "não", "no", "n" };
            //form.Confirm("Os dados abaixo estão corretos?");
            form.OnCompletion(async (context, pedido) => {
                //Salvar na base de dados
                //Gerar pedido
                //Integrar com o serviço xpto.
                Random random = new Random();
                await context.PostAsync($"Seu pedido número {random.Next(1, 99999)} foi gerado e em breve será entregue.");
            });
            return form.Build();
        }
    }

    public enum FormaPagamento
    {
        Boleto = 1,
        CartãoDeCrédito,
        TransferênciaBancária,
        Paypal
    }

    public enum TipoEntrega
    {
        Transportadora = 1,
        RetiradaNoLocal
    }
}