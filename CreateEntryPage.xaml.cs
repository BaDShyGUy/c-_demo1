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
    /// Interaction logic for CreateEntryPage.xaml
    /// </summary>
    public partial class CreateEntryPage : Page
    {
        SqlConnection conn;
        SqlCommand cmdToRun;

        public CreateEntryPage()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "SearchPage"))
            {
                SearchPage searchPage = new SearchPage();
                this.NavigationService.Navigate(searchPage);
            }
        } //end btnSearch_Click

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "CreateEntryPage"))
            {
                CreateEntryPage createPage = new CreateEntryPage();
                this.NavigationService.Navigate(createPage);
            }
        } //end btnCreate_Click

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "UpdateEntryPage"))
            {
                UpdateEntryPage updatePage = new UpdateEntryPage();
                this.NavigationService.Navigate(updatePage);
            }
        } //end btnUpdate_Click

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.NavigationService.Content.GetType().Name.ToString() == "DeleteEntryPage"))
            {
                DeleteEntryPage deletePage = new DeleteEntryPage();
                this.NavigationService.Navigate(deletePage);
            }
        } //end btnDelete_Click

        private void btnRunCreate_Click(object sender, RoutedEventArgs e)
        {
            //check the values of the text boxes first before even beginning to send the query
            var txtBoxes = CreateEntryPageGrid.Children.OfType<TextBox>();

            foreach (TextBox txtbox in txtBoxes)
            {
                if (string.IsNullOrEmpty(txtbox.Text))
                {
                    MessageBox.Show("One or more fields are empty.");
                    return;
                }
                if (txtbox.Text == "Enter a card name" ||
                    txtbox.Text == "Enter a set name (3 characters)" ||
                    txtbox.Text == "Enter number of cards owned" ||
                    txtbox.Text == "Enter 1 if card is foil, 0 if not" ||
                    txtbox.Text == "Enter set number" ||
                    txtbox.Text == "Enter scryfall card image url" ||
                    txtbox.Text == "Enter normal card price (0 if only in foil)" ||
                    txtbox.Text == "Enter foil card price (0 if has no foil)"
                    )
                {
                    MessageBox.Show("One or more fields haven't been changed.");
                    return;
                }
            }

            if (txtBoxSetNameEntry.Text.Length != 3)
            {
                MessageBox.Show("Invalid set name.");
                return;
            }

            if (!(Convert.ToInt32(txtBoxNumOwnedEntry.Text) >= 0))
            {
                MessageBox.Show("Invalid number of owned cards.");
                return;
            }

            if (!(Convert.ToInt32(txtBoxSetNumberEntry.Text) != 3))
            {
                MessageBox.Show("Invalid set number.");
                return;
            }

            if (Convert.ToDecimal(txtBoxNormPriceEntry.Text) < 0.00M)
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            if (Convert.ToDecimal(txtBoxFoilPriceEntry.Text) < 0.00M)
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            //Check all the easier/less memory/processing consuming checks first so that it doesn't have to process this one unless all the rest are valid.
            string url = txtBoxImageUrlEntry.Text;
            var finalUrl = url.Split("\t\n ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Where(l => l.StartsWith("http://") || l.StartsWith("www.") || l.StartsWith("https://") && l.Contains("c1.scryfall")).ToList();
            
            if (finalUrl == null || finalUrl.Count <= 0)
            {
                MessageBox.Show("Invalid image url.");
                return;
            }

            //0 = ID
            //1 = CardName
            //2 = SetName 
            //3 = NumberOwned
            //4 = IsFoil
            //5 = SetNumber
            //6 = ImageUrl
            //7 = NormPrice
            //8 = FoilPrice

            string cname = txtBoxCardNameEntry.Text;
            string setname = txtBoxSetNameEntry.Text.ToUpper();
            int numown = Convert.ToInt32(txtBoxNumOwnedEntry.Text);
            var foil = Convert.ToByte(txtBoxFoilEntry.Text);
            int setnum = Convert.ToInt32(txtBoxSetNumberEntry.Text);
            //finalurl[0]
            decimal nprice = Convert.ToDecimal(txtBoxNormPriceEntry.Text);
            decimal fprice = Convert.ToDecimal(txtBoxFoilPriceEntry.Text);
            
            try
            {

                using (conn = new SqlConnection("Data Source=localhost;Initial Catalog=CardApp;Integrated Security=true"))
                {
                    conn.Open();
                    cmdToRun = new SqlCommand();

                    cmdToRun.Connection = conn;

                    cmdToRun.CommandText = $"INSERT INTO dbo.Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) VALUES(\'{cname}\', \'{setname}\', \'{numown}\', \'{foil}\', \'{setnum}\', \'{finalUrl[0]}\', \'{nprice}\', \'{fprice}\')";

                    DataSet ds = new DataSet();

                    SqlDataAdapter adapter = new SqlDataAdapter();

                    adapter.SelectCommand = cmdToRun;

                    adapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            }
            MessageBox.Show($"Successfully added card to the database, with name: {cname}.");
            txtBoxCardNameEntry.Text = "Enter a card name";
            txtBoxSetNameEntry.Text = "Enter a set name (3 characters)";
            txtBoxNumOwnedEntry.Text = "Enter number of cards owned";
            txtBoxFoilEntry.Text = "Enter 1 if card is foil, 0 if not";
            txtBoxSetNumberEntry.Text = "Enter set number";
            txtBoxImageUrlEntry.Text = "Enter scryfall card image url";
            txtBoxNormPriceEntry.Text = "Enter normal card price (0 if only in foil)";
            txtBoxFoilPriceEntry.Text = "Enter foil card price (0 if has no foil)";
        }

        private void txtBoxCardNameEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxCardNameEntry.IsFocused && txtBoxCardNameEntry.Text == "Enter a card name")
            {
                txtBoxCardNameEntry.Text = string.Empty;
            }
        }

        private void txtBoxSetNameEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxSetNameEntry.IsFocused && txtBoxSetNameEntry.Text == "Enter a set name (3 characters)")
            {
                txtBoxSetNameEntry.Text = string.Empty;
            }
        }

        private void txtBoxNumOwnedEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxNumOwnedEntry.IsFocused && txtBoxNumOwnedEntry.Text == "Enter number of cards owned")
            {
                txtBoxNumOwnedEntry.Text = string.Empty;
            }
        }

        private void txtBoxFoilEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxFoilEntry.IsFocused && txtBoxFoilEntry.Text == "Enter 1 if card is foil, 0 if not")
            {
                txtBoxFoilEntry.Text = string.Empty;
            }
        }

        private void txtBoxSetNumberEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxSetNumberEntry.IsFocused && txtBoxSetNumberEntry.Text == "Enter set number")
            {
                txtBoxSetNumberEntry.Text = string.Empty;
            }
        }

        private void txtBoxImageUrlEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxImageUrlEntry.IsFocused && txtBoxImageUrlEntry.Text == "Enter scryfall card image url")
            {
                txtBoxImageUrlEntry.Text = string.Empty;
            }
        }

        private void txtBoxNormPriceEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxNormPriceEntry.IsFocused && txtBoxNormPriceEntry.Text == "Enter normal card price (0 if only in foil)")
            {
                txtBoxNormPriceEntry.Text = string.Empty;
            }
        }

        private void txtBoxFoilPriceEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxFoilPriceEntry.IsFocused && txtBoxFoilPriceEntry.Text == "Enter foil card price (0 if has no foil)")
            {
                txtBoxFoilPriceEntry.Text = string.Empty;
            }
        }



    } //end class
} //end namespace
