using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PocketCards.Models
{
    public class ObservableCard : INotifyPropertyChanged
    {
        public Card Card { get; set; }
        public bool HasFlipped { get; private set; } = false;
        public bool ShowBack { get; private set; } = false;
        public bool ShowFront => !ShowBack;
        public Color BorderColor => HasFlipped ? Color.LightGreen : Color.Orange ;
        public ObservableCard(Card card)
        {
            Card = card;
        }
        public async Task Flip()
        {
            ShowBack = !ShowBack;
            OnPropertyChanged("ShowBack");
            OnPropertyChanged("ShowFront");

            if (!HasFlipped)
            {
                Card.LastFlip = DateTime.Today;

                await App.Database.UpdateCardAsync(Card);

                HasFlipped = true;
                OnPropertyChanged("HasFlipped");
                OnPropertyChanged("ButtonText");
                OnPropertyChanged("ButtonCommand");
                OnPropertyChanged("BorderColor");
            }
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
