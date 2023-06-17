using System;
using System.Collections.Generic;
public class Game {
    //private static Random rnd = new Random();
    private IOHandler ioh;
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
        ioh.printTurnOrder(players);
        Deck deck = new Deck();
        deck.shuffle();
        foreach(Player p in players) {
            for(int i = 0; i < 5; i++) {
                deck.giveCard(p);
            }
        }
        Card topCard = deck.top();
        GameState gs = new GameState(players, deck, topCard);
        GameRules gr = new GameRules();
        while(gr.winnerOfTheGame(gs) == null) {
            Player cp = gs.currentPlayer();
            ioh.printGameState(gs);
            ioh.playersTurn(cp);
            Move m;
            while(true) {
                m = cp.makeMove();
                if(gr.isLegal(gs, m)) break;
            }
            gr.changeState(ref gs, m);
            ioh.printMove(cp, m);
            gs.nextTurn();
            if(cp is User)
                ioh.clearConsole();
        }
        ioh.announceWinner(gr.winnerOfTheGame(gs));
    }
    public void doNothing() {}
}