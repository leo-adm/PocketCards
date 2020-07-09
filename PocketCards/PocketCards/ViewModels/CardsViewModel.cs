using PocketCards.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PocketCards.ViewModels
{
    public class CardsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ObservableCard> Cards { get; set; }
        private enum ExibitionMode
        {
            All,
            lvl1,
            lvl2,
            lvl3,
            lvl4
        }
        private ExibitionMode Mode { get; set; }
        public Command FilterAll => GetChangeModeCommand(ExibitionMode.All);
        public Command Filter1 => GetChangeModeCommand(ExibitionMode.lvl1);
        public Command Filter2 => GetChangeModeCommand(ExibitionMode.lvl2);
        public Command Filter3 => GetChangeModeCommand(ExibitionMode.lvl3);
        public Command Filter4 => GetChangeModeCommand(ExibitionMode.lvl4);
        public CardsViewModel()
        {
            Cards = new ObservableCollection<ObservableCard>();
        }
        private Command GetChangeModeCommand(ExibitionMode Mode)
        {
            return new Command(async () => { this.Mode = Mode; await LoadCards(); });
        }
        public async Task LoadCards()
        {
            List<Card> cards = await App.Database.GetCardsAsync();
            Cards.Clear();
            foreach (var card in cards)
                AddCard(card);
        }
        public void AddCard(Card card)
        {
            //if (!ShouldShowCard(card))
            //    return;

            if (Mode == ExibitionMode.All || card.Level == (int)Mode)
                Cards.Add(new ObservableCard(card));
        }

        public bool ShouldShowCard(Card card)
        {
            TimeSpan TimeSinceLastFlip = DateTime.Today - card.LastFlip;
            switch (card.Level)
            {
                case 1:
                    if (TimeSinceLastFlip.Days < UserConfig.Level1_Days)
                        return false;
                    break;
                case 2:
                    if (TimeSinceLastFlip.Days < UserConfig.Level2_Days)
                        return false;
                    break;
                case 3:
                    if (TimeSinceLastFlip.Days < UserConfig.Level3_Days)
                        return false;
                    break;
                case 4:
                    if (TimeSinceLastFlip.Days < UserConfig.Level4_Days)
                        return false;
                    break;
            }
            return true;
        }

        #region "PropertyChanged"
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
