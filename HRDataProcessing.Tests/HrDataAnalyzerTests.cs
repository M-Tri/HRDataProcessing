using HRDataProcessing;
using Xunit;

namespace HRDataProcessing.Tests;

public class HrDataAnalyzerTests
{
    [Fact]
    public void GetEmployeesSortedByTrainingCost_ReturnsEmployeesFromLowestToHighestCost()
    {
        var employees = new[]
        {
            CreateEmployee(employeeId: 2, trainingCost: 300, trainingDate: new DateTime(2023, 1, 1)),
            CreateEmployee(employeeId: 1, trainingCost: 100, trainingDate: new DateTime(2023, 1, 1)),
            CreateEmployee(employeeId: 3, trainingCost: 200, trainingDate: new DateTime(2023, 1, 1))
        };

        var sorted = HrDataAnalyzer.GetEmployeesSortedByTrainingCost(employees);

        Assert.Equal([1, 3, 2], sorted.Select(employee => employee.EmployeeID));
    }

    [Fact]
    public void GetMostExpensiveTrainingYear_ReturnsYearWithHighestTotalTrainingCost()
    {
        var employees = new[]
        {
            CreateEmployee(employeeId: 1, trainingCost: 100, trainingDate: new DateTime(2022, 7, 1)),
            CreateEmployee(employeeId: 2, trainingCost: 250, trainingDate: new DateTime(2023, 7, 1)),
            CreateEmployee(employeeId: 3, trainingCost: 300, trainingDate: new DateTime(2022, 8, 1))
        };

        var mostExpensiveYear = HrDataAnalyzer.GetMostExpensiveTrainingYear(employees);

        Assert.NotNull(mostExpensiveYear);
        Assert.Equal(2022, mostExpensiveYear.Year);
        Assert.Equal(400, mostExpensiveYear.TotalTrainingCost);
    }

    [Fact]
    public void CustomDateTimeConverter_ParsesSupportedDateFormat()
    {
        var converter = new CustomDateTimeConverter();

        var result = converter.ConvertFromString("15-Jul-23", null!, null!);

        Assert.Equal(new DateTime(2023, 7, 15), result);
    }

    private static Employee CreateEmployee(int employeeId, double trainingCost, DateTime trainingDate)
    {
        return new Employee
        {
            EmployeeID = employeeId,
            Title = "Test Employee",
            TrainingCost = trainingCost,
            TrainingDate = trainingDate
        };
    }
}
