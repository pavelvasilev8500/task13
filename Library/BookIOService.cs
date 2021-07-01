using System;
using Newtonsoft.Json;
using System.Xml;
using System.IO;

namespace Library
{
    class BookIOService
    {
        private string PATH;
        private Book book;
        private StreamWriter swrite;
        private StreamReader sreader;
        private JsonSerializer serializer = new JsonSerializer();

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
            if (!fileExists)
            {
                Console.WriteLine("No file for input data.");
            }
            else
            {
                using (sreader = new StreamReader(PATH))
                using (JsonReader reader = new JsonTextReader(sreader))
                {
                    try
                    {
                        object book = serializer.Deserialize(reader);
                        Console.WriteLine(book);

                    }
                    catch 
                    {

                        Console.WriteLine("No data for output.");
                    }
                }
            }
        }

        public void SaveBookasjson(Book book)
        {
            using (swrite = new StreamWriter(PATH))
            using (JsonWriter writer = new JsonTextWriter(swrite))
            {
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.Serialize(writer, book);
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
                using (XmlReader reader =  XmlReader.Create(PATH, new XmlReaderSettings { IgnoreWhitespace = true }))
                {
                    reader.MoveToContent();
                    try
                    {
                        reader.ReadStartElement("Books");
                        Console.WriteLine($"Name: {reader.GetAttribute("Name")}\n" +
                                            $"Author: {reader.GetAttribute("Author")}\n" +
                                            $"Year: {reader.GetAttribute("Year")}\n");
                    }
                    catch 
                    {

                        Console.WriteLine("No data for output.");
                    }
                }
            }
        }

        public void SaveBookasxml(Book book)
        {
            using (XmlWriter writer = XmlWriter.Create(
                PATH,
                new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Books");
                writer.WriteStartElement("Book");
                writer.WriteAttributeString("Name", $"{book.Name}");
                writer.WriteAttributeString("Author", $"{book.Author}");
                writer.WriteAttributeString("Year", $"{book.Year}");
                writer.WriteEndElement();
                writer.Flush();
            }
            Console.WriteLine("Save Success.");
        }

        #endregion

    }
}
