namespace ProductosAppQ42025.Services;

using ProductosAppQ42025.Models;
using SQLite;

public class DataBaseService : IDataBaseService
{
    private SQLiteAsyncConnection _db;

    public DataBaseService()
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Productos.db3");
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<Producto>();
    }

    public async Task<int> CreateProducto(Producto producto)
    {
        return await _db.InsertAsync(producto);
    }

    public async Task<int> DeleteProducto(Producto producto)
    {
        return await _db.DeleteAsync(producto);
    }

    public async Task<List<Producto>> GetAllProductos()
    {
        return await _db.Table<Producto>().ToListAsync();
    }

    public async Task<int> UpdateProdcuto(Producto producto)
    {
        return await _db.UpdateAsync(producto);
    }
}