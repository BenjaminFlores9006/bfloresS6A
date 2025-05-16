using System.Collections.ObjectModel;
using bfloresS6A.Models;
using Newtonsoft.Json;

namespace bfloresS6A.Views;

public partial class vCrud : ContentPage
{
	private const string URL = "https://localhost:7069/api/Producto";
	private HttpClient cliente = new HttpClient();
	private ObservableCollection<Producto> _productoTem;
	public vCrud()
	{
		InitializeComponent();
		mostrarProducto();
	}

	public async void mostrarProducto()
	{
		var content = await cliente.GetStringAsync(URL);
		List<Producto> lista = JsonConvert.DeserializeObject<List<Producto>>(content);
		_productoTem = new ObservableCollection<Producto>(lista);
		lvProducto.ItemsSource = _productoTem;
	}

}