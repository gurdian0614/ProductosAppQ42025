namespace ProductosAppQ42025.ViewModels;

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductosAppQ42025.Models;
using ProductosAppQ42025.Services;

public partial class ProductoViewModel : ObservableObject
{
    private DataBaseService _dbService;

    [ObservableProperty]
    private Producto _productoSeleccionado;

    [ObservableProperty]
    private ObservableCollection<Producto> _productoCollection;

    public ProductoViewModel()
    {
        _dbService = new DataBaseService();
        ProductoCollection = new ObservableCollection<Producto>();
        LoadProductosCommand.ExecuteAsync(null);
        ProductoSeleccionado = new Producto();
    }

    private void Alerta(string mensaje)
    {
        Application.Current!.MainPage!.DisplayAlert("", mensaje, "Aceptar");
    }

    [RelayCommand]
    private async Task LoadProductos()
    {
        List<Producto> productos =  await _dbService.GetAllProductos();
        ProductoCollection.Clear();

        foreach (Producto producto in productos)
        {
            ProductoCollection.Add(producto);
        }
    }

    [RelayCommand]
    private async Task GuardarActualizarProducto()
    {
        try
        {
            if (ProductoSeleccionado.Nombre is null || ProductoSeleccionado.Nombre == "")
            {
                Alerta("Escriba el nombre del producto");
                return;
            }

            if (ProductoSeleccionado.Id == 0)
            {
                await _dbService.CreateProducto(ProductoSeleccionado);
            } else
            {
                await _dbService.UpdateProdcuto(ProductoSeleccionado);
            }

            await LoadProductos();
            ProductoSeleccionado = new Producto();
        }
        catch (Exception ex)
        {
            Alerta($"Ha ocurrido un error: {ex.Message}");
        }
    }

    [RelayCommand]
    private void NuevoProducto()
    {
        ProductoSeleccionado = new Producto();
    }

    [RelayCommand]
    private async Task EliminarProducto()
    {
        try
        {
            if (ProductoSeleccionado.Id == 0)
            {
                Alerta("Debe seleccionar un producto antes de eliminar");
                return;
            }

            bool respuesta = await Application.Current!.MainPage!.DisplayAlert("ELIMINAR PRODUCTO", "¿Desea eliminar este producto?", "Si", "No");

            if (respuesta)
            {
                await _dbService.DeleteProducto(ProductoSeleccionado);
                await LoadProductos();
                ProductoSeleccionado = new Producto();
                Alerta("Producto eliminado correctamente");
            }
        }
        catch (Exception ex)
        {
            Alerta($"Ha ocurrido un error: {ex.Message}");
        }
    }
}