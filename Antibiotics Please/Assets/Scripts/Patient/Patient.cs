using UnityEngine;

public class Patient {

    public Patient() {
        System.Random rand = new System.Random();
        bacterial = rand.NextDouble() <= Constants.CHANCE_BACTERIAL;

        // resistances
        resistance[0] = rand.NextDouble() <= PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F);
        resistance[1] = rand.NextDouble() <= PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F);
        resistance[2] = rand.NextDouble() <= PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F);

        state = Health.getInitialState();
        doses = rand.Next(Constants.DOSAGE_MIN, Constants.DOSAGE_MAX + 1);
    }

    private bool bacterial;
    private bool[] resistance = new bool[3];
    private Health.state state;
    private int doses;

    public bool bacterialInfection {
        get { return bacterial; }
    }

    public bool resistantToA {
        get { return resistance[0]; }
    }
    public bool resistantToB {
        get { return resistance[1]; }
    }
    public bool resistantToC {
        get { return resistance[2]; }
    }

    public Health.state apparentHealth {
        get { return state; }
        set { state = value; }
    }

    public int dosesRemaining {
        get { return doses; }
    }
    public void incrementDoses() {
        doses++;
    }
    public void decrementDoses() {
        doses--;
    }
}
