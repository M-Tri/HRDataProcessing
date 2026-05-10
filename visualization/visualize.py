import os
import tempfile
from pathlib import Path

os.environ.setdefault(
    "MPLCONFIGDIR", str(Path(tempfile.gettempdir()) / "hrdataprocessing-matplotlib")
)

import matplotlib
import pandas as pd

matplotlib.use("Agg")
import matplotlib.pyplot as plt

BASE_DIR = Path(__file__).resolve().parent
OUTPUT_DIR = BASE_DIR.parent / "output"
CHARTS_DIR = BASE_DIR / "charts"


def read_summary_csv(file_name):
    file_path = OUTPUT_DIR / file_name

    if not file_path.exists():
        raise FileNotFoundError(
            f"Missing {file_path}. Run the C# app first with: dotnet run"
        )

    return pd.read_csv(file_path)


def print_table(title, data_frame):
    print()
    print(title)
    print("-" * len(title))
    print(data_frame.to_string(index=False))


def save_training_cost_by_year_chart(data_frame):
    sorted_data = data_frame.sort_values("Year")

    plt.figure(figsize=(8, 5))
    plt.bar(sorted_data["Year"].astype(str), sorted_data["TotalTrainingCost"])
    plt.title("Training Cost by Year")
    plt.xlabel("Year")
    plt.ylabel("Total Training Cost")
    plt.tight_layout()
    plt.savefig(CHARTS_DIR / "training_cost_by_year.png")
    plt.close()


def save_training_cost_by_business_unit_chart(data_frame):
    sorted_data = data_frame.sort_values("TotalTrainingCost", ascending=False)

    plt.figure(figsize=(10, 6))
    plt.bar(sorted_data["BusinessUnit"], sorted_data["TotalTrainingCost"])
    plt.title("Training Cost by Business Unit")
    plt.xlabel("Business Unit")
    plt.ylabel("Total Training Cost")
    plt.xticks(rotation=45, ha="right")
    plt.tight_layout()
    plt.savefig(CHARTS_DIR / "training_cost_by_business_unit.png")
    plt.close()


def main():
    CHARTS_DIR.mkdir(parents=True, exist_ok=True)

    training_cost_by_year = read_summary_csv("training_cost_by_year.csv")
    training_cost_by_business_unit = read_summary_csv(
        "training_cost_by_business_unit.csv"
    )

    print_table("Training Cost by Year", training_cost_by_year)
    print_table("Training Cost by Business Unit", training_cost_by_business_unit)

    save_training_cost_by_year_chart(training_cost_by_year)
    save_training_cost_by_business_unit_chart(training_cost_by_business_unit)

    print()
    print("Charts saved to:")
    print("- charts/training_cost_by_year.png")
    print("- charts/training_cost_by_business_unit.png")


if __name__ == "__main__":
    main()
