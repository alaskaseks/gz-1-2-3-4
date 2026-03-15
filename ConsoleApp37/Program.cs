using ConsoleApp37;
using ConsoleApp37.Data;

namespace ConsoleApp37
{
    public class Program
    {
        static DataContext db = new DataContext();
        static CategoryMenu menu = new CategoryMenu(db);
        public static void Main(string[] args)
        {
            menu.Run();
        }
    }
}