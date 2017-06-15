using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.Experimental.EditorVR.Input;
using UnityEditor.Experimental.EditorVR.Proxies;
using UnityEngine.InputNew;
using Timer = System.Diagnostics.Stopwatch;

public class VcrTests : ScriptableObject
{
    static Timer timer;

    [MenuItem("VCR/TestRecord - 10s")]
    static void DoIt()
    {
        EvrVCR.tape = new Tape();
        timer = new Timer();
        timer.Start();
        EvrVCR.tape.label = "menu record test";
        EvrVCR.StartRecording();
        EditorApplication.update += OnEditorUpdate;
    }

    static void OnEditorUpdate()
    {
        // check real time here
        if (timer.ElapsedMilliseconds > 10000)
        {
            Debug.Log("past 10 seconds");
            EvrVCR.StopRecording();
            EditorApplication.update -= OnEditorUpdate;
        }
        else
        {
            //Debug.Log("seconds since start: " + timer.ElapsedMilliseconds / 1000);
        }
    }

    [MenuItem("VCR/tape json test 4")]
    static void RecordJsonTest4()
    {
        //EvrVCR.FindActiveProxy();
        Debug.Log("test start");
        var input = InputSystem.CreateEvent<GenericControlEvent>();
        input.time = 4.20f;
        input.deviceIndex = 3;
        input.value = 0.666f;
        input.controlIndex = 1;
        input.deviceType = typeof(VRInputDevice);

        var vrinput = InputSystem.CreateEvent<VREvent>();
        vrinput.time = 6.66f;
        vrinput.deviceIndex = 3;
        vrinput.localPosition = new Vector3(0f, 1f, 0.5f);
        vrinput.localRotation = new Quaternion(0f, 0.5f, 1f, -0.5f);
        vrinput.deviceType = typeof(VRInputDevice);

        EvrVCR.RecordInput(input);
        EvrVCR.RecordInput(vrinput);
        EvrVCR.tape.label = "DEMO TAPE";
        Debug.Log("pre-save");
        EvrVCR.tape.SaveTapeJson();
    }

    [MenuItem("VCR/record transforms test")]
    static void RecordTransforms()
    {
        
        EvrVCR.tape.label = "TRANSFORM TAPE";
        EvrVCR.tape.SaveTapeJson();
    }
}