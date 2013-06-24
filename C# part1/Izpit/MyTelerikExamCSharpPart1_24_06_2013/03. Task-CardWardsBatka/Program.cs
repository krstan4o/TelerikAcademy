using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace _03.Task_CardWardsBatka
{
    class Program
    {
        static void Main()
        {
            string[] firstPlayerCards = new string[3];
            string[] secoundPlayerCards = new string[3];
            int N = int.Parse(Console.ReadLine());

            int firstPlayerHandStrength = 0; 
            int secoundPlayerHandStrength = 0;

            BigInteger firstPlayerScore = 0;
            BigInteger secoundPlayerScore = 0;
           

            int firstPlayerWonMatches = 0;
            int secoundPlayerWonMatches = 0;
            
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string input = Console.ReadLine();
                    firstPlayerCards[j] = input;
                   
                    switch (firstPlayerCards[j])
                    {
                        case "2":
                            firstPlayerHandStrength += 10; 
                            break;
                        case "3":
                            firstPlayerHandStrength += 9; 
                            break;
                        case "4":
                            firstPlayerHandStrength += 8; 
                            break;
                        case "5":
                            firstPlayerHandStrength += 7; 
                            break;
                        case "6":
                            firstPlayerHandStrength += 6; 
                            break;
                        case "7":
                            firstPlayerHandStrength += 5; 
                            break;
                        case "8":
                            firstPlayerHandStrength += 4; 
                            break;
                        case "9":
                            firstPlayerHandStrength += 3; 
                            break;
                        case "10":
                            firstPlayerHandStrength += 2; 
                            break;
                        case "A":
                            firstPlayerHandStrength += 1; 
                            break;
                        case "J":
                            firstPlayerHandStrength += 11; 
                            break;
                        case "Q":
                            firstPlayerHandStrength += 12; 
                            break;
                        case "K":
                            firstPlayerHandStrength += 13; 
                            break;
                        case "Z":
                            firstPlayerScore *= 2;
                            break;
                        case "Y":
                            firstPlayerScore -= 200;
                            break;
                        default:
                            break;
                    }
                }
                for (int j = 0; j < 3; j++)
                {
                    string input = Console.ReadLine();
                    secoundPlayerCards[j] = input;
                    switch (secoundPlayerCards[j])
                    {
                        case "2":
                            secoundPlayerHandStrength += 10;
                            break;
                        case "3":
                            secoundPlayerHandStrength += 9;
                            break;
                        case "4":
                            secoundPlayerHandStrength += 8;
                            break;
                        case "5":
                            secoundPlayerHandStrength += 7;
                            break;
                        case "6":
                            secoundPlayerHandStrength += 6;
                            break;
                        case "7":
                            secoundPlayerHandStrength += 5;
                            break;
                        case "8":
                            secoundPlayerHandStrength += 4;
                            break;
                        case "9":
                            secoundPlayerHandStrength += 3;
                            break;
                        case "10":
                            secoundPlayerHandStrength += 2;
                            break;
                        case "A":
                            secoundPlayerHandStrength += 1;
                            break;
                        case "J":
                            secoundPlayerHandStrength += 11;
                            break;
                        case "Q":
                            secoundPlayerHandStrength += 12;
                            break;
                        case "K":
                            secoundPlayerHandStrength += 13;
                            break;
                        case "Z":
                            secoundPlayerScore *= 2;
                            break;
                        case "Y":
                            secoundPlayerScore -= 200;
                            break;
                        default:
                            break;
                    }
                }
                // Check X
                if (firstPlayerCards.Contains("X") && secoundPlayerCards.Contains("X"))
                {
                    firstPlayerScore += 50;
                    secoundPlayerScore += 50;
                }
                else if (firstPlayerCards.Contains("X") && !secoundPlayerCards.Contains("X"))
                {
                    Console.WriteLine("X card drawn! Player one wins the match!");
                    return;
                }
                else if (!firstPlayerCards.Contains("X") && secoundPlayerCards.Contains("X"))
                {
                     Console.WriteLine("X card drawn! Player two wins the match!");
                     return;
                }

                if (firstPlayerHandStrength > secoundPlayerHandStrength)
                {
                    firstPlayerWonMatches++;
                    firstPlayerScore += firstPlayerHandStrength;
                }
                else if (firstPlayerHandStrength < secoundPlayerHandStrength)
                {
                    secoundPlayerWonMatches++;
                    secoundPlayerScore += secoundPlayerHandStrength;
                }
                else
                {
                    //We do nothing
                }
                firstPlayerHandStrength = 0;
                secoundPlayerHandStrength = 0;
            }

            if (firstPlayerScore > secoundPlayerScore)
            {
                Console.WriteLine("First player wins!");
                Console.WriteLine("Score: {0}", firstPlayerScore);
                Console.WriteLine("Games won: {0}", firstPlayerWonMatches);
            }
            else if (firstPlayerScore < secoundPlayerScore)
            {
                Console.WriteLine("Second player wins!");
                Console.WriteLine("Score: {0}", secoundPlayerScore);
                Console.WriteLine("Games won: {0}", secoundPlayerWonMatches);
            }
            else //firstPlayerScore == secoundPlayerScore
            {
                Console.WriteLine("It's a tie!");
                Console.WriteLine("Score: {0}", firstPlayerScore);
            }
        }
    }
}
