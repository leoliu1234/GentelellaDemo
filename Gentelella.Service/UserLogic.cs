using Gentelella.Common.Exception;
using Gentelella.Interface;
using Gentelella.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gentelella.Service
{
    public class UserLogic : IUserLogic
    {
        public PosCredential Login(PosCredential credential)
        {
            var posCredential = GetUser(credential.EmailAddress);

            if (posCredential == null || posCredential.Password != credential.Password)
            {
                throw new CommonException(ExceptionCodes.CanNotFoundUser);
            }

            posCredential.Password = null;

            return posCredential;
        }

        public PosCredential GetUser(string emailAddress)
        {
            return DBHelper.ExecuteDataReader<PosCredential>("select * from [user] where email_address = @email_address", new[] { emailAddress }, (reader) =>
            {
                while (reader.Read())
                {
                    var tempCredential = new PosCredential();
                    tempCredential.UserName = reader.GetString("nick_name");
                    tempCredential.EmailAddress = reader.GetString("email_address");
                    tempCredential.Password = reader.GetString("password");
                    return tempCredential;
                }
                return null;
            });
        }



        public bool Register(PosCredential credential)
        {
            var posCredential = GetUser(credential.EmailAddress);
            if (posCredential != null)
            {
                throw new CommonException(ExceptionCodes.UserExisted);
            }

            DBHelper.ExecuteNonQuery("insert into [user]([nick_name], [email_address], [password]) values(@nick_name, @email_address, @password)", new[] { credential.UserName, credential.EmailAddress, credential.Password });

            return true;
        }
    }
}
