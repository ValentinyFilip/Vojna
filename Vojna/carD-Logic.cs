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