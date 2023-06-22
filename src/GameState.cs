using System;
using System.Collections.Generic;
public class GameState {
    public class PlayerState {
        public int roundsToSkip;
        public PlayerState() => roundsToSkip = 0;
    }
    private Dictionary<Player, PlayerState> playersStates = new Dictionary<Player, PlayerState>();
    public List<Player> players {get;}
    public Player currentPlayer {
        get { return players[0]; }
    }
    public List<Tuple<Player,List<Move>>> previousMoves {get;}
    public Deck deck {get;}
    public Card topCard;
    public Card.Suit topCardSuit;
    public Card.Face topCardFace;
    public Player playerPlayingJack;
    public int cardsToDraw;
    public int roundsToSkip;
    public bool startedPlaying;
    public bool jackPlayed;
    public bool jackPlayedThisTurn;
    public bool jackActive;
    public bool acePlayed;
    public GameState(List<Player> players, Deck deck, Card topCard) {
        this.players = players;
        this.deck = deck;
        this.topCard = topCard;
        topCardSuit = topCard.suit;
        topCardFace = topCard.face;
        foreach(Player p in players) {
            playersStates.Add(p, new PlayerState());
        }
        cardsToDraw = 0;
        roundsToSkip = 0;
        startedPlaying = false;
        jackPlayed = false;
        jackActive = false;
        jackPlayedThisTurn = false;
        acePlayed = false;
        playerPlayingJack = null;
        previousMoves = new List<Tuple<Player,List<Move>>>();
    }
    public void nextTurn(List<Move> moves) {
        previousMoves.Add(new Tuple<Player, List<Move>>(currentPlayer, moves));
        if(previousMoves.Count == players.Count) 
            previousMoves.RemoveAt(0);
        Player p = players[0];
        players.RemoveAt(0);
        players.Add(p);
        startedPlaying = false;
    }
    public bool isStopped(Player p) {
        return getStops(p) > 0;
    }
    public void playerSkips(Player p) {
        playersStates[p].roundsToSkip--;
    }
    public int getStops(Player p) {
        return playersStates[p].roundsToSkip;
    }
    public void setStops(Player p, int x) {
        playersStates[p].roundsToSkip = x;
    }
}

