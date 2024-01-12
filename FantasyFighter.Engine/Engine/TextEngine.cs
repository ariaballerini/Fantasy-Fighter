namespace FantasyFighter.Engine.Engine
{
    internal static class TextEngine
    {
        internal static void DisplayTextFromFile(string fileName)
        {
            // Specifing the path to the folder containing the text files
            var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "txt");
            var fullName = Path.Combine(basePath, $"{fileName}.txt");

            if (File.Exists(fullName))
            {
                var readText = File.ReadAllText(fullName);
                Console.WriteLine(readText);
            }
            else
            {
                Console.WriteLine($"File '{fullName}' doesn't exist");
            }


            //var fullName = $"{fileName}.txt";

            //Console.WriteLine(Directory.GetCurrentDirectory());

            //if (File.Exists(fullName))
            //{
            //    var readText = File.ReadAllText(fullName);
            //    Console.WriteLine(readText);
            //}
            //
            //else { Console.WriteLine("File doesn't exist"); }
        }
    }
}
