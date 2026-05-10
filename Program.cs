using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace HRDataProcessing;

internal class Program
{
    private const int MaxEmployeesToDisplay = 10;

    private static int Main(string[] args)
    {
        var filePath = args.Length > 0 ? args[0] : "data.csv";

        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: CSV file not found: {filePath}");
            Console.Error.WriteLine("Usage: dotnet run -- [path-to-csv]");
            return 1;
        }

        var employees = LoadEmployees(filePath);
        PrintResults(employees, filePath);
        PrintExportedFiles(CsvExporter.ExportTableauFiles(employees, "output"));

        return 0;
    }

    private static List<Employee> LoadEmployees(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

        return csv.GetRecords<Employee>().ToList();
    }

    private static void PrintResults(IReadOnlyCollection<Employee> employees, string filePath)
    {
        Console.WriteLine("HR Data Processing");
        Console.WriteLine("==================");
        Console.WriteLine($"File: {filePath}");
        Console.WriteLine($"Employees loaded: {employees.Count}");

        Console.WriteLine();
        Console.WriteLine($"Lowest training costs (first {Math.Min(MaxEmployeesToDisplay, employees.Count)} records)");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"{"Employee ID",-12} {"Title",-32} {"Cost",10} {"Training Date",-14}");

        foreach (var employee in HrDataAnalyzer.GetEmployeesSortedByTrainingCost(employees).Take(MaxEmployeesToDisplay))
        {
            Console.WriteLine(
                $"{employee.EmployeeID,-12} {TrimForDisplay(employee.Title, 32),-32} {employee.TrainingCost,10:C2} {employee.TrainingDate:dd-MMM-yyyy}");
        }

        Console.WriteLine();
        Console.WriteLine("Training cost by year");
        Console.WriteLine("---------------------");

        foreach (var yearSummary in HrDataAnalyzer.GetTotalTrainingCostByYear(employees))
        {
            Console.WriteLine($"{yearSummary.Year}: {yearSummary.TotalTrainingCost:C2}");
        }

        var mostExpensiveYear = HrDataAnalyzer.GetMostExpensiveTrainingYear(employees);
        if (mostExpensiveYear is not null)
        {
            Console.WriteLine();
            Console.WriteLine("Most expensive training year");
            Console.WriteLine("----------------------------");
            Console.WriteLine($"{mostExpensiveYear.Year}: {mostExpensiveYear.TotalTrainingCost:C2}");
        }
    }

    private static string TrimForDisplay(string value, int maxLength)
    {
        if (value.Length <= maxLength)
        {
            return value;
        }

        return value[..(maxLength - 3)] + "...";
    }

    private static void PrintExportedFiles(IEnumerable<string> exportedFiles)
    {
        Console.WriteLine();
        Console.WriteLine("Generated Tableau-ready CSV files:");

        foreach (var filePath in exportedFiles)
        {
            Console.WriteLine($"- {filePath}");
        }
    }
}
