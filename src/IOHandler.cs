using System;
using System.Collections.Generic;
using static System.Console;
public class IOHandler {
    public IOHandler() {}
    public List<string> getUsers() {
        WriteLine("How many players will be playing?");
        int input = parseInt(1,5);
        List<string> users = new List<string>();
        for(int i = 1; i <= input; i++) {
            Write("Player no. " + i + " name: ");
            users.Add(Console.ReadLine());
        }
        return users;
    }
    public List<string> getBots() {
        WriteLine("With how many bots do you want to play?");
        int input = parseInt(0,5);
        List<string> bots = new List<string>();
        for(int i = 1; i <= input; i++) {
            bots.Add("Bot" + Convert.ToString(i));
        }
        return bots;
    }
    public void printTurnOrder(List<Player> players) {
        WriteLine("Turn order of the game:");
        string s = "";
        foreach(Player p in players) {
            s += p.name + ", ";
        }
        WriteLine(s.Substring(0, s.Length-2));
    }
    public void playersTurn(Player p) {
        WriteLine(p.name + "'s turn:");
    }
    public void printMove(Player p, Move m) {
        Write(p.name);
        if(m is WaitingMove) Console.WriteLine(" is waiting");
        else {
            PlayingMove m2 = (PlayingMove)m;
            Card c = m2.cardPlayed;
            WriteLine(" has played " + c);
        }
    }
    public void printGameState(GameState gs) {
        foreach(Player p in gs.players) {
            WriteLine(p.name + "'s hand size is: " + p.handSize);
        }
        WriteLine("Cards left in deck: " + gs.deck.Count);
        WriteLine("Top card is " + gs.topCard);
    }
    public Move askForMove(string s, GameState gs, GameRules gr) {
        playersTurn(gs.currentPlayer());
        Write("Press when ready to start: ");
        ReadLine();
        clearConsole();
        playersTurn(gs.currentPlayer());
        printGameState(gs);
        WriteLine("Your hand:");
        foreach(Card c in gs.currentPlayer().hand) {
            Write(c + " ");
        }
        WriteLine();
        List<Move> options = new List<Move>();
        foreach(Move m in gr.allOptions(gs)) {
            options.Add(m);
            WriteLine(m.display());
        }
        int input = Convert.ToInt32(ReadLine());
        return options[input];
    }
    public void clearConsole() {
        Clear();
    }
    public void announceWinner(Player p) {
        WriteLine(p.name + " has won!");
    }
    private int parseInt(int a, int b) {
        int? input = null;
        while(!input.HasValue) {
            input = int.TryParse(ReadLine(), out int i) ? i : new int?();
            if(!input.HasValue || input.Value < a || input.Value > b) {
                WriteLine($"The input has to be a number between {a} and {b}");
                input = new int?();
            }
        }
        return input.Value;
    }
}