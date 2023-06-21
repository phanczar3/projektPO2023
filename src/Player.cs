using System;
using System.Collections.Generic;
public abstract class Move {
    public abstract string display();
}
public class PlayingMove : Move {
    public Card cardPlayed;
    public PlayingMove(Card c) => cardPlayed = c;
    public override string display() {
        return "Play " + cardPlayed;
    }
}
public class WaitingMove : Move {
    public WaitingMove() {}
    public override string display() {
        return "Wait";
    }
}
public class SkippingMove : Move {
    public SkippingMove() {}
    public override string display() {
        return "Skip";
    }
}
public class FinishingMove : Move {
    public FinishingMove() {}
    public override string display() {
        return "Finish";
    }
}
public abstract class Player {
    public List<Card> hand { get; }
    public int handSize {
        get { return hand.Count; }
    }
    public readonly string name;
    protected Player(string s) {
        name = s;
        hand = new List<Card>();
    }

    public void playCard(Card c) => hand.Remove(c);
    public void drawCard(Card c) => hand.Add(c);

    public void showHand() {
        foreach(Card c in hand) {
            Console.WriteLine(c);
        }
    }
    public abstract Move makeMove(GameState gs, GameRules gr);
}