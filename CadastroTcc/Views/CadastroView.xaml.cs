using System.Text.RegularExpressions;

namespace CadastroTcc.Views;

public partial class CadastroView : ContentPage
{
    public CadastroView()
    {
        InitializeComponent();
        BindingContext = new CadastroTcc.ViewModels.CadastroViewModel();
    }

    private void TelefoneEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            string apenasNumeros = Regex.Replace(e.NewTextValue, "[^0-9]", "");
            if (TelefoneEntry.Text != apenasNumeros)
                TelefoneEntry.Text = apenasNumeros;
        }
    }
}
