using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
 

    public class Game
    {
        private readonly TextMessages textMessages = new TextMessages();
        private readonly Settings settings;

        private readonly IStorageProvider storageProvider;
        private readonly IReadInputProvider readInputProvider;
        private readonly ISendOutputProvider sendOutputProvider;
        private readonly IRandomProvider randomProvider;
        private readonly ITextMessagesProvider textMessagesProvider;
        private readonly ISettingsProvider settingsProvider;      

        private MyType[] currentNumbers;
        private MyType inputValue = 1;

        public Game
            (

            IStorageProvider storageProvider,
            IReadInputProvider readInputProvider,
            ISendOutputProvider sendOutputProvider,
            IRandomProvider randomProvider,
            //ITextMessagesProvider textMessagesProvider,
            ISettingsProvider settingsProvider
            )
        {

            this.storageProvider = storageProvider;
            this.readInputProvider = readInputProvider;
            this.sendOutputProvider = sendOutputProvider;
            this.randomProvider = randomProvider;
            //this.textMessagesProvider = textMessagesProvider;
            this.settingsProvider = settingsProvider;
            try
            {
                this.settings = settingsProvider.getSettings();
            }
            catch (Exception e)
            {
                sendOutputProvider.printOutput(e.ToString());
                sendOutputProvider.printOutput("Using degault values instead");
                this.settings = new Settings();
            }
            
            this.currentNumbers = new MyType[settings.NumberOfValues];
        }

        private void checkInput()
        {
            bool inputCheck = false;

            do
            {
                try
                {
                    string input = readInputProvider.readInput();
                    inputValue = (MyType)Convert.ToUInt64(input);

                    if (currentNumbers.Contains(inputValue))
                    {
                        inputCheck = true;
                    }
                    else
                    {
                        sendOutputProvider.printOutput("Please choose one of the numbers on the screen");
                    }
                }
                catch (Exception)
                {
                    sendOutputProvider.printOutput("Incorrect input");
                    sendOutputProvider.printOutput("Please enter a single integer value");
                }
            }
            while (inputCheck == false);
        }

        public void run()
        {
            var randomNumbers = randomProvider.rand(settings);

            for (int i = 0; i < randomNumbers.Length; ++i)
            {
                currentNumbers[i] = randomNumbers[i];
            }

            sendOutputProvider.sendOutput(currentNumbers);

            bool exitCondition = false;

            while (!exitCondition)
            {
                checkInput();

                if (inputValue != settings.GoBack)
                {
                    MyType[] tempNumbers = new MyType[settings.NumberOfValues];

                    try
                    {
                        for (int i = 0; i < currentNumbers.Length; ++i)
                        {
                            tempNumbers[i] = checked(currentNumbers[i] * inputValue);
                        }

                        for (int i = 0; i < currentNumbers.Length; ++i)
                        {
                            currentNumbers[i] = tempNumbers[i];
                        }

                        storageProvider.push(inputValue);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Maximum level reached, going back");

                        if (storageProvider.Count > 0)
                        {
                            for (int i = 0; i < currentNumbers.Length; ++i)
                            {
                                currentNumbers[i] = currentNumbers[i] / storageProvider.peek();
                            }

                            storageProvider.pop();
                        }
                    }

                    sendOutputProvider.sendOutput(currentNumbers);
                }
                else
                {
                    if (storageProvider.Count > 0)
                    {
                        for (int i = 0; i < currentNumbers.Length; ++i)
                        {
                            currentNumbers[i] = currentNumbers[i] / storageProvider.peek();
                        }

                        sendOutputProvider.sendOutput(currentNumbers);

                        storageProvider.pop();
                    }
                    else
                    {
                        sendOutputProvider.printOutput("End");
                        sendOutputProvider.printOutput("Press enter to close the program");
                        Console.ReadKey();
                        exitCondition = true;
                    }
                }
            }
        }
    }
}

