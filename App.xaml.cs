using ProductosAppQ42025.Views;

namespace ProductosAppQ42025;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new ProductoView();
	}
}
