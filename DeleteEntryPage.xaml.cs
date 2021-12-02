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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace FinalProjectWPF_CD
{
    /// <summary>
    /// Interaction logic for DeleteEntryPage.xaml
    /// </summary>
    public partial class DeleteEntryPage : Page
    {
        SqlConnection conn;
        SqlCommand cmdToRun;
        bool confirmDelete = false;

        public DeleteEntryPage()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "SearchPage"))
            {
                confirmDelete = false;
                SearchPage searchPage = new SearchPage();
                this.NavigationService.Navigate(searchPage);
            }
        } //end btnSearch_Click

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "CreateEntryPage"))
            {
                confirmDelete = false;
                CreateEntryPage createPage = new CreateEntryPage();
                this.NavigationService.Navigate(createPage);
            }
        } //end btnCreate_Click

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "UpdateEntryPage"))
            {
                confirmDelete = false;
                UpdateEntryPage updatePage = new UpdateEntryPage();
                this.NavigationService.Navigate(updatePage);
            }
        } //end btnUpdate_Click

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "DeleteEntryPage"))
            {
                confirmDelete = false;
                DeleteEntryPage deletePage = new DeleteEntryPage();
                this.NavigationService.Navigate(deletePage);
            }
        } //end btnDelete_Click

        private void txtBoxIDField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxIDField.IsFocused && txtBoxIDField.Text == "Enter unique card ID")
            {
                txtBoxIDField.Text = string.Empty;
            }
        }

        private void btnSubmitDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtBoxIDField.Text) >= 1)
                {
                    using (conn = new SqlConnection("Data Source=localhost;Initial Catalog=CardApp;Integrated Security=true"))
                    {
                        if (confirmDelete)
                        {
                            conn.Open();

                            cmdToRun = new SqlCommand();

                            cmdToRun.Connection = conn;

                            cmdToRun.CommandText = $"DELETE FROM dbo.Cards WHERE ID = \'{txtBoxIDField.Text}\'";

                            DataSet ds = new DataSet();

                            SqlDataAdapter adapter = new SqlDataAdapter();

                            adapter.SelectCommand = cmdToRun;

                            adapter.Fill(ds);

                            imgCardDeleted.Source = new Image().Source;
                            MessageBox.Show($"Successfully deleted card with ID: {txtBoxIDField.Text}.");
                            confirmDelete = false;
                        }
                        else
                        {
                            conn.Open();

                            cmdToRun = new SqlCommand();

                            cmdToRun.Connection = conn;

                            cmdToRun.CommandText = $"SELECT ImageUrl FROM dbo.Cards WHERE ID = \'{txtBoxIDField.Text}\'";

                            using (var reader = cmdToRun.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int ordinalVal = reader.GetOrdinal("ImageUrl");
                                    if (!(string.IsNullOrEmpty(reader.GetString(ordinalVal))))
                                    {
                                        BitmapImage bitmap = new BitmapImage();
                                        bitmap.BeginInit();
                                        bitmap.UriSource = new Uri(reader.GetString(ordinalVal), UriKind.Absolute);
                                        bitmap.EndInit();

                                        imgCardDeleted.Source = bitmap;
                                    }
                                }
                            }


                            MessageBox.Show("Are you sure? Press the delete button again to confirm.");
                            confirmDelete = true;
                        }
                    }
                }
            } catch (Exception ex1)
            {
                confirmDelete = false;
                MessageBox.Show(ex1.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (cmdToRun != null)
                {
                    cmdToRun.Dispose();
                }

            }
        }

    } //end class
} //end namespace
