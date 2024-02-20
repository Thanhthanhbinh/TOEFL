using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class examItemController : MonoBehaviour
{
    // Start is called before the first frame update
    public FileInfo fileInformation;

    public void startExam() {
        Debug.Log(fileInformation.Name);
    }

}
