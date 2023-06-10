public class User : Player {
    static public IOHandler ioh;
    public User(string s) : base(s) {}
    public override Move makeMove() {
        return ioh.askForMove(name, hand);
    }
}