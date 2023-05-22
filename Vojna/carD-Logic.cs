using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vojna;

public class carD_Logic
{
    private Random rnd = new Random();
    public void initialize()
    {
        string[] cards = File.ReadAllLines("Vojna/defaulT-Cards.json");
    }

    public void getPlayerCards()
    {
        string[] cards = File.ReadAllLines("D:/Vojna/Vojna/defaulT-Cards.json");
        string[] playerOneCards;
        string[] playerTwoCards;
        
        foreach (var card in cards)
        {
            if JsonSerializer.Deserialize(card)<
        }

        
    }

}



