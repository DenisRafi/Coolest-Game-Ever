using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void PrintOnPosition(int x, int y, char c,
    ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
    }
    static void Main()
    {
        Console.Title = ("By Denis Rafi");
        double speed = 100.0;
        double acceleration = 0.8;
        int playfieldWidth = 10;
        int livesCount = 10;
        Console.BufferHeight = Console.WindowHeight = 20;
        Console.BufferWidth = Console.WindowWidth = 30;
        Object userCar = new Object();
        userCar.x = 2;
        userCar.y = Console.WindowHeight - 1;
        userCar.c = '$';
        userCar.color = ConsoleColor.Blue;
        Random randomGenerator = new Random();
        List<Object> objects = new List<Object>();
        while (true)
        {
            speed += acceleration;
            if (speed > 400)
            {
                speed = 400;
            }
            bool hitted = false;
            {
                int chance = randomGenerator.Next(0, 100);
                if (chance < 10)
                {
                    Object newObject = new Object();
                    newObject.color = ConsoleColor.Blue;
                    newObject.c = '$';
                    newObject.x = randomGenerator.Next(0, playfieldWidth);
                    newObject.y = 0;
                    objects.Add(newObject);
                }
                else if (chance < 20)
                {
                    Object newObject = new Object();
                    newObject.color = ConsoleColor.Blue;
                    newObject.c = '$';
                    newObject.x = randomGenerator.Next(0, playfieldWidth);
                    newObject.y = 0;
                    objects.Add(newObject);
                }
                else
                {
                    Object newCar = new Object();
                    newCar.color = ConsoleColor.Green;
                    newCar.x = randomGenerator.Next(0, playfieldWidth);
                    newCar.y = 0;
                    newCar.c = 'X';
                    objects.Add(newCar);
                }
            }
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (userCar.x - 1 >= 0)
                    {
                        userCar.x = userCar.x - 1;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if (userCar.x + 1 < playfieldWidth)
                    {
                        userCar.x = userCar.x + 1;
                    }
                }
            }
            List<Object> newList = new List<Object>();
            for (int i = 0; i < objects.Count; i++)
            {
                Object oldCar = objects[i];
                Object newObject = new Object();
                newObject.x = oldCar.x;
                newObject.y = oldCar.y + 1;
                newObject.c = oldCar.c;
                newObject.color = oldCar.color;
                if (newObject.c == '$' && newObject.y == userCar.y && newObject.x == userCar.x)
                {
                    speed -= 20;
                }
                if (newObject.c == '$' && newObject.y == userCar.y && newObject.x == userCar.x)
                {
                    livesCount++;
                }
                if (newObject.c == 'X' && newObject.y == userCar.y && newObject.x == userCar.x)
                {
                    livesCount--;
                    hitted = true;
                    speed += 50;
                    if (speed > 400)
                    {
                        speed = 400;
                    }
                    if (livesCount <= 0)
                    {
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                if (newObject.y < Console.WindowHeight)
                {
                    newList.Add(newObject);
                }
            }
            objects = newList;
            Console.Clear();
            if (hitted)
            {
                objects.Clear();
            }
            else
            {
                PrintOnPosition(userCar.x, userCar.y, userCar.c, userCar.color);
            }
            foreach (Object car in objects)
            {
                PrintOnPosition(car.x, car.y, car.c, car.color);
            }         
            Thread.Sleep((int)(500 - speed));
        }
    }
}
struct Object
{
    public int x;
    public int y;
    public char c;
    public ConsoleColor color;
}