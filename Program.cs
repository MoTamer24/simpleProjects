using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Transactions;
namespace OOP_proejct
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dataBase Data = new dataBase();
            dataBase.staffList.Add(new staffs ( 1,"mohamed","admin","1234"));
            dataBase.staffList.Add(new staffs(2, "ayman", "worker","1111"));
           mainMenu: while (true)
            {
                Console.WriteLine("Enter your password");
                string enterPass = Console.ReadLine();
                if (dataBase.staffList.Find(staff => staff.pass == enterPass) == null)
                {
                    Console.WriteLine("Wrong password ");
                }
                else
                {
                    if (enterPass == "1234")
                    {
                        Console.WriteLine("your are an admin");
                        while (true)
                        {

                            Console.WriteLine("choose 1.add 2.delete 3.find 4.end 5.show all ");
                            int choose = 0;
                            choose = int.Parse(Console.ReadLine());
                            if (choose == 4)
                            {
                                goto mainMenu;
                            }
                            switch (choose)
                            {
                                case 1:
                                    Console.WriteLine("enter id , title ,author , availability");
                                    int id = int.Parse(Console.ReadLine());
                                    string title = Console.ReadLine();
                                    string author = Console.ReadLine();
                                    bool avail = bool.Parse(Console.ReadLine());
                                    operations.AddBook(id, title, author, avail);
                                    break;
                                case 2:
                                    Console.WriteLine("id ");
                                    int deleteid = int.Parse(Console.ReadLine());
                                    operations.RemoveBook(deleteid);
                                    break;
                                case 3:
                                    Console.WriteLine("id "); int findid = int.Parse(Console.ReadLine());
                                    operations.FindBook(findid);
                                    break;
                                case 5:
                                    operations.showBooks();
                                    break;
                            }

                        }


                    }
                    else if (enterPass == "1111")
                    {
                        Console.WriteLine("your are a worker ");
                        while (true)
                        {

                            Console.WriteLine("choose 1.find 2.show all books 3.end ");
                            int choose = 0;
                            choose = int.Parse(Console.ReadLine());
                            if (choose == 3)
                            {
                                goto mainMenu;
                            }
                            switch (choose)
                            {
                                case 1:
                                    Console.WriteLine("id "); int findid = int.Parse(Console.ReadLine());
                                    operations.FindBook(findid);
                                    break;
                                case 2:
                                    operations.showBooks();
                                    break;
                            }

                        }

                    }
                }
            }
           
        }
    }
    class dataBase
    {
    public static  List<book> bookList = new List<book>();
    public static List<staffs> staffList = new List<staffs>();        
    }
    class operations
    {
        public static void showBooks()
        {
            dataBase.bookList.Sort((book1, book2) => book1.ISBN.CompareTo(book2.ISBN));
            foreach (book item in dataBase.bookList)
            {
                Console.WriteLine($"{item.ISBN}:{item.Title}:by {item.Author} : {item.Available}");
            }
        }
        public static void FindBook(int id)
        {
            book foundBook = dataBase.bookList.Find(book1 => book1.ISBN == id);
            if (foundBook is null)
            {
                Console.WriteLine("NOT FOUND");
               
            }
            else
            {
                foundBook.details();
            }
        }
        public static void AddBook(int id , string BTitle , string authorName,bool available )
        {
            if (dataBase.bookList.Find((book) => book.ISBN == id) == null)
            {
                dataBase.bookList.Add(new book(id, BTitle, authorName, available));
            }
            else
            {
                Console.WriteLine("already exists");
            }
        }
        public static void RemoveBook(int id)
        {
            int removed=dataBase.bookList.RemoveAll(book1 => book1.ISBN == id);
            if (removed>0)
            {
                Console.WriteLine("* Book Removed *");
            }
            else
            {
                Console.WriteLine("NOT FOUND");
            }
        }

    }
public abstract class StaffInfo
    {
        public int id  { get; set; }
        public string name { get; set; }
        public string level { get; set; }   // 1.adminstrator  2.normal staff 
        public string pass { get; set; }
        public virtual void staffDetails()
        {
            Console.WriteLine($"{id} : {name} // {level}");
        }
    }
public class staffs:StaffInfo
    {
        public staffs(int id ,string name ,string level,string pass)
        {
            base.id = id;
            base.name = name;
            base.level = level;
            base.pass = pass;
        }
    }
    public abstract class bookOrigin
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool Available { get; set; }

        public virtual void details()
        {
            Console.WriteLine($"{ISBN} : {Title} by : {Author} : {Available}");
        }
    }

    public class book : bookOrigin
    {
        public book(int isbn, string title, string author, bool available)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Available = available;
        }
    }

}
