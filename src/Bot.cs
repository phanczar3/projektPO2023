using System;
public class Bot : Player {
    private Random rnd;
    public Bot(string s) : base(s) {
        rnd = new Random();
    }
    public override Move makeMove(Card c) {
        int r = rnd.Next(0, hand.Count);
        if(r == 0) return new WaitingMove();
        else return new PlayingMove(hand[r-1]);
    }
}