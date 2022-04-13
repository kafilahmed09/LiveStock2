using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LIVESTOCK.API
{
    public class ZongSMS
    {
        private readonly string LoginId;
        private readonly string LoginPassword;        
        private readonly string Mask;
        private readonly string Message;
        private readonly string UniCode = "0";
        private readonly string ShortCodePrefered = "n";

        public ZongSMS(string loginId, string loginPassword, string mask, string message)
        {            
            this.LoginId = loginId;
            this.LoginPassword = loginPassword;
            this.Mask = mask;
            this.Message = message;
        }
        public ZongSMS()
        {            
        }
        public async Task<string> SendSingleSMS(string msg, string sendTo, string language)//923188170832
        {
            using (var stringContent = new StringContent("{\"loginId\":\"923188170832\",\"loginPassword\":\"Gpeb##1234\",\"Destination\":\"" + sendTo + "\",\"Mask\":\"LIVESTOCK\",\"Message\":\""+ msg +"\",\"UniCode\":\""+ language +"\",\"ShortCodePrefered\":\"n\"}", System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync("http://cbs.zong.com.pk/reachrestapi/home/SendQuickSMS", stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                    return result;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                return "Failed to send!";
            }
        }

        public async void SendSingleSMSCovid(string msg, string sendTo)
        {
            using (var stringContent = new StringContent("{\"loginId\":\"923188057099\",\"loginPassword\":\"Gpeb##1234\",\"Destination\":\"" + sendTo + "\",\"Mask\":\"PMU\",\"Message\":\"" + msg + "\",\"UniCode\":\"1\",\"ShortCodePrefered\":\"n\"}", System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync("http://cbs.zong.com.pk/reachrestapi/home/SendQuickSMS", stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }
    }
}
