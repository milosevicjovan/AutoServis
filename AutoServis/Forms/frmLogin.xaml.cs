using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoServis.Data;

namespace AutoServis.Forms
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        public frmLogin()
        {
            InitializeComponent();
            lblPoruka.Content = "";
            txtKorisnickoIme.Text = "";
            psbLozinka.Password = "";
            txtKorisnickoIme.Focus();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            //zatvara celu aplikaciju
        }

        private void btnUlogujSe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtKorisnickoIme.Text))
                {
                    lblPoruka.Content = "Korisničko ime nije uneto!";
                    txtKorisnickoIme.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(psbLozinka.Password))
                {
                    lblPoruka.Content = "Lozinka nije uneta!";
                    psbLozinka.Focus();
                    return;
                }


                string unetoKorisnickoIme = txtKorisnickoIme.Text;
                string unetaLozinka = psbLozinka.Password;

                if (AutoServisData.IspravnoKorisnickoIme(unetoKorisnickoIme) != true)
                {
                    lblPoruka.Content = "Uneto korisničko ime ne postoji!";
                    txtKorisnickoIme.Focus();
                    return;
                }

                if (AutoServisData.IspravnaLozinka(unetoKorisnickoIme,unetaLozinka) != true)
                {
                    lblPoruka.Content = "Lozinka nije ispravna!";
                    psbLozinka.Focus();
                    return;
                }

                lblPoruka.Content = "";
                frmPocetna pocetna = new frmPocetna();
                pocetna.Show();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: { ex.Message }. Podaci nisu učitani.", "Greška",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
