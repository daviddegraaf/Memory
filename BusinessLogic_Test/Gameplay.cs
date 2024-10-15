using BusinessLogic;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Repositories;
using NUnit.Framework;

namespace BusinessLogic_Test
{
    public class Gameplay
    {
        private CardManager _cardManager;
        private HighscoreManager _highscoreManager;
        private Game _game;

        private IRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new ConsoleTestRepository();

            _cardManager = new CardManager(_repository);
            _highscoreManager = new HighscoreManager(_repository);

            List<Card> cards = _cardManager.GenerateCardList(8);
            _game = new Game(cards);
        }

        [Test]
        public void A_GameCards()
        {
            if (_game.Cards.Count != 16) Assert.Fail("There are not 16 cards in the deck.");
        }

        [Test]
        public void B_StartGame()
        {
            var startEvent = false;
            _game.GameStartEvent += (o, e) => startEvent = true;

            _game.Start();

            Assert.IsTrue(_game.IsPlaying && startEvent);
        }

        [Test]
        public void C_SelectValidPair()
        {
            var validPairEvent = false;
            _game.FoundPairEvent += (o, e) => validPairEvent = true;

            int moves = _game.Moves;

            List<Card> pair = _game.Cards.FindAll(x => x.ConsoleLetter == 'A');
            if (pair.Count != 2) Assert.Fail("No pair present of letter A");

            _game.ValidatePair(pair[0], pair[1]);

            pair = _game.Cards.FindAll(x => x.ConsoleLetter == 'A' && x.IsFound == true);
            if (pair.Count != 2) Assert.Fail("Pair is not found after validating.");

            if (moves + 1 != _game.Moves) Assert.Fail("The game moves did not increment.");

            Assert.IsTrue(validPairEvent);
        }

        [Test]
        public void D_SelectInvalidPair()
        {
            var invalidPairEvent = false;
            _game.InvalidPairEvent += (o, e) => invalidPairEvent = true;

            int moves = _game.Moves;

            List<Card> pair = [];
            pair.Add(_game.Cards.Find(x => x.ConsoleLetter == 'B')!);
            pair.Add(_game.Cards.Find(x => x.ConsoleLetter == 'C')!);

            _game.ValidatePair(pair[0], pair[1]);

            if (moves + 1 != _game.Moves) Assert.Fail("The game moves did not increment.");

            Assert.IsTrue(invalidPairEvent);
        }

        [Test]
        public void E_FindAllPairs()
        {
            var foundAllPairsEvent = false;
            _game.FoundAllPairsEvent += (o, e) => foundAllPairsEvent = true;

            List<char> chars = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];

            chars.ForEach(c =>
            {
                List<Card> pair = [];
                pair = _game.Cards.FindAll(x => x.ConsoleLetter == c);
                _game.ValidatePair(pair[0], pair[1]);
            });

            Assert.IsTrue(foundAllPairsEvent);
        }

        [Test]
        public void F_StopGame()
        {

            var stopEvent = false;
            _game.GameEndEvent += (o, e) => stopEvent = true;

            _game.Stop();

            Assert.IsTrue(!_game.IsPlaying && stopEvent);
        }

        [Test]
        public void G_GetPoints()
        {
            _game.Duration = TimeSpan.FromSeconds(10);

            int points = _game.GetPoints();
            Assert.Greater(points, 0);
        }

        [Test]
        public void H_SetHighscore()
        {
            Score score = new Score("Test", 1000, 8);

            if(_highscoreManager.IsHighscore(score)) {
                _highscoreManager.NewHighscore(score);
            }

            Assert.IsTrue(_repository.GetHighscores().Contains(score));
        }
    }
}