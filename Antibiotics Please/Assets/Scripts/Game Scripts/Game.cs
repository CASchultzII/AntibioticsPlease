using UnityEngine;
using System;

public class Game : MonoBehaviour {

    public Chart underChart, overChart;
    public Animator overChartAnimator;
    public Animator clipBoardAnimator;
    public Sprite[] portraits;

    private Patient currentPatient;
    private DateTime date = DateTime.Today;

    // Use this for initialization
    void Start() {
        // Make a new patient so we can do things
        currentPatient = new Patient(portraits);

        // Render our data to the UI
        reset(underChart);
        render(underChart);
        reset(overChart);
        render(overChart);
    }

    public void confirm() {
        // Decide on what action to do
        if (overChart.treatToggle.isOn) {
            // launch prescription pad and setup hooks

        } else if (overChart.waitToggle.isOn) {
            // perform wait calculations
            if (currentPatient.dosesRemaining > 0 || !currentPatient.bacterialInfection) {
                // adjust player health
                currentPatient.apparentHealth = Health.prevState(currentPatient.apparentHealth);

                // adjust superbug stats
                float adjustVal = Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER;
                if (currentPatient.treatedWithA) {
                    float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F) + adjustVal;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, adjusted);
                }
                if (currentPatient.treatedWithB) {
                    float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F) + adjustVal;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, adjusted);
                }
                if (currentPatient.treatedWithC) {
                    float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F) + adjustVal;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, adjusted);
                }
            } else {
                // Nothing happens
            }
        } else if (overChart.referToggle.isOn) {
            // dismiss patient and summarize (virus only)

        } else if (overChart.dismissToggle.isOn) {
            // dismiss patient and summarize

        }
    }

    public void treat(string antibiotic) {

    }

    private void reset(Chart chart) {
        chart.treatToggle.isOn = false;
        chart.waitToggle.isOn = false;
        chart.referToggle.isOn = false;
        chart.dismissToggle.isOn = false;
    }

    private void render(Chart chart) {
        // Render patient health
        chart.patientHealthBar.color = Color.Lerp(Color.red, Color.green, currentPatient.healthDouble);
        chart.patientHealthBar.transform.localScale = new Vector3(currentPatient.healthDouble, 1.0F, 1.0F);

        // Render superbug bar
        float scale = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F) +
                PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F) +
                PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F);
        scale /= 3;
        chart.superbugBar.color = Color.Lerp(Color.green, Color.red, scale);
        chart.superbugBar.transform.localScale = new Vector3(scale, 1.0F, 1.0F);

        // Set Portrait and Date
        chart.patientPortrait.sprite = currentPatient.portrait;
        chart.dateText.text = date.ToString("MMMM dd, yyyy");
    }
}
