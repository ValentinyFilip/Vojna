namespace Vojna;

public class CarDLogic
{
    public void Initialize()
    {
        Vars.TotalMoves = 0;
        Vars.MainDeck = new WarDeck();
        Vars.MainDeck.GenerateDeck();
        Vars.MainDeck.ShuffleDeck ();

        // Split deck into 2 (1 for each player)
        Vars.PlayerOne = new WarDeck();
        Vars.PlayerTwo = new WarDeck();
        bool toggle = false;

        foreach (WarCard card in Vars.MainDeck.Stack)
        {
            if (toggle)
            {
                Vars.PlayerOne.Stack.Add(card);
            }
            else
            {
                Vars.PlayerTwo.Stack.Add(card);
            }

            toggle = !toggle;
        }
    }

    public List<WarCard> DrawCard()
    {
        List<WarCard> valuesToReturn = new List<WarCard>();
        if (!Vars.PlayerOne.IsEmpty() && !Vars.PlayerTwo.IsEmpty())
        {
            WarCard playerOneDraw = (WarCard) Vars.PlayerOne.DrawCard ();
            WarCard playerTwoDraw = (WarCard) Vars.PlayerTwo.DrawCard ();
            Vars.TotalMoves++;
            valuesToReturn.Add(playerOneDraw);
            valuesToReturn.Add(playerTwoDraw);
        }
        return valuesToReturn;
    }

    public int War()
    {
        int playerOneWarTotal = new int();
        int playerTwoWarTotal = new int();
        Card playerOneCard;
        Card playerTwoCard;
        int won = 0;
        WarDeck warDeck = new WarDeck();
        if (!Vars.PlayerOne.IsEmpty() && !Vars.PlayerTwo.IsEmpty()) {
            do
            {
                if (!Vars.PlayerOne.IsEmpty() && !Vars.PlayerTwo.IsEmpty()) {
                    for (int i = 0; i < 3; i++)
                    {
                        try {
                            playerOneCard = (WarCard)Vars.PlayerOne.DrawCard();
                            playerOneWarTotal += playerOneCard.GetFaceValue();
                        }
                        catch (Exception e) {
                            Console.WriteLine(e);
                            throw;
                        }
                    
                        try {
                            playerTwoCard = (WarCard)Vars.PlayerTwo.DrawCard();
                            playerTwoWarTotal += playerTwoCard.GetFaceValue();
                        }
                        catch (Exception e) {
                            Console.WriteLine(e);
                            throw;
                        }
                        warDeck.PlaceInDeck(playerOneCard, playerTwoCard);
                    }
                    
                    if (playerOneWarTotal > playerTwoWarTotal) 
                    {
                        Vars.PlayerOne.PlaceInDeckFromList(warDeck);
                        return 1;
                    } 
                    if (playerOneWarTotal < playerTwoWarTotal)
                    {
                        Vars.PlayerTwo.PlaceInDeckFromList(warDeck);
                        return 2;
                    }
                }
                
            } while (false);
        }
        return won;
    }
    
    public int EvaluateResults(WarCard playerOneCard, WarCard playerTwoCard)
    {
        int result;
        if ( playerOneCard.GetFaceValue() > playerTwoCard.GetFaceValue()) {
            Vars.PlayerOne.PlaceInDeck (playerOneCard, playerTwoCard);
            result = 1;
        } else if ( playerOneCard.GetFaceValue() < playerTwoCard.GetFaceValue()) {
            Vars.PlayerTwo.PlaceInDeck (playerOneCard, playerTwoCard);
            result = 2;
        } else {
            result = 3;
        }
        return result;
    }
    
    public class WarCard : Card
    {
        public WarCard(Suite s, Face f) : base(s, f)
        {
        }

        public override void PrintCard()
        {
            throw new NotImplementedException();
        }

        public override int GetFaceValue()
        {
            return ((int)suite);
        }
    }

    /// <summary>
    /// A standard deck for the game of War.
    /// </summary>
    public class WarDeck : Deck
    {
        public int Wins;
        public override void GenerateDeck()
        {
            // Creation of each card suite deck chunk
            for (int k = 0; k < 4; k++)
            {
                // Creation of the individual card
                for (int i = 1; i < 14; i++)
                {
                    Stack.Add(new WarCard((Card.Suite)k, (Card.Face)i));
                }
            }
        }

        public override void ShuffleDeck()
        {
            Random rng = new Random();

            for (int i = Stack.Count - 1; i > 1; i--)
            {
                int k = rng.Next(i);

                WarCard v = (WarCard)Stack[k];
                Stack[k] = Stack[i];
                Stack[i] = v;
            }
        }

        public override Card DrawCard()
        {
            WarCard popCard = (WarCard)Stack[0];
            Stack.Remove(popCard);

            return (popCard);
        }

        public void PlaceInDeckFromList(Deck deck)
        {
            foreach (Card card in deck.Stack)
            {
                Stack.Add(card);
            }
            ShuffleDeck();
        }
        
        public override void PlaceInDeck(Card c1, Card c2)
        {
            Stack.Add(c1);
            Stack.Add(c2);
            ShuffleDeck();
        }

        public override bool IsEmpty()
        {
            return (Stack.Count <= 0);
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

        public abstract void PrintCard();
        public abstract int GetFaceValue();
    }

    /// <summary>
    /// An abstract card deck.
    /// </summary>
    public abstract class Deck
    {
        public List<Card> Stack;
        

        public Deck()
        {
            this.Stack = new List<Card>();
        }

        public abstract void GenerateDeck();
        public abstract void ShuffleDeck();
        public abstract Card DrawCard();
        public abstract void PlaceInDeck(Card c1, Card c2);
        public abstract bool IsEmpty();
    }
}