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
        
            dataBase.staffList.Add(new staffs ( 1,"mohamed","admin","1234"));
            dataBase.staffList.Add(new staffs(2, "ayman", "worker","1111"));
            operations.AddBook(1, "The Art of War", "Chin Zu");
            operations.AddBook(2, "To Kill a Mockingbird", "Harper Lee");
            operations.AddBook(3, "1984", "George Orwell");
            operations.AddBook(4, "Pride and Prejudice", "Jane Austen");
            operations.AddBook(5, "The Lord of the Rings", "J.R.R. Tolkien");
            operations.AddBook(6, "The Catcher in the Rye", "J.D. Salinger");
            operations.AddBook(7, "The Great Gatsby", "F. Scott Fitzgerald");
            operations.AddBook(8, "Moby-Dick", "Herman Melville");
            operations.AddBook(9, "War and Peace", "Leo Tolstoy");
            operations.AddBook(10, "The Picture of Dorian Gray", "Oscar Wilde");
            operations.addMember(1, "Yasser");
            operations.addMember(2, "Amal");
            operations.addMember(3, "Mohammed");
            operations.addMember(4, "Sara");
            operations.addMember(5, "John");
            operations.addMember(6, "Emily");
            operations.addMember(7, "Michael");
            operations.addMember(8, "Lily");
            operations.addMember(9, "David");
            operations.addMember(10, "Olivia");
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
                mainMenu:while (true)
                        {

                            Console.WriteLine("choose 1.add book 2.delete book 3.find book 4.end 5.show all books \n 6.add member 7.member unsubscribe 8.borrow 9.all loans ");
                            int choose = 0;
                            choose = int.Parse(Console.ReadLine());
                            if (choose == 4)
                            {
                            break;
                            }
                            switch (choose)
                            {
                                case 1:
                                operations.AddBook();
                                    break;
                                case 2:
                                operations.RemoveBook();
                                    break;
                                case 3:
                                operations.FindBook();
                                    break;
                                case 5:
                                operations.showBooks();
                                    break;
                                case 6:
                                operations.addMember();
                                    break;
                                case 7:
                                operations.removeMember();
                                    break;
                                case 8:
                                    operations.borrowBook();
                                    break;
                                case 9:
                                    operations.displayLoans();
                                    break;
                            }

                        }
                    }   
            }
        }
    }
    class dataBase
    {
    public static List<book>    bookList    = new List<book>();
    public static List<staffs>  staffList   = new List<staffs>();     
    public static List<loans>   loansList   = new List<loans>();
    public static List<Members> membersList = new List<Members>();
    }

    class operations
    {
        public static void displayLoans()
        {
            foreach (loans loan in dataBase.loansList)
            {
                Console.WriteLine($"{loan.member} borrowed {loan.loanedBook} at {loan.date} to {loan.endDate}");
            }
        }
        public static void addMember(int id, string name)
        {
            if (dataBase.membersList.Find((memb) => memb.id == id) == null)
            {
                dataBase.membersList.Add(new Members(id, name));
            }
            else
            {
                Console.WriteLine("already exists");
            }
        }
        public static void addMember()
        {
            Console.WriteLine("member id , name ");
            int id = int.Parse(Console.ReadLine());
            string Name = Console.ReadLine();
            operations.addMember(id, Name);
        }

        public static void removeMember(int id)
        {
            int removed = dataBase.membersList.RemoveAll(memb => memb.id == id);
            if (removed > 0)
            {
                Console.WriteLine("* member unsubscribed *");
            }
            else
            {
                Console.WriteLine("NOT FOUND");
            }
        }
        public static void removeMember()
        {
            Console.WriteLine("member id  ");
            int removeMembId = int.Parse(Console.ReadLine());
            operations.removeMember(removeMembId);
        }
        public static void borrowBook(int idMember, int idBook, DateOnly endDate)
        {
            Members member = dataBase.membersList.Find((memb) => memb.id == idMember);
            book book = dataBase.bookList.Find((book) => book.ISBN == idBook);
            dataBase.loansList.Add(new loans(book.Title, member.name, endDate));
        }
        public static void borrowBook()
        {
            Console.WriteLine("book  id , member id,retrun date  ");
            int loanMembId = int.Parse(Console.ReadLine());
            int loanBooK = int.Parse(Console.ReadLine());
            DateOnly endDate = DateOnly.Parse(Console.ReadLine());
            operations.borrowBook(loanMembId, loanBooK, endDate);
        }
        public static void showBooks()
        {
            dataBase.bookList.Sort((book1, book2) => book1.ISBN.CompareTo(book2.ISBN));
            foreach (book item in dataBase.bookList)
            {
                Console.WriteLine($"{item.ISBN}:{item.Title}:by {item.Author}");
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
        public static void FindBook ()
        {
            Console.WriteLine("id :");
            int id = int.Parse(Console.ReadLine());
            operations.FindBook(id);
        }
        public static void AddBook(int id, string BTitle, string authorName)
        {
            if (dataBase.bookList.Find((book) => book.ISBN == id) == null)
            {
                dataBase.bookList.Add(new book(id, BTitle, authorName));
            }
            else
            {
                Console.WriteLine("already exists");
            }
        }
        public static void AddBook()
        {
            Console.WriteLine("enter id , title ,author , availability");
            int id = int.Parse(Console.ReadLine());
            string title = Console.ReadLine();
            string author = Console.ReadLine();
           AddBook(id, title, author);
        }
        public static void RemoveBook(int id)
        {
            int removed = dataBase.bookList.RemoveAll(book1 => book1.ISBN == id);
            if (removed > 0)
            {
                Console.WriteLine("* Book Removed *");
            }
            else
            {
                Console.WriteLine("NOT FOUND");
            }
        }
        public static void RemoveBook()
        {
            Console.WriteLine("id ");
            int id = int.Parse(Console.ReadLine());
            RemoveBook(id);
        }

    }
    public  class Members
    {
        public int id { get; set; }
        public string name { get; set; }

        public List<book> loansList;
        public Members(int id, string name)
        {
       this.id = id;
       this.name = name;
        }

    }
  
    public class loans
    {
        public string loanedBook { get; set; }
        public string member { get; set; }
        public string date = DateTime.Now.ToString();
        public DateOnly endDate { get; set; }
        public loans (string bookTitle , string membName,DateOnly endDate)
        {
            loanedBook = bookTitle;
            member = membName;
            this.endDate = endDate;
        }
        public  void loanDetails()
        {
            Console.WriteLine($"{loanedBook}  borrowed by  {member} at {date}  should be returned at {endDate}");
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
            Console.WriteLine($"{ISBN} : {Title} by : {Author} ");
        }
    }

    public class book : bookOrigin
    {
        public book(int isbn, string title, string author)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            //Available = available;
        }
    }

}
