using SVPortalAPI;
using SVPortalAPIHelper.Models;
using SVPrayingAssistantDesktop.ViewModels;
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
using System.Windows.Shapes;

namespace SVPrayingAssistantDesktop.Views {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        private MainViewModel mainViewModel;
        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
            var deckCodeBinding = new Binding {
                Path = new PropertyPath(nameof(mainViewModel.DeckCode)),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
            DeckCodeText.SetBinding(TextBox.TextProperty, deckCodeBinding);
        }

        private async void GetDeck(string code) {
            MessageBox.Show("デッキのインポートを開始");
            try {
                var res = await SVPortalHelper.FetchDeckAsync(code);
                mainViewModel.Deck.Cards.Clear();
                mainViewModel.Deck.Cards.AddRange(res.Cards);
                mainViewModel.Deck.Clan = res.Clan;
                MessageBox.Show("デッキのインポートに成功");
            } catch {
                MessageBox.Show("デッキの取得に失敗");
            }
        }

        private void GetDeckButton_Click(object sender, RoutedEventArgs e) {
            if (mainViewModel.DeckCode.Length == 4) {
                GetDeck(mainViewModel.DeckCode);
            } else {
                MessageBox.Show("デッキコードを正しく入力してください");
            }
        }

        private List<List<Card>> GetCardListByName() {
            return mainViewModel.Deck.Cards.GroupBy(card => card.CardName)
                .Select(group => group.Select(card => card).ToList())
                .ToList();
        }
    }
}
