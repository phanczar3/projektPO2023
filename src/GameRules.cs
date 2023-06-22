using System;
using System.Collections.Generic;
public class GameRules {
    public GameRules() {}
    public bool isLegal(GameState gs, Move m) {
        if(m is ChoosingFaceMove && gs.jackPlayed) return true;
        else if(m is ChoosingFaceMove || gs.jackPlayed) return false;
        if(m is ChoosingSuitMove && gs.acePlayed) return true;
        else if(m is ChoosingSuitMove || gs.acePlayed) return false;
        if(m is SkippingMove && gs.isStopped(gs.currentPlayer)) return true;
        else if(gs.isStopped(gs.currentPlayer) || m is SkippingMove) return false;
        if(gs.jackActive) {
            if(gs.jackPlayedThisTurn) {
                if(m is PlayingMove) {
                    PlayingMove pm = (PlayingMove)m;
                    if(pm.cardPlayed.face == Card.Face.Jack) return true;
                    else return false;
                }
                if(m is FinishingMove) return true;
                return false;
            } else {
                if(m is PlayingMove) {
                    PlayingMove pm = (PlayingMove)m;
                    if(gs.topCard.face == Card.Face.Jack) {
                        if(pm.cardPlayed.face == gs.topCardFace ||
                        pm.cardPlayed.face == Card.Face.Jack) return true;
                    } else {
                        if(pm.cardPlayed.face == gs.topCardFace) return true;
                    }
                    return false;
                }
                if(gs.startedPlaying) {
                    if(m is FinishingMove) return true;
                    return false;
                } else {
                    if(m is WaitingMove) return true;
                    return false;
                } 
            }
        }
        if(gs.startedPlaying) {
            if(m is FinishingMove) return true;
            else if(m is PlayingMove) {
                PlayingMove pm = (PlayingMove)m;
                Card c = pm.cardPlayed;
                Card.Face topFace = gs.topCardFace;
                if(c.face == topFace) return true;
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
                Card.Suit suit = gs.topCardSuit;
                Card.Face face = gs.topCardFace;
                if(c.suit == suit || c.face == face 
                || c.face == Card.Face.Queen || face == Card.Face.Queen) return true;
                else return false;
                
            }
        }
    }
    public void changeState(ref GameState gs, Move m) {
        if(m is ChoosingFaceMove) {
            ChoosingFaceMove cfm = (ChoosingFaceMove)m;
            gs.topCardFace = cfm.face;
            gs.jackPlayed = false;
            gs.jackActive = true;
            gs.jackPlayedThisTurn = true;
        }
        if(m is ChoosingSuitMove) {
            ChoosingSuitMove csm = (ChoosingSuitMove)m;
            gs.topCardSuit = csm.suit;
            gs.acePlayed = false;
        }
        if(gs.jackActive && gs.currentPlayer == gs.playerPlayingJack && !gs.jackPlayedThisTurn) {
            gs.jackActive = false;
            gs.playerPlayingJack = null;
        }
        if(m is WaitingMove) {
            if(gs.cardsToDraw > 0) {
                for(int i = 0; i < gs.cardsToDraw; i++) {
                    gs.currentPlayer.drawCard(gs.deck.top());
                }
                gs.cardsToDraw = 0;
            } else if(gs.roundsToSkip > 0) {
                gs.setStops(gs.currentPlayer, gs.roundsToSkip);
                gs.roundsToSkip = 0;
            } else {
                gs.currentPlayer.drawCard(gs.deck.top());
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
            if(c.face == Card.Face.Ace) gs.acePlayed = true;
            if(c.face == Card.Face.Jack) gs.jackPlayed = true;
            gs.startedPlaying = true;
            gs.deck.usedCard(gs.topCard);
            gs.topCard = c;
            gs.topCardSuit = c.suit;
            gs.topCardFace = c.face;
            gs.currentPlayer.playCard(c);
        } else if(m is FinishingMove) {
            if(gs.jackPlayedThisTurn)
                gs.playerPlayingJack = gs.currentPlayer;
            gs.jackPlayedThisTurn = false;
        } else if(m is SkippingMove) {
            gs.playerSkips(gs.currentPlayer);
        }

    }
    public List<Move> allOptions(GameState gs) {
        List<Move> options = new List<Move>();
        options.Add(new WaitingMove());
        options.Add(new SkippingMove());
        options.Add(new FinishingMove());
        foreach(Card.Suit s in Enum.GetValues(typeof(Card.Suit))) {
            options.Add(new ChoosingSuitMove(s));
        }
        foreach(Card.Face f in Enum.GetValues(typeof(Card.Face))) {
            if(!Card.magicCards.Contains(f))
                options.Add(new ChoosingFaceMove(f));
        }
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