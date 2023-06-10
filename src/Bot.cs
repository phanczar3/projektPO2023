using System;
public class Bot : Player {
    private static Random rnd = new Random();
    public Bot(string s) : base(s) {}
    public override Move makeMove() {
        int r = rnd.Next(0, hand.Count);
        if(r == 0) return new WaitingMove();
        else return new PlayingMove(hand[r-1]);
    }
}