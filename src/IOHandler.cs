using System;
using System.Collections.Generic;
public class IOHandler {
    public IOHandler() {}
    public Move askForMove(string s, List<Card> hand) {
        Console.WriteLine(s + "'s turn:");
        int input = Convert.ToInt32(Console.ReadLine());
        return parseInput(input, hand);
    }
    private Move parseInput(int i, List<Card> hand) {
        if(i == 0) return new WaitingMove();
        return new PlayingMove(hand[i]);
    }
}