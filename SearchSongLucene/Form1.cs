using SongSearchBL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchSongLucene
{
    public partial class SearchSongForm : Form
    {
        public SearchSongForm()
        {
            InitializeComponent();
        }

        private void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SearchSongForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                MessageBox.Show("Please type your search term.");
            }
            else
            {

                SearchParameters searchParams = new SearchParameters
                {
                    Terms = txtSearch.Text.Split(' ')
                };

                if (ddlLanguage.SelectedItem != null)
                {
                    Enum.TryParse<Languages>(ddlLanguage.SelectedItem.ToString(), out Languages chosenLanguage);
                    searchParams.Language = chosenLanguage;
                }

                // TODO: CHANGE SEARCH strategies (the second parameter) here to try to LEARN searching
                gvResults.DataSource = Searcher.Search(searchParams, SearchStrategies.MultiFieldParserWithBooleanQuery);
            }
        }


        private void btnIndexData_Click(object sender, EventArgs e)
        {
            Indexer.DeleteIndexes();
            Indexer.IndexSongs();
        }
    }
}
