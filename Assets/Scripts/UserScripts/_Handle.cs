using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using SimpleJSON;
using System.Text;
using System;
using DigitalRuby.RainMaker;
public partial class Wit3D : MonoBehaviour
{
	public AudioClip debugAudio;
	string _name = "";
	int totalAttempt;
	//first scnee shesh hole btns ashbe, 
	//each scene e buttons chole jabe.
	// 10 sec er beshi hold kore rakhle abar reset hobe.. 

	void UpdateConfidence(int i, float newConfidence)
	{
		confidences[i] += newConfidence;
	}
	
	void Handle(string textToParse)
	{
		Debug.LogError(textToParse);
		totalAttempt++;
		try
		{
			
			var N = JSON.Parse(textToParse);

			float intent_Confidence = float.Parse(N["intents"][0]["confidence"].Value);
			string intent_Name = N["intents"][0]["name"].Value.ToLower();
			string userSpoken_text = N["text"];

					Debug.LogError(intent_Confidence);
					Debug.LogError(intent_Name);
					Debug.LogError(userSpoken_text);


			// what function should I call?
			if (intent_Name.Equals("drinking_water_intent"))
			{

				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0] + intent_Confidence, confidences[1], confidences[2], confidences[3],
													confidences[4], confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(0, intent_Confidence);

				if (intent_Confidence >= 0.97f)
				{
					recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					rainPrefab.GetComponent<RainScript>().RainIntensity = 1f;
					rainPrefab.GetComponent<RainScript>().EnableWind = true;

					StartCoroutine(AfterRain());
				}
				else
					HandleException();

			}
			else if (intent_Name.Equals("name_intent"))
			{
				_name = userSpoken_text.Substring(9);
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1] + intent_Confidence, confidences[2], confidences[3],
													confidences[4], confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(1, intent_Confidence);

				if (intent_Confidence >= 0.98f)
				{
					recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(0);
				}
				else
					HandleException();
			}
			else if (intent_Name.Equals("im_fine_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2] + intent_Confidence, confidences[3],
													confidences[4], confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(2, intent_Confidence);

				if (intent_Confidence >= 0.97f)
				{
					//মি.গাছ - কিন্তু আমি ভালো নেই। আমার মন ভীষণ খারাপ।
					recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(1);
				}
				else
					HandleException();
			}
			else if (intent_Name.Equals("asking_sad_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2], confidences[3] + intent_Confidence,
													confidences[4], confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(3, intent_Confidence);

				if (intent_Confidence >= 0.97f)
				{
					//মি.গাছ - সে অনেক কথা! (খুশি হয়ে) আমি এক সময় তোমার মত ছোট ছিলাম। 
					//এখন আমার বয়স ১৫০ বছর। 
					//তুমি কি জানো আমি কিভাবে এতো বড় হলাম?
					recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(2);
				}
				else
					HandleException();
			}
			else if (intent_Name.Equals("asking_getting_big_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2], confidences[3],
													confidences[4] + intent_Confidence, confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(4, intent_Confidence);

				if (intent_Confidence >= 0.95f)
				{
					//মি.গাছ - তাহলে শোনো, আমি যখন খুব ছোট ছিলাম তখন একদিন এক দয়ালু মানুষ আমাকে তার বাড়ির বাগানে বপন করে। 
					recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(3);
				}
				else
					HandleException();
			}
			else if (intent_Name.Equals("asking_getting_big2_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2], confidences[3],
													confidences[4], confidences[5] + intent_Confidence, confidences[6], totalPlayedTime);
				UpdateConfidence(5, intent_Confidence);

				if (intent_Confidence >= 0.92f)
				{
					//মি.গাছ - না, গাছের বড় হওয়ার যত্ন এবং পানির প্রয়োজন। 

					recordingButton.SetActive(false);
					TextManager.instance.ResetDisplayTexts();
					NextAnim(4);

					//TAP পানির পাত্র এবং আমাদের আরও পানি দিয়ে বড় হতে সাহায্য করো!
					//মি.গাছ - আহ ধন্যবাদ!
					//জানো আগে আমার সাথে আমার অনেক ভাই - বোন, বন্ধু - বান্ধব ছিল। কিন্তু এখানে আমি একা!তুমি কি জানো তারা এখন কোথায় ?
					//These are handled in stateHandler script..
				}
				else
					HandleException();
			}
			else if (intent_Name.Equals("asking_where_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2], confidences[3],
													confidences[4], confidences[5], confidences[6] + intent_Confidence, totalPlayedTime);
				UpdateConfidence(6, intent_Confidence);

				if (intent_Confidence >= 0.96f)
				{
					//Debug.LogError(5);
					//end
					endPanel.SetActive(true);
					TextManager.instance.ResetDisplayTexts();
					recordingButton.SetActive(false);

				}
				else
				{
					//Debug.LogError(0000);
					HandleException();
				}
			}
			else
			{
				
				HandleException();
			}
		}
		catch(Exception e)
		{
			Debug.LogError(e);
			HandleException();
		}

	}

	IEnumerator AfterRain()
	{
		yield return new WaitForSeconds(4f);
		rainPrefab.GetComponent<RainScript>().RainIntensity = 0f;
		rainPrefab.GetComponent<RainScript>().EnableWind = false;
		yield return new WaitForSeconds(3f);
		character.GetComponent<StateHandler>().PlayC3();

	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			//character.GetComponent<Animator>().Play("start");
			character.transform.GetChild(0).GetComponent<Animator>().enabled = true;
			AudioSource s = character.GetComponent<AudioSource>();
			s.clip = debugAudio;
			s.Play();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			character.GetComponent<StateHandler>().PlayC3();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
			NextAnim(0);
		else if (Input.GetKeyDown(KeyCode.Alpha5))
			NextAnim(1);
		else if (Input.GetKeyDown(KeyCode.Alpha6))
			NextAnim(2);
		else if (Input.GetKeyDown(KeyCode.Alpha7))
			NextAnim(3);
		else if (Input.GetKeyDown(KeyCode.Alpha8))
			NextAnim(4);
		else if (Input.GetKeyDown(KeyCode.Alpha9))
		character.GetComponent<StateHandler>().PlayC9();
	}
	void NextAnim(int which)
	{
		if(which == 0)
			character.GetComponent<StateHandler>().PlayC4();
		else if (which == 1)
			character.GetComponent<StateHandler>().PlayC5();
		else if (which == 2)
			character.GetComponent<StateHandler>().PlayC6();
		else if (which == 3)
			character.GetComponent<StateHandler>().PlayC7();
		else if (which == 4)
			character.GetComponent<StateHandler>().PlayC8();

	}

}
