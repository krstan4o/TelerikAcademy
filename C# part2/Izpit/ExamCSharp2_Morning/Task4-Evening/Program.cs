using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
  
    private static string message;
    private static string cipher;
    private static StringBuilder cypherText;

    static void Main()
    {
       
        cypherText = new StringBuilder();

        message = Console.ReadLine();
        cipher = Console.ReadLine();


        Encrypt();

        cypherText.Append(cipher);

        Encode(cypherText.ToString());
        cypherText.Append(cipher.Length);
        Console.WriteLine(cypherText);

    }

    private static string GetCipherLength()
    {
        int currentIndex = message.Length - 1;
       
        while (char.IsDigit(message[currentIndex]))
        {
            currentIndex--;
        }
        string result = message.Substring(currentIndex + 1, message.Length - 1 - currentIndex);
        return result;
    }

    private static void Decode(string textToDecode)
    {
        StringBuilder builder = new StringBuilder();
        StringBuilder diggit = new StringBuilder();
        for (int i = 0; i < textToDecode.Length; i++)
        {
            if (char.IsDigit(textToDecode[i]))
            {
                diggit.Append(textToDecode[i]);
            }
            else
            {
                if (diggit.Length == 0)
                {
                    builder.Append(textToDecode[i]);
                }
                else
                {
                    int repeat = int.Parse(diggit.ToString());
                    diggit.Clear();
                    for (int j = 0; j < repeat; j++)
                    {
                        builder.Append(textToDecode[i]);
                    }
                }
            }
        }
        message = builder.ToString();
    }

    private static void Encode(string textToEncode)
    {
        cypherText.Clear();
        StringBuilder diggit = new StringBuilder();
        char element = textToEncode[0];
        int startIndex = 0;

        int count = 1;

        for (int i = 1; i < textToEncode.Length; i++)
        {
            if (textToEncode[i] == element)
            {
                count++;
            }
            else
            {
                string sequence = textToEncode.Substring(startIndex, count);
                string sequenceAndLen = count + element.ToString();
                if (sequenceAndLen.Length >= sequence.Length)
                {
                    cypherText.Append(sequence);
                }
                else
                {
                    cypherText.Append(sequenceAndLen);
                }
                element = textToEncode[i];
                startIndex = i;
                count = 1;
            }
        }
        string lastQSquence = textToEncode.Substring(startIndex, count);
        string sequenceAndLenLast = count + element.ToString();

        if (sequenceAndLenLast.Length >= lastQSquence.Length)
        {
            cypherText.Append(lastQSquence);
        }
        else
        {
            cypherText.Append(sequenceAndLenLast);
        }

    }

    private static char EncriptLetter(char p1, char p2)
    {
        return (char)(((p1 - 65) ^ (p2 - 65)) + 65);
    }
    private static void Encrypt()
    {
        int biggerLength = Math.Max(cipher.Length, message.Length);
        bool isMeessageBigger = false;
        if (biggerLength == message.Length)
        {
            isMeessageBigger = true;
        }

        int indexOfCipher = 0;
        int indexOfMessage = 0;
        char resultXor = '\0';

        for (int i = 0; i < biggerLength; i++)
        {
            if (indexOfCipher >= cipher.Length)
            {
                indexOfCipher = 0;
            }
            if (indexOfMessage >= message.Length)
            {
                indexOfMessage = 0;
            }


            if (isMeessageBigger)
            {

                resultXor = EncriptLetter(message[indexOfMessage], cipher[indexOfCipher]);
                cypherText.Append(resultXor);
            }
            else
            {
                if (indexOfCipher <= indexOfMessage)
                {
                    resultXor = EncriptLetter(message[indexOfMessage], cipher[indexOfCipher]);
                    cypherText.Append(resultXor);
                }
                else
                {
                    resultXor = EncriptLetter(cypherText[indexOfMessage], cipher[indexOfCipher]);
                    cypherText[indexOfMessage] = resultXor;
                }
            }

            indexOfCipher++;
            indexOfMessage++;
        }

    }

}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.IO;//only for testing
//using System.Text;
//using System.Threading.Tasks;


/////
///// 4. Decode and Decrypt
/////


//    class Problem04
//    {
//        static void Main()
//        {
//            //only for testing
//            string file = @"..\..\..\t1.txt";
//            if (File.Exists(file))
//            {
//                Console.SetIn(new StreamReader(file));
//            }
//            //only for testing

//            string message = Console.ReadLine();
//            int cypherLenghtInString = GetCyperLenght(message);
//            int cypherLenght = int.Parse(message.Substring(message.Length - cypherLenghtInString, cypherLenghtInString));
//            string decodedMessage = Decode(message.Substring(0, message.Length - cypherLenghtInString));
//            string cypher = decodedMessage.Substring(decodedMessage.Length - cypherLenght, cypherLenght);
//            string encriptedMessage = decodedMessage.Substring(0, decodedMessage.Length - cypher.Length);
//            Console.WriteLine(Encript(encriptedMessage, cypher));
//        }

//        static string Decode(string message)
//        {
//            StringBuilder output = new StringBuilder();
//            StringBuilder Digit = new StringBuilder();
//            for (int i = 0; i < message.Length; i++)
//            {
//                if (char.IsDigit(message[i]))
//                {
//                    Digit.Append(message[i]);
//                }
//                else
//                {
//                    if (Digit.Length == 0)
//                    {
//                        output.Append(message[i]);
//                    }
//                    else
//                    {
//                        int repeat = int.Parse(Digit.ToString());
//                        for (int j = 0; j < repeat; j++)
//                        {
//                            output.Append(message[i]);
//                        }
//                        Digit.Clear();
//                    }

//                }
//            }
//            return output.ToString();
//        }

//        static string Encript(string message, string cypher)
//        {
//            StringBuilder output = new StringBuilder(message.Length);

//            if (message.Length >= cypher.Length)
//            {
//                int cypherIndex = 0;
//                for (int i = 0; i < message.Length; i++)
//                {
//                    cypherIndex = i % cypher.Length;
//                    output.Append(EncriptLetter(message[i], cypher[cypherIndex]));

//                }
//            }
//            else
//            {

//                for (int i = 0; i < cypher.Length; i++)
//                {
//                    if (i < message.Length)
//                    {
//                        output.Append(EncriptLetter(message[i], cypher[i]));
//                    }
//                    else
//                    {
//                        output[i % message.Length] = EncriptLetter(output[i % message.Length], cypher[i]);
//                    }
//                }
//            }

//            return output.ToString();
//        }

//        private static char EncriptLetter(char p1, char p2)
//        {
//            return (char)(((p1 - 65) ^ (p2 - 65)) + 65);
//        }

//        static int GetCyperLenght(string message)
//        {
//            int result = 0;
//            int currentIndex = message.Length - 1;

//            while (char.IsDigit(message[currentIndex]))
//            {
//                currentIndex--;
//            }

//            return message.Length - (currentIndex + 1);
//        }
//    }
