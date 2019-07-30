namespace DrawGame.Interfaces
{
    internal interface IBoard
    {
        int BoardSizeX { get; set; }

        /// <summary>
        /// Gets or sets the BoardSizeY
        /// </summary>
        int BoardSizeY { get; set; }
    }
}
