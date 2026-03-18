using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        // envolvimento do try catch para não crachear na tela do usuario
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)

        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {

        // envolvimento do try catch para não crachear na tela do usuario
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)

        {
            DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {

        // envolvimento do try catch para não crachear na tela do usuario

        try
        {
            string q = e.NewTextValue;
            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)

        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        // envolvimento do try catch para não crachear na tela do usuario
        // com isso se houver algum erro durante a execução do aplicativo, aparecerá uma tela de alerta ao usuario informando qual erro apresenta.

        try
        {

            // pergunta ao usuario qual item da lista de produtos ele deseja remover antes dele estar removendo de vez

            MenuItem selecionado = sender as MenuItem;
            Produto p = selecionado.BindingContext as Produto;

            bool confirm = await DisplayAlert(
                "Tem certeza?", $"Deseja remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)

        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }

    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            // selected item será o item que o usuario clicou, sendo convertido para produto. Descobrindo qual produto ele selecionou 

            Produto p = e.SelectedItem as Produto;

            // em seguida iremos navegar para uma pagina onde será editado qual produto o usuario selecionou,
            // fazendo com que os dados do produto apareçam automaticamente na tela de edição.

            Navigation.PushAsync(new Views.EditarProduto
            {
                
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("OPS", ex.Message, "OK");
        }
    }
}