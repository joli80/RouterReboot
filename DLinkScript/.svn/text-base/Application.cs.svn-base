using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace DLinkScript
{
    public class Application
    {
        readonly string ip;

        public Application(string ip)
        {
            this.ip = ip;
        }

        public void Login(string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}/", ip));
            request.Timeout = 10000;

            string body = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    body = sr.ReadToEnd();
                }
            }

            string salt = "";
            string authId = "";
            if (!string.IsNullOrEmpty(body))
            {
                salt = body.Substring(body.IndexOf("var salt = ", 0) + 12, 8);
                authId = body.Substring(body.IndexOf("&auth_id=", 0) + 9, 5);
            }

            string loginHash = CreateHash(salt, password);

            request = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}/post_login.xml?hash={1}&auth_code=&auth_id={2}", ip, loginHash, authId));

            body = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    body = sr.ReadToEnd();
                }
            }

            if (!body.Contains("<login>Status/Device_Info.shtml</login>"))
            {
                throw new System.Security.SecurityException("Login failed");
            }
        }

        public void Reboot()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}/reboot.cgi?reset=false", ip));
            request.Timeout = 10000;
            request.GetResponse();
        }

        private string CreateHash(string salt, string password)
        {
            password = password.PadRight(16, Convert.ToChar(1));

            string input = salt + password;
            input = input.PadRight(64, Convert.ToChar(1));

            string hash = CreateMd5Hash(input);

            return salt + hash;
        }

        private string CreateMd5Hash(string input)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] bytes = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
