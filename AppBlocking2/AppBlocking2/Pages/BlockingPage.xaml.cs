using AppBlocking2.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBlocking2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BlockingPage : ContentPage
	{
		public BlockingPage ()
		{
			InitializeComponent ();

            BindingContext = new BlockingViewModel()
            {
                Navigation = this.Navigation
            };
        }
	}
}