using System;
using System.Collections.Generic;

namespace Task6._11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            aquarium.Work();
        }
    }
    class Aquarium
    {
        public void Work()
        {
            List<Fish> fishes = new List<Fish>();
            bool isWork = true;
            bool isDeadFish = false;
            int maxAge = 4;
            int countOfDeadFishes = 0;
            int maxSizeOfAquarium = 100;
            int freePlace = maxSizeOfAquarium;
            Console.WriteLine("Рыбки ждали тебя.");

            while (isWork)
            {
                Console.WriteLine("1.Забросить рыбку в аквариум. \n2.Забрать рыбку. \n3.Покормить рыбок. \n4.Посмотреть на рыбок. \n5.Покинуть рыбок. \nВыбери вариант действия:");
                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                        AddFishes(fishes, ref freePlace);
                        break;
                    case "2":
                        RemoveFish(fishes);
                        break;
                    case "3":
                        FeedFishes(fishes);
                        break;
                    case "4":
                        ShowOnFishes(fishes);
                        break;
                    case "5":
                        isWork = false;
                        break;
                    default:
                        isWork = Break();
                        break;
                }

                for (int i = 0; i < fishes.Count; i++)
                {
                    if (IsMaxAge(fishes, i, maxAge))
                    {
                        isDeadFish = true;
                        fishes.RemoveAt(i);
                        countOfDeadFishes++;
                    }
                }

                if (isDeadFish)
                {
                    WriteSadMessage(countOfDeadFishes);
                    isDeadFish = false;
                    countOfDeadFishes = 0;
                }

                Console.WriteLine(" \nДля продолжнения нажмите любую клавишу: \n");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddFishes(List<Fish> fishes, ref int freePlace)
        {
            bool isRepeating = true;

            while (isRepeating)
            {
                int age = 0;
                Console.WriteLine($"Введите размер рыбки :");
                string userInput = Console.ReadLine();

                if (TryToParse(userInput, out int sizeOfFish))
                {
                    if (sizeOfFish <= freePlace)
                    {
                        freePlace -= sizeOfFish;
                        fishes.Add(new Fish(age, sizeOfFish));
                        isRepeating = false;
                        AddYearsToFishes(fishes);
                    }
                    else if (freePlace >= 0)
                    {
                        WriteError("Не хватает места! ");

                        if (freePlace == 0)
                        {
                            isRepeating = false;
                            Console.WriteLine("Аквариум забит рыбками полностью!");
                        }
                    }
                }
            }
        }

        private void RemoveFish(List<Fish> fishes)
        {
            if (IsExistFish(fishes))
            {
                bool isRepeating = true;

                while (isRepeating)
                {
                    ShowOnFishes(fishes);
                    Console.WriteLine("Выбери номер рыбки:");
                    string userInput = Console.ReadLine();

                    if (TryToParse(userInput, out int number))
                    {
                        number -= 1;

                        if (number < fishes.Count)
                        {
                            fishes.RemoveAt(number );
                            Console.WriteLine("Рыбка забрана успешно! Зачем-то...");
                            isRepeating = false;
                            AddYearsToFishes(fishes);
                        }
                        else
                        {
                            WriteError();
                        }
                    }
                }
            }
        }

        private void FeedFishes(List<Fish> fishes)
        {
            if (IsExistFish(fishes))
            {
                int option = 2;
                Console.WriteLine("Ты покормил(а) рыбок каким-то странным кормом. Ты видишь как они молодеют");
                ChangeLifeYearsOfFishes(fishes, option);
            }
        }

        private void ShowOnFishes(List<Fish> fishes)
        {
            if (IsExistFish(fishes))
            {
                for (int i = 0; i < fishes.Count; i++)
                {
                    fishes[i].ShowInfo(i + 1);
                }
            }
        }

        private void AddYearsToFishes(List<Fish> fishes)
        {
            int option = 1;
            Console.WriteLine("Вот и прошел еще один год.");
            ChangeLifeYearsOfFishes(fishes, option);
        }

        private bool Break()
        {
            Console.WriteLine("Одним неаккуратным движением ты разбил аквариум. Рыбки погибли");
            bool isWork = false;
            return isWork;
        }

        private void ChangeLifeYearsOfFishes(List<Fish> fishes, int option)
        {
            for (int i = 0; i < fishes.Count; i++)
            {
                fishes[i].ChangeLifeYears(option);
            }
        }

        private bool IsMaxAge(List<Fish> fishes, int numberOfFish, int maxAge)
        {
            bool isMaxAge = false;

            if (fishes[numberOfFish].Age >= maxAge)
            {
                isMaxAge = true;
            }

            return isMaxAge;
        }

        private bool TryToParse(string userInput, out int number)
        {
            bool isParse = true;

            if (int.TryParse(userInput, out number) == false)
            {
                isParse = false;
                WriteError();
            }

            return isParse;
        }

        private void WriteError(string text = "Странное число...  Попробуйте еще раз ")
        {
            Console.WriteLine($"{text}\n");
        }

        private void WriteSadMessage(int countOfDeadFishes)
        {
            string text = $"погибли {countOfDeadFishes} рыбок";

            if (countOfDeadFishes == 1)
            {
                text = "погибла одна рыбка";
            }
            else if (countOfDeadFishes > 1 && countOfDeadFishes <= 4)
            {
                text = $"погибли {countOfDeadFishes} рыбки";
            }

            Console.WriteLine($"Плохой день. Сегодня {text}.");
        }

        private bool IsExistFish(List<Fish> fishes)
        {
            bool IsExist = false;

            if (fishes.Count > 0)
            {
                IsExist = true;
            }
            else
            {
                Console.WriteLine("Аквариум пуст!");
            }

            return IsExist;
        }

        class Fish
        {
            public int Age { get; private set; }
            private int _size;

            public Fish(int age, int size)
            {
                Age = age;
                _size = size;
            }

            public void ShowInfo(int id)
            {
                Console.WriteLine($"{id}. Возраст рыбки: {Age}. Занимает места: {_size}.");
            }

            public void ChangeLifeYears(int option)
            {
                if (option == 1)
                {
                    Age++;
                }
                else
                {
                    if (Age > 0)
                        Age--;
                }
            }
        }
    }
}
