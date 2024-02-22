using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
    Dictionary<int, QuestionPictures> questionPicturesPaires;

    [SerializeField] QuestionsWrapper questionsWrapper;

    [SerializeField] int currentQuestionIndex;

    float totalProgress;

    bool isClicked;
    private void Awake()
    {
        questionPicturesPaires = new Dictionary<int, QuestionPictures>();
    }

    private void Start()
    {
        StartCoroutine(DownloadJson());

    }
    private IEnumerator DownloadJson()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://raw.githubusercontent.com/mariartidang/aiimagequiz/main/online-json-editor.json");
        yield return request.SendWebRequest();

        //while(!op.isDone )
        //{
        //    UIManager.Instance.SetSliderValue(op.progress);
        //    yield return null;
        //}

        //UIManager.Instance.EndLoading();

        if (request.result != UnityWebRequest.Result.Success)
        {
            print(request.error);
            
        }
        else
        {
            string json = request.downloadHandler.text;

            questionsWrapper = JsonUtility.FromJson<QuestionsWrapper>(json);
            UIManager.Instance.SetSliderMaxValue(questionsWrapper.questions.Count * 4 * 5.5f);
            StartCoroutine(DownloadImages());

        }
    }

    private IEnumerator DownloadImages()
    {

        for (int i = 0; i < questionsWrapper.questions.Count; i++)
        {

            QuestionPictures questionPictures = new QuestionPictures();

            questionPictures.pictures = new Texture[4];

            for (int j = 0; j < 4; j++)
            {

                UnityWebRequest request = UnityWebRequestTexture.GetTexture(questionsWrapper.questions[i].pictureLinks[j]);
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    UIManager.Instance.SetSliderValue(operation.progress);
                    yield return null;
                }


                if (request.result == UnityWebRequest.Result.Success)
                {
                    questionPictures.pictures[j] = ((DownloadHandlerTexture)request.downloadHandler).texture;
                }
            }

            questionPicturesPaires.Add(i, questionPictures);
        }

        UIManager.Instance.EndLoading();
        StartGame();
    }

    public void StartGame()
    {
        ShowQuestion(currentQuestionIndex);
    }

    public async void CheckAnswer(int answer)
    {
        if (isClicked)
            return;

        isClicked = true;

        if (answer == questionsWrapper.questions[currentQuestionIndex].correctAnswer)
        {
            print("правильно");
        }
        else
        {
            print("дибил");
        }

        await Task.Delay(1000);
        NextQuestion();
     

        
    }

    public void NextQuestion()
    {   
        isClicked = false;
        if(currentQuestionIndex == questionsWrapper.questions.Count)

        {
            return;
        }
        currentQuestionIndex++;



        ShowQuestion(currentQuestionIndex);
    }


    public void ShowQuestion(int questionIndex)
    {
        UIManager.Instance.SetQuestionText(questionsWrapper.questions[questionIndex].question);

        for (int i = 0; i < 4; i++)
        {
            UIManager.Instance.SetPicture(i, questionPicturesPaires[currentQuestionIndex].pictures[i]);
        }
    }
}

[Serializable]
public class QuestionData
{
    public string[] pictureLinks;
    public string question;
    public string answerDescription;
    public int correctAnswer;
}

[Serializable]
public class QuestionsWrapper
{
    public List<QuestionData> questions;
}

[Serializable]
public class QuestionPictures
{
    public Texture[] pictures;

}




