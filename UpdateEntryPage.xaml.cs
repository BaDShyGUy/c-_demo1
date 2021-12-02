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

namespace FinalProjectWPF_CD
{
    /// <summary>
    /// Interaction logic for UpdateEntryPage.xaml
    /// </summary>
    public partial class UpdateEntryPage : Page
    {
        SqlConnection conn;
        SqlCommand cmdToRun;
        bool confirmUpdate = false;
        
        public UpdateEntryPage()
        {
            InitializeComponent();
        }

        #region Nav buttons
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "SearchPage"))
            {
                confirmUpdate = false;
                SearchPage searchPage = new SearchPage();
                this.NavigationService.Navigate(searchPage);
            }
        } //end btnSearch_Click

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "CreateEntryPage"))
            {
                confirmUpdate = false;
                CreateEntryPage createPage = new CreateEntryPage();
                this.NavigationService.Navigate(createPage);
            }
        } //end btnCreate_Click

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "UpdateEntryPage"))
            {
                confirmUpdate = false;
                UpdateEntryPage updatePage = new UpdateEntryPage();
                this.NavigationService.Navigate(updatePage);
            }
        } //end btnUpdate_Click

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "DeleteEntryPage"))
            {
                confirmUpdate = false;
                DeleteEntryPage deletePage = new DeleteEntryPage();
                this.NavigationService.Navigate(deletePage);
            }
        } //end btnDelete_Click
        #endregion


        private void txtBoxIDField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxIDField.IsFocused && txtBoxIDField.Text == "Enter card's unique ID (only required field)")
            {
                txtBoxIDField.Text = string.Empty;
            }
        }

        private void txtBoxCardNameField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxCardNameField.IsFocused && txtBoxCardNameField.Text == "Enter card's name")
            {
                txtBoxCardNameField.Text = string.Empty;
            }
        }

        private void txtBoxSetNameField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxSetNameField.IsFocused && txtBoxSetNameField.Text == "Enter card's set name (three characters)")
            {
                txtBoxSetNameField.Text = string.Empty;
            }
        }

        private void txtBoxNumOwnedField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxNumOwnedField.IsFocused && txtBoxNumOwnedField.Text == "Enter number owned")
            {
                txtBoxNumOwnedField.Text = string.Empty;
            }
        }

        private void txtBoxFoilField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxFoilField.IsFocused && txtBoxFoilField.Text == "Enter 1 if foil, 0 if not")
            {
                txtBoxFoilField.Text = string.Empty;
            }
        }

        private void txtBoxSetNumberField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxSetNumberField.IsFocused && txtBoxSetNumberField.Text == "Enter set number")
            {
                txtBoxSetNumberField.Text = string.Empty;
            }
        }

        private void txtBoxImageUrlField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxImageUrlField.IsFocused && txtBoxImageUrlField.Text == "Enter scryfall image url")
            {
                txtBoxImageUrlField.Text = string.Empty;
            }
        }

        private void txtBoxNormPriceField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxNormPriceField.IsFocused && txtBoxNormPriceField.Text == "Enter normal price, empty if none")
            {
                txtBoxNormPriceField.Text = string.Empty;
            }
        }

        private void txtBoxFoilPriceField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxFoilPriceField.IsFocused && txtBoxFoilPriceField.Text == "Enter foil price, empty if none")
            {
                txtBoxFoilPriceField.Text = string.Empty;
            }
        }

        private void btnSubmitUpdate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Convert.ToInt32(txtBoxIDField.Text) >= 1)
                {
                    if (confirmUpdate)
                    {
                        try
                        {
                            using (conn = new SqlConnection("Data Source=localhost;Initial Catalog=CardApp;Integrated Security=true"))
                            {
                                conn.Open();

                                cmdToRun = new SqlCommand();

                                cmdToRun.Connection = conn;

                                cmdToRun.CommandText = $"UPDATE dbo.Cards SET "; //start of command


                                if (!(string.IsNullOrEmpty(txtBoxCardNameField.Text)) && txtBoxCardNameField.Text != "Enter card's name")
                                {
                                    cmdToRun.CommandText += $"CardName = \'{txtBoxCardNameField.Text}\',";
                                }

                                if (!(string.IsNullOrEmpty(txtBoxSetNameField.Text)) && txtBoxSetNameField.Text != "Enter card's set name (three characters)" && txtBoxSetNameField.Text.Length == 3)
                                {
                                    cmdToRun.CommandText += $" SetName = \'{txtBoxSetNameField.Text.ToUpper()}\',";
                                }

                                if (!(string.IsNullOrEmpty(txtBoxNumOwnedField.Text)) && txtBoxNumOwnedField.Text != "Enter number owned" && Convert.ToInt32(txtBoxNumOwnedField.Text) >= 0)
                                {
                                    cmdToRun.CommandText += $" NumberOwned = \'{txtBoxNumOwnedField.Text}\',";
                                }

                                if (!(string.IsNullOrEmpty(txtBoxFoilField.Text)) && txtBoxFoilField.Text != "Enter 1 if foil, 0 if not" && Convert.ToInt32(txtBoxFoilField.Text) <= 1 && Convert.ToInt32(txtBoxFoilField.Text) >= 0)
                                {
                                    cmdToRun.CommandText += $" IsFoil = \'{Convert.ToByte(txtBoxCardNameField.Text)}\',";
                                }

                                if (!(string.IsNullOrEmpty(txtBoxSetNumberField.Text)) && txtBoxSetNumberField.Text != "Enter set number" && Convert.ToInt32(txtBoxSetNumberField.Text) > 0)
                                {
                                    cmdToRun.CommandText += $" SetNumber = \'{txtBoxSetNumberField.Text}\',";
                                }

                                if (!(string.IsNullOrEmpty(txtBoxImageUrlField.Text)) && txtBoxImageUrlField.Text != "Enter scryfall image url" && txtBoxImageUrlField.Text.Contains("c1.scryfall"))
                                {
                                    cmdToRun.CommandText += $" ImageUrl = \'{txtBoxImageUrlField.Text}\',";
                                }

                                if (!(string.IsNullOrEmpty(txtBoxNormPriceField.Text)) && txtBoxNormPriceField.Text != "Enter normal price, empty if none" && Convert.ToDecimal(txtBoxNormPriceField.Text) >= 0.00M)
                                {
                                    cmdToRun.CommandText += $" NormPrice = \'{txtBoxNormPriceField.Text}\',";
                                }

                                if (!(string.IsNullOrEmpty(txtBoxFoilPriceField.Text)) && txtBoxFoilPriceField.Text != "Enter foil price, empty if none" && Convert.ToDecimal(txtBoxFoilPriceField.Text) >= 0.00M)
                                {
                                    cmdToRun.CommandText += $" FoilPrice = \'{txtBoxFoilPriceField.Text}\'";
                                }

                                if (cmdToRun.CommandText.EndsWith(","))
                                {
                                    cmdToRun.CommandText = cmdToRun.CommandText.Substring(0, (cmdToRun.CommandText.Length - 1)); //removes the last comma if it's there which messes with sql syntax
                                }

                                if (cmdToRun.CommandText.EndsWith("SET "))
                                {
                                    MessageBox.Show("Nothing to update.");
                                    return;
                                }

                                cmdToRun.CommandText += $" WHERE ID = \'{txtBoxIDField.Text}\'"; //end of command

                                DataSet ds = new DataSet();

                                SqlDataAdapter adapter = new SqlDataAdapter();

                                adapter.SelectCommand = cmdToRun;

                                adapter.Fill(ds);

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

                                            imgCardToUpdate.Source = bitmap;
                                        }
                                    }
                                }

                                MessageBox.Show("Update successful.");
                            }
                        }
                        catch (Exception ex2)
                        {
                            MessageBox.Show(ex2.Message);
                            return;
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
                            confirmUpdate = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Are you sure? Click the update button again to submit changes.");
                        confirmUpdate = true;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid card ID.");
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

    } //end class
} //end namespace
