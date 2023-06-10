using System;
using System.Collections.Generic;
public class GameRules {
    public GameRules() {

    }
    public bool isLegal(GameState gs, Move m) {
        if(m is WaitingMove) return true;
        if(gs.cardsToDraw > 0) {
            PlayingMove m2 = (PlayingMove)m;
            Card c = m2.cardPlayed;
            if(c.face != Card.Face.Two && c.face != Card.Face.Three && c.face != Card.Face.King) return false;
            else return true;
        } else {
            Card top = gs.topCard;
            PlayingMove m2 = (PlayingMove)m;
            Card c = m2.cardPlayed;
            if(c.suit == top.suit || c.face == top.face) return true;
            else return false;
        }
    }
    public void changeState(ref GameState gs, Move m) {
        Player current = gs.players[0];
        if(m is WaitingMove) {
            for(int i = 0; i < Math.Max(1, gs.cardsToDraw); i++) {
                gs.deck.giveCard(current);
            }
            gs.cardsToDraw = 0;
        } else {
            PlayingMove m2 = (PlayingMove)m;
            Card c = m2.cardPlayed;
            if(c.face == Card.Face.Two) gs.cardsToDraw += 2;
            else if(c.face == Card.Face.Three) gs.cardsToDraw += 3;
            else if(c.face == Card.Face.King) gs.cardsToDraw += 5;
            gs.deck.usedCard(gs.topCard);
            gs.topCard = c;
            current.playCard(c);
        }
    }
    public Player winnerOfTheGame(GameState gs) {
        foreach(Player p in gs.players) {
            if(p.handSize == 0) return p;
        }
        return null;
    }
}