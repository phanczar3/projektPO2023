using System;
using System.Collections.Generic;
public abstract class Move {}
public class PlayingMove : Move {
    public Card cardPlayed;
}
public class WaitingMove : Move {}
public abstract class Player {
    private List<Card> hand;

    public void playCard(Card c) => hand.Remove(c);
    public void drawCard(Card c) => hand.Add(c);

    public void showHand() {
        foreach(Card c in hand) {
            Console.WriteLine(c.displayName());
        }
    }
    public abstract Move makeMove(Card c);
}