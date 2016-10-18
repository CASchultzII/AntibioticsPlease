using UnityEngine;
using System;

public class Game : MonoBehaviour {

    public Chart underChart, overChart;
    public Animator overChartAnimator;
    public Animator clipBoardAnimator;
    public Animator folderAnimator;
    public PrescriptionControl ppControl; // I AM OPPOSED TO THIS
    public Sprite[] portraits;

    private Patient currentPatient;
    private DateTime date = DateTime.Today;

    // Use this for initialization
    void Start() {
        // Make a new patient so we can do things
        currentPatient = new Patient(portraits);

        // reset data
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A_OLD, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B_OLD, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C_OLD, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F);

        // Render our data to the UI
        reset(underChart);
        render(underChart);
        reset(overChart);
        render(overChart);
    }

    public void confirm() {
        date = date.AddDays(1);

        // Decide on what action to do
        if (overChart.treatToggle.isOn) {
            // launch prescription pad and setup hooks
            ppControl.Show();
        } else if (overChart.waitToggle.isOn) {
            // perform wait calculations
            if (currentPatient.dosesRemaining > 0 || !currentPatient.bacterialInfection) {
                // adjust player health
                currentPatient.apparentHealth = Health.nextState(currentPatient.apparentHealth);
                currentPatient.incrementDoses();

                // adjust superbug stats
                float adjustVal = Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER;
                if (currentPatient.treatedWithA) {
                    float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F) + adjustVal;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, Math.Min(adjusted, 1F));
                }
                if (currentPatient.treatedWithB) {
                    float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F) + adjustVal;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, Math.Min(adjusted, 1F));
                }
                if (currentPatient.treatedWithC) {
                    float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F) + adjustVal;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, Math.Min(adjusted, 1F));
                }
            } else {
                // Nothing happens
            }

            render(underChart);
            overChartAnimator.SetTrigger("SlideOff");
        } else if (overChart.referToggle.isOn) {
            // dismiss patient and summarize (virus only)

            if (!currentPatient.bacterialInfection) {
                PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "Good job!  You recognized that the patient did not have a bacterial infection and referred them to the proper doctor.");
            } else {
                if (currentPatient.dosesRemaining > 0) {
                    if (currentPatient.treatedWithA || currentPatient.treatedWithB || currentPatient.treatedWithC) {

                        // adjust superbug stats
                        float adjustVal = Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER * 2;
                        if (currentPatient.treatedWithA) {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, Math.Min(adjusted, 1F));
                        }
                        if (currentPatient.treatedWithB) {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, Math.Min(adjusted, 1F));
                        }
                        if (currentPatient.treatedWithC) {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, Math.Min(adjusted, 1F));
                        }

                        PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You referred a patient that you could have treated further.");
                    } else
                        PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You referred a patient that you could have treated.  You could have cured them but you did not.  As a result, the patient died.");
                } else {
                    PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "THe person you referred was already healthy, but because you referred them another docotor took credit for your work.");
                }
            }

            clipBoardAnimator.SetTrigger("SlideOFf");
            folderAnimator.SetTrigger("SlideOn");
        } else if (overChart.dismissToggle.isOn) {
            // dismiss patient and summarize

            if (!currentPatient.bacterialInfection) {

                // adjust superbug stats
                float adjustVal = Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER * 2;
                if (currentPatient.treatedWithA) {
                    float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F) + adjustVal;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, Math.Min(adjusted, 1F));
                }
                if (currentPatient.treatedWithB) {
                    float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F) + adjustVal;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, Math.Min(adjusted, 1F));
                }
                if (currentPatient.treatedWithC) {
                    float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F) + adjustVal;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, Math.Min(adjusted, 1F));
                }

                PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You failed to refer you patient.  As a result, they did not receive the appropriate treatment for their illness and died.");
            } else {
                if (currentPatient.dosesRemaining > 0) {
                    if (currentPatient.treatedWithA || currentPatient.treatedWithB || currentPatient.treatedWithC) {

                        // adjust superbug stats
                        float adjustVal = Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER * 2;
                        if (currentPatient.treatedWithA) {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, Math.Min(adjusted, 1F));
                        }
                        if (currentPatient.treatedWithB) {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, Math.Min(adjusted, 1F));
                        }
                        if (currentPatient.treatedWithC) {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, Math.Min(adjusted, 1F));
                        }

                        PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You dismissed your patient prematurely.  You could have treated them further but you did not.");
                    } else
                        PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You dismissed your patient prematurely.  You could have cured them but you did not.  As a result, the patient died.");
                } else {
                    PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "Good job!  You successfully treated your patient.");
                }
            }

            clipBoardAnimator.SetTrigger("SlideOFf");
            folderAnimator.SetTrigger("SlideOn");
        }
    }

    public void treat(string antibiotic) {
        float resistance = PlayerPrefs.GetFloat(antibiotic, 0F) + Constants.BASE_INCREASE;
        PlayerPrefs.SetFloat(antibiotic, resistance);

        if (!currentPatient.bacterialInfection) {
            currentPatient.apparentHealth = Health.nextState(currentPatient.apparentHealth);
        } else {
            bool resistant = false;
            if (antibiotic == Constants.PREFS_ANTIBIOTIC_A) {
                resistant = currentPatient.resistantToA;
                currentPatient.treatedWithA = true;
            } else if (antibiotic == Constants.PREFS_ANTIBIOTIC_B) {
                resistant = currentPatient.resistantToB;
                currentPatient.treatedWithB = true;
            } else {
                resistant = currentPatient.resistantToC;
                currentPatient.treatedWithC = true;
            }

            if (!resistant) {
                currentPatient.apparentHealth = Health.prevState(currentPatient.apparentHealth);
                currentPatient.decrementDoses();
            } else {
                currentPatient.apparentHealth = Health.nextState(currentPatient.apparentHealth);
                currentPatient.incrementDoses();

                if (antibiotic != Constants.PREFS_ANTIBIOTIC_A && currentPatient.treatedWithA) {
                    float val = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F) + Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, val);
                }
                if (antibiotic != Constants.PREFS_ANTIBIOTIC_B && currentPatient.treatedWithB) {
                    float val = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F) + Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, val);
                }
                if (antibiotic != Constants.PREFS_ANTIBIOTIC_C && currentPatient.treatedWithC) {
                    float val = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F) + Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER;
                    PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, val);
                }
            }
        }
    }

    public void reset(Chart chart) {
        chart.treatToggle.isOn = false;
        chart.waitToggle.isOn = false;
        chart.referToggle.isOn = false;
        chart.dismissToggle.isOn = false;
    }

    public void render(Chart chart) {
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
