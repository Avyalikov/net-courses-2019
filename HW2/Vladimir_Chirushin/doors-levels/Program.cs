using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_levels
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputOutputDevice inputOutputDevice = new ConsoleIODevice();
            IDataStorage dataStorage = new DataStorage();
            IFileParser fileParser = new JsonFileParser();
            IGameSettings gameSetting = new GameSettings(fileParser, "settings.json");
            gameSetting.InitiateSettings();
            IPhraseProvider phraseProvider = new PhraseProvider(fileParser, gameSetting.GetLanguagePath());
            IDoorsGenerator doorsGenerator = new DoorsGenerator(gameSetting.GetMinDoorValue(), gameSetting.GetMaxDoorValue());

            DoorsGame doorsGame = new DoorsGame(inputOutputDevice, doorsGenerator, dataStorage, phraseProvider, gameSetting);

            doorsGame.Run();
        }
    }
}
