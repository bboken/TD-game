using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    
    public static int Money;
    public static int Life;
    public static int Income;
    public int IncomeRate = 1;

    public  Text userMoney;
    public  Text UserLife;
    public  Text UserIncome;

    int startMonet = 50;
    int startLife = 30;
    int startIncome = 0;

    public static bool isspawn = false;

    int x;
    // Use this for initialization
    void Start () {
        Money = startMonet;
        Life = startLife;
        Income = startIncome;

        x = IncomeRate;
    }

    // Update is called once per frame
    void Update() {
        userMoney.text = Money.ToString();
        UserIncome.text = Income.ToString();
        UserLife.text = Life.ToString();
        //Debug.Log(Time.time);
        
        if (x == Timer4.gettime() )
        {
            increaseMoney();
        }


    }

    public void increaseMoney()
    {
        Money += Income;
        x += IncomeRate;
    }

    public  void modifyIncome(int value)
    {
        if((Income + value) < 0)
        {
            Income = 0;
        }else
        {
            Income += value;
        }
       
    }

    public static void decreaseLife(int value)
    {
        Life -= value;
    }

    public void buyOj(int value)
    {
        Money -= value;
    }

    public int getMoney()
    {
        return Money;
    }
    public int getLife()
    {
        return Life;
    }

    public int getIncome()
    {
        return Income;
    }
    
}
