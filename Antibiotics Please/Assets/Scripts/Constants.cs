using System;

public static class Constants {

    
    //// Patient Constants
    // Chance that patient has a bacterial infection
	public static double CHANCE_BACTERIAL {
        get { return 0.95; }
    }

    // Parameters for required dosages (bacterial only)
    public static int DOSAGE_MIN {
        get { return 3; }
    }
    public static int DOSAGE_MAX {
        get { return 5; }
    }

    //// Global Resistance
    // Base increase for using an AB
    public static double BASE_INCREASE {
        get { return .02; }
    }
    // Increase Multiplier for failure to continue medication
    public static double COMPLIANCE_MULTIPLIER {
        get { return 3; }
    }

    //// PlayerPrefs Constant Names
    public static string PREFS_ANTIBIOTIC_A {
        get { return "ANTIBIOTIC A"; }
    }
    public static string PREFS_ANTIBIOTIC_B {
        get { return "ANTIBIOTIC B"; }
    }
    public static string PREFS_ANTIBIOTIC_C {
        get { return "ANTIBIOTIC C"; }
    }

    private static string[] firstName = {"Mike", "John", "Michael", "Jane" };
    private static string[] lastName = {"Green", "Jackson" };

    
    public static string getName()
    {
        Random rand = new Random();
        return firstName[rand.Next(firstName.Length)] + " " + firstName[rand.Next(lastName.Length)];
    }
}
