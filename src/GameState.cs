using System;
using System.Collections.Generic;
public class GameState {
    public List<Player> players {get;}
    public Player currentPlayer {
        get { return players[0]; }
    }
    public bool startedPlaying;
    public List<Tuple<Player,List<Move>>> previousMoves {get;}
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
}

