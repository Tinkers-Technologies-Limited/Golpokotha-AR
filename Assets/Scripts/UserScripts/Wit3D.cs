/***********************************************************************************
MIT License

Copyright (c) 2016 Aaron Faucher

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in all
	copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
	SOFTWARE.

***********************************************************************************/

using UnityEngine;
using UnityEngine.Experimental.Networking;
using System.Collections;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TMPro;
// using System.Web;

public partial class Wit3D : MonoBehaviour {

	// Class Variables
	float[] confidences = new float[10];
	float totalPlayedTime;

	public GameObject recordingButton;
	public GameObject tryAgainTxt;
	[SerializeField] GameObject tutPanel;
	[SerializeField] GameObject endPanel;
	[SerializeField] GameObject rainPrefab;
	[SerializeField] GameObject character;
	// Audio variables
	//public AudioSource s;
	public AudioClip recording;
	private float startRecordingTime;
	int samplerate;

	// API access parameters
	string url;
	string token = "5SVCV6GAY3DKSOX3C64L5Y5IDWD4FDQD"; // Bangla API
	//string token = "65ZAO2ZP2MCSFIGD5FYHSPTXJPKMPG3Q"; // English API
	//UnityWebRequest wr;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < confidences.Length; i++)
		{
			confidences[i] = 0f;
		}
		// If you are a Windows user and receiving a Tlserror
		// See: https://github.com/afauch/wit3d/issues/2
		// Uncomment the line below to bypass SSL
		// System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };

		// set samplerate to 16000 for wit.ai
		samplerate = 16000;

/*		foreach (string device in Microphone.devices)
		{
			print("Name: " + device);
			//debugPanel.text = "\n\n"+device + "\n";

			deviceName = device;
		}*/

	}

	//this is called when button in hold down
	public void StartRecording()
	{
		if (tutPanel.activeInHierarchy) 
		{
			tutPanel.SetActive(false);
		}
		//deactive try again buton..
		tryAgainTxt.SetActive(false);

		try
		{
			//Get the max frequency of a microphone, if it's less than 44100 record at the max frequency, else record at 44100
			int minFreq;
			int maxFreq;
			int freq = 44100;
			Microphone.GetDeviceCaps("", out minFreq, out maxFreq);
			if (maxFreq < 44100)
				freq = maxFreq;

			//Start the recording, the length of 300 gives it a cap of 5 minutes
			recording = Microphone.Start("", false, 10, 44100);
			startRecordingTime = Time.time;
		}
		catch(Exception e)
		{
			//UnityEngine.Debug.LogError(e);
			HandleException();
		}
	}

	private void HandleException()
	{
		tryAgainTxt.SetActive(true);
		recordingButton.SetActive(true);
	}

	//this is called when button in released
	public void StopRecording()
	{
		//Hide the button..
		//recordingButton.SetActive(false);

		try
		{
			//End the recording when the mouse comes back up, then play it
			Microphone.End("");

			//Trim the audioclip by the length of the recording
			AudioClip recordingNew = AudioClip.Create(recording.name, (int)((Time.time - startRecordingTime) * recording.frequency), recording.channels, recording.frequency, false);
			float[] data = new float[(int)((Time.time - startRecordingTime) * recording.frequency)];
			recording.GetData(data, 0);
			recordingNew.SetData(data, 0);
			this.recording = recordingNew;
			//s.clip = recording;
			//s.Play();
			//save audio..
			SavWav.Save("sample", recording);
			//UnityEngine.Debug.LogError(SavWav.filepath);
			// At this point, we can delete the existing audio clip
			recording = null;

			//Grab the most up-to-date JSON file
			// url = "https://api.wit.ai/message?v=20160305&q=Put%20the%20box%20on%20the%20shelf";

			//Start a coroutine called "WaitForRequest" with that WWW variable passed in as an argument
			string witAiResponse = GetJSONText(SavWav.filepath);

			Handle(witAiResponse);
		}
		catch(Exception e)
		{
			//UnityEngine.Debug.Log(5645);

			//debugPanel.text = "\n\n" + e + "\n";
			HandleException();
		}
	}

	string GetJSONText(string file) {

		// get the file w/ FileStream
		FileStream filestream = new FileStream (file, FileMode.Open, FileAccess.Read);
		BinaryReader filereader = new BinaryReader (filestream);
		byte[] BA_AudioFile = filereader.ReadBytes ((Int32)filestream.Length);
		filestream.Close ();
		filereader.Close ();

		// create an HttpWebRequest
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.wit.ai/speech");

		request.Method = "POST";
		request.Headers ["Authorization"] = "Bearer " + token;
		request.ContentType = "audio/wav";
		request.ContentLength = BA_AudioFile.Length;
		request.GetRequestStream ().Write (BA_AudioFile, 0, BA_AudioFile.Length);

		// Process the wit.ai response
		try
		{
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			if (response.StatusCode == HttpStatusCode.OK)
			{
				StreamReader response_stream = new StreamReader(response.GetResponseStream());
				return response_stream.ReadToEnd();
			}
			else
			{
				return "Error: " + response.StatusCode.ToString();
				return "HTTP ERROR";
			}
		}
		catch (Exception ex)
		{
			return "Error: " + ex.Message;
			return "HTTP ERROR";
		}       
	}

}