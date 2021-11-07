using System;
using Authentication;
using Firebase.Database;
using UnityEngine;

public class Database
{

   private string name;

   private DataUser user;
   
   public string Name { set; get; }

   public Database(string name)
   {
      this.name = name;
      FirebaseDatabase.DefaultInstance
         .GetReference("Users")
         .ValueChanged += GetUser;
   }
   
   private void GetUser(object sender2, ValueChangedEventArgs e2)
   {
      if (e2.DatabaseError != null)
      {
         Debug.LogError(e2.DatabaseError.Message);
      }

      if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
      {
         foreach (var childSnapshot in e2.Snapshot.Children)
         {
            var n = childSnapshot.Child("name").Value.ToString();
            if (n == name)
            {
               SetValueUser(childSnapshot);
            }
         }
      }
   }

   private void SetValueUser(DataSnapshot snapshot)
   {
      Name = snapshot.Child("name").Value.ToString();
      Debug.Log(Name);
      user.score = Convert.ToUInt32(snapshot.Child("score").Value.ToString());
      user.coin = Convert.ToUInt32(snapshot.Child("coin").Value.ToString());
      user.password = snapshot.Child("password").Value.ToString();
      user.imgProfile = snapshot.Child("imgProfile").Value.ToString();
      user.character1 = Convert.ToBoolean(snapshot.Child("character1").Value.ToString());
      user.character2 = Convert.ToBoolean(snapshot.Child("character2").Value.ToString());
   }

   public DataUser GetDataUser()
   {
      Debug.Log(user.name);
      return user;
   }

   public string GetName()
   {
      return Name;
   }
}
