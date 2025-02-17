using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace dotnetProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the file path
            var filePath = "data.csv";

            // Define padding sizes for each column
            int employeeIdPadding = 15;
            int namePadding = 30;
            int trainingCostPadding = 20;
            int startDatePadding = 20;

            // Calculate the separator line length
            int separatorLineLength = employeeIdPadding + namePadding + trainingCostPadding + startDatePadding;

            // Read and process the CSV file
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    // Read the records from the CSV file
                    var records = csv.GetRecords<Employee>().ToList();

                    // Order the records by Training Cost in ascending order
                    var sortedRecords = records.OrderBy(r => r.TrainingCost).ToList();

                    // Print header with padding
                    Console.WriteLine("Employee ID".PadRight(employeeIdPadding) + 
                                      "Name".PadRight(namePadding) + 
                                      "Training Cost".PadRight(trainingCostPadding) + 
                                      "Start Date".PadRight(startDatePadding));

                    // Print a separator line
                    Console.WriteLine(new string('-', separatorLineLength));

                    // Output the sorted records with padding between columns
                    foreach (var record in sortedRecords)
                    {
                        Console.WriteLine(
                            record.EmployeeID.ToString().PadRight(employeeIdPadding) +
                            record.Title.PadRight(namePadding) +
                            record.TrainingCost.ToString("F2").PadRight(trainingCostPadding) + // Format Training Cost to 2 decimal places
                            record.StartDate.ToString("dd-MMM-yyyy").PadRight(startDatePadding)
                        );
                    }

                    // Sum Training Costs by year
                    var trainingCostByYear = records
                        .GroupBy(r => r.StartDate.Year)  // Group by year
                        .Select(g => new { Year = g.Key, TotalTrainingCost = g.Sum(r => r.TrainingCost) })  // Sum Training Costs for each year
                        .OrderByDescending(g => g.TotalTrainingCost)  // Order by total cost in descending order
                        .FirstOrDefault();  // Get the year with the highest total cost

                    // Output the most expensive year
                    if (trainingCostByYear != null)
                    {
                        Console.WriteLine("\nThe most expensive year for training is: ");
                        Console.WriteLine($"Year: {trainingCostByYear.Year}");
                        Console.WriteLine($"Total Training Cost: {trainingCostByYear.TotalTrainingCost:F2}");
                    }
                }
            }
        }
    }
}
