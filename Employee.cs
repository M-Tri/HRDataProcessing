using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace HRDataProcessing;

public class Employee
{
    [Name("Employee ID")]
    public int EmployeeID { get; set; }

    [Name("StartDate")]
    [TypeConverter(typeof(CustomDateTimeConverter))]
    public DateTime StartDate { get; set; }

    [Name("Title")]
    public string Title { get; set; } = string.Empty;

    [Name("BusinessUnit")]
    public string BusinessUnit { get; set; } = string.Empty;

    [Name("EmployeeStatus")]
    public string EmployeeStatus { get; set; } = string.Empty;

    [Name("EmployeeType")]
    public string EmployeeType { get; set; } = string.Empty;

    [Name("PayZone")]
    public string PayZone { get; set; } = string.Empty;

    [Name("EmployeeClassificationType")]
    public string EmployeeClassificationType { get; set; } = string.Empty;

    [Name("DepartmentType")]
    public string DepartmentType { get; set; } = string.Empty;

    [Name("Division")]
    public string Division { get; set; } = string.Empty;

    [Name("DOB")]
    [TypeConverter(typeof(CustomDateTimeConverter))]
    public DateTime DOB { get; set; }

    [Name("State")]
    public string State { get; set; } = string.Empty;

    [Name("GenderCode")]
    public string GenderCode { get; set; } = string.Empty;

    [Name("RaceDesc")]
    public string RaceDesc { get; set; } = string.Empty;

    [Name("MaritalDesc")]
    public string MaritalDesc { get; set; } = string.Empty;

    [Name("Performance Score")]
    public string PerformanceScore { get; set; } = string.Empty;

    [Name("Current Employee Rating")]
    public int CurrentEmployeeRating { get; set; }

    [Name("Survey Date")]
    [TypeConverter(typeof(CustomDateTimeConverter))]
    public DateTime SurveyDate { get; set; }

    [Name("Engagement Score")]
    public int EngagementScore { get; set; }

    [Name("Satisfaction Score")]
    public int SatisfactionScore { get; set; }

    [Name("Work-Life Balance Score")]
    public int WorkLifeBalanceScore { get; set; }

    [Name("Training Date")]
    [TypeConverter(typeof(CustomDateTimeConverter))]
    public DateTime TrainingDate { get; set; }

    [Name("Training Program Name")]
    public string TrainingProgramName { get; set; } = string.Empty;

    [Name("Training Type")]
    public string TrainingType { get; set; } = string.Empty;

    [Name("Training Outcome")]
    public string TrainingOutcome { get; set; } = string.Empty;

    [Name("Training Duration(Days)")]
    public int TrainingDurationDays { get; set; }

    [Name("Training Cost")]
    public double TrainingCost { get; set; }

    [Name("Age")]
    public int Age { get; set; }
}
