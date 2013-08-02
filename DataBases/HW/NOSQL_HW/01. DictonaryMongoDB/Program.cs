using System;
using System.Text;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace _01.MongoDb_Dictionary
{
    class Program
    {
        static void Main()
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            MongoServer server = client.GetServer();
            var dictionaryDB = server.GetDatabase("dictionary");
            var wordCollection = dictionaryDB.GetCollection<Word>("words");
            var wordCollectionQueery = wordCollection.AsQueryable();

            Console.WriteLine("This is MongoDB console dictionary");

            while (true)
            {
                WriteOptions();

                string command = Console.ReadLine();

                if (command == "exit")
                {
                    break;
                }

                int parsedCommand = 0;
                try
                {
                    parsedCommand = int.Parse(command);
                }
                catch (Exception)
                {

                    Console.WriteLine("Please, enter number from 1 to 3 including.");
                }

                switch (parsedCommand)
                {
                    case 1: AddNewWordAndTranslation(wordCollection); break;
                    case 2: ListWordsAndTranslations(wordCollectionQueery); break;
                    case 3: FindTranslation(wordCollectionQueery); break;
                    default:
                        break;
                }
            }
        }

        private static void FindTranslation(IQueryable<Word> wordCollection)
        {
            Console.Write("Search for: ");
            var searchWord = Console.ReadLine();

            var word = wordCollection.Where(w => w.name == searchWord).ToList();

            foreach (Word item in word)
            {
                Console.WriteLine("Translation: " + item.translation);
            }
        }

        private static void ListWordsAndTranslations(IQueryable<Word> wordCollection)
        {
            var words = wordCollection.ToList();

            StringBuilder sb = new StringBuilder();
            foreach (Word item in words)
            {
                sb.Append(item.name + " --> " + item.translation);
                sb.Append("\n");
            }

            Console.WriteLine(sb);
        }

        private static void AddNewWordAndTranslation(MongoCollection<Word> wordCollection)
        {
            Console.Write("Word: ");
            string word = Console.ReadLine();
            Console.Write("Translation: ");
            string translation = Console.ReadLine();

            Word newWord = new Word()
            {
                name = word,
                translation = translation
            };

            wordCollection.Insert(newWord);
        }

        private static void WriteOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Press 1, to add new word and translataion.");
            Console.WriteLine("Press 2, to list all words and their translataions.");
            Console.WriteLine("Press 3, to find translation of given word.");
            Console.WriteLine("For exit write: exit");
            Console.WriteLine();
        }
    }

    public class Word
    {
        public ObjectId Id { get; set; }

        public string name { get; set; }

        public string translation { get; set; }
    }
}