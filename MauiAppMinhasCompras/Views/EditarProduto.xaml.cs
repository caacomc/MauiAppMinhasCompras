using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    
    // Procedimento será executado para quando o usuario clicar no botão salvará as alterações feitas de um produto
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {

            // produto que está sendo editado
            Produto produto_anexado = BindingContext as Produto;
        
            Produto p = new Produto
            {

            // criando novo objeto com dados atualizados
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text), 
                Preco = Convert.ToDouble(txt_preco.Text) 
            };
            
            // salva as alterações
            await App.Db.Update(p);
           
           // informa ao usuario que foi salvo as alterações
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

            // volta pra tela inicial
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}