public class Card {
    public enum Suit {Spades=0, Hearts, Clubs, Diamonds};
    public enum Face {Two=2, Three, Four, Five, Six, Seven,
    Eight, Nine, Ten, Jack, Queen, King, Ace};
    
    public readonly Suit suit;
    public readonly Face face;

    public Card(Suit s, Face f) {
        suit = s;
        face = f;
    }
    public override string ToString() {
        string f = ((int)face).ToString();
        switch(f) {
            case "11":
                f = "J";
                break;
            case "12":
                f = "Q";
                break;
            case "13":
                f = "K";
                break;
            case "14":
                f = "A";
                break;
        }
        string s = ((int)suit).ToString();
        switch(s) {
            case "0":
                s = "\u2660";
                break;
            case "1":
                s = "\u2665";
                break;
            case "2":
                s = "\u2666";
                break;
            case "3":
                s = "\u2663";
                break;
        }
        return f+s;
    }
}