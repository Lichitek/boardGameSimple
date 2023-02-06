using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class questions : MonoBehaviour
{
    public Dropdown variants;
    public InputField questionField;
    public InputField answerField;

    public Text questionTexts;

    public InputField questionID;

    public string easyFNameAdmin, hardFNameAdmin;
    public string easyFNameUser, hardFNameUser;

    string easyFPathAdmin, hardFPathAdmin;
    string easyFPathUser, hardFPathUser;

    public string[] questionsEasy;
    public string[] questionsHard;

    void Start()
    {
        StartPathWay();
        //combineQuestions();
    }


    void Update()
    {

    }


    void StartPathWay()
    {
        easyFPathAdmin = Application.dataPath + "/" + easyFNameAdmin + ".txt";
        hardFPathAdmin = Application.dataPath + "/" + hardFNameAdmin + ".txt";
        easyFPathUser = Application.dataPath + "/" + easyFNameUser + ".txt";
        hardFPathUser = Application.dataPath + "/" + hardFNameUser + ".txt";
        questionsEasy = File.ReadAllLines(easyFPathAdmin);
        questionsHard = File.ReadAllLines(hardFPathAdmin);
    }

    public void AddUserQuestion()
    {
        string newQuestion = questionField.text + " =" + answerField.text;
        switch (variants.value)
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
    public string[] lines;
    public void DeleteUserQuestion()
    {
        switch (variants.value)
        {
            case 0:
                lines = File.ReadAllLines(easyFPathUser);                
                break;

            case 1:
                lines = File.ReadAllLines(hardFPathUser);
                break;
        }
        string[] newLines = new string[lines.Length - 1];
        int j = 0;
        for(int i = 0; i<lines.Length; i++)
        {
            if ((i + 1).ToString() == questionID.text)
                Debug.Log(lines[i]);
            else
            {
                newLines[j] = lines[i];
                j++;
            }              
            
        }
        switch (variants.value)
        {
            case 0:
                File.WriteAllLines(easyFPathUser, newLines);
                break;

            case 1:
                File.WriteAllLines(hardFPathUser, newLines);
                break;
        }
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

    public void combineQuestions()
    {
        StartPathWay();
        string[] questionsEasyAdmin = File.ReadAllLines(easyFPathAdmin);
        string[] questionsEasyUser = File.ReadAllLines(easyFPathUser);
        questionsEasy = new string[questionsEasyUser.Length + questionsEasyAdmin.Length];
        int j = 0;
        for (int i = 0; i < (questionsEasyAdmin.Length + questionsEasyUser.Length); i++)
        {
            if (i < questionsEasyAdmin.Length)
            {
                questionsEasy[i] = questionsEasyAdmin[i];
            }                
            else
            {
                questionsEasy[i] = questionsEasyUser[j];
                j++;
            }                
        }

        string[] questionsHardAdmin = File.ReadAllLines(hardFPathAdmin);
        string[] questionsHardUser = File.ReadAllLines(hardFPathUser);
        questionsHard = new string[questionsHardUser.Length + questionsHardAdmin.Length];
        j = 0;
        for (int i = 0; i < (questionsHardAdmin.Length + questionsHardUser.Length); i++)
        {
            if (i < questionsHardAdmin.Length)
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
