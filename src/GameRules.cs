using System;
using System.Collections.Generic;
public class GameRules {
    public GameRules() {}
    public bool isLegal(GameState gs, Move m) {
        if(m is WaitingMove) return true;
        if(gs.isPlayerStopped()) return false;
        PlayingMove pm = (PlayingMove)m;
        Card c = pm.cardPlayed;
        if(gs.cardsToDraw > 0) {
            if(c.face == Card.Face.Two || c.face == Card.Face.Three 
            || c.face == Card.Face.King || c.face == Card.Face.Queen) return true;
            else return false;
        } else if(gs.roundsToSkip > 0) {
            if(c.face == Card.Face.Four || c.face == Card.Face.Queen) return true;
            else return false;
        } else {
            Card top = gs.topCard;
            if(c.suit == top.suit || c.face == top.face 
            || c.face == Card.Face.Queen || top.face == Card.Face.Queen) return true;
            else return false;
        }
    }
    public void changeState(ref GameState gs, Move m) {
        Player current = gs.players[0];
        if(m is WaitingMove) {
            if(gs.cardsToDraw > 0) {
                for(int i = 0; i < gs.cardsToDraw; i++) {
                    current.drawCard(gs.deck.top());
                }
                gs.cardsToDraw = 0;
            } else if(gs.roundsToSkip > 0) {
                gs.playerSetStops(gs.roundsToSkip);
                gs.roundsToSkip = 0;
            } else {
                current.drawCard(gs.deck.top());
            }
        } else {
            PlayingMove pm = (PlayingMove)m;
            Card c = pm.cardPlayed;
            if(c.face == Card.Face.Two) gs.cardsToDraw += 2;
            else if(c.face == Card.Face.Three) gs.cardsToDraw += 3;
            else if(c.face == Card.Face.King) gs.cardsToDraw += 5;
            else if(c.face == Card.Face.Four) gs.roundsToSkip += 1;
            else if(c.face == Card.Face.Queen) {
                gs.cardsToDraw = 0;
                gs.roundsToSkip = 0;
            }
            gs.deck.usedCard(gs.topCard);
            gs.topCard = c;
            current.playCard(c);
        }
    }
    public List<Move> allOptions(GameState gs) {
        List<Move> options = new List<Move>();
        if(gs.isPlayerStopped()) options.Add(new WaitingMove("Skip"));
        else options.Add(new WaitingMove("Wait"));
        foreach(Card c in gs.currentPlayer().hand) {
            Move m = new PlayingMove(c);
            if(isLegal(gs, m))
                options.Add(m);
        }
        return options;
    }
    public Player winnerOfTheGame(GameState gs) {
        foreach(Player p in gs.players) {
            if(p.handSize == 0) return p;
        }
        return null;
    }
}