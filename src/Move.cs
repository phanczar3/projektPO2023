
public abstract class Move {
    public abstract string display();
}
public class PlayingMove : Move {
    public Card cardPlayed {get;}
    public PlayingMove(Card c) => cardPlayed = c;
    public override string display() {
        return "Play " + cardPlayed;
    }
}
public class WaitingMove : Move {
    public WaitingMove() {}
    public override string display() {
        return "Wait";
    }
}
public class SkippingMove : Move {
    public SkippingMove() {}
    public override string display() {
        return "Skip";
    }
}
public class ChoosingSuitMove : Move {
    public Card.Suit suit {get;}
    public ChoosingSuitMove(Card.Suit s) => suit = s;
    public override string display() {
        return "Choose " + Card.SuitToString(suit);
    }
}
public class ChoosingFaceMove : Move {
    public Card.Face face {get;}
    public ChoosingFaceMove(Card.Face f) => face = f;
    public override string display() {
        return "Choose " + Card.FaceToString(face);
    }
}
public class FinishingMove : Move {
    public FinishingMove() {}
    public override string display() {
        return "Finish";
    }
}