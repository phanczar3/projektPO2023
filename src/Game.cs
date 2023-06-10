using System;
using System.Collections.Generic;
public class Game {
    private List<Player> players;
    private IOHandler ioh;
    public Game() {
        ioh = new IOHandler();
        List<string> usersNames = ioh.getUsers();
        User.ioh = ioh;
        foreach(string s in usersNames) {
            players.Add(new User(s));
        }
        List<string> botsNames = ioh.getBots();
        foreach(string s in botsNames) {
            players.Add(new Bot(s));
        }
        ioh.printTurnOrder(players);
        
    }
}