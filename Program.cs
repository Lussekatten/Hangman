using System;
using System.Text;

namespace HangmanByLuc
{
    class Program
    {
        static void Main(string[] args)
        {
            // attemptsLeft keeeps track of the number of failed attempts the user has left before the game is lost
            int mistakesLeft = 10;
            bool keepRunning = true;
            int lettersFound = 0;
            StringBuilder sb = new StringBuilder();
            string[] wordsRepository = new string[] {"Firstword", "Secondword","Thirdword"};

            //Select (randomly) a word from the repository
            Random rnd = new Random();
            int index = rnd.Next(0, wordsRepository.Length);
            string secretWord = wordsRepository[index];
            string[] userCompleteGuess = new string[secretWord.Length];

            //Initialize the string to be guessed by the user with empty dashes
            InitializeWithDashes(userCompleteGuess);


            while (keepRunning)
            {        
                Console.WriteLine("Hangman is running. Guess a letter/word");
                string guess = Console.ReadLine();
                if (guess.Length==1)
                {
                    //The user is atempting to guess a single letter
                    Console.WriteLine("You are trying to guess a single letter");
                    if (secretWord.IndexOf(guess.ToLower())==-1 && secretWord.IndexOf(guess.ToUpper()) == -1)
                    {
                        //The letter was not found
                        Console.WriteLine("Wrong guess. Try again!");
                        //If the letter is not found in the secretWord, append it to the list of letters already checked
                        //this means adding it to the string builder variable.
                        sb.Append(guess);
                        mistakesLeft--;
                    }
                    else
                    {
                        int indexPos = -1;
                        //Insert the correct letter in the userCompleteGuess by replacing the corresponding dash(es) with the letter
                        //Observe that ALL occurences of the guessed letter will be replaced.
                        
                        for (int i = 0; i < secretWord.Length; i++)
                        {
                            //We must use the IndexOf method with a starting index position (see the second parameter)
                            //Otherwise we will just keep finding the first occcurence of the letter over and over
                            indexPos = secretWord.ToLower().IndexOf(guess,i);
                            if (indexPos==i)
                            {
                                userCompleteGuess[i] = guess;

                                //If this was the last letter found from the secret word, 
                                //congratulate the user and exit the loop
                                lettersFound++;
                                if (lettersFound == secretWord.Length)
                                {
                                    keepRunning = false;
                                }
                            }
                        }
                    }

                    UpdateStatus(mistakesLeft, userCompleteGuess, sb);
                    Console.WriteLine($"Secret word: {secretWord}");

                }
                else
                {
                    Console.WriteLine("You are trying to guess the whole word");
                    if (secretWord.ToLower()==guess)
                    {
                        //congrats and exit
                        Console.Clear();
                        Console.WriteLine($"Congratulations! You guessed correctly. The word was indeed: {secretWord}");
                        keepRunning = false;
                    }
                    else
                    {
                        mistakesLeft--;
                        UpdateStatus(mistakesLeft, userCompleteGuess, sb);
                        Console.WriteLine($"Secret word: {secretWord}");
                    }
                }
                //We must also check if the user has any guessing attempts left and exit the loop if the limit
                //is reached
                if (mistakesLeft==0)
                {
                    Console.WriteLine("You are out of guesses! Better luck next time!");
                    keepRunning = false;
                }
            }

            Console.WriteLine("---- The end ----");
        }

        private static void InitializeWithDashes(string[] arr)
        {
            //string result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = "-";
            }
        }

        private static void UpdateStatus(int mistakesToGo, string[] guessSoFar, StringBuilder sb1)
        {
            Console.Clear();
            string guessed = "";
            for (int i = 0; i < guessSoFar.Length; i++)
            {
                guessed += guessSoFar[i];
            }
            Console.WriteLine($"You have {mistakesToGo} more attempts left.");
            Console.WriteLine($"Wrong letters so far: {sb1}");
            Console.WriteLine($"Your guess (so far): {guessed}");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
