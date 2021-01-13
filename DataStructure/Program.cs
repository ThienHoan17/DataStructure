using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Menu();
        }
        static public void Menu()
        {
            int Choosen;
            string filePath = @"word.txt";
            string[] lines = File.ReadAllLines(filePath);
            var dict = new StringDictionary(20);
            foreach (string s in lines)
            {
                string[] a = s.Split(": ");
                dict.Add(a[0], a[1]);
            }
            do
            {

                Console.WriteLine("<<<<<<<<<< DICTIONARY >>>>>>>>>>");
                Console.WriteLine("1. FIND WORD");
                Console.WriteLine("2. ADDING NEW WORD");
                Console.WriteLine("3. DELETE WORD IN DATABASE");
                Console.WriteLine("4. EXIT");
                Console.Write("YOUR CHOOSEN:");

                Choosen = int.Parse(Console.ReadLine());
                switch (Choosen)
                {
                    case 1:
                        Console.WriteLine("Input the word you need to translate into Vietnamese(Upcase the first character):");
                        string val = Console.ReadLine();
                        var result = dict.Get(val);
                        Console.WriteLine("Meaning in Vietnamese:");
                        Console.WriteLine(result);
                        Console.WriteLine("Press any key to continue!");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 2:
                        string NewWord;
                        Console.WriteLine("Enter Your Word and it's meanning in Vietnamese:");
                        Console.WriteLine("Ex: School: Truong Hoc");
                        NewWord = Console.ReadLine();
                        string[] nword = NewWord.Split(": ");
                        dict.Add(nword[0], nword[1]);
                        Console.WriteLine("Press any key to continue!");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 3:
                        string DeLeteWord;
                        Console.Write("Enter word you want to delete!");
                        Console.WriteLine("Ex: School: Truong Hoc");
                        DeLeteWord = Console.ReadLine();
                        dict.Delete(DeLeteWord);
                        Console.WriteLine("Press any key to continue!");
                        Console.ReadKey();

                        Console.Clear();
                        break;
                    case 4:
                        string fileName = @"word.txt";
                        //Kiểm tra nếu có file rồi thì xóa
                        FileInfo fi = new FileInfo(fileName);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                        //Tạo ra file mới
                        FileStream fs = new FileStream(fileName, FileMode.Create);
                        StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
                        // write some code here <3
                        for (int i = 0; i < 20; i++)
                        {
                            sWriter.WriteLine(dict.Save(i));
                        }
                        //End
                        sWriter.Flush();
                        fs.Close();
                        break;
                    default:
                        Console.WriteLine("Re-Enter your choose:");
                        break;
                }

            } while (Choosen != 4);
            Console.ReadLine();
        }
    }
    public class StringDictionary
    {
        private LinkedList<KeyValue>[] data;
        private readonly int maxKeyValue;
        public StringDictionary(int maxKeyValue)
        {
            this.maxKeyValue = maxKeyValue;
            data = new LinkedList<KeyValue>[maxKeyValue];
        }
        public string Get(string key)
        {
            var index = Hash(key);
            var list = data[index];
            string result = "The word is not in the database!";
            if (list != null)
            {
                foreach (var keyValue in list)
                {
                    if (keyValue.Key == key)
                    {
                        result = keyValue.Value;
                        break;
                    }
                }
            }
            return result;
        }
        public bool Add(string key, string value)
        {
            var index = Hash(key);
            var list = data[index];
            if (list != null)
            {
                list.AddLast(new KeyValue { Key = key, Value = value });
            }
            else
            {
                list = new LinkedList<KeyValue>();
                list.AddLast(new KeyValue { Key = key, Value = value });
                data[index] = list;
            }
            return true;
        }
        public string Delete(string key)
        {
            var index = Hash(key);
            var list = data[index];
            string result = "Can't find your word!";
            if (list != null)
            {
                foreach (var keyValue in list)
                {
                    if (keyValue.Key == key)
                    {
                        list.Remove(keyValue);
                        break;
                    }
                }
            }
            return result;
        }
        public string Save(int index)
        {
            string res = "";
            var list = data[index];
            int i = 0;
            if (list != null)
            {
                foreach (var keyValue in list)
                {
                    if (list.Count > 1)
                    {
                        while (i < list.Count)
                        {
                            res = keyValue.Key + ": " + keyValue.Value;
                            i++;
                            return res;
                        }
                    }
                    else if (list.Count == 1)
                    {
                        res = keyValue.Key + ": " + keyValue.Value;
                        return res;
                    }
                    else if (data == null)
                    {
                        res = "null: null";
                        return res;
                    }
                }
            }
            return res;
        }
        private int Hash(string key)
        {
            int code = GetHashCode(key);
            return code % this.maxKeyValue;
        }
        private int GetHashCode(string key)
        {
            int Val = 0;
            foreach (char c in key)
            {
                var x = 0;
                x = System.Convert.ToInt32(c);
                Val += x;
            }
            return Val + Val * key.Length;
        }
    }
    public struct KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
