// See https://aka.ms/new-console-template for more information

//primeira validação [\d.]*

//(\d{1,3})([.])(\d{3})([.]?\d{3})*\b

// não passa (\d{1,3})([.])(\d{1,3})([.]\d{1,2})*\b

//(\d*)([.])(\d*)([.]\d*)*\b

using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] testCases = {
            "Lei 14.113",
            "Lei 100.100",
            "Lei 1.333",
            "Lei 13.122",
            "Lei 13 122",
            "Lei 13.122.122",
            "Lei 100100",
            "Lei 14113",
            "Lei 13.122.22",
            "Lei 13.122.2",
            "Lei 13.122.222"
        };


        foreach (var testCase in testCases)
        {
            string result = ProcessNumber(testCase);
            Console.WriteLine($"Original: {testCase}, Processed: {result}");
        }
    }

    static string ProcessNumber(string input)
    {

        // Define regex pattern
        string pattern = @"(\d*)([.])(\d*)([.]\d*)*\b";

        var isMatch = Regex.Match(input, pattern);
        
        if(isMatch.Success){
            string test = isMatch.Groups[0].Value;
            string removedDot = input.Replace(".", "");

            string inputDecimalTransformed = DecimalSeparator(removedDot);
            bool inputEqualsDot = inputDecimalTransformed.Equals(input);

            // it input not valid continue search
            if(!inputEqualsDot) return input;

           return removedDot;

        }

        string IdentifyNumbers = @"\b([5-9]|\d{4,})\b";
        var haveMoreAtFourDigits = Regex.Match(input, IdentifyNumbers);

        if(haveMoreAtFourDigits.Success) {
        string inputDecimalTransformed = DecimalSeparator(input);

        return inputDecimalTransformed;
        }


    return input;
    }


    static string DecimalSeparator(string input){
        var formatoCorreto = Regex.Replace(input, @"(?<=\d)(?=(\d{3})+(?!\d))", ".");

        return formatoCorreto;

    }
}