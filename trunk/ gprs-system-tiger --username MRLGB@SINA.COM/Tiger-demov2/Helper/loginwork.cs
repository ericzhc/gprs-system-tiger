using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;

namespace Tiger.Helper
{
    public static class Loginwork
    {

        #region Fields & Properties

        //this field should set in settings application
        private const string FileName = @"UserData.xml";

        //the login flag (true if the user is logged)
        private static bool _loggedDefault;
        private static bool _loggedDb;
        public static bool Logged { get; private set; }

        // this list store the all registered users
        private static List<UserData> _userDataList = new List<UserData>();

        // the user data who is logged
        private static UserData _user;

        public static UserData User
        {
            get { return _user; }
        }


        #endregion

        #region Private Procedures

        /// <summary>
        /// Check exist this nikname in the users list
        /// </summary>
        /// <param name="nikName">Nikname of the user</param>
        /// <returns></returns>
        private static bool IsExistNikName(string nikName)
        {

            if (_userDataList.Count == 0)
            {
                return false;
            }
            // here I use a lambda expression for the searching the nikname in the user data list
            _user = _userDataList.FirstOrDefault(userData => userData.Nikname == nikName);

            if (String.IsNullOrEmpty(_user.Nikname))
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// Load  from the file registered users data in the list
        /// </summary>
        /// <returns></returns>
        private static List<UserData> Load()
        {

            var returnUserData = new List<UserData>();

            if (File.Exists(FileName))
            {
                using (Stream fileStream = File.OpenRead(FileName))
                {
                    var serializer = new XmlSerializer(typeof(List<UserData>));
                    returnUserData = (List<UserData>)serializer.Deserialize(fileStream);
                }
            }

            return returnUserData;
        }

        #endregion

        #region Public Procedures
        /// <summary>
        /// The hash function
        /// returns the hash as a 32-character, hexadecimal-formatted string
        /// for more information see ms-help
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HashString(string s)
        {
            //encode string to the array of bytes
            byte[] data = Encoding.Default.GetBytes(s);

            //hashing
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);

            //do hexadecimal-formatted string
            var sb = new StringBuilder();
            foreach (byte item in result)
            {
                sb.Append(item.ToString("X"));
            }

            return sb.ToString();

        }

        public static void Initialization()
        {
            _userDataList = Load();
        }

        /// <summary>
        /// Check the nikname and the password in the users list
        /// </summary>
        /// <param name="nikName"></param>
        /// <param name="password"></param>
        public static void DoLogin(string nikName, string password)
        {

          if (IsExistNikName(nikName))
          {
            if (_user.Password == HashString(password))
            {
              _loggedDefault = true;
            }
          }

          using (var context = new DbTigerEntities())
          {
              try
              {
                  logininfor c = context.logininfors
                                 .First(i => i.username == nikName);
                  if (c.Equals(null))
                  {
                      _loggedDb = false;
                  }
                  else
                  if (c.password == password)
                  {
                          Global.Currentuser = c.username;
                          _loggedDb = true;
                   }
                   else
                   {
                       _loggedDb = false;
                   }
              }
              catch (Exception)
              {
                  
              }
          }

          Logged = (_loggedDefault || _loggedDb);

    }

        /// <summary>
        /// Saving the new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool Save(UserData user)
        {

            if (!IsExistNikName(user.Nikname))
            {

                _userDataList.Add(user);

                using (Stream fileStream = File.Create(FileName))
                {
                    var serializer = new XmlSerializer(typeof(List<UserData>));
                    serializer.Serialize(fileStream, _userDataList);
                }

                return true;
            }

            return false;

        }

        #endregion

    }

    /// <summary>
    /// This structure need for store the user login data
    /// we use the serialization for store data
    /// </summary>
    [Serializable]
    public struct UserData
    {
        public string Name { get; set; }

        public string Nikname { get; set; }

        public string Password { get; set; }
    }
}
