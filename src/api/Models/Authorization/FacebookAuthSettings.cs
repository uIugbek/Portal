using System;

namespace Portal.Apis.Models
{
    public class FacebookAuthSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }

        public string GetAppAccessUrl()
        {
            return "https://graph.facebook.com/oauth/access_token" +
                   "?client_id=" + AppId +
                   "&client_secret=" + AppSecret +
                   "&grant_type=client_credentials";
        }

        public string GetUserAccessUrl(string input_token, string access_token)
        {
            return "https://graph.facebook.com/debug_token" +
                   "?input_token=" + input_token +
                   "&access_token=" + access_token;
        }

        public string GetUserInfoUrl(string access_token)
        {
            return "https://graph.facebook.com/v2.8/me" +
                   "?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture" +
                   "&access_token=" + access_token;
        }
    }
}
