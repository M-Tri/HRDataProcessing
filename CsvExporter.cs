using CsvHelper;
using System.Globalization;

namespace HRDataProcessing;

public static class CsvExporter
{
    public static IReadOnlyList<string> ExportTableauFiles(IEnumerable<Employee> employees, string outputFolder)
    {
        Directory.CreateDirectory(outputFolder);

        var employeeList = employees.ToList();
        var exportedFiles = new List<string>
        {
            ExportTrainingCostByYear(employeeList, outputFolder),
            ExportTrainingCostByBusinessUnit(employeeList, outputFolder),
            ExportTopTrainingCostEmployees(employeeList, outputFolder),
            ExportTrainingProgramSummary(employeeList, outputFolder)
        };

        return exportedFiles;
    }

    public static string ExportTrainingCostByYear(IEnumerable<Employee> employees, string outputFolder)
    {
        var filePath = Path.Combine(outputFolder, "training_cost_by_year.csv");
        var records = HrDataAnalyzer.GetTotalTrainingCostByYear(employees)
            .Select(summary => new TrainingCostByYearExport(
                summary.Year,
                FormatCost(summary.TotalTrainingCost)));

        WriteRecords(filePath, records);
        return filePath;
    }

    public static string ExportTrainingCostByBusinessUnit(IEnumerable<Employee> employees, string outputFolder)
    {
        var filePath = Path.Combine(outputFolder, "training_cost_by_business_unit.csv");
        var records = HrDataAnalyzer.GetTotalTrainingCostByBusinessUnit(employees)
            .Select(summary => new TrainingCostByBusinessUnitExport(
                summary.BusinessUnit,
                FormatCost(summary.TotalTrainingCost)));

        WriteRecords(filePath, records);
        return filePath;
    }

    public static string ExportTopTrainingCostEmployees(IEnumerable<Employee> employees, string outputFolder)
    {
        var filePath = Path.Combine(outputFolder, "top_training_cost_employees.csv");
        var records = HrDataAnalyzer.GetTopEmployeesByTrainingCost(employees)
            .Select(employee => new TopTrainingCostEmployeeExport(
                employee.EmployeeID,
                employee.Title,
                employee.BusinessUnit,
                employee.TrainingProgramName,
                employee.TrainingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                FormatCost(employee.TrainingCost),
                employee.CurrentEmployeeRating));

        WriteRecords(filePath, records);
        return filePath;
    }

    public static string ExportTrainingProgramSummary(IEnumerable<Employee> employees, string outputFolder)
    {
        var filePath = Path.Combine(outputFolder, "training_program_summary.csv");
        var records = HrDataAnalyzer.GetTrainingProgramSummary(employees)
            .Select(summary => new TrainingProgramSummaryExport(
                summary.TrainingProgramName,
                summary.EmployeeCount,
                FormatCost(summary.TotalTrainingCost),
                FormatCost(summary.AverageTrainingCost)));

        WriteRecords(filePath, records);
        return filePath;
    }

    private static void WriteRecords<T>(string filePath, IEnumerable<T> records)
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteRecords(records);
    }

    private static string FormatCost(double value)
    {
        return value.ToString("F2", CultureInfo.InvariantCulture);
    }
}

public sealed record TrainingCostByYearExport(int Year, string TotalTrainingCost);

public sealed record TrainingCostByBusinessUnitExport(string BusinessUnit, string TotalTrainingCost);

public sealed record TopTrainingCostEmployeeExport(
    int EmployeeId,
    string Title,
    string BusinessUnit,
    string TrainingProgramName,
    string TrainingDate,
    string TrainingCost,
    int CurrentEmployeeRating);

public sealed record TrainingProgramSummaryExport(
    string TrainingProgramName,
    int EmployeeCount,
    string TotalTrainingCost,
    string AverageTrainingCost);
