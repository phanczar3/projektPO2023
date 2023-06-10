using System;
using System.Collections.Generic;
public class Deck{
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
    public void giveCard(Player p) {
        Card c = cards[0];
        cards.Remove(c);
        p.drawCard(c);
    }
    public void usedCard(Card c) {
        usedCards.Add(c);
    }
    public Card top() {
        Card c = cards[0];
        cards.RemoveAt(0);
        return c;
    }
}