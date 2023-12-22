using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartButtonClicked : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI giftText;
    public TextMeshProUGUI starText;
    public TextMeshProUGUI wonRewardText;
    public TextMeshProUGUI lockedDiceText;

    int rollCount = 0;
    public Transform diceCountArea;
    public Transform diceGroup;
    public Transform faceUpdate;
    public Transform table;
    public GameObject starPrefab;

    public AudioSource boo;
    public AudioSource wow;
    public AudioSource yeah;
    public AudioSource cheers;

    int numberOfDice;
    bool rollingDone = false;
    List<Transform> lockedDice;

    public LayerMask clickLayer;
    public Transform giftGroup;
    List<Transform> remainingGifts = new List<Transform>();

    bool alreadyGiveReward = false;

    int giftCount = 0;
    int startCount = 0;

    Vector3 impulsePosition = new Vector3(0.3f, 1.7f, 0.2f);

    private void Start()
    {
        buttonText.text = "START";
        numberOfDice = diceGroup.childCount;
        GetRemainingGifts();

        boo.volume = 0.15f;
    }

    private void Update()
    {
        // !EventSystem.current.IsPointerOverGameObject()

        faceUpdate.GetComponent<FaceCount>().UpdateFaceCount();
        CheckRolling();

        // Show face count when finish rolling
        if (rollingDone && buttonText.text == "ROLLING" && rollCount != 3)
        {
            buttonText.text = "ROLL";
            diceCountArea.Find("FaceCount").gameObject.SetActive(true);
        }
        else if (rollingDone && buttonText.text == "ROLLING" && rollCount == 3)      // Case when finished all three times roll
        {
            buttonText.text = "RESTART";
            diceCountArea.Find("FaceCount").gameObject.SetActive(true);
        }

        // Disable "dice click to roll" when dice stopped
        if (buttonText.text != "START" || (buttonText.text == "ROLL" && rollCount != 0))
        {
            for (int i = 0; i < numberOfDice; i++)
            {
                if (diceGroup.GetChild(i).GetComponent<Rigidbody>().IsSleeping())
                {
                    diceGroup.GetChild(i).GetComponent<Dice>().enabled = false;
                }
                else
                {
                    diceGroup.GetChild(i).GetComponent<Dice>().enabled = true;
                }
            }
        }

        // Enable "dice click to roll" for all dice when not start the game
        if (buttonText.text == "START")
        {
            for (int i = 0; i < numberOfDice; i++)
            {
                diceGroup.GetChild(i).GetComponent<Dice>().enabled = true;
            }

            alreadyGiveReward = false;
        }

        if (buttonText.text == "RESTART" && alreadyGiveReward == false)
        {
            Debug.Log("Checking the rewards!");
            UpdateReward();
        }

        if (buttonText.text == "RESTART") { ResetDiceColor(); }
        LockDice();

    }

    public void OnButtonClick()
    {
        if(rollingDone || buttonText.text == "START")
        {
            diceCountArea.Find("FaceCount").gameObject.SetActive(false);
            switch (buttonText.text)
            {
                case "START":
                    buttonText.text = "ROLL";
                    rollCount = 0;
                    diceCountArea.gameObject.SetActive(true);
                    lockedDice = new List<Transform>();
                    break;
                case "ROLL":
                    buttonText.text = "ROLLING";
                    rollCount++;
                    RollingDice();
                    break;
                case "RESTART":
                    buttonText.text = "START";
                    diceCountArea.gameObject.SetActive(false);
                    break;
            }
        }
    }

    void RollingDice()
    {
        // Reset dice position
        for (int i = 0; i < numberOfDice; i++)
        {
            if (!lockedDice.Contains(diceGroup.GetChild(i)))
            {
                switch (diceGroup.GetChild(i).gameObject.name)
                {
                    case "Dice 01":
                        diceGroup.GetChild(i).position = new Vector3(-0.028f, 1.492f, 0.328f);
                        break;
                    case "Dice 02":
                        diceGroup.GetChild(i).position = new Vector3(0.573f, 1.492f, -0.070f);
                        break;
                    case "Dice 03":
                        diceGroup.GetChild(i).position = new Vector3(-0.028f, 1.492f, -0.05f);
                        break;
                    case "Dice 04":
                        diceGroup.GetChild(i).position = new Vector3(0.308f, 1.492f, 0.179f);
                        break;
                    case "Dice 05":
                        diceGroup.GetChild(i).position = new Vector3(0.68f, 1.492f, 0.328f);
                        break;
                }
                diceGroup.GetChild(i).rotation = Quaternion.Euler(Random.Range(-360f, 360f), Random.Range(-360f, 360f), Random.Range(-360f, 360f));

                Rigidbody diceRb = diceGroup.GetChild(i).GetComponent<Rigidbody>();
                diceRb.AddForceAtPosition(Vector3.up * 50f, impulsePosition, ForceMode.Impulse);
                diceRb.AddTorque(Random.onUnitSphere * 100f, ForceMode.Impulse);
            }
        }
    }
    public int GetRollCount()
    {
        return rollCount;
    }

    public void CheckRolling()
    {
        int sleepingDice = 0;
        Rigidbody rb;
        for (int i = 0; i < numberOfDice; i++)
        {
            rb = diceGroup.GetChild(i).GetComponent<Rigidbody>();
            if (rb.IsSleeping())
            {
                sleepingDice++;
            }
        }

        if(sleepingDice == 5)
        {
            rollingDone = true;
        }
        else
        {
            rollingDone = false;
        }
    }

    void LockDice()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool isDice = Physics.Raycast(ray, out hit, Mathf.Infinity, clickLayer) && hit.collider.CompareTag("Dice");

        if (buttonText.text == "ROLL" && rollCount != 0 && isDice && Input.GetMouseButtonDown(0) && hit.transform.GetComponent<Rigidbody>().IsSleeping())
        {
            if (!lockedDice.Contains(hit.transform))
            {
                lockedDice.Add(hit.transform);
                hit.transform.GetComponent<Renderer>().material.color = new Color(75f / 255f, 255f / 255f, 64f / 255f);
                lockedDiceText.text = "Dice Locked!";
                lockedDiceText.GetComponent<Animator>().SetTrigger("ShowLocked");
                Debug.Log("Locked: " + hit.transform.name);
            }
            else
            {
                lockedDice.Remove(hit.transform);
                lockedDiceText.text = "Dice Unlocked!";
                lockedDiceText.GetComponent<Animator>().SetTrigger("ShowLocked");
                Debug.Log("Unlocked: " + hit.transform.name);
                hit.transform.GetComponent<Renderer>().material.color = new Color(178f / 255f, 160f / 255f, 164f / 255f);
            }
        }
    }

    void UpdateReward()
    {
        int numberOfGift = remainingGifts.Count;
        int randomGiftIndex = Random.Range(0, numberOfGift);

        if (faceUpdate.GetComponent<FaceCount>().GetGift())
        {
            if (remainingGifts.Count != 0)
            {
                giftGroup.Find(remainingGifts[randomGiftIndex].name).gameObject.SetActive(true);
                remainingGifts.Remove(remainingGifts[randomGiftIndex]);
                alreadyGiveReward = true;
                giftCount++;

                giftText.text = "GIFTS: " + giftCount + "/10";
                wonRewardText.text = "You won a gift!";
                wonRewardText.GetComponent<Animator>().SetTrigger("ShowReward");
            }
            wow.Play();
            Debug.Log("A gift!");

        }
        else if (faceUpdate.GetComponent<FaceCount>().GetOneStar())
        {
            // Give a Star
            Instantiate(starPrefab, new Vector3(0.3f, 2f, 0.2f), Quaternion.identity);

            startCount++;

            starText.text = startCount + " STARS";
            wonRewardText.text = "You won a star!";
            wonRewardText.GetComponent<Animator>().SetTrigger("ShowReward");
            yeah.Play();
            Debug.Log("1 star!");
        }
        else if (faceUpdate.GetComponent<FaceCount>().GetThreeStar())
        {
            for (int i = 0; i < 3; i++)
            {
                startCount++;
                Instantiate(starPrefab, new Vector3(0.3f, 2f, 0.2f), Quaternion.identity);
            }
            starText.text = startCount + " STARS";
            wonRewardText.text = "Congratulation!";
            wonRewardText.GetComponent<Animator>().SetTrigger("ShowReward");
            cheers.Play();
            Debug.Log("3 star!");
        }
        else
        {
            Debug.Log("Nothing get!");
            wonRewardText.text = "Booooo! ";
            boo.Play();
            wonRewardText.GetComponent<Animator>().SetTrigger("ShowReward");
        }

        alreadyGiveReward = true;
    }

    void GetRemainingGifts()
    {
        for (int i = 0; i < giftGroup.childCount; i++)
        {
            remainingGifts.Add(giftGroup.GetChild(i));
        }
    }

    void ResetDiceColor()
    {
        for(int i = 0; i < numberOfDice; i++)
        {
            diceGroup.GetChild(i).GetComponent<Renderer>().material.color = new Color(178f / 255f, 160f / 255f, 164f / 255f);
        }
    }
}
