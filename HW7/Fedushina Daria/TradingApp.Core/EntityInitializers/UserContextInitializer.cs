using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Models;

namespace TradingApp.Core.EntityInitializers
{
    public class UserContextInitializer
    {
        private string[] Names;
        
        private Random r = new Random();
        private void getNames(string fileName)
        {
            var jsonFile = new FileInfo(fileName);
            if (!jsonFile.Exists)
            {
                throw new ArgumentException($"Can't find language file in {jsonFile}");
            }

            var jsonFileContent = File.ReadAllText(jsonFile.FullName);

            try
            {
                var jsonFileData = JsonConvert.DeserializeObject<string[]>(jsonFileContent);
                Names = (string[])jsonFileData;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    "Can't extract file Names.json", ex);
            }
        }
        public List<UserEntity> ContextInitializer()
        {
            List<UserEntity> Users = new List<UserEntity>();
            getNames("Names.json");
            char[] spearator = { ' ' };
            foreach (string name in Names)
            {
                UserEntity user = new UserEntity();
                user.Name = name.Split(spearator)[0];
                user.Surname = name.Split(spearator)[1];
                user.PhoneNumber = r.Next(1000000, 9999999).ToString();

                Users.Add(user);
            }
            return Users;
        }
    }
}
