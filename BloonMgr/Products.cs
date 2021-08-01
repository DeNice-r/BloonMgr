using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace BloonMgr
{

    static class Products
    {
        static List<String> Denoms = new List<string> { "фол Звезда 32\"", "мат Шар 11\"", "фол Звезда 28\"", "мат Шар 12\"", "фол Цифра '5' 30\"" }; // denominations
        static List<Int32> LastPrice = new List<int> { 0, 0, 0, 0, 0 };

        static Products()
        {
            Denoms = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("Denoms.json"));
        }

        public static Int32 GetID(String denom)
        {
            Int32 idx = Denoms.IndexOf(denom);
            if (idx == -1)
                if (MessageBox.Show("Похоже, наименование '" + denom + "' ещё не встречалось. Добавить его в базу товаров?", "Неизвестный товар.", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    // adding the denom to db
                    Denoms.Add(denom);
                    return Denoms.Count() - 1;
                }
                else return -1;
            else return idx;
        }

        public static String GetName(Int32 ID)
        {
            try
            {
                return Denoms[ID];
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Вероятно, база данных повреждена. Замените файл базы данных сохранённой ранее копией и уведомите об ошибке администратора. Работу лучше приостановить для обеспечения целостности оставшихся данных.", "Ошибка!");
                return "Ошибка!";
            }
        }

        public static void AddName(String denom)
        {

        }

        public static Int32 RandomID()
        {
            return Program.rnd.Next(0, Denoms.Count());
        }

        public static void Save()
        {
            File.WriteAllText("Denoms.json", JsonConvert.SerializeObject(Denoms));
        }
    }
}
