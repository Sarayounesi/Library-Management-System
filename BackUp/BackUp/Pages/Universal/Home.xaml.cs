using System.Windows.Controls;

namespace Library_Manager.Pages.Universal
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home(string type, string name = "")
        {
            InitializeComponent();
            homeName.Text = type + " Panel";
            wel.Text += " " + name;
        }
    }
}
