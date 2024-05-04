namespace CSharpNutshell
{
    using System.Reflection;
    using System.Text;
    using NumArray = int[];
    internal class Program
    {
        static void Main(string[] args)
        {
            Arrays();
            Strings();
            Files().GetAwaiter().GetResult();
            Conversions();
            Modulus();
            Bits();
            Reflection();
        }

        static void Arrays()
        {
            Console.WriteLine("Arrays:");

            // New vs. old style array declarations
            int[] oldstyle = { 1, 2, 3 };
            int[] newstyle = [1, 2, 3];
            NumArray numarray = [1, 2, 3];

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"oldstyle[{i}]: {oldstyle[i]}, newstyle[{i}]: {newstyle[i]}, numarray[{i}]: {numarray[i]}");
            }

            // Rectangular arrays
            int[,] rectangularMatrix = new int[3,4];

            for (int i = 0; i < rectangularMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < rectangularMatrix.GetLength(1); j++)
                {
                    rectangularMatrix[i,j] = i * 3 + j;
                }
            }

            Console.WriteLine("Rectangular Matrix:");
            for (int i = 0; i < rectangularMatrix.GetLength(0); i++)
            {
                Console.Write("[ ");
                for (int j = 0; j < rectangularMatrix.GetLength(1); j++)
                {
                    Console.Write($"{rectangularMatrix[i, j]} ");
                }

                Console.WriteLine("]");
            }

            // Jagged arrays: can only instantiate the outer array
            int[][] jaggedMatrix = new int[3][];

            for (int i = 0; i < jaggedMatrix.GetLength(0); i++)
            {
                // Must manually declare each inner array
                jaggedMatrix[i] = new int[4];

                for (int j = 0; j < rectangularMatrix.GetLength(1); j++)
                {
                    rectangularMatrix[i, j] = i * 3 + j;
                }
            }

            Console.WriteLine("Jagged Matrix:");
            for (int i = 0; i < rectangularMatrix.GetLength(0); i++)
            {
                Console.Write("[ ");
                for (int j = 0; j < rectangularMatrix.GetLength(1); j++)
                {
                    Console.Write($"{rectangularMatrix[i, j]} ");
                }

                Console.WriteLine("]");
            }



            Console.WriteLine();
        }

        static void Strings()
        {
            Console.WriteLine("Strings:");

            // Implicit escaping
            string json1 = $$"""{ "TimeStamp": "{{DateTime.Now}}" }""";
            Console.WriteLine(json1);

            string json2 = $$"""
                {
                    "TimeStamp": "{{DateTime.Now}}"
                }
                """;
            Console.WriteLine(json2);

            // Character assignment
            string str1 = "Hello, world! Nice to meet you.";
            Console.WriteLine(str1);
            StringBuilder stringBuilder = new StringBuilder(str1);
            stringBuilder[str1.IndexOf('w')] = 'W';
            Console.WriteLine(stringBuilder);

            // Reverse words
            string[] words = str1.Split(' ');
            int startIndex = 0;
            int endIndex = words.Length - 1;

            while (startIndex < endIndex)
            {
                string temp = words[startIndex];
                words[startIndex] = words[endIndex];
                words[endIndex] = temp;

                startIndex++;
                endIndex--;
            }

            string reversed = string.Join(" ", words);

            Console.WriteLine(reversed);

            Console.WriteLine();
        }

        static async Task Files()
        {
            Console.WriteLine("Files:");

            // Using statements
            if (File.Exists(@"Data\text.txt"))
            {
                // Dispose of the File object when done
                using Task<string> readTask = File.ReadAllTextAsync(@"Data\text.txt");

                string lines = await readTask;

                Console.WriteLine($"Lines: {lines}");

                // Dispose of the StreamWriter object when done
                using (StreamWriter writer = new StreamWriter(@"Data\newtext.txt"))
                {
                    writer.WriteLine(lines);
                }
            }

            Console.WriteLine();
        }

        static void Conversions()
        {
            Console.WriteLine("Conversions:");

            // Convert string to int and int to string
            string str1 = "42";

            int x = 0;
            int.TryParse(str1, out x);

            Console.WriteLine($"string parsed to int: {x}");
            Console.WriteLine($"int converted to string: {x.ToString()}");

            Console.WriteLine();
        }

        static void Modulus()
        {
            Console.WriteLine("Modulus:");

            int num = 9104;

            Console.WriteLine($"num: {num}, num % 1000: {num % 1000}, num % 100: {num % 100}, num%10: {num % 10}");
            Console.WriteLine($"num: {num}, num / 1000: {num / 1000}, num / 100: {num / 100}, num / 10: {num / 10}");
            Console.WriteLine($"num: {num}, (num / 1000) % 10: {(num / 1000) % 10}, (num / 100) % 10: {(num / 100) % 10}, (num / 10) % 10: {(num / 10) % 10}, (num / 1) % 10: {(num / 1) % 10}");

            Console.WriteLine();
        }

        static void Bits()
        {
            Console.WriteLine("Bits:");

            int x = 255;
            int cur = x;
            int count = 0;

            while (x != 0)
            {
                cur = x & 1;

                if (cur == 1)
                {
                    count++;
                }

                x >>= 1;
            }

            Console.WriteLine($"Set bits: {count}");

            Console.WriteLine();
        }

        public static void Reflection()
        {
            Console.WriteLine("Reflection:");

            // Create an instance of MyClass
            var myObject = new MyClass();

            // Get the Type of the object
            Type type = myObject.GetType();

            // Get the MethodInfo for the private method
            MethodInfo? methodInfo = type.GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);

            if (methodInfo != null)
            {
                // Invoke the private method with parameter array
                string? result = (string?)methodInfo.Invoke(myObject, new object[] { "Hello, World!" });

                // Output the result
                Console.WriteLine(result);  // Output: Private: Hello
            }

            Console.WriteLine();
        }

        public class MyClass
        {
            private string PrivateMethod(string input)
            {
                Console.WriteLine("In private method 'PrivateMethod'");
                return "Private: " + input;
            }
        }
    }
}
