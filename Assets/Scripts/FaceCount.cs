using TMPro;
using UnityEngine;

public class FaceCount : MonoBehaviour
{
    private int numberOfCoke = 0;
    private int numberOfChickenDrum = 0;
    private int numberOfHouse = 0;
    private int numberOfMoney = 0;
    private int numberOfCar = 0;
    private int numberOfGuitar = 0;

    public TextMeshProUGUI cokeText;
    public TextMeshProUGUI chickenDrumText;
    public TextMeshProUGUI houseText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI carText;
    public TextMeshProUGUI guitarText;

    bool isGift;
    bool isOneStar;
    bool isThreeStar;

    private void Update()
    {
        IsGift();
        IsOneStar();
        IsThreeStar();
    }

    public void UpdateFaceCount()
    {
        cokeText.text = new string('I', numberOfCoke);
        chickenDrumText.text = new string('I', numberOfChickenDrum);
        houseText.text = new string('I', numberOfHouse);
        moneyText.text = new string('I', numberOfMoney);
        carText.text = new string('I', numberOfCar);
        guitarText.text = new string('I', numberOfGuitar);
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Coke":
                numberOfCoke++;
                break;
            case "ChickenDrum":
                numberOfChickenDrum++;
                break;
            case "House":
                numberOfHouse++;
                break;
            case "Money":
                numberOfMoney++;
                break;
            case "Car":
                numberOfCar++;
                break;
            case "Guitar":
                numberOfGuitar++;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.name)
        {
            case "Coke":
                numberOfCoke--;
                break;
            case "ChickenDrum":
                numberOfChickenDrum--;
                break;
            case "House":
                numberOfHouse--;
                break;
            case "Money":
                numberOfMoney--;
                break;
            case "Car":
                numberOfCar--;
                break;
            case "Guitar":
                numberOfGuitar--;
                break;
        }
    }

    void IsGift()
    {
        if(numberOfCoke == 3 || numberOfChickenDrum == 3 || numberOfHouse == 3 || numberOfMoney == 3 || numberOfCar == 3 || numberOfGuitar == 3)
        {
            isGift = true;
        }
        else
        {
            isGift = false;
        }
    }

    void IsOneStar()
    {
        if (numberOfCoke == 4 || numberOfChickenDrum == 4 || numberOfHouse == 4 || numberOfMoney == 4 || numberOfCar == 4 || numberOfGuitar == 4)
        {
            isOneStar = true;
        }
        else
        {
            isOneStar = false;
        }
    }

    void IsThreeStar()
    {
        if (numberOfCoke == 5 || numberOfChickenDrum == 5 || numberOfHouse == 5 || numberOfMoney == 5 || numberOfCar == 5 || numberOfGuitar == 5)
        {
            isThreeStar = true;
        }
        else
        {
            isThreeStar = false;
        }
    }

    public bool GetGift()
    {
        return isGift;
    }

    public bool GetOneStar()
    {
        return isOneStar;
    }

    public bool GetThreeStar()
    {
        return isThreeStar;
    }
}
