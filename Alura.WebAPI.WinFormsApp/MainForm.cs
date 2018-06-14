using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alura.WebAPI.WinFormsApp
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TipoListaCombo.Items.Clear();
            var tipos = Enum.GetValues(typeof(TipoListaLeitura));
            foreach (var tipo in tipos)
            {
                TipoListaCombo.Items.Add(tipo);
            }
            TipoListaCombo.SelectedIndex = 0;
        }

        private void TipoListaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipo = (TipoListaLeitura)TipoListaCombo.SelectedItem;

            Task.Run(async () => {
                //autenticando
                HttpClient authHttpClient = new HttpClient();
                authHttpClient.BaseAddress = new Uri("http://localhost:5000/api/");
                var loginInfo = new { Login = "admin", Password = "123" };
                var response = await authHttpClient.PostAsJsonAsync("login", loginInfo);
                response.EnsureSuccessStatusCode();
                var token = await response.Content.ReadAsAsync<Token>();

                //recuperando os livros da lista
                HttpClient apiHttpClient = new HttpClient();
                apiHttpClient.BaseAddress = new Uri("http://localhost:6000/api/");
                apiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                response = await apiHttpClient.GetAsync($"listaleitura/{tipo}");
                response.EnsureSuccessStatusCode();
                var lista = await response.Content.ReadAsAsync<ListaLivros>();

                LivrosListBox.Items.Clear();
                LivrosListBox.Items.AddRange(lista.Livros.ToArray());

            });

        }

        private void LivrosListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetalhesButton.Enabled = (LivrosListBox.SelectedIndex >= 0);
        }
    }
}
