using System;
namespace MacauCardGame {
    class Program {
        static void Main(string[] args) {
            Card c = new Card(Card.Suit.Spades, Card.Face.Two);

            Console.WriteLine(c.displayName());
            
        }
    }
}