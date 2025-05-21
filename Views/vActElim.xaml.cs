using bfloresS6A.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace bfloresS6A.Views;

public partial class vActElim : ContentPage
{
    private Producto _producto;

    public vActElim(Producto datos)
    {
        InitializeComponent();
        _producto = datos;

        // Muestra los datos en los campos de texto
        txtId.Text = _producto.Id.ToString();
        txtNombre.Text = _producto.Nombre;
        txtPrecio.Text = _producto.Precio.ToString("F2");
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var productoActualizado = new Producto
            {
                Id = _producto.Id,
                Nombre = txtNombre.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text)
            };

            var json = JsonConvert.SerializeObject(productoActualizado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var url = $"https://localhost:7069/api/Producto/{_producto.Id}";

            var response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Producto actualizado correctamente", "Cerrar");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", $"Error al actualizar: {response.StatusCode}", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var client = new HttpClient();
            var url = $"https://localhost:7069/api/Producto/{_producto.Id}";

            var response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Producto eliminado correctamente", "Cerrar");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", $"Error al eliminar: {response.StatusCode}", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }
}
