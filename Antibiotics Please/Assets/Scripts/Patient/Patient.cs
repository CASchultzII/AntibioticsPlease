using UnityEngine;
using System;

public class Patient {

    public Patient(Sprite[] portraits) {
        System.Random rand = new System.Random();

        sprite = portraits[rand.Next(portraits.Length)];

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
    private Sprite sprite;

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

    public float healthDouble {
        get {
            switch (state) {
                case Health.state.HEALTHY:
                    return 1.0F;
                case Health.state.SICKLY:
                    return 0.75F;
                case Health.state.DISEASED:
                    return 0.5F;
                case Health.state.MORBIDLY_ILL:
                    return 0.25F;
                case Health.state.DECEASED:
                    return 0.0F;
                default:
                    throw new Exception("This should not happen...");
            }
        }
    }

    public Sprite portrait {
        get { return sprite; }
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
