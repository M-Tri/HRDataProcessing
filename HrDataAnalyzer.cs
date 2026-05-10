namespace HRDataProcessing;

public static class HrDataAnalyzer
{
    public static IReadOnlyList<Employee> GetEmployeesSortedByTrainingCost(IEnumerable<Employee> employees)
    {
        return employees
            .OrderBy(employee => employee.TrainingCost)
            .ThenBy(employee => employee.EmployeeID)
            .ToList();
    }

    public static IReadOnlyList<Employee> GetTopEmployeesByTrainingCost(IEnumerable<Employee> employees, int count = 25)
    {
        return employees
            .OrderByDescending(employee => employee.TrainingCost)
            .ThenBy(employee => employee.EmployeeID)
            .Take(count)
            .ToList();
    }

    public static TrainingCostByYear? GetMostExpensiveTrainingYear(IEnumerable<Employee> employees)
    {
        return GetTotalTrainingCostByYear(employees).FirstOrDefault();
    }

    public static IReadOnlyList<TrainingCostByYear> GetTotalTrainingCostByYear(IEnumerable<Employee> employees)
    {
        return employees
            .GroupBy(employee => employee.TrainingDate.Year)
            .Select(group => new TrainingCostByYear(group.Key, group.Sum(employee => employee.TrainingCost)))
            .OrderByDescending(summary => summary.TotalTrainingCost)
            .ThenBy(summary => summary.Year)
            .ToList();
    }

    public static IReadOnlyList<TrainingCostByBusinessUnit> GetTotalTrainingCostByBusinessUnit(IEnumerable<Employee> employees)
    {
        return employees
            .GroupBy(employee => employee.BusinessUnit)
            .Select(group => new TrainingCostByBusinessUnit(group.Key, group.Sum(employee => employee.TrainingCost)))
            .OrderByDescending(summary => summary.TotalTrainingCost)
            .ThenBy(summary => summary.BusinessUnit)
            .ToList();
    }

    public static IReadOnlyList<TrainingProgramSummary> GetTrainingProgramSummary(IEnumerable<Employee> employees)
    {
        return employees
            .GroupBy(employee => employee.TrainingProgramName)
            .Select(group => new TrainingProgramSummary(
                group.Key,
                group.Count(),
                group.Sum(employee => employee.TrainingCost),
                group.Average(employee => employee.TrainingCost)))
            .OrderByDescending(summary => summary.TotalTrainingCost)
            .ThenBy(summary => summary.TrainingProgramName)
            .ToList();
    }
}

public sealed record TrainingCostByYear(int Year, double TotalTrainingCost);

public sealed record TrainingCostByBusinessUnit(string BusinessUnit, double TotalTrainingCost);

public sealed record TrainingProgramSummary(
    string TrainingProgramName,
    int EmployeeCount,
    double TotalTrainingCost,
    double AverageTrainingCost);
