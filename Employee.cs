using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;

public class Employee
{
    [Name("Employee ID")]
    public int EmployeeID { get; set; }

    [Name("StartDate")]
    [TypeConverter(typeof(CustomDateTimeConverter))]
    public DateTime StartDate { get; set; }

    [Name("Title")]
    public string Title { get; set; }

    [Name("BusinessUnit")]
    public string BusinessUnit { get; set; }

    [Name("EmployeeStatus")]
    public string EmployeeStatus { get; set; }

    [Name("EmployeeType")]
    public string EmployeeType { get; set; }

    [Name("PayZone")]
    public string PayZone { get; set; }

    [Name("EmployeeClassificationType")]
    public string EmployeeClassificationType { get; set; }

    [Name("DepartmentType")]
    public string DepartmentType { get; set; }

    [Name("Division")]
    public string Division { get; set; }

    [Name("DOB")]
    [TypeConverter(typeof(CustomDateTimeConverter))]
    public DateTime DOB { get; set; }

    [Name("State")]
    public string State { get; set; }

    [Name("GenderCode")]
    public string GenderCode { get; set; }

    [Name("RaceDesc")]
    public string RaceDesc { get; set; }

    [Name("MaritalDesc")]
    public string MaritalDesc { get; set; }

    [Name("Performance Score")]
    public string PerformanceScore { get; set; }

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
    public string TrainingProgramName { get; set; }

    [Name("Training Type")]
    public string TrainingType { get; set; }

    [Name("Training Outcome")]
    public string TrainingOutcome { get; set; }

    [Name("Training Duration(Days)")]
    public int TrainingDurationDays { get; set; }

    [Name("Training Cost")]
    public double TrainingCost { get; set; }

    [Name("Age")]
    public int Age { get; set; }
}
