using System;
using System.Collections.Generic;
public class GameRules {
    public GameRules() {}
    public bool isLegal(GameState gs, Move m) {
        if(m is SkippingMove && gs.isStopped(gs.currentPlayer)) return true;
        else if(gs.isStopped(gs.currentPlayer) || m is SkippingMove) return false;
        if(gs.startedPlaying) {
            if(m is FinishingMove) return true;
            else if(m is PlayingMove) {
                PlayingMove pm = (PlayingMove)m;
                Card c = pm.cardPlayed;
                Card top = gs.topCard;
                if(c.face == top.face) return true;
            } 
            return false;
        } else {
            if(m is WaitingMove) return true;
            if(m is FinishingMove) return false;
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
    }
    public void changeState(ref GameState gs, Move m) {
        Player current = gs.currentPlayer;
        if(m is WaitingMove) {
            if(gs.cardsToDraw > 0) {
                for(int i = 0; i < gs.cardsToDraw; i++) {
                    current.drawCard(gs.deck.top());
                }
                gs.cardsToDraw = 0;
            } else if(gs.roundsToSkip > 0) {
                gs.setStops(gs.currentPlayer, gs.roundsToSkip);
                gs.roundsToSkip = 0;
            } else {
                current.drawCard(gs.deck.top());
            }
        } else if(m is PlayingMove) {
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
            gs.startedPlaying = true;
            gs.deck.usedCard(gs.topCard);
            gs.topCard = c;
            current.playCard(c);
        } else if(m is FinishingMove) {
        } else if(m is SkippingMove) {
            gs.playerSkips(gs.currentPlayer);
        }

    }
    public List<Move> allOptions(GameState gs) {
        List<Move> options = new List<Move>();
        options.Add(new WaitingMove());
        options.Add(new SkippingMove());
        options.Add(new FinishingMove());
        foreach(Card c in gs.currentPlayer.hand)
            options.Add(new PlayingMove(c));
        options.RemoveAll(m => !isLegal(gs, m));
        return options;
    }
    public Player winnerOfTheGame(GameState gs) {
        foreach(Player p in gs.players) {
            if(p.handSize == 0) return p;
        }
        return null;
    }
}