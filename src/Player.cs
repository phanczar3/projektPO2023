using System;
using System.Collections.Generic;
public abstract class Move {}
public class PlayingMove : Move {
    public Card cardPlayed;
    public PlayingMove(Card c) => cardPlayed = c;
}
public class WaitingMove : Move {}
public abstract class Player {
    protected List<Card> hand = new List<Card>();
    public int handSize {
        get { return hand.Count; }
    }
    public readonly string name;
    protected Player(string s) {
        name = s;
    }

    public void playCard(Card c) => hand.Remove(c);
    public void drawCard(Card c) => hand.Add(c);

    public void showHand() {
        foreach(Card c in hand) {
            Console.WriteLine(c.displayName());
        }
    }
    public abstract Move makeMove();
}