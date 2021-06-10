using System;
using System.Text.Json;
using System.Xml.Serialization;
using System.IO;

namespace Library
{
    class BookIOService
    {
        private string PATH;
        private Book book;

        public BookIOService(string path)
        {
            PATH = path;
        }

        public Book CreateBook()
        {
            Console.WriteLine("Input name of book:");
            string name = Console.ReadLine();
            Console.WriteLine("Input Author:");
            string author = Console.ReadLine();
            Console.WriteLine("Input Year:");
            string input = Console.ReadLine();
            bool success = int.TryParse(input, out int year);
            while (success != true)
            {
                Console.WriteLine("Input Year:");
                input = Console.ReadLine();
                success = int.TryParse(input, out year);
            }
            if (success == true)
                year = int.Parse(input);
            book = new Book
            {
                Name = name,
                Author = author,
                Year = year
            };
            return book;
        }

        #region json
        public void LoadBookfromjson()
        {
            var fileExists = File.Exists(PATH);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            if(!fileExists)
            {
                Console.WriteLine("No file for input data.");
            }
            else
            {
                using (FileStream reader = new FileStream(PATH, FileMode.Open))
                {
                    byte[] restoredBook = new byte[reader.Length];
                    reader.Read(restoredBook, 0, restoredBook.Length);
                    var utf8Reader = new Utf8JsonReader(restoredBook);
                    Book deserializeBook =
                        JsonSerializer.Deserialize<Book>(ref utf8Reader, options);
                    if (deserializeBook == null)
                        Console.WriteLine("No data for output.");
                    else
                        Console.WriteLine($"Name: {deserializeBook.Name}\n" +
                                          $"Author: {deserializeBook.Author}\n" +
                                          $"Year: {deserializeBook.Year}\n");

                }
            }
        }

        public void SaveBookasjson(Book book)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            using (FileStream writer = new FileStream(PATH, FileMode.Create))
            {
                byte[] json = JsonSerializer.SerializeToUtf8Bytes<Book>(book, options);
                writer.Write(json);
            }
            Console.WriteLine("Save Success.");
        }

        #endregion

        #region xml
        public void LoadBookfromxml()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                Console.WriteLine("No file for input data.");
            }
            else
            {
                using (FileStream reader = new FileStream(PATH, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Book));
                    Book deserializeBook = (Book)xmlSerializer.Deserialize(reader);
                    if (deserializeBook == null)
                        Console.WriteLine("No data for output.");
                    else
                        Console.WriteLine($"Name: {deserializeBook.Name}\n" +
                                          $"Author: {deserializeBook.Author}\n" +
                                          $"Year: {deserializeBook.Year}\n");

                }
            }
        }

        public void SaveBookasxml(Book book)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Book));
            using(FileStream writer = new FileStream(PATH, FileMode.Create))
            {
                xmlSerializer.Serialize(writer, book);
                Console.WriteLine("Save Success.");
            }
        }

        #endregion

    }
}
