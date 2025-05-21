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
    }

    // ? Recarga la lista cada vez que la vista aparece
    protected override void OnAppearing()
    {
        base.OnAppearing();
        mostrarProducto();
    }

    public async void mostrarProducto()
    {
        try
        {
            var content = await cliente.GetStringAsync(URL);
            List<Producto> lista = JsonConvert.DeserializeObject<List<Producto>>(content);
            _productoTem = new ObservableCollection<Producto>(lista);
            lvProducto.ItemsSource = _productoTem;

            // Limpia la selección por si quedó algo seleccionado
            lvProducto.SelectedItem = null;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo cargar la lista: " + ex.Message, "Cerrar");
        }
    }

    private void btnAñadir_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vAñadirEstudiante());
    }

    private void lvProducto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objProducto = (Producto)e.SelectedItem;
        if (objProducto != null)
        {
            Navigation.PushAsync(new vActElim(objProducto));
        }
    }
}
