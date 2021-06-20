using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using SimpleJSON;
using System.Text;
using System;

public partial class Wit3D : MonoBehaviour
{
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
		totalAttempt++;
		try
		{
			var N = JSON.Parse(textToParse);

			float intent_Confidence = float.Parse(N["intents"][0]["confidence"].Value);
			string intent_Name = N["intents"][0]["name"].Value.ToLower();
			string userSpoken_text = N["text"];

			/*		Debug.LogError(intent_Confidence);
					Debug.LogError(intent_Name);
					Debug.LogError(userSpoken_text);*/


			// what function should I call?
			if (intent_Name.Equals("drinking_water_intent"))
			{

				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0] + intent_Confidence, confidences[1], confidences[2], confidences[3],
													confidences[4], confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(0, intent_Confidence);

				if (intent_Confidence >= 0.98f)
				{

					// এইটা রিডিং পড়ার সাথে সাথে  গাছের উপর বৃষ্টির মত পানি পড়বে

				}

			}
			else if (intent_Name.Equals("name_intent"))
			{
				_name = userSpoken_text.Substring(9);
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1] + intent_Confidence, confidences[2], confidences[3],
													confidences[4], confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(1, intent_Confidence);

				if (intent_Confidence >= 0.99f)
				{
					//কিরেনা - খুব সুন্দর। তুমি কেমন আছো?
				}
			}
			else if (intent_Name.Equals("im_fine_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2] + intent_Confidence, confidences[3],
													confidences[4], confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(2, intent_Confidence);

				if (intent_Confidence >= 0.98f)
				{
					//মি.গাছ - কিন্তু আমি ভালো নেই। আমার মন ভীষণ খারাপ।
				}
			}
			else if (intent_Name.Equals("asking_sad_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2], confidences[3] + intent_Confidence,
													confidences[4], confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(3, intent_Confidence);

				if (intent_Confidence >= 0.974f)
				{
					//মি.গাছ - সে অনেক কথা! (খুশি হয়ে) আমি এক সময় তোমার মত ছোট ছিলাম। 
					//এখন আমার বয়স ১৫০ বছর। 
					//তুমি কি জানো আমি কিভাবে এতো বড় হলাম?
				}
			}
			else if (intent_Name.Equals("asking_getting_big_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2], confidences[3],
													confidences[4] + intent_Confidence, confidences[5], confidences[6], totalPlayedTime);
				UpdateConfidence(4, intent_Confidence);

				if (intent_Confidence >= 0.96f)
				{
					//মি.গাছ - তাহলে শোনো, আমি যখন খুব ছোট ছিলাম তখন একদিন এক দয়ালু মানুষ আমাকে তার বাড়ির বাগানে বপন করে। 
				}
			}
			else if (intent_Name.Equals("asking_getting_big2_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2], confidences[3],
													confidences[4], confidences[5] + intent_Confidence, confidences[6], totalPlayedTime);
				UpdateConfidence(5, intent_Confidence);

				if (intent_Confidence >= 0.93f)
				{
					//মি.গাছ - না, গাছের বড় হওয়ার যত্ন এবং পানির প্রয়োজন। 

					//TAP পানির পাত্র এবং আমাদের আরও পানি দিয়ে বড় হতে সাহায্য করো!
					//মি.গাছ - আহ ধন্যবাদ!
					//জানো আগে আমার সাথে আমার অনেক ভাই - বোন, বন্ধু - বান্ধব ছিল। কিন্তু এখানে আমি একা!তুমি কি জানো তারা এখন কোথায় ?
				}
			}
			else if (intent_Name.Equals("asking_where_intent"))
			{
				FirebaseManager.instance.saveData(_name, totalAttempt, confidences[0], confidences[1], confidences[2], confidences[3],
													confidences[4], confidences[5], confidences[6] + intent_Confidence, totalPlayedTime);
				UpdateConfidence(6, intent_Confidence);

				if (intent_Confidence >= 0.97f)
				{
					//end
				}
			}
			else
			{
				HandleException();
			}
		}
		catch(Exception e)
		{
			HandleException();
		}

	}

}
