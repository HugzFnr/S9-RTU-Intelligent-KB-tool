using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace KB_tool_console
{
    class Program
    {
        static void Main(string[] args)
        {
            const float minimumThreshold = 20; //under this threshold entry is useless and deleted
            const float defaultTrustLevel = 30;
            const float PKnewcondition = 0.05f;

            int indexOfNewEntry = 0;

            float[,] PkTable = new float[1000,1000];
            List<string> Entries = new List<string>();
            
            string input;
            do
            {
                Console.WriteLine("\nType 1 to add a name and surname entry \n 2 to add one entry and ask for suggestion \n 3 to see stored entries");
                input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("Write a name and surname");

                    string nameSurname = Console.ReadLine();

                    string[] strTab = new string[2];
                    strTab = nameSurname.Split(new char[] { ' ' });
                    string name = strTab[0];
                    string surname = strTab[1];

                    AddEntry(name);
                    AddEntry(surname);

                    Console.WriteLine("Your name is : " + name);
                    Console.WriteLine("Your surname is : " + surname);
                    Console.WriteLine("Entries added");

                    DisplayCurrentPkTable();
                }
                else if (input == "2")
                {
                    Console.WriteLine("Write a name or surname");
                    string entry = Console.ReadLine();
                    AddEntry(entry);

                    Console.WriteLine("Suggestion : " + AskSuggestion(entry));
                    DisplayCurrentPkTable();
                }
                else if (input=="3")
                {
                    Console.Write("\n");
                    foreach (string s in Entries)
                    {
                        Console.Write(s + " ");
                    }
                }

            } while (input != "1" || input != "2" || input != "3");

            if (Console.ReadLine() == "1")
            {
                Console.WriteLine("Write a name and surname");

                string nameSurname = Console.ReadLine();

                string[] strTab = new string[2];
                strTab = nameSurname.Split(new char[] { ' ' });
                string name = strTab[0];
                string surname = strTab[1];

                AddEntry(name);
                AddEntry(surname);

                Console.WriteLine("Your name is : " + name);
                Console.WriteLine("Your surname is : " + surname);
                Console.WriteLine("Entries added");

                DisplayCurrentPkTable();
            }
            else if (Console.ReadLine()=="2")
            {
                Console.WriteLine("Write a name or surname");
                string entry = Console.ReadLine();
                AddEntry(entry);

                Console.WriteLine("Suggestion : " + AskSuggestion(entry));
                DisplayCurrentPkTable();
            }

            void AddEntry(string entry)
            {
                if (!Entries.Exists(element => element==entry)) //doesn't duplicate identical entries
                {
                    Entries.Add(entry);

                    for (int x = 0; x < 1000; x++)
                    {
                        PkTable[x, indexOfNewEntry] = defaultTrustLevel;
                    }
                    for (int y = 0; y < 1000; y++)
                    {
                        PkTable[indexOfNewEntry, y] = defaultTrustLevel;
                    }

                    indexOfNewEntry++;
                }
            }

            string AskSuggestion(string entry)
            {
                int concernedIndex = Entries.FindIndex(element => element == entry);

                float MaxPk = PkTable[0, concernedIndex];
                int indexConclusion = 0;
                for (int q=0;q<indexOfNewEntry;q++)
                {
                    if (PkTable[q, concernedIndex] > MaxPk)
                    {
                        MaxPk = PkTable[q, concernedIndex];
                        indexConclusion = q;
                    }
                    
                }
                float newPk = MaxPk + (1 - MaxPk) * PKnewcondition;
                PkTable[indexConclusion, concernedIndex] = newPk;

                return Entries[indexConclusion];
            }

            void DisplayCurrentPkTable()
            {
                Console.WriteLine("Current PkTable : ");
                for (int x=0;x<indexOfNewEntry;x++)
                {
                    Console.Write("\n");
                    for (int y=0;y<indexOfNewEntry;y++)
                    {
                        Console.Write(PkTable[x, y] + " ");
                    }
                }
            }

        }
    }
}
