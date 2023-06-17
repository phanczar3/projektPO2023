using System;
using System.Collections.Generic;
public class Bot : Player {
    private static Random rnd = new Random();
    public Bot(string s) : base(s) {}
    public override Move makeMove(GameState gs, GameRules gr) {
        List<Move> options = gr.allOptions(gs);
        int r = rnd.Next(0, options.Count);
        return options[r];
    }
}