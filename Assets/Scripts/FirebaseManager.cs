using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Analytics;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;
    string userId;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference DBReference;
    public FirebaseAuth auth;
    public FirebaseUser user;
    FirebaseApp app;

    long cnt;
    void Start()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

        //StartCoroutine(testCor());

    }

  /*  IEnumerator testCor()
    {
        yield return new WaitForSeconds(3f);
        //getNextChildID();
        //yield return new WaitForSeconds(3f);
        //Debug.LogError("l: " + cnt);
        //userId = cnt.ToString();
        //Debug.LogError("u: " + userId);
        saveData("test", 5, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 100);
    }*/

    void InitFirebase()
    {
        app = FirebaseApp.DefaultInstance;

        auth = FirebaseAuth.DefaultInstance;

        DBReference = FirebaseDatabase.DefaultInstance.RootReference;
        


    }

    public class User
    {
        public string username;
        public int totalAttempt;
        public float playTime, interaction_1, interaction_2, interaction_3, interaction_4, interaction_5, interaction_6, interaction_7;

        public User()
        {
        }

        public User(string username, int totalAttempt,  float interaction_1, float interaction_2, float interaction_3, float interaction_4,
        float interaction_5, float interaction_6, float interaction_7, float time)
        {
            this.username = username;
            this.totalAttempt = totalAttempt;
            this.interaction_1 = interaction_1;
            this.interaction_2 = interaction_2;
            this.interaction_3 = interaction_3;
            this.interaction_4 = interaction_4;
            this.interaction_5 = interaction_5;
            this.interaction_6 = interaction_6;
            this.interaction_7 = interaction_7;
            this.playTime = time;
        }

        public Dictionary<string, System.Object> ToDictionary()
        {
            Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();

            result["username"] = username;
            result["totalAttempt"] = totalAttempt;
            result["interaction_1"] = interaction_1;
            result["interaction_2"] = interaction_2;
            result["interaction_3"] = interaction_3;
            result["interaction_4"] = interaction_4;
            result["interaction_5"] = interaction_5;
            result["interaction_6"] = interaction_6;
            result["interaction_7"] = interaction_7;
            result["playTime"] = playTime;

            return result;
        }

    }

    private void writeNewUsers(string userId, string name, int totalAttempt, float interaction_1, float interaction_2, float interaction_3, float interaction_4,
        float interaction_5, float interaction_6, float interaction_7, float time)
    {
        // Create new entry at /user-scores/$userid/$scoreid and at
        // /leaderboard/$scoreid simultaneously
        string key = DBReference.Child("users").Push().Key;

        User user = new User(name, totalAttempt, interaction_1, interaction_2, interaction_3, interaction_4,
            interaction_5, interaction_6, interaction_7, time);

        Dictionary<string, System.Object> entryValues = user.ToDictionary();

        Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
        childUpdates["/users/" + key] = entryValues;
        //childUpdates["/user-scores/" + userId + "/" + key] = entryValues;

        DBReference.UpdateChildrenAsync(childUpdates);
    }

   /* void writeNewUser(string userId,  string name, int totalAttempt, float interaction_1, float interaction_2, float interaction_3, float interaction_4,
        float interaction_5, float interaction_6, float interaction_7, float time)
    {
        User user = new User(name, totalAttempt, interaction_1, interaction_2, interaction_3, interaction_4,
            interaction_5, interaction_6, interaction_7, time);
        string json = JsonUtility.ToJson(user);

        DBReference.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }*/

    public void saveData(string name, int totalAttempt, float interaction_1, float interaction_2, float interaction_3, float interaction_4,
        float interaction_5, float interaction_6, float interaction_7, float playTime)
    {
        writeNewUsers(userId, name, totalAttempt, interaction_1, interaction_2, interaction_3, interaction_4,
            interaction_5, interaction_6, interaction_7, Time.time);
    }

    /*public void getNextChildID()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                // Handle the error...
                cnt = 9999;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                //Debug.LogError(snapshot.Value);
                // Do something with snapshot...
                cnt = snapshot.ChildrenCount + 1;
               
                //Debug.LogError(cnt);

            }
        });

    }*/

}
