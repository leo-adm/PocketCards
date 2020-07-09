using PocketCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCardPage : ContentPage
    {
        public NewCardPage()
        {
            InitializeComponent();
        }

        private async void AddCard(object sender, EventArgs e)
        {
            int level = 1;
            string front = EntryPergunta.Text;
            string back = EntryResposta.Text;
            Card newCard = new Card() 
            { 
                Front = front, 
                Back = back, 
                Level = level, 
                LastFlip = DateTime.Today 
            };

            _ = await App.Database.AddCardAsync(newCard);
            
            Clear();

            _ = EntryPergunta.Focus();
        }

        private void Clear()
        {
            EntryPergunta.Text = "";
            EntryResposta.Text = "";
        }
    }
}