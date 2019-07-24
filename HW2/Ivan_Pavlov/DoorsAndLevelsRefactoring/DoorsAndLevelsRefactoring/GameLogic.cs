namespace DoorsAndLevelsRefactoring
{
    using DoorsAndLevelsRefactoring.Interface;
    using DoorsAndLevelsRefactoring.Provider;

    class GameLogic
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputAndOutput inputAndOutput;


        private readonly StackStorageProvider doorsStorage;
        private readonly GameSettings gameSetting;
        
        public int[] Doors { get; set; }
                
        public GameLogic(
            IPhraseProvider phraseProvider,
            IInputAndOutput inputAndOutput,
            IGetDoorsNumber getDoors,
            IChooseDoorsStorage doorsStorage,
            ISettingProvider settingProvider)
        {
            this.phraseProvider = phraseProvider;
            this.inputAndOutput = inputAndOutput;
            

            this.gameSetting = settingProvider.GetGameSettings();

            this.Doors = getDoors.GetDoorsNumber(gameSetting.DoorsAmount);
        }

        


    }
}
