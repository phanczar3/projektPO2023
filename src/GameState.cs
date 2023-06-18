using System;
using System.Collections.Generic;
public class GameState {
    public class PlayerState {
        public int roundsToSkip;
        public PlayerState() => roundsToSkip = 0;
    }
    private Dictionary<Player, PlayerState> playersStates = new Dictionary<Player, PlayerState>();
    public List<Player> players {get;}
    public Deck deck {get;}
    public Card topCard {get; set;}
    public int cardsToDraw {get; set;}
    public int roundsToSkip {get; set;}
    public GameState(List<Player> players, Deck deck, Card topCard) {
        this.players = players;
        this.deck = deck;
        this.topCard = topCard;
        foreach(Player p in players) {
            playersStates.Add(p, new PlayerState());
        }
        cardsToDraw = 0;
        roundsToSkip = 0;
    }
    public Player currentPlayer() {
        return players[0];
    }
    public void nextTurn(Move m) {
        Player p = players[0];
        players.RemoveAt(0);
        players.Add(p);
    }
    public bool isPlayerStopped() {
        return playersStates[currentPlayer()].roundsToSkip > 0;
    }
    public void playerSkips() {
        playersStates[currentPlayer()].roundsToSkip--;
    }
    public void playerSetStops(int x) {
        playersStates[currentPlayer()].roundsToSkip = x;
    }
}

