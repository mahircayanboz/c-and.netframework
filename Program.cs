using System;

// Main program class that serves as the entry point
class Program
{
    static void Main(string[] args)
    {
        // Start the shipping quote process
        ShippingQuoteProcessor.ProcessQuote();
    }
}

// Static class handling the shipping quote processing using functional approach
static class ShippingQuoteProcessor
{
    // Constants for validation
    private const double MaxWeight = 50;
    private const double MaxDimensions = 50;

    // Main processing method that orchestrates the quote calculation flow
    public static void ProcessQuote()
    {
        Console.WriteLine("Welcome to Package Express. Please follow the instructions below.");

        // Get and validate weight
        var weight = GetValidatedWeight();
        if (!weight.HasValue) return;

        // Get and validate dimensions
        var dimensions = GetValidatedDimensions();
        if (!dimensions.HasValue) return;

        // Calculate and display quote
        DisplayQuote(CalculateQuote(weight.Value, dimensions.Value));
    }

    // Get and validate package weight
    private static double? GetValidatedWeight()
    {
        Console.WriteLine("Please enter the package weight:");
        if (!TryGetNumericInput(out double weight))
            return null;

        if (weight > MaxWeight)
        {
            Console.WriteLine("Package too heavy to be shipped via Package Express. Have a good day.");
            return null;
        }

        return weight;
    }

    // Get and validate package dimensions
    private static (double width, double height, double length)? GetValidatedDimensions()
    {
        // Get width
        Console.WriteLine("Please enter the package width:");
        if (!TryGetNumericInput(out double width))
            return null;

        // Get height
        Console.WriteLine("Please enter the package height:");
        if (!TryGetNumericInput(out double height))
            return null;

        // Get length
        Console.WriteLine("Please enter the package length:");
        if (!TryGetNumericInput(out double length))
            return null;

        // Validate total dimensions
        if (width + height + length > MaxDimensions)
        {
            Console.WriteLine("Package too big to be shipped via Package Express.");
            return null;
        }

        return (width, height, length);
    }

    // Helper method to get numeric input from user
    private static bool TryGetNumericInput(out double result)
    {
        result = 0;
        try
        {
            result = Convert.ToDouble(Console.ReadLine());
            return true;
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return false;
        }
    }

    // Pure function to calculate shipping quote
    private static double CalculateQuote(double weight, (double width, double height, double length) dimensions)
    {
        return (dimensions.width * dimensions.height * dimensions.length * weight) / 100;
    }

    // Display the calculated quote
    private static void DisplayQuote(double quote)
    {
        Console.WriteLine($"Your estimated total for shipping this package is: ${quote:F2}");
        Console.WriteLine("Thank you!");
    }
}