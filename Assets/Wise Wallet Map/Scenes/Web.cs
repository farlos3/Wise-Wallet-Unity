using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Web : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(GetText()); 
    }

    IEnumerator GetText() {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost:8080/login")) {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(webRequest.error);
            }
            else {
                Debug.Log(webRequest.downloadHandler.text);

                byte[] results = webRequest.downloadHandler.data;
            }
        }
    }
}

//     IEnumerator GetItems() {
//         using (UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/Wise%20Wallet/GetItems.php")){
//             yield return webRequest.SendWebRequest();

//             if (webRequest.result == UnityWebRequest.Result.ProtocolError) {
//                 Debug.Log(webRequest.error);
//             }
//             else {
//                 Debug.Log(webRequest.downloadHandler.text);

//                 byte[] results = webRequest.downloadHandler.data;
//             }
//         }
//     }
// }