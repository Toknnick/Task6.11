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
        private List<Fish> _fishes = new List<Fish>();
        private int _freePlace = 100;

        public void Work()
        {
            bool isWork = true;
            bool isDeadFish = false;
            int countOfDeadFishes = 0;
            Console.WriteLine("Рыбки ждали тебя.");

            while (isWork)
            {
                Console.WriteLine("1.Забросить рыбку в аквариум. \n2.Забрать рыбку. \n3.Покормить рыбок. \n4.Посмотреть на рыбок. \n5.Покинуть рыбок. \nВыбери вариант действия:");
                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                        AddFishes();
                        break;
                    case "2":
                        RemoveFish();
                        break;
                    case "3":
                        FeedFishes();
                        break;
                    case "4":
                        ShowOnFishes();
                        break;
                    case "5":
                        isWork = false;
                        break;
                    default:
                        isWork = Break();
                        break;
                }

                isDeadFish = IsDeadFish(ref countOfDeadFishes);

                if (isDeadFish == true)
                {
                    WriteSadMessage(countOfDeadFishes);
                    countOfDeadFishes = 0;
                }

                Console.WriteLine(" \nДля продолжнения нажмите любую клавишу: \n");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private bool IsDeadFish(ref int countOfDeadFishes)
        {
            int maxAge = 4;
            bool isDeadFish = false;

            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].IsMaxAge(maxAge))
                {
                    isDeadFish = true;
                    _fishes.RemoveAt(i);
                    i--;
                    countOfDeadFishes++;
                }
            }

            return isDeadFish;
        }

        private void AddFishes()
        {
            bool isRepeating = true;

            while (isRepeating)
            {
                int age = 0;
                Console.WriteLine($"Введите размер рыбки :");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int sizeOfFish))
                {
                    if (sizeOfFish <= _freePlace)
                    {
                        _freePlace -= sizeOfFish;
                        _fishes.Add(new Fish(age, sizeOfFish));
                        isRepeating = false;
                        AddYearsToFishes();
                    }
                    else
                    {
                        WriteError("Не хватает места!");

                        if (_freePlace == 0)
                        {
                            Console.WriteLine("Аквариум забит рыбками полностью!");
                        }

                        isRepeating = false;
                    }
                }
                else
                {
                    WriteError();
                }
            }
        }

        private void RemoveFish()
        {
            if (IsExistFish())
            {
                bool isRepeating = true;

                while (isRepeating)
                {
                    ShowOnFishes();
                    Console.WriteLine("Выбери номер рыбки:");
                    string userInput = Console.ReadLine();

                    if (int.TryParse(userInput, out int number))
                    {
                        number -= 1;

                        if (number < _fishes.Count)
                        {
                            _fishes.RemoveAt(number);
                            Console.WriteLine("Рыбка забрана успешно! Зачем-то...");
                            isRepeating = false;
                            AddYearsToFishes();
                        }
                        else
                        {
                            WriteError();
                        }
                    }
                    else
                    {
                        WriteError();
                    }
                }
            }
        }

        private void FeedFishes()
        {
            if (IsExistFish())
            {
                Console.WriteLine("Ты покормил(а) рыбок каким-то странным кормом. Ты видишь как они молодеют");
                ReduceLifeYearsOfFishes();
            }
        }

        private void ShowOnFishes()
        {
            if (IsExistFish())
            {
                for (int i = 0; i < _fishes.Count; i++)
                {
                    _fishes[i].ShowInfo(i + 1);
                }
            }
        }

        private void AddYearsToFishes()
        {
            Console.WriteLine("Вот и прошел еще один год.");
            AddLifeYearsOfFishes();
        }

        private bool Break()
        {
            Console.WriteLine("Одним неаккуратным движением ты разбил аквариум. Рыбки погибли");
            bool isWork = false;
            return isWork;
        }

        private void AddLifeYearsOfFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].AddLifeYears();
            }
        }

        private void ReduceLifeYearsOfFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].ReduceLifeYears();
            }
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

        private bool IsExistFish()
        {
            bool isExist = false;

            if (_fishes.Count > 0)
            {
                isExist = true;
            }
            else
            {
                Console.WriteLine("Аквариум пуст!");
            }

            return isExist;
        }

    }

    class Fish
    {
        private int _size;

        public int Age { get; private set; }

        public Fish(int age, int size)
        {
            Age = age;
            _size = size;
        }

        public void ShowInfo(int id)
        {
            Console.WriteLine($"{id}. Возраст рыбки: {Age}. Занимает места: {_size}.");
        }

        public void AddLifeYears()
        {
                Age++;
        }

        public void ReduceLifeYears()
        {
            if(Age > 0)
            Age--;
        }

        public bool IsMaxAge(int maxAge)
        {
            bool isMaxAge = false;

            if (Age >= maxAge)
            {
                isMaxAge = true;
            }

            return isMaxAge;
        }

    }
}
