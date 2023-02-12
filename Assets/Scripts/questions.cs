using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class questions : MonoBehaviour
{
    public Dropdown variantsAdd;
    public Dropdown variantsDel;
    public InputField questionField;
    public InputField answerField;
    public Text questionTexts;
    public InputField questionID;

    public bool check1;
    public bool check2;

    public string easyFNameAdmin, hardFNameAdmin;
    public string easyFNameUser, hardFNameUser;

    string easyFPathAdmin, hardFPathAdmin;
    string easyFPathUser, hardFPathUser;

    public string[] questionsEasy;
    public string[] questionsHard;

    public Text allertQuestion;
    public Text allertAnsw;

    public bool checkIn;

    void Start()
    {
        StartPathWay();
        //combineQuestions();
        check1 = false;
        check2 = false;
}


    void Update()
    {
        if (checkIn)
            checkInput();
    }

    void checkInput()
    {
        if (questionField.text.Length > 373)
        {
            allertQuestion.gameObject.SetActive(true);
            allertQuestion.text = "Лишние символы: " + (questionField.text.Length - 373).ToString();
            Debug.Log("Длинно на" + (questionField.text.Length - 373));
        }
        else
            allertQuestion.gameObject.SetActive(false);
        if (answerField.text.Length > 250)
        {
            allertAnsw.gameObject.SetActive(true);
            allertAnsw.text = "Лишние символы: " + (answerField.text.Length - 250).ToString();
            Debug.Log("Длинно на" + (answerField.text.Length - 250));
        }
        else
            allertAnsw.gameObject.SetActive(false);
    }
    void StartPathWay()
    {
        easyFPathAdmin = Application.dataPath + "/StreamingAssets" + "/" + easyFNameAdmin + ".txt";
        hardFPathAdmin = Application.dataPath + "/StreamingAssets" + "/" + hardFNameAdmin + ".txt";
        easyFPathUser = Application.dataPath + "/StreamingAssets" + "/" + easyFNameUser + ".txt";
        hardFPathUser = Application.dataPath + "/StreamingAssets" + "/" + hardFNameUser + ".txt";
        //questionsEasy = File.ReadAllLines(easyFPathAdmin);
        //questionsHard = File.ReadAllLines(hardFPathAdmin);
    }

    public void AddUserQuestion()
    {
        string newQuestion = questionField.text + " =" + answerField.text;
        if (!(allertAnsw.gameObject.activeSelf && allertQuestion.gameObject.activeSelf) && questionField.text.Length > 0 && answerField.text.Length > 0)
        {
            switch (variantsAdd.value)
            {
                case 0:
                    File.AppendAllText(easyFPathUser, newQuestion + "\n");
                    break;

                case 1:
                    File.AppendAllText(hardFPathUser, newQuestion + "\n");
                    break;
            }
            questionField.Select();
            questionField.text = "";
            answerField.Select();
            answerField.text = "";
        }
        else
        {
            Debug.Log("lol");
        }
    }
    
    public void DeleteUserQuestion()
    {
        string[] linesEasy = File.ReadAllLines(easyFPathUser);
        string[] linesHard = File.ReadAllLines(hardFPathUser);

        string[] newLinesEasy = new string[linesEasy.Length - 1];
        string[] newLinesHard = new string[linesHard.Length - 1];
        int j = 0;
        switch (variantsDel.value)
        {
            case 0:
                for (int i = 0; i < linesEasy.Length; i++)
                {
                    if ((i + 1).ToString() == questionID.text)
                        Debug.Log(linesEasy[i]);
                    else
                    {
                        newLinesEasy[j] = linesEasy[i];
                        j++;
                    }
                }
                File.WriteAllLines(easyFPathUser, newLinesEasy);
                break;

            case 1:
                for (int i = 0; i < linesHard.Length; i++)
                {
                    if ((i + 1).ToString() == questionID.text)
                        Debug.Log(linesHard[i]);
                    else
                    {
                        newLinesHard[j] = linesHard[i];
                        j++;
                    }
                }
                File.WriteAllLines(hardFPathUser, newLinesHard);
                break;
        }

        variantsAdd.Select();
        variantsDel.value = 0;
        questionID.Select();
        questionID.text = "";
    }

    public string[] questionList;
    public void questionFillList(Button btn)
    {
        string typeList = btn.gameObject.name;
        //string[] questionList;
        questionTexts.text = "";

        switch (typeList)
        {
            case "easyAdminButton":
                questionList = File.ReadAllLines(easyFPathAdmin);            
                break;
            case "hardAdminButton":
                questionList = File.ReadAllLines(hardFPathAdmin);
                break;
            case "easyUserButton":
                questionList = File.ReadAllLines(easyFPathUser);
                break;
            case "hardUserButton":
                questionList = File.ReadAllLines(hardFPathUser);
                break;
        }
        for (int i = 0; i < questionList.Length; i++)
        {
            string str = questionList[i].Substring(0, questionList[i].IndexOf("=")) + "(Ответ: " + questionList[i].Substring(questionList[i].IndexOf("=") + 1) + ")";
            //questionList[i].Substring(0, questionList[i].IndexOf("="));
            questionTexts.text += (i + 1).ToString() + ") " + str + "\n";
        }
    }
    public void checkek1(Toggle toggle)
    {
        check1 = toggle.isOn;

    }
    public void checkek2(Toggle toggle)
    {
        check2 = toggle.isOn;

    }
    public void combineQuestions()
    {
        StartPathWay();
        string[] questionsEasyAdmin = new string[0];
        string[] questionsEasyUser = new string[0];
        string[] questionsHardAdmin = new string[0];
        string[] questionsHardUser = new string[0];
        if (check1)
        {
            questionsEasyAdmin = File.ReadAllLines(easyFPathAdmin);
            questionsHardAdmin = File.ReadAllLines(hardFPathAdmin);
        }
        if (check2)
        {
            questionsEasyUser = File.ReadAllLines(easyFPathUser);
            questionsHardUser = File.ReadAllLines(hardFPathUser);
        }

        
        questionsEasy = new string[questionsEasyUser.Length + questionsEasyAdmin.Length];
        int j = 0;
        for (int i = 0; i < (questionsEasyAdmin.Length + questionsEasyUser.Length); i++)
        {
            if (i < questionsEasyAdmin.Length && questionsEasyAdmin.Length > 0)
            {
                questionsEasy[i] = questionsEasyAdmin[i];
            }                
            else
            {
                questionsEasy[i] = questionsEasyUser[j];
                j++;
            }                
        }

        
        questionsHard = new string[questionsHardUser.Length + questionsHardAdmin.Length];
        j = 0;
        for (int i = 0; i < (questionsHardAdmin.Length + questionsHardUser.Length); i++)
        {
            if (i < questionsHardAdmin.Length && questionsHardAdmin.Length > 0)
            {
                questionsHard[i] = questionsHardAdmin[i];
            }
            else
            {
                questionsHard[i] = questionsHardUser[j];
                j++;
            }
        }
    }
    

}
