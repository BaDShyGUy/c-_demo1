using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        DataSet ds = null;
        SqlConnection conn = null;
        SqlCommand cmdToRun = null;
        SqlDataAdapter adapter = null;


        public SearchPage()
        {
            InitializeComponent();
            listBoxDisplay.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
        }

        private void txtBoxSearchField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxSearchField.IsFocused)
            {
                txtBoxSearchField.Text = string.Empty;
            }
        } //end txtBoxSearchField_GotFocus

        #region Nav buttons
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
        #endregion

        private void btnEnterSearch_Click(object sender, RoutedEventArgs e)
        {
            string fullQuery = ReplaceCharacters(txtBoxSearchField.Text.ToLower().Trim());
            int conditionalColon = fullQuery.IndexOf(":");
            
            if (conditionalColon <= 0)
            {
                MessageBox.Show("Try name:CHARACTERS or set:CHARACTERS to return results.");
                return;
            }

            string searchKeyword = fullQuery.Substring(0, conditionalColon);

            listBoxDisplay.Items.Clear();

            switch (searchKeyword)
            {
                case "name":
                    //Go to database stuff here, or call a function that handles it
                    //listBoxDisplay.Items.Add("name " + fullQuery.Substring(conditionalColon + 1));
                    FindCardsByName(fullQuery.Substring(conditionalColon + 1).Trim());
                    break;
                case "set":
                    //Go to database stuff here, or call a function that handles it.
                    FindCardsBySetName(fullQuery.Substring(conditionalColon + 1).Trim());
                    break;
                default:
                    MessageBox.Show("Try name:CHARACTERS or set:CHARACTERS to return results.");
                    break;
            }
        } //end btnEnterSearch_Click


        #region Utility
        private string ReplaceCharacters(string input)
        {
            return Regex.Replace(input, @"[\<\>\/\\\*\;]+", "");
        }

        private void FindCardsByName(string cname)
        {
            try
            {
                using (conn = new SqlConnection("Data Source=localhost;Initial Catalog=CardApp;Integrated Security=true"))
                {
                    conn.Open();
                    cmdToRun = new SqlCommand();

                    cmdToRun.Connection = conn;

                    cmdToRun.CommandText = $"SELECT * FROM dbo.Cards WHERE CardName LIKE \'{cname}%\'";

                    ds = new DataSet();

                    adapter = new SqlDataAdapter();

                    adapter.SelectCommand = cmdToRun;

                    adapter.Fill(ds);

                    int count = 0;

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //0 = ID
                        //1 = CardName
                        //2 = SetName 
                        //3 = NumberOwned
                        //4 = IsFoil
                        //5 = SetNumber
                        //6 = ImageUrl
                        //7 = NormPrice
                        //8 = FoilPrice
                        if (count > 50) //limit to the first 50 results
                        {
                            break;
                        }
                        if (row != null && !(string.IsNullOrEmpty(row[6].ToString())) )
                        {
                            listBoxDisplay.Items.Add(new Card(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), Convert.ToInt32(row[3]), Convert.ToBoolean(row[4]), Convert.ToInt32(row[5]), row[6].ToString(), Convert.ToDecimal(row[7]), Convert.ToDecimal(row[8])));
                            
                        }
                        count++;
                    }

                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            } finally
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

        private void FindCardsBySetName(string sname)
        {
            try
            {
                using (conn = new SqlConnection("Data Source=localhost;Initial Catalog=CardApp;Integrated Security=true"))
                {
                    conn.Open();
                    cmdToRun = new SqlCommand();

                    cmdToRun.Connection = conn;

                    cmdToRun.CommandText = $"SELECT * FROM dbo.Cards WHERE SetName=\'{sname.ToUpper()}\'";

                    ds = new DataSet();

                    adapter = new SqlDataAdapter();

                    adapter.SelectCommand = cmdToRun;

                    adapter.Fill(ds);

                    int count = 0;

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {

                        if (count > 50) //limit to the first 50 results
                        {
                            break;
                        }

                        if (row != null && !(string.IsNullOrEmpty(row[6].ToString())))
                        {
                            listBoxDisplay.Items.Add(new Card(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), Convert.ToInt32(row[3]), Convert.ToBoolean(row[4]), Convert.ToInt32(row[5]), row[6].ToString(), Convert.ToDecimal(row[7]), Convert.ToDecimal(row[8])));
                        }
                        count++;
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
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



        #endregion



























    } //end class
} //end namespace
