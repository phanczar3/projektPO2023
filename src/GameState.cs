using System;
using System.Collections.Generic;
public class GameState {
    public class PlayerState {
        public int roundsToSkip;
        public PlayerState() => roundsToSkip = 0;
    }
    public Dictionary<Player, PlayerState> playersStates = new Dictionary<Player, PlayerState>();
    public List<Player> players;
    public Move lastMove;
    public Player lastPlayer;
    public Deck deck;
    public Card topCard;
    public int cardsToDraw;
    public GameState(List<Player> players, Deck deck, Card topCard) {
        this.players = players;
        this.deck = deck;
        this.topCard = topCard;
        foreach(Player p in players) {
            playersStates[p] = new PlayerState();
        }
        cardsToDraw = 0;
        lastMove = null;
    }
    public Player currentPlayer() {
        return players[0];
    }
    public void nextTurn(Move m) {
        lastMove = m;
        Player p = players[0];
        lastPlayer = p;
        players.RemoveAt(0);
        players.Add(p);
    }
}

