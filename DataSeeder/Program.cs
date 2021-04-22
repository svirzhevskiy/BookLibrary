using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.Database;
using BookLibrary.Database.Entities;

namespace DataSeeder
{
    class Program
    {
        static void Main(string[] args)
        {
            string file;
            do
            {
                Console.WriteLine("Enter path to data file:");
                file = Console.ReadLine();
            } while (!File.Exists(file));

            List<BookData> data;
            try
            {
                data = ReadCsv(file);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Problem with data in file.\n{e.Message}\n{e.StackTrace}");
                throw;
            }

            using (var context = new AppDbContext())
            {
                HashSet<Author> authors = new();
                HashSet<Publisher> publishers = new();

                var current = 0;
                
                foreach (var bookData in data)
                {
                    Guid authorId;
                    Guid publisherId;
                    
                    if (authors.All(x => x.Name != bookData.Author))
                    {
                        var author = new Author
                        {
                            Id = Guid.NewGuid(),
                            Name = bookData.Author
                        };

                        authorId = author.Id;

                        authors.Add(author);
                        context.Authors.Add(author);
                    }
                    else
                    {
                        authorId = authors.First(x => x.Name == bookData.Author).Id;
                    }

                    if (publishers.All(x => x.Title != bookData.Publisher))
                    {
                        var publisher = new Publisher
                        {
                            Id = Guid.NewGuid(),
                            Title = bookData.Publisher
                        };

                        publisherId = publisher.Id;

                        publishers.Add(publisher);
                        context.Publishers.Add(publisher);
                    }
                    else
                    {
                        publisherId = publishers.First(x => x.Title == bookData.Publisher).Id;
                    }

                    context.Books.Add(new Book
                    {
                        Id = Guid.NewGuid(),
                        AuthorId = authorId,
                        PublisherId = publisherId,
                        Title = bookData.Title,
                        Year = bookData.Year,
                        ISBN = bookData.ISBN,
                        Blurb = bookData.Blurb
                    });

                    current++;
                    Console.WriteLine($"Progress: {(double)current / data.Count * 100}%. Book {bookData.Title} added.");
                }

                context.SaveChanges();
            }
            
            Console.ReadLine();
        }

        private static List<BookData> ReadCsv(string file)
        {
            var result = new List<BookData>();

            using var reader = new StreamReader(file);
            reader.ReadLine();
            
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                var data = line.Split(',', 6);
                
                if (data.Length != 6) continue;
                
                ushort.TryParse(data[3], out var year);
                result.Add(new BookData
                {
                    ISBN = data[0],
                    Title = data[1],
                    Author = data[2],
                    Year = year,
                    Publisher = data[4],
                    Blurb = data[5]
                });
            }

            return result;
        }

        private record BookData
        {
            public string ISBN { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Publisher { get; set; }
            public ushort Year { get; set; }
            public string Blurb { get; set; }
        }
    }
}