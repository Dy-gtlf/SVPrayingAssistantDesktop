using SVPortalAPIHelper.Models;
using System.Collections.Generic;

namespace SVPrayingAssistantDesktop.ViewModels {
    public class MainViewModel {
        public Deck Deck { get; set; }

        public string DeckCode { get; set; }

        public List<Card> Cards { get; set; }

        public MainViewModel() {
            Deck = new Deck();
            DeckCode = "";
            Cards = new List<Card>();
        }
    }
}
