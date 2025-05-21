using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace bfloresS6A.Views;

public partial class vAñadirEstudiante : ContentPage
{
	public vAñadirEstudiante()
	{
		InitializeComponent();
	}

    private async void btnAñadir_Clicked(object sender, EventArgs e)
    {
        try
        {
            var producto = new
            {
                Nombre = txtNombre.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text)
            };

            var json = JsonConvert.SerializeObject(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                
                var response = await client.PostAsync("https://localhost:7069/api/Producto", content);

                if (response.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"No se pudo añadir: {error}", "Cerrar");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }
}