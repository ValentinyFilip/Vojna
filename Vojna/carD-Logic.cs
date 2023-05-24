namespace Vojna;

public class carD_Logic
{
    public void initialize()
    {
        vars.totalMoves = 0;
        vars.mainDeck = new WarDeck();
        vars.mainDeck.generateDeck();
        vars.mainDeck.shuffleDeck ();

        // Split deck into 2 (1 for each player)
        vars.playerOne = new WarDeck();
        vars.playerTwo = new WarDeck();
        bool toggle = false;

        foreach (WarCard card in vars.mainDeck.stack)
        {
            if (toggle)
            {
                vars.playerOne.stack.Add(card);
            }
            else
            {
                vars.playerTwo.stack.Add(card);
            }

            toggle = !toggle;
        }
    }

    public List<WarCard> drawCard()
    {
        List<WarCard> valuesToReturn = new List<WarCard>();
        if (!vars.playerOne.isEmpty() && !vars.playerTwo.isEmpty())
        {
            WarCard playerOneDraw = (WarCard) vars.playerOne.drawCard ();
            WarCard playerTwoDraw = (WarCard) vars.playerTwo.drawCard ();
            vars.totalMoves++;
            valuesToReturn.Add(playerOneDraw);
            valuesToReturn.Add(playerTwoDraw);
        }
        return valuesToReturn;
    }

    public int war()
    {
        WarDeck warDeck = new WarDeck();
        int playerOneWarTotal = new int();
        int playerTwoWarTotal = new int();
        Card playerOneCard;
        Card playerTwoCard;
        int won = 0;
        vars.warDeck = new List<Card>();
        
        if (vars.playerOne.isEmpty())
        {
            return 20;
        }

        if (vars.playerTwo.isEmpty())
        {
            return 10;
        }

        do
        {
            for (int i = 0; i < 3; i++)
            {
                
                try {
                    playerOneCard = (WarCard)vars.playerOne.drawCard();
                    playerOneWarTotal += playerOneCard.getFaceValue();
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    throw;
                }

                try {
                    playerTwoCard = (WarCard)vars.playerTwo.drawCard();
                    playerTwoWarTotal += playerTwoCard.getFaceValue();
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    throw;
                }
                warDeck.placeInDeck(playerOneCard, playerTwoCard);
            }

            
            if (playerOneWarTotal > playerTwoWarTotal) 
            {
                MessageBox.Show("more prvni cyp vyhral");
                vars.playerOne.placeInDeckFromList(vars.warDeck);
                return 1;
            } 
            else if (playerOneWarTotal < playerTwoWarTotal)
            {
                MessageBox.Show("more druhy cyp vyhral");
                vars.playerTwo.placeInDeckFromList(vars.warDeck);
                return 2;
            }
        } while (true);
        
        return won;
    }
    
    public List<string> evaluateResults(WarCard playerOneCard, WarCard playerTwoCard)
    {
        List<string> result = new List<string>();
        if ((int) playerOneCard.face > (int) playerTwoCard.face) {
            vars.playerOne.placeInDeck (playerOneCard, playerTwoCard);
            result.Add("The Player one has won the cards.\nThe cards have been placed in your deck.\n;\n");
            result.Add("1");
        } else if ((int) playerOneCard.face < (int) playerTwoCard.face) {
            vars.playerTwo.placeInDeck (playerOneCard, playerTwoCard);
            result.Add("The Player two has won the cards.\nThe cards have been placed in the yours deck.\n\n");
            result.Add("2");
        } else {
            result.Add("It's a war!\n\n");
            result.Add("3");
        }
        return result;
    }
    
    public class WarCard : Card
    {
        public WarCard(Suite s, Face f) : base(s, f)
        {
        }

        public override void printCard()
        {
            throw new NotImplementedException();
        }

        public override int getFaceValue()
        {
            return ((int)suite);
        }
    }

    /// <summary>
    /// A standard deck for the game of War.
    /// </summary>
    public class WarDeck : Deck
    {
        public override void generateDeck()
        {
            // Creation of each card suite deck chunk
            for (int k = 0; k < 4; k++)
            {
                // Creation of the individual card
                for (int i = 1; i < 14; i++)
                {
                    stack.Add(new WarCard((Card.Suite)k, (Card.Face)i));
                }
            }
        }

        public override void shuffleDeck()
        {
            Random rng = new Random();

            for (int i = stack.Count - 1; i > 1; i--)
            {
                int k = rng.Next(i);

                WarCard v = (WarCard)stack[k];
                stack[k] = stack[i];
                stack[i] = v;
            }
        }

        public override Card drawCard()
        {
            WarCard popCard;
            try
            {
                popCard = (WarCard)stack[0];
                stack.Remove(popCard);
            }
            catch (Exception e)
            {
                throw;
            }
            

            return (popCard);
        }

        public void placeInDeckFromList(List<Card> deck)
        {
            foreach (Card card in deck)
            {
                stack.Add(card);
            }
            shuffleDeck();
        }
        
        public override void placeInDeck(Card c1, Card c2)
        {
            stack.Add(c1);
            stack.Add(c2);
            shuffleDeck();
        }

        public override bool isEmpty()
        {
            return (stack.Count <= 0);
        }
    }

    /// <summary>
    /// An abstract playing card.
    /// </summary>
    public abstract class Card
    {
        public enum Suite
        {
            Spades = 0,
            Hearts,
            Clubs,
            Diamonds
        };

        public enum Face
        {
            Ace = 1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

        public Suite suite;
        public Face face;

        public Card(Suite s, Face f)
        {
            this.suite = s;
            this.face = f;
        }

        public abstract void printCard();
        public abstract int getFaceValue();
    }

    /// <summary>
    /// An abstract card deck.
    /// </summary>
    public abstract class Deck
    {
        public List<Card> stack;

        public Deck()
        {
            this.stack = new List<Card>();
        }

        public abstract void generateDeck();
        public abstract void shuffleDeck();
        public abstract Card drawCard();
        public abstract void placeInDeck(Card c1, Card c2);
        public abstract bool isEmpty();
    }
}