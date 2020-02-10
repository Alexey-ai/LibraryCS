using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryCS.Models;

namespace LibraryCS.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();
            {
                if (context.Books.Any() || context.Readers.Any() || context.Orders.Any())
                {
                    return;   // DB has been seeded
                 }

                var Readers = new List<Reader>
            {
            new Reader{ReaderName="Alex",ReaderLastName="Ivanov", Age=30, Adress="Kaluga Jukov st.", Phone="8-900-555-2020",Passport="2809-546237", AddDate=DateTime.Parse("2019-10-05")},
            new Reader{ReaderName="Olga",ReaderLastName="Petrova", Age=22, Adress="Moscow Neglinnaya st.", Phone="8-900-555-1000",Passport="2809-343337", AddDate=DateTime.Parse("2019-11-05")},
            new Reader{ReaderName="Fu",ReaderLastName="Kim", Age=40, Adress="New York Broadway st.", Phone="8-900-555-6767",Passport="2809-234322", AddDate=DateTime.Parse("2019-12-05")},
            new Reader{ReaderName="Vasil",ReaderLastName="Johnson", Age=15, Adress="Kaluga Pukhova st.", Phone="8-900-555-2666",Passport="2809-34333", AddDate=DateTime.Parse("2020-01-08")}
            };

                Readers.ForEach(s => context.Readers.Add(s));
                context.SaveChanges();

                var Books = new List<Book>
            {
            new Book{BookID=1,NameBook="Sobranie sochineniy",Author="Lenin V.I.",Description="Full set of works Lenin",Edition="RED ARMY",Genre="Political",Aviability=true,BookAddDate=DateTime.Parse("2019-12-12")},
            new Book{BookID=2,NameBook="Number the Stars",Author="Lois Lowry",Description="Book about second world war",Edition="RED ARMY",Genre="Drama",Aviability=true,BookAddDate=DateTime.Parse("2019-12-12")},
            new Book{BookID=3,NameBook="Winnie-the-Pooh",Author="A. A. Milne",Description="Fairy tale about bear",Edition="RED ARMY",Genre="Kids book",Aviability=true,BookAddDate=DateTime.Parse("2019-12-12")},
            new Book{BookID=4,NameBook="Northern Lights",Author="Philip Pullman",Description="Fantasy world",Edition="RED ARMY",Genre="Fantasy",Aviability=true,BookAddDate=DateTime.Parse("2019-12-12")},
            new Book{BookID=5,NameBook="Prestuplenie i nakazanie",Author="F.M.Dostoevsky",Description="Roman",Edition="RED ARMY",Genre="Drama",Aviability=true,BookAddDate=DateTime.Parse("2019-12-12")}
            };
                Books.ForEach(s => context.Books.Add(s));
                context.SaveChanges();

                var TakeBooks = new List<Order>
            {
            new Order{BookID=2, ReaderID=1,  OrderDate=DateTime.Parse("2019-12-12"),OrderReturnDate=DateTime.Parse("2019-12-15")},
            new Order{BookID=3, ReaderID=2,  OrderDate=DateTime.Parse("2019-12-17")},
            new Order{BookID=4, ReaderID=3,  OrderDate=DateTime.Parse("2019-12-19")},


            };
                TakeBooks.ForEach(s => context.Orders.Add(s));
                context.SaveChanges();
            }
        }
    }
}
