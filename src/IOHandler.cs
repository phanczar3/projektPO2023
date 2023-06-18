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
    private List<String> showGameState(GameState gs) {
        List<String> res = new List<String>();
        playersTurn(gs.currentPlayer());
        foreach(Player p in gs.players) {
           res.Add(p.name + "'s hand size is: " + p.handSize);
        }
        res.Add("Cards left in deck: " + gs.deck.Count);
        res.Add("Top card is " + gs.topCard);
        res.Add("Your hand:");
        string hand = "";
        foreach(Card c in gs.currentPlayer().hand) {
            hand += (c + " ");
        }
        res.Add(hand);
        return res;
    }
    public Move askForMove(string s, GameState gs, GameRules gr) {
        playersTurn(gs.currentPlayer());
        WriteLine("Press any key to start");
        ReadKey(true);
        clearConsole();
        
        List<Move> options = gr.allOptions(gs);
        List<String> optionsString = new List<String>();
        foreach(Move m in options)
            optionsString.Add(m.display());
        KeyboardInterface ki = new KeyboardInterface(showGameState(gs).ToArray(), optionsString.ToArray());
        int input = ki.run();    
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