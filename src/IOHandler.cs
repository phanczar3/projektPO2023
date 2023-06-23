using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
public class IOHandler {
    public IOHandler() {}
    public List<string> getUsers() {
        WriteLine("How many players will be playing?");
        int input = parseInt(1,4);
        List<string> users = new List<string>();
        for(int i = 1; i <= input; i++) {
            Write("Player no. " + i + " name: ");
            string s = parseString(users);
            users.Add(s);
        }
        return users;
    }
    public List<string> getBots() {
        WriteLine("With how many bots do you want to play?");
        int input = parseInt(0,4);
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
    private string playersTurn(Player p) {
        return p.name + "'s turn:";
    }
    private List<String> showGameState(GameState gs) {
        List<String> res = new List<String>();
        res.Add(playersTurn(gs.currentPlayer));
        List<String> previousMovesStrings = new List<String>();
        for(int i = gs.players.Count-1; i > 0; i--) {
            int cnt = gs.players.Count-i;
            Player curPlayer = gs.players[i];
            if(curPlayer.lastMoves != null) {
                previousMovesStrings.Add($"[{cnt}] {curPlayer.name} {movesToString(curPlayer.lastMoves)}");
            }
        }
        if(previousMovesStrings.Count > 0) {
            res.Add("---- Previous Moves ----");
            foreach(string s in previousMovesStrings)
                res.Add(s);
        }
        res.Add("------ Hand sizes ------");
        foreach(Player p in gs.players)
           if(gs.currentPlayer != p)
               res.Add($" {p.name}'s hand size is: {p.handSize}");
        res.Add("-- Cards on the table --");
        res.Add(" Cards left in deck: " + gs.deck.Count);
        res.Add(" Top card is " + gs.topCard);
        res.Add("------ Your  hand ------");
        string hand = "";
        for(int i = 0; i < gs.currentPlayer.hand.Count; i++) {
            if(i % 8 == 0 && i > 0) hand += "\n";
            Card c = gs.currentPlayer.hand[i];
            hand += (c + " ");
        }
        res.Add(hand);
        res.Add("----- Your options -----");
        return res;
    }
    private string movesToString(List<Move> moves) {
        if(moves[0] is WaitingMove) return "is waiting";
        else if(moves[0] is SkippingMove) {
            return "had to skip";
        } else {
            string res = "played ";
            for(int i = 0; i < moves.Count-1; i++) {
                if(moves[i] is PlayingMove) {        
                    PlayingMove pm = (PlayingMove)moves[i];
                    res += pm.cardPlayed + (pm.cardPlayed.face != Card.Face.Ace && pm.cardPlayed.face != Card.Face.Jack?", ":"");
                } else if(moves[i] is ChoosingSuitMove) {
                    ChoosingSuitMove csm = (ChoosingSuitMove)moves[i];
                    res += $"(chose {Card.SuitToString(csm.suit)}), ";
                } else if(moves[i] is ChoosingFaceMove) {
                    ChoosingFaceMove cfm = (ChoosingFaceMove)moves[i];
                    res += $"(chose {Card.FaceToString(cfm.face)}), ";
                }
            }
            return res.Substring(0, res.Length-2);
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
    public void announceEndOfGame(string msg) {
        WriteLine($"{msg}, game has ended");
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
    private string parseString(List<String> forbidden) {
        string s;
        while(true) {
            s = Console.ReadLine();
            if(s.All(c => Char.IsLetter(c)) && !forbidden.Contains(s))
                break;
            if(!s.All(c => Char.IsLetter(c)))
                Console.WriteLine("The input has to be a string consisting of latin letters");
            if(forbidden.Contains(s))
                Console.WriteLine("The name already belongs to another player");
        }
        return s;
    }
}