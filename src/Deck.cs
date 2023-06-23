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
        cards = Card.generateStandardDeck();
    }
    public void usedCard(Card c) {
        usedCards.Add(c);
    }
    public Card top() {
        if(cards.Count == 0) {
            shuffleList(ref usedCards);
            cards = usedCards;
            usedCards = new List<Card>();
        }
        if(cards.Count == 0) {
            throw new System.NullReferenceException("no more cards left");
        }
        Card c = cards[0];
        cards.RemoveAt(0);
        return c;
    }
    public void shuffle() {
        shuffleList(ref cards);
    }
    private void shuffleList(ref List<Card> cards) {
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