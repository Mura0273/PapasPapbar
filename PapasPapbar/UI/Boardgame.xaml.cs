﻿using PapasPapbar.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PapasPapbar.UI
{
    /// <summary>
    /// Interaction logic for Boardgame.xaml
    /// </summary>
    public partial class Boardgame : Window
    {
        public Boardgame()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private SqlDataReader reader;


        Controller control = new Controller();
        BoardgameRepos BGR = new BoardgameRepos();

        private void Boardgame_Tilbage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Switch = new MainWindow();
            Switch.Show();
            Close();
        }


        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            reader = cmd.ExecuteReader();

            dt.Load(reader);
            txtBrætspil.Focus();
            control.ReadBoardGameData();
            DataGrid1.Columns[0].Visibility = Visibility.Collapsed;
            DataGrid1.ItemsSource = dt.DefaultView;

        }

        //Get Data for datagrid


        //Nulstil Boardgame
        public void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtBrætspil.Text = "";
            txtAntal.Text = "";
            txtAldersgruppe.Text = "";
            txtSpilletid.Text = "";
            txtDistrubutør.Text = "";
            txtGenre.Text = "";
            txtBrætspil.Focus();
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnInsert.IsEnabled = true;
        }



        //Indsætfunktion til Boardgame
        public void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                control.AddBoardGame();
                BGR.BoardgameName = txtBrætspil.Text;
                BGR.PlayerCount = txtAntal.Text;
                BGR.Audience = txtAldersgruppe.Text;
                BGR.GameTime = txtSpilletid.Text;
                BGR.Distributor = txtDistrubutør.Text;
                BGR.GameTag = txtGenre.Text;
                control.UpdateBoardGame();
                MessageBox.Show("Record Save Successfully", "Saved", MessageBoxButton.OK);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Slettefunktion til Boardgame
        public void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            BGR.BoardgameId = txtId.Text;
            control.DeleteBoardGame();
            MessageBox.Show("Record Deleted Successfully", "Deleted", MessageBoxButton.OK);
        }

        //Updatefunktion til Boardgame
        public void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            BGR.BoardgameName = txtBrætspil.Text;
            BGR.PlayerCount = txtAntal.Text;
            BGR.Audience = txtAldersgruppe.Text;
            BGR.GameTime = txtSpilletid.Text;
            BGR.Distributor = txtDistrubutør.Text;
            BGR.GameTag = txtGenre.Text;
            BGR.BoardgameId = txtId.Text;
            control.UpdateBoardGame();
            MessageBox.Show("Record Update Successfully", "Updated", MessageBoxButton.OK);
        }

        //Søgefunktion til Boardgame
        public void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(reader);
            DataGrid1.ItemsSource = dt.DefaultView;
            DataGrid1.Columns[0].Visibility = Visibility.Collapsed;
        }

        public void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowSelected = dg.SelectedItem as DataRowView;
            if (rowSelected != null)
            {
                txtId.Text = rowSelected[0].ToString();
                txtBrætspil.Text = rowSelected[1].ToString();
                txtAntal.Text = rowSelected[2].ToString();
                txtAldersgruppe.Text = rowSelected[3].ToString();
                txtSpilletid.Text = rowSelected[4].ToString();
                txtDistrubutør.Text = rowSelected[5].ToString();
                txtGenre.Text = rowSelected[6].ToString();
            }
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnInsert.IsEnabled = false;
        }
    }
}