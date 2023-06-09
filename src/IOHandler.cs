using System;
using System.Collections.Generic;
public class IOHandler {
    public IOHandler() {}
    public List<string> getUsers() {
        Console.WriteLine("How many players will be playing?");
        int input = Convert.ToInt32(Console.ReadLine());
        List<string> users = new List<string>();
        for(int i = 1; i <= input; i++) {
            Console.Write("Player no. " + i + " name: ");
            users.Add(Console.ReadLine());
        }
        return users;
    }
    public List<string> getBots() {
        Console.WriteLine("With how many bots do you want to play?");
        int input = Convert.ToInt32(Console.ReadLine());
        List<string> bots = new List<string>();
        for(int i = 1; i <= input; i++) {
            bots.Add("Bot" + Convert.ToString(i));
        }
        return bots;
    }
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