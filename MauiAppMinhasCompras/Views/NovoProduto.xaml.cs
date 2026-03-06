using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
    }

    // Método executado quando o botão do Toolbar é clicado
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria um novo objeto Produto com os dados digitados nos campos
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte quantidade para número
                Preco = Convert.ToDouble(txt_preco.Text) // Converte preço para número
            };
            // Insere o produto no banco de dados
            await App.Db.Insert(p);
            // Exibe mensagem de sucesso ao salvar
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

        } catch (Exception ex)
        {
            // Caso ocorra erro, mostra a mensagem na tela
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}