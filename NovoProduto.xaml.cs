using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Verificar se os campos não estão vazios
            if (string.IsNullOrWhiteSpace(txt_descricao.Text) ||
                string.IsNullOrWhiteSpace(txt_quantidade.Text) ||
                string.IsNullOrWhiteSpace(txt_preco.Text))
            {
                await DisplayAlert("Erro", "Todos os campos devem ser preenchidos.", "OK");
                return;
            }

            // Verificar se as conversões são válidas
            if (!double.TryParse(txt_quantidade.Text, out double quantidade))
            {
                await DisplayAlert("Erro", "Quantidade inválida. Por favor, insira um valor numérico válido.", "OK");
                return;
            }

            if (!double.TryParse(txt_preco.Text, out double preco))
            {
                await DisplayAlert("Erro", "Preço inválido. Por favor, insira um valor numérico válido.", "OK");
                return;
            }

            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = quantidade,
                Preco = preco
            };

            // Inserir na base de dados
            var resultado = await App.Db.Insert(p);

            if (resultado > 0)
            {
                await DisplayAlert("Sucesso!", "Produto inserido com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao inserir produto na base de dados.", "OK");
            }
        }
        catch (Exception ex)
        {
            // Exibir uma mensagem de erro mais detalhada para facilitar a depuração
            await DisplayAlert("Ops", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }
}
