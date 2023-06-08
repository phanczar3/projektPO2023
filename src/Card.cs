public class Card {
    public enum Suit {Spades=0, Hearts, Clubs, Diamonds};
    public enum Face {Two=2, Three, Four, Five, Six, Seven,
    Eight, Nine, Ten, Jack, Queen, King, Ace};
    
    private Suit suit;
    private Face face;

    public Card(Suit s, Face f) {
        suit = s;
        face = f;
    }
    public string displayName() {
        return face.ToString() + " of " + suit.ToString();
    }
}