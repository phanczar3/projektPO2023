using System;
using System.Collections.Generic;
public class GameState {
    public List<Player> players {get;}
    public Player currentPlayer {
        get { return players[0]; }
    }
    public bool startedPlaying;
    public Deck deck {get;}
    public Card topCard;
    public Card.Suit topCardSuit;
    public Card.Face topCardFace;
    public int cardsToDraw;
    public int roundsToSkip;
    public Player playerPlayingJack;
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
        cardsToDraw = 0;
        roundsToSkip = 0;
        startedPlaying = false;
        jackPlayed = false;
        jackActive = false;
        jackPlayedThisTurn = false;
        acePlayed = false;
        playerPlayingJack = null;
    }
    public void nextTurn(List<Move> moves) {
        Player p = players[0];
        p.lastMoves = moves;
        players.RemoveAt(0);
        players.Add(p);
        startedPlaying = false;
    }
}

