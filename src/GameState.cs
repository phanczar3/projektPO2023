using System;
using System.Collections.Generic;
public class GameState {
    public class PlayerState {
        public int roundsToSkip;
        public PlayerState() => roundsToSkip = 0;
    }
    public List<PlayerState> playersStates;
    public List<Player> turnOrder;
    public List<Card> deck;
    public Card topCard;
    public int cardsToDraw;
}

