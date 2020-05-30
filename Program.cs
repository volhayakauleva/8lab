using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    delegate void Welcome(string firstname);
    delegate void Goodbye(string firstname);

    public class Person
    {
        public delegate void PeopleHandler(string message);
        public event PeopleHandler Notify;
        public static int personCounter = 0;
        protected string FirstName { get; set; }
        protected string LastName { get; set; }
        protected int Age { get; set; }

        public Person(string firstName, string lastName, int age)
        {
            personCounter++;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string GetLastName() => LastName;
        public string GetFirstName() => FirstName;

        public void Print()
        {
            Console.WriteLine(LastName + " " + FirstName + " Возраст: " + Age);
        }
        public void PrintLastFirstName()
        {
            Notify?.Invoke("Были запрошены фамилия и имя: ");
            Console.WriteLine(LastName + " " + FirstName);
        }
        public static void peopleAmount()
        {
            Console.WriteLine("Общее количество людей: " + personCounter);
        }
    }

    public class Sportman : Person
    {
        public string Sport { get; set; }

        public Sportman(string firstName, string lastName, int age, string sport) : base(firstName, lastName, age)
        {
            Sport = sport;
        }

        public new void Print()
        {
            Console.WriteLine(LastName + " " + FirstName + " Возраст: " + Age + " " + Sport);
        }

        delegate void MessageHandler(string message);

        public void ShowEx()
        {
            try
            {
                throw new ArgumentException("Ошибка");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ShowMessage("hello", (string mes) => Console.WriteLine(mes));           
        }
        static void ShowMessage(string mes, MessageHandler handler)
        {
            handler(mes);
        }
    }

    class Professional : Sportman
    {
        int Level { get; set; }

        public Professional(string FirstName, string LastName, int Age, string sport, int level)
            : base(FirstName, LastName, Age, sport)
        {
            Level = level;
        }

        public new void Print()
        {
            Console.WriteLine(LastName + " " + FirstName + " Возраст: " + Age + " " + Sport + " Уровень: " + Level);
        }
    }

    class Program
    {
        static void Hello(string firstName)
        {
            Console.WriteLine("Здравствуй, " + firstName);
        }
        static void Bye(string firstName)
        {
            Console.WriteLine("До свидания, " + firstName);
        }

        static void Main(string[] args)
        {
            Professional specialist1 = new Professional("Иван", "Иванов", 20, "Футбол", 2);
            Professional specialist2 = new Professional("Петр", "Петров", 22, "Баскетбол", 3);
            specialist2.Notify += (string mes) => Console.WriteLine(mes);
            specialist1.Print();
            specialist2.Print();

            Welcome welcome = Hello;
            welcome += Bye;
            welcome(specialist1.GetFirstName());
            welcome -= Bye;
            welcome(specialist1.GetFirstName());
            welcome -= Hello;
            Goodbye handler = delegate (string firstName)
            {
                Console.WriteLine("Пока, " + firstName);
            };
            handler(specialist1.GetFirstName());

            specialist1.ShowEx();
            Console.ReadKey();
        }
    }
}
