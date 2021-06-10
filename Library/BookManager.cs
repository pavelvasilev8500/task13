using System;

namespace Library
{
    class BookManager
    {
        private Book book;
        private BookIOService bookIOService;
        private readonly string PATH = $"{Environment.CurrentDirectory}";
        private readonly string JPATH = $"{Environment.CurrentDirectory}\\book.json";
        private readonly string XPATH = $"{Environment.CurrentDirectory}\\book.xml";

        public void Manager()
        {
            Console.Write("Menu:\n1 - Create info about book;" +
                                        "\n2 - Save info about book as json;" +
                                        "\n3 - Load info about book from json;" +
                                        "\n4 - Save info about book as xml;" +
                                        "\n5 - Load info about book from xml;" +
                                        "\nq - exit." +
                                        "\n");
            while (true)
            {
                const string Quit = "q";
                Console.WriteLine("Choose action:");
                string input = Console.ReadLine();
                if (input.Trim() == Quit)
                    break;
                bool success = int.TryParse(input, out int switcher);
                while (success != true)
                {
                    input = Console.ReadLine();
                    success = int.TryParse(input, out switcher);
                }
                switch (switcher)
                {
                    case 1:
                        bookIOService = new BookIOService(PATH);
                        book = bookIOService.CreateBook();
                        break;
                    case 2:
                        try
                        {
                            if (book == null)
                            {
                                Console.WriteLine("No data for save.");
                            }
                            else
                            {
                                bookIOService = new BookIOService(JPATH);
                                bookIOService.SaveBookasjson(book);
                            }
                        }
                        catch (Exception e)
                        {

                            Console.Write(e);
                        }
                        break;
                    case 3:
                        try
                        {
                            bookIOService = new BookIOService(JPATH);
                            bookIOService.LoadBookfromjson();
                        }
                        catch (Exception e)
                        {

                            Console.Write(e);
                        }
                        break;
                    case 4:
                        try
                        {
                            if (book == null)
                            {
                                Console.WriteLine("No data for save.");
                            }
                            else
                            {
                                bookIOService = new BookIOService(XPATH);
                                bookIOService.SaveBookasxml(book);
                            }
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e);
                        }
                        break;
                    case 5:
                        try
                        {
                            bookIOService = new BookIOService(XPATH);
                            bookIOService.LoadBookfromxml();
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
