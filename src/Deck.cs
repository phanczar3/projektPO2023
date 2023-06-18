using System;
using System.Collections.Generic;
public class Deck{
    private static Random rnd = new Random();
    private List<Card> cards = new List<Card>();
    private List<Card> usedCards = new List<Card>();
    public int Count {
        get { return cards.Count; }
    }
    public Deck() {
        foreach(Card.Suit s in Enum.GetValues(typeof(Card.Suit))) {
            foreach(Card.Face f in Enum.GetValues(typeof(Card.Face))) {
                cards.Add(new Card(s, f));
            }
        }
    }
    public Deck(int seed) : this() {
        rnd = new Random(seed);
    }
    public void usedCard(Card c) {
        usedCards.Add(c);
    }
    public Card top() {
        Card c = cards[0];
        cards.RemoveAt(0);
        return c;
    }
    public void shuffle() {
        for(int i = 0; i < cards.Count; i++) {
            int j = rnd.Next(i, cards.Count);
            Card tmp = cards[i];
            cards[i] = cards[j];
            cards[j] = tmp;
        }
    }
    public void printDeck() {
        foreach(Card c in cards) {
            Console.WriteLine(c);
        }
    }
}