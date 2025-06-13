namespace MissingNumberApp
{
    public interface IMissingNumberFinder
    {
        int FindMissingNumber(int[] nums);
    }

    public class SummationMissingNumberFinder : IMissingNumberFinder
    {
        public int FindMissingNumber(int[] nums)
        {
            if (nums == null)
            {
                throw new ArgumentNullException(nameof(nums), "Input array cannot be null.");
            }

            // get the length of the array and work out the total
            int n = nums.Length;
            int expectedSum = 0;
            for (int i = 0; i <= n; i++)
            {
                expectedSum += i;
            }

            Console.WriteLine($"Expected sum for numbers 0 to {n} is: {expectedSum}");

            // add the sum of the numbers in the array.
            long actualSum = 0;
            foreach (int num in nums)
            {
                actualSum += num;
            }

            Console.WriteLine($"Actual sum of provided numbers is: {actualSum}");

            return (int)(expectedSum - actualSum);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Provide an array of integers in the range 1 to n");
                Console.WriteLine("Example: MissingNumber.exe [3,0,1]");
                Console.WriteLine("Example: MissingNumber.exe [9,6,4,2,3,5,7,0,1]");
                Console.WriteLine("Be sure to use quotes around your parameter if they have spaces \"[9, 6, 4, 2, 3, 5, 7, 0, 1]\"");
                return;
            }

            // --- Step 2: Parse the Input String into an Array of Numbers ---
            int[] numbers;
            // The user's input string is the first argument (e.g., "3,0,1").
            string inputString = args[0];

            //trim off any square brackets if they were used in the input
            inputString = inputString.Trim('[', ']');

            try
            {

                numbers = inputString
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s =>
                    {
                        if (int.TryParse(s.Trim(), out int num))
                        {
                            return num; // If successful, use the number.
                        }
                        else
                        {
                            // If it's not a valid number (like "hello"), throw an error.
                            throw new FormatException($"Not a number: '{s.Trim()}'. Make sure your parameters are all numbers!");
                        }
                    })
                    .ToArray();
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error parsing input: {ex.Message}");
                return;
            }
            // find the missing number using one of the implementations of IMissingNumberFinder
            IMissingNumberFinder finder = new SummationMissingNumberFinder();
            int missingNumber = finder.FindMissingNumber(numbers);
            Console.WriteLine($"Input: [{string.Join(", ", numbers)}]");
            Console.WriteLine($"Output: {missingNumber}");
        }
    }
}