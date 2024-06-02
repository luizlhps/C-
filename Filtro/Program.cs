// See https://aka.ms/new-console-template for more information

//primeira validação [\d.]*

//(\d{1,3})([.])(\d{3})([.]?\d{3})*\b

// não passa (\d{1,3})([.])(\d{1,3})([.]\d{1,2})*\b

//(\d*)([.])(\d*)([.]\d*)*\b


//(?<![^\s"º°])(\d{1,2}\.\d{3})(?:\/(?:\d{2}|\d{4}))?\b(?![^\s""])

using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] testCases = {
            "Lei Lei 14123/2000 14.113/2000,",
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
            "Lei 13.122.222",
            "Lei 10000/60",
            "nº13.21/22",
            "nº13.221/22"
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
        string patternWithDot = @"(?<![^\s""º°])(\d{1,2}\.\d{3})(?:\/(?:\d{2}|\d{4}))?\b(?![^\s"",])";
        string patternWithoutDot = @"(?<![^\s""º°])(\d{4}|\d{5})(?:\/(?:\d{2}|\d{4}))?\b(?![^\s"",])";

        var matchWithDot = Regex.Match(input, patternWithDot);
        var matchWithoutDot = Regex.Match(input, patternWithoutDot);

        return DetermineProcessingOrder(matchWithoutDot, matchWithDot, input);

    }

    static string DetermineProcessingOrder(Match matchWithoutDot, Match matchWithDot, string input)
    {
        bool hasMatchWithoutDot = matchWithoutDot.Success;
        bool hasMatchWithDot = matchWithDot.Success;

        if (hasMatchWithoutDot && hasMatchWithDot)
        {
            var firstMatchWithDot = matchWithDot.Groups[0].Index;
            var firstMatchWithoutDot = matchWithoutDot.Groups[0].Index;


            if (firstMatchWithoutDot > firstMatchWithDot) return FormatLawWithoutSeparator(matchWithDot, input);
            return FormatLawWithSeparator(matchWithoutDot, input);
        }
        else if (!hasMatchWithoutDot && hasMatchWithDot)
        {
            return FormatLawWithoutSeparator(matchWithDot, input);

        }
        else if (hasMatchWithoutDot && !hasMatchWithDot)
        {
            return FormatLawWithSeparator(matchWithoutDot, input);
        }

        return input;
    }

    static string FormatLawWithoutSeparator(Match matchLawWithDot, string input)
    {
        string lawWithDot = matchLawWithDot.Groups[1].Value;
        string removedDot = lawWithDot.Replace(".", "");

        return input.Replace(lawWithDot, removedDot);
    }
    static string FormatLawWithSeparator(Match matchLawWithoutDot, string input)
    {

        string lawWithoutDot = matchLawWithoutDot.Groups[1].Value;
        string inputDecimalTransformed = AddDecimalSeparator(lawWithoutDot);
        return input.Replace(lawWithoutDot, inputDecimalTransformed);


    }
    static string AddDecimalSeparator(string input)
    {
        var formatoCorreto = Regex.Replace(input, @"(?<=\d)(?=(\d{3})+(?!\d))", ".");
        return formatoCorreto;

    }
}