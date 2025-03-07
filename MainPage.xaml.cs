using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras
{
    public partial class MainPage : ContentPage
    {
        private SQLiteDatabaseHelper _db;
        public ObservableCollection<Produto> Produtos { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _db = new SQLiteDatabaseHelper(Path.Combine(FileSystem.AppDataDirectory, "produtos.db"));
            Produtos = new ObservableCollection<Produto>();
            BindingContext = this;
            CarregarProdutos();
        }

        private async void CarregarProdutos()
        {
            var lista = await _db.GetAll();
            Produtos.Clear();
            foreach (var item in lista)
            {
                Produtos.Add(item);
            }
        }

        private async void OnAdicionarProduto(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ProdutoEntry.Text))
            {
                var produto = new Produto { Nome = ProdutoEntry.Text };
                await _db.Insert(produto);
                Produtos.Add(produto);
                ProdutoEntry.Text = "";
            }
        }

        private async void OnRemoverProduto(object sender, EventArgs e)
        {
            var button = sender as Button;
            var produto = button?.CommandParameter as Produto;
            if (produto != null)
            {
                await _db.Delete(produto.Id);
                Produtos.Remove(produto);
            }
        }
    }
}
