using System;
using System.Collections.Generic;
public abstract class Player {
    public List<Card> hand { get; }
    public int handSize {
        get { return hand.Count; }
    }
    private int roundsToSkip;
    public readonly string name;
    protected Player(string s) {
        name = s;
        hand = new List<Card>();
        roundsToSkip = 0;
        lastMoves = null;
    }

    public void playCard(Card c) => hand.Remove(c);
    public void drawCard(Card c) => hand.Add(c);

    public bool isStopped() => roundsToSkip > 0;
    public void isSkipping() => roundsToSkip--;
    public void setStops(int x) => roundsToSkip = x; 

    public List<Move> lastMoves;
    public void showHand() {
        foreach(Card c in hand) {
            Console.WriteLine(c);
        }
    }
    public abstract Move makeMove(GameState gs, GameRules gr);
}