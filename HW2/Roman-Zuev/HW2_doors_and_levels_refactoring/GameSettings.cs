namespace HW2_doors_and_levels_refactoring
{
    public class GameSettings
    {
        public int DoorsAmount { get; }
        public int PreviousLevelNumber { get; }
        public string ExitCode { get; }
        public GameSettings (int doorsAmount, int previousLevelNumber, string exitCode)
        {
            this.DoorsAmount = doorsAmount;
            this.PreviousLevelNumber = previousLevelNumber;
            this.ExitCode = exitCode;
        }
    }
}
