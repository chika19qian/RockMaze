using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;

public class BedrockManager : MonoBehaviour
{
    public TMP_InputField inputField; // 使用TextMeshPro的输入框
    public Button sendButton;
    public TMP_Text responseText; // 使用TextMeshPro的文本显示

    private string apiUrl = "https://lnspjimgeh.execute-api.us-west-2.amazonaws.com/prod";

    void Start()
    {
        responseText.text = "Let's rock and roll, baby!";
        sendButton.onClick.AddListener(SendRequest);
    }

    void SendRequest()
    {
        responseText.text = "Uh, let me see...";
        StartCoroutine(PostRequest());
    }

    IEnumerator PostRequest()
    {
        var requestData = new { key1 = inputField.text };
        string jsonRequest = JsonConvert.SerializeObject(requestData);

        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequest);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
                responseText.text = $"エラーが発生しました: {www.error}";
            }
            else
            {
                string responseJson = www.downloadHandler.text;
                Debug.Log($"Response: {responseJson}");

                // 检查响应内容是否为JSON对象
                if (IsJson(responseJson))
                {
                    try
                    {
                        JObject jsonResponse = JObject.Parse(responseJson);
                        responseText.text = jsonResponse["response"].ToString();
                    }
                    catch (JsonReaderException e)
                    {
                        Debug.LogError($"JSON Parsing Error: {e.Message}");
                        responseText.text = $"JSON解析エラーが発生しました: {e.Message}";
                    }
                }
                else
                {
                    // 如果不是JSON对象，则直接显示响应内容
                    responseText.text = responseJson;
                }
            }
        }
    }

    // 检查字符串是否为JSON对象的方法
    private bool IsJson(string str)
    {
        str = str.Trim();
        return (str.StartsWith("{") && str.EndsWith("}")) || (str.StartsWith("[") && str.EndsWith("]"));
    }
}



