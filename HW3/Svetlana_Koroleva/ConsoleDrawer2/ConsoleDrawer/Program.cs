using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawer
{
    class Program
    {
        static void Main(string[] args)
        {         
           
           ISettingsProvider settingsProvider = new JSONSettingsProvider();
            DrawSettings drawSettings = settingsProvider.GetDrawSettings();
            ICurveDrawer curveDrawer=new CurveDrawer(drawSettings);
            IBoard board=new Board(curveDrawer);
            IIOComponent iOComponent = new ConsoleIOComponent();
            IPhraseProvider phraseProvider = new JSONPhraseProvider(drawSettings.Language);
            
            Drawer drawer = new Drawer(board,curveDrawer,settingsProvider,iOComponent,phraseProvider);
            drawer.RunDrawer();

        }
    }
}
