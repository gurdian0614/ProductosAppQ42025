public interface IDataBaseService
{
    public Task<List<Producto>> GetAllProductos();
    public Task<int> CreateProducto(Producto produxto);
    public Task<int> UpdateProdcuto(Producto producto);
    public Task<int> DeleteProducto(Producto producto);
}