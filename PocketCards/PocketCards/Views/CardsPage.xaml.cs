using PocketCards.Models;
using PocketCards.ViewModels;
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
    public partial class CardsPage : ContentPage
    {
        CardsViewModel viewModel;
        public CardsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CardsViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadCards();
        }
        private async void FlipCard(object sender, EventArgs e)
        {
            Frame CardFrame = (Frame)sender;
            ObservableCard Card = (ObservableCard)CardFrame.BindingContext;

            // PERFORM FIRST HALF OF THE FLIP
            await CardFrame.RotateYTo(90, 200, Easing.CubicInOut);

            // UPDATES ITEM
            await Card.Flip();

            // PERFORM SECOND HALF OF THE FLIP
            if (Card.ShowBack)
                await CardFrame.RotateYTo(180, 200, Easing.CubicInOut);
            else
                await CardFrame.RotateYTo(0, 200, Easing.CubicInOut);
        }
    }
}