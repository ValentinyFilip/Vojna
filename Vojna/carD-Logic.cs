namespace Vojna;

/// <summary>
/// A standard playing card for the game of War.
/// </summary>
public class WarCard : Card {
    public WarCard(Suite s, Face f) : base(s, f) {
    }

    public override void PrintCard() {
        throw new NotImplementedException();
    }

    public override int GetFaceValue() {
        return ((int)suite);
    }
}

/// <summary>
/// A standard deck for the game of War.
/// </summary>
public class WarDeck : Deck {
    public void InitiliazeDecks() {
        WarDeck mainDeck = new WarDeck ();
        mainDeck.GenerateDeck();
        mainDeck.ShuffleDeck();

        bool Toggle = false;
        
        foreach (WarCard card in mainDeck.Stack) {
            if (Toggle) {
                Vars.Decks[0].Stack.Add(card);
            } else {
                Vars.Decks[1].Stack.Add(card);
            }
            Toggle = !Toggle;
        }
        
    }

    public void OnePlay() {
        WarCard playerDraw = (WarCard) Vars.Decks[0].DrawCard();
        WarCard aiDraw = (WarCard) Vars.Decks[1].DrawCard();

        
        if ( playerDraw.GetFaceValue() > aiDraw.GetFaceValue()) {
            //Console.WriteLine ("The Player has won the cards.\n\n");
            Vars.Decks[0].PlaceInDeck (playerDraw, aiDraw);
        } else if ( playerDraw.GetFaceValue() < aiDraw.GetFaceValue()) {
            //Console.WriteLine ("The Computer has won the cards.\n\n");
            Vars.Decks[1].PlaceInDeck (playerDraw, aiDraw);
        } else {
            //Console.WriteLine ("It's a war!\n\n");
            int won = War();
            if (won == 0) {
                //Console.WriteLine ("The Player has won the cards.\n\n");
                Vars.Decks[0].PlaceInDeck (playerDraw, aiDraw);
            } else {
                Vars.Decks[1].PlaceInDeck (playerDraw, aiDraw);
            }
        }
    }

    private int War() {
        int playerWarTotal = new int();
        int aiWarTotal = new int();
        int won;
        
        for (int i = 0; i < 3; i++) {
            playerWarTotal += ((WarCard) Vars.Decks[0].DrawCard()).GetFaceValue();
            aiWarTotal += ((WarCard) Vars.Decks[1].DrawCard()).GetFaceValue();
        }

        if (playerWarTotal > aiWarTotal) {
            won = 0;
        } else  {
            won = 1;
        }

        return won;
    }

    public override void GenerateDeck() {
        // Creation of each card suite deck chunk
        for (int k = 0; k < 4; k++) {
            // Creation of the individual card
            for (int i = 1; i < 14; i++) {
                Stack.Add(new WarCard((Card.Suite)k, (Card.Face)i));
            }
        }
    }

    public override void ShuffleDeck() {
        Random rng = new Random();

        for (int i = Stack.Count - 1; i > 1; i--) {
            int k = rng.Next(i);

            WarCard v = (WarCard)Stack[k];
            Stack[k] = Stack[i];
            Stack[i] = v;
        }
    }

    public override Card DrawCard() {
        WarCard popCard = (WarCard)Stack[0];
        Stack.Remove(popCard);

        return (popCard);
    }

    public override void PlaceInDeck(Card c1, Card c2) {
        Stack.Add(c1);
        Stack.Add(c2);
        ShuffleDeck();
    }

    public override bool IsEmpty() {
        return (Stack.Count <= 0);
    }
}

/// <summary>
/// An abstract playing card.
/// </summary>
public abstract class Card {
    public enum Suite {
        Spades = 0,
        Hearts,
        Clubs,
        Diamonds
    };

    public enum Face {
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

    public Card(Suite s, Face f) {
        this.suite = s;
        this.face = f;
    }

    public abstract void PrintCard();
    public abstract int GetFaceValue();
}

/// <summary>
/// An abstract card deck.
/// </summary>
public abstract class Deck {
    public List<Card> Stack;

    public Deck() {
        this.Stack = new List<Card>();
    }

    public abstract void GenerateDeck();
    public abstract void ShuffleDeck();
    public abstract Card DrawCard();
    public abstract void PlaceInDeck(Card c1, Card c2);
    public abstract bool IsEmpty();
}