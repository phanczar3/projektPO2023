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
    public void printTurnOrder(List<Player> players) {
        Console.WriteLine("Turn order of the game:");
        foreach(Player p in players) {
            Console.Write(p.name + ", ");
        }
        Console.WriteLine();
    }
    public void playersTurn(Player p) {
        Console.WriteLine(p.name + "'s turn:");
    }
    public void printMove(Player p, Move m) {
        Console.Write(p.name);
        if(m is WaitingMove) Console.WriteLine(" is waiting");
        else {
            PlayingMove m2 = (PlayingMove)m;
            Card c = m2.cardPlayed;
            Console.WriteLine(" has played " + c.displayName());
        }
    }
    public void printGameState(GameState gs) {
        foreach(Player p in gs.players) {
            Console.WriteLine(p.name + "'s hand size is: " + p.handSize);
        }
        Console.WriteLine("Cards left in deck: " + gs.deck.Count);
        Console.WriteLine("Top card is " + gs.topCard.displayName());
    }
    public Move askForMove(string s, List<Card> hand) {
        Console.WriteLine("Your hand:");
        foreach(Card c in hand) {
            Console.WriteLine(c.displayName());
        }
        int input = Convert.ToInt32(Console.ReadLine());
        return parseInput(input, hand);
    }
    public void clearConsole() {
        Console.Clear();
    }
    public void announceWinner(Player p) {
        Console.WriteLine(p.name + " has won!");
    }
    private Move parseInput(int i, List<Card> hand) {
        if(i == 0) return new WaitingMove();
        return new PlayingMove(hand[i-1]);
    }
}