﻿using UnityEngine;
using System;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public Chart underChart, overChart;
    public Animator overChartAnimator;
    public Animator clipBoardAnimator;
    public Animator feedbackAnimator;
    public Animator resultAnimator;
    public PrescriptionControl ppControl; // I AM OPPOSED TO THIS
    public Sprite[] portraits;

    public Text deathCountText, successCountText;

    [HideInInspector]
    public int deathCount, successCount;

    bool death, success;
    bool gameOver;

    private Patient currentPatient;
    private DateTime date = DateTime.Today;

    public Text TotalMessage, AMessage, BMessage, CMessage;

    public AudioSource buttonClickSound, checkMarkSound, paperSound, signatureSound, patientDeathSound, patientSuccessSound;

    // Use this for initialization
    void Start() {

        // reset data
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A_OLD, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B_OLD, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C_OLD, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F);
        PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F);

        generatePatient();

        deathCount = 0;
        successCount = 0;

        UpdateDeathCount();
        UpdateSuccessCount();

        gameOver = false;
    }

    public void generatePatient()
    {
        // Make a new patient so we can do things
        currentPatient = new Patient(portraits);

        // Render our data to the UI
        reset(underChart);
        render(underChart);
        reset(overChart);
        render(overChart);

        death = false;
        success = false;
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
                success = true;
                successCount++;
                PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "Good job!  You recognized that the patient did not have a bacterial infection and referred them to the proper doctor.");
            } else {
                if (currentPatient.dosesRemaining > 0) {
                    if (currentPatient.treatedWithA || currentPatient.treatedWithB || currentPatient.treatedWithC)
                    {

                        // adjust superbug stats
                        float adjustVal = Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER * 2;
                        if (currentPatient.treatedWithA)
                        {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, Math.Min(adjusted, 1F));
                        }
                        if (currentPatient.treatedWithB)
                        {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, Math.Min(adjusted, 1F));
                        }
                        if (currentPatient.treatedWithC)
                        {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, Math.Min(adjusted, 1F));
                        }

                        PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You referred a patient that you could have treated further.");
                    }
                    else
                    {
                        deathCount++;
                        death = true;
                        PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You referred a patient that you could have treated.  You could have cured them but you did not.  As a result, the patient died.");
                    }
                } else {
                    PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "The person you referred was already healthy, but because you referred them another docotor took credit for your work.");
                }
            }

            TotalMessage.text = PlayerPrefs.GetString(Constants.PREFS_SUMMARY_MESSAGE);
            AMessage.text = "Resist to Antibiotic A: " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A_OLD) + " -> " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A);
            BMessage.text = "Resist to Antibiotic B: " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B_OLD) + " -> " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B);
            CMessage.text = "Resist to Antibiotic C: " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C_OLD) + " -> " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C);

            AMessage.color = Color.Lerp(Color.green, Color.red, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A));
            BMessage.color = Color.Lerp(Color.green, Color.red, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B));
            CMessage.color = Color.Lerp(Color.green, Color.red, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C));

            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A_OLD, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A));
            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B_OLD, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B));
            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C_OLD, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C));

            feedbackAnimator.SetTrigger("Show");

            //overChartAnimator.SetTrigger("SlideOff");
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
                death = true;
                deathCount++;
                PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You failed to refer you patient.  As a result, they did not receive the appropriate treatment for their illness and died.");
            } else {
                if (currentPatient.dosesRemaining > 0) {
                    if (currentPatient.treatedWithA || currentPatient.treatedWithB || currentPatient.treatedWithC)
                    {

                        // adjust superbug stats
                        float adjustVal = Constants.BASE_INCREASE * Constants.COMPLIANCE_MULTIPLIER * 2;
                        if (currentPatient.treatedWithA)
                        {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A, Math.Min(adjusted, 1F));
                        }
                        if (currentPatient.treatedWithB)
                        {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B, Math.Min(adjusted, 1F));
                        }
                        if (currentPatient.treatedWithC)
                        {
                            float adjusted = PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C, 0F) + adjustVal;
                            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C, Math.Min(adjusted, 1F));
                        }

                        PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You dismissed your patient prematurely.  You could have treated them further but you did not.");
                    }
                    else
                    {
                        death = true;
                        deathCount++;
                        PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "You dismissed your patient prematurely.  You could have cured them but you did not.  As a result, the patient died.");
                    }
                } else {
                    success = true;
                    successCount++;
                    PlayerPrefs.SetString(Constants.PREFS_SUMMARY_MESSAGE, "Good job!  You successfully treated your patient.");
                }
            }

            TotalMessage.text = PlayerPrefs.GetString(Constants.PREFS_SUMMARY_MESSAGE);
            AMessage.text = "Resist to Antibiotic A: " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A_OLD) + " -> " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A);
            BMessage.text = "Resist to Antibiotic B: " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B_OLD) + " -> " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B);
            CMessage.text = "Resist to Antibiotic C: " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C_OLD) + " -> " + PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C);

            AMessage.color = Color.Lerp(Color.green, Color.red, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A));
            BMessage.color = Color.Lerp(Color.green, Color.red, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B));
            CMessage.color = Color.Lerp(Color.green, Color.red, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C));

            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_A_OLD, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_A));
            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_B_OLD, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_B));
            PlayerPrefs.SetFloat(Constants.PREFS_ANTIBIOTIC_C_OLD, PlayerPrefs.GetFloat(Constants.PREFS_ANTIBIOTIC_C));

            feedbackAnimator.SetTrigger("Show");
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
        scale /= 1.5f;
        chart.superbugBar.color = Color.Lerp(Color.green, Color.red, scale);
        chart.superbugBar.transform.localScale = new Vector3(scale, 1.0F, 1.0F);

        // Set Portrait and Date
        chart.patientPortrait.sprite = currentPatient.portrait;
        chart.dateText.text = date.ToString("MMMM dd, yyyy");
    }

    public void UpdateDeathCount()
    {
        deathCountText.text = "Deaths: " + deathCount;
    }

    public void UpdateSuccessCount()
    {
        successCountText.text = "Saves: " + successCount;
    }

    public void GameOver()
    {
        gameOver = true;
        resultAnimator.SetTrigger("Show");
    }

    public void playButtonClickSound()
    {
        buttonClickSound.Play();
    }

    public void playCheckMarkSound()
    {
        checkMarkSound.Play();
    }

    public void playPaperSound()
    {
        paperSound.Play();
    }

    public void playSignatureSound()
    {
        signatureSound.Play();
    }

    public void playResultSound()
    {
        if(death)
            patientDeathSound.Play();
        if (success)
            patientSuccessSound.Play();
    }
}
