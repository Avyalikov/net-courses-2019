namespace Doors_and_levels_game.Interfaces
{
    public enum Phrase { Welcome, YourChoose, MoveForward, MoveBack, YouWin, WrongValue }
    public interface IPhraseProvider
    {
        string GetPhrase(Phrase phrase);
    }
}