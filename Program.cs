using System ;

namespace dotnetProject
{
    class Program {
        static void Main(String[] args){
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine($"Hello, {name}!");
        }
    }
}