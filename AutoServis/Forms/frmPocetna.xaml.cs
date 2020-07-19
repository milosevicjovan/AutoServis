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
    /// Interaction logic for frmPocetna.xaml
    /// </summary>
    public partial class frmPocetna : Window
    {
        public frmPocetna()
        {
            InitializeComponent();
            tbNaslov.Text = "";
            lblDatum.Content = $"Datum: { DateTime.Now.ToShortDateString() }";
            lblBrojVozila.Content = $"Broj vozila u bazi: { AutoServisData.BrojRedovaUBazi("tblVozilo", "", "").ToString() }";
            lblNaloga.Content = $"Broj radnih naloga u bazi: { AutoServisData.BrojRedovaUBazi("tblRadniNalog", "", "").ToString() }";
            lblBrojFaktura.Content = $"Broj faktura u bazi: { AutoServisData.BrojRedovaUBazi("tblFaktura", "", "").ToString() }";
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            frmLogin login = new frmLogin();
            this.Close();
            login.ShowDialog();
        }

        private void btnVlasnik_Click(object sender, RoutedEventArgs e)
        {
            pgVlasnici vlasnici = new pgVlasnici();
            var content = vlasnici.Content;
            vlasnici.Content = null;
            grid.Children.Clear();
            grid.Children.Add((UIElement)content);
            vlasnici.txtID.Focus();
            tbNaslov.Text = "Vlasnici";
        }

        private void btnZaposleni_Click(object sender, RoutedEventArgs e)
        {
            pgZaposleni zaposleni = new pgZaposleni();
            var content = zaposleni.Content;
            zaposleni.Content = null;
            grid.Children.Clear();
            grid.Children.Add((UIElement)content);
            zaposleni.txtID.Focus();
            tbNaslov.Text = "Zaposleni";
        }

        private void btnMarka_Click(object sender, RoutedEventArgs e)
        {
            pgMarke marke = new pgMarke();
            var content = marke.Content;
            marke.Content = null;
            grid.Children.Clear();
            grid.Children.Add((UIElement)content);
            marke.txtIDMarke.Focus();
            tbNaslov.Text = "Marke i modeli";
        }

        private void btnRadniNalozi_Click(object sender, RoutedEventArgs e)
        {
            pgRadniNalozi nalozi = new pgRadniNalozi();
            var content = nalozi.Content;
            nalozi.Content = null;
            grid.Children.Clear();
            grid.Children.Add((UIElement)content);
            nalozi.txtID.Focus();
            tbNaslov.Text = "Radni nalozi";
        }

        private void btnFakture_Click(object sender, RoutedEventArgs e)
        {
            pgFakture fakture = new pgFakture();
            var content = fakture.Content;
            fakture.Content = null;
            grid.Children.Clear();
            grid.Children.Add((UIElement)content);
            fakture.txtID.Focus();
            tbNaslov.Text = "Fakture";
        }

        private void btnDelovi_Click(object sender, RoutedEventArgs e)
        {
            pgRadovi radovi = new pgRadovi();
            var content = radovi.Content;
            radovi.Content = null;
            grid.Children.Clear();
            grid.Children.Add((UIElement)content);
            tbNaslov.Text = "Delovi/Usluge";

        }

        private void btnGarancije_Click(object sender, RoutedEventArgs e)
        {
            pgGarancije garancije = new pgGarancije();
            var content = garancije.Content;
            garancije.Content = null;
            grid.Children.Clear();
            grid.Children.Add((UIElement)content);
            garancije.cmbFaktura.Focus();
            tbNaslov.Text = "Garancije";
        }

        private void btnVozila_Click(object sender, RoutedEventArgs e)
        {
            pgVozila vozila = new pgVozila();
            var content = vozila.Content;
            vozila.Content = null;
            grid.Children.Clear();
            grid.Children.Add((UIElement)content);
            vozila.txtID.Focus();
            tbNaslov.Text = "Vozila";
        }
    }
}
