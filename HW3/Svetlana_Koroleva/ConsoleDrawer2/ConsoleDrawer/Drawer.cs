// <copyright file="Drawer.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ConsoleDrawer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Drawer description
    /// </summary>
    public class Drawer
    {
        private readonly IBoard board;
        private readonly ICurveDrawer curveDrawer;
        private readonly ISettingsProvider settingsProvider;
        private readonly IPhraseProvider phraseProvider;
        private readonly DrawSettings drawSettings;
        private readonly IIOComponent iO;
        delegate void Draw(IBoard board);
        private delegate void DrawMethod(IBoard board);
        private Dictionary<int, DrawMethod> drawers;

        public Drawer(IBoard board, ICurveDrawer cdrawer, ISettingsProvider settingsProvider, IIOComponent iOComponent, IPhraseProvider phraseProvider)
        {
            this.board = board;
            this.curveDrawer = cdrawer;
            this.settingsProvider = settingsProvider;
            this.drawSettings = settingsProvider.GetDrawSettings();
            this.iO = iOComponent;
            this.phraseProvider = phraseProvider;

            this.drawers = new Dictionary<int, DrawMethod>();
            drawers.Add(1, this.curveDrawer.DrawDot);
            drawers.Add(2, this.curveDrawer.DrawHorizontalLine);
            drawers.Add(3, this.curveDrawer.DrawVerticalLine);
            drawers.Add(4, this.curveDrawer.DrawAnotherCurve);
        }


        private int GetUserNumber()
        {
            bool isNumber = false;
            int enteredNum;
            do
            {
               iO.SetCursor(0, drawSettings.InputCoordinateY);
                if (int.TryParse(iO.ReadInput(), out enteredNum))
                {
                    
                    iO.WriteOutput(phraseProvider.GetPhrase("Selected") + $"{enteredNum}");
                    if (drawers.ContainsKey(enteredNum))
                    {
                        isNumber = true;
                    }
                }

                else
                {
                    iO.SetCursor(0, drawSettings.WrongNumCoordinateY);
                    iO.WriteOutput(phraseProvider.GetPhrase("WrongValue"));
                }
            }

            while (!isNumber);
            return enteredNum;
        }

        private void DrawInputes()
        {
        }



        public void RunDrawer()
        {

            Board dboard = new Board(this.curveDrawer, drawSettings.BoardSizeX, drawSettings.BoardSizeY, drawSettings.StartCoordinateX, drawSettings.StartCoordinateY);
           
            phraseProvider.ReadResourceFile();
            
            string exit = String.Empty;

            do
            { 
                iO.Clear();
                iO.SetCursor(drawSettings.StartCoordinateX,drawSettings.StartCoordinateY);
                Point2D current= dboard.GetCurrenttPosition();
                current.XCoordinate = drawSettings.StartCoordinateX;
                current.YCoordinate = drawSettings.StartCoordinateY;
                dboard.DrawBoard(drawSettings.HorizontalFiller,drawSettings.VerticalFiller,drawSettings.CornerFiller);
                iO.SetCursor(0, drawSettings.InfoCoordinateY);
                iO.WriteOutput(phraseProvider.GetPhrase("rules"));
           
                iO.SetCursor(settingsProvider.GetDrawSettings().StartCoordinateX + 1, settingsProvider.GetDrawSettings().StartCoordinateY + 1);
                DrawMethod draw = null;
                int count = 0;
                while (count != drawers.Count())
                {
                    int number = this.GetUserNumber();
                    draw = this.drawers[number];

                    draw(dboard);
                    count++;
                }


                iO.SetCursor(0, drawSettings.ExitCoordinateY);
                iO.WriteOutput(phraseProvider.GetPhrase("Exit"));
                exit=iO.ReadInput();

            }

            while (exit!=settingsProvider.GetDrawSettings().Exit);

        }

    }
}
