using System;
using System.Collections.Generic;
public class Game {
    private IOHandler ioh;
    private Card topCard;
    private GameState gs;
    private GameRules gr;
    public Game() {
        ioh = new IOHandler();
        List<string> usersNames = ioh.getUsers();
        User.ioh = ioh;
        List<Player> players = new List<Player>();
        foreach(string s in usersNames) {
            players.Add(new User(s));
        }
        List<string> botsNames = ioh.getBots();
        foreach(string s in botsNames) {
            players.Add(new Bot(s));
        }
        ioh.clearConsole();
        ioh.printTurnOrder(players);
        Deck deck = new Deck();
        deck.shuffle();
        foreach(Player p in players) {
            for(int i = 0; i < 5; i++) {
                p.drawCard(deck.top());
            }
        }
        topCard = deck.top();
        gs = new GameState(players, deck, topCard);
        gr = new GameRules();
    }
    public void gameLoop() {
        try{
            while(gr.winnerOfTheGame(gs) == null) {
                Player cp = gs.currentPlayer;
                Move m;
                ioh.askIfReady(gs);
                List<Move> moves = new List<Move>();
                do {
                    m = cp.makeMove(gs, gr);
                    gr.changeState(ref gs, m);
                    moves.Add(m);
                } while(!(m is WaitingMove || m is FinishingMove || m is SkippingMove));
                gs.nextTurn(moves);
                if(cp is User)
                    ioh.clearConsole();
            }
            ioh.announceWinner(gr.winnerOfTheGame(gs));
        } catch(NullReferenceException e) {
            ioh.announceEndOfGame(e.Message);
        }
    }
}