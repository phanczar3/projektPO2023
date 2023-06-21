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
    public string playersTurn(Player p) {
        return p.name + "'s turn:";
    }
    private List<String> showGameState(GameState gs) {
        List<String> res = new List<String>();
        res.Add(playersTurn(gs.currentPlayer));
        for(int i = 0; i < gs.previousMoves.Count; i++) {
            res.Add(gs.previousMoves[i].Item1.name + " " + movesToString(gs.previousMoves[i].Item2, gs));
        }
        foreach(Player p in gs.players)
           if(gs.currentPlayer != p)
               res.Add(p.name + "'s hand size is: " + p.handSize);
        res.Add("Cards left in deck: " + gs.deck.Count);
        res.Add("Top card is " + gs.topCard);
        res.Add("Your hand:");
        string hand = "";
        foreach(Card c in gs.currentPlayer.hand) {
            hand += (c + " ");
        }
        res.Add(hand);
        return res;
    }
    private string movesToString(List<Move> moves, GameState gs) {
        if(moves[0] is WaitingMove) return "is waiting";
        else if(moves[0] is SkippingMove) {
            int rounds = gs.getStops(gs.currentPlayer);
            return $"had to skip, {rounds} rounds left";
        } else {
            string res = "played ";
            for(int i = 0; i < moves.Count-1; i++) {
                PlayingMove pm = (PlayingMove)moves[i];
                res += pm.cardPlayed + " ";
            }
            return res;
        }
        
    }
    public void askIfReady(GameState gs) {
        WriteLine(playersTurn(gs.currentPlayer));
        WriteLine("Press any key to start");
        ReadKey(true);
    }
    public Move askForMove(string s, GameState gs, GameRules gr) {
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