using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Integration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async void Post(IFormCollection formData)
        {
            FormModel myForm = new FormModel();
            ContactFormModel contactForm = new ContactFormModel();

            if (formData != null)
            {         
                //Retriving data from webhook.
                myForm.FormId = formData["formID"];
                myForm.SubmissionId = formData["submissionID"];
                myForm.WebhookURL = formData["webhookURL"];
                myForm.IP = formData["ip"];
                myForm.FormTitle = formData["formTitle"];
                myForm.Pretty = formData["pretty"];
                myForm.Username = formData["username"];
                myForm.RawRequest = formData["rawRequest"];
                myForm.Type = formData["type"];


                string[] webhook = myForm.WebhookURL.Split("website=");
                if (webhook.Length > 0) {
                    contactForm.Website = new[] { new Websites() { Text = webhook[1] } };
                }
                
                

                string personDetails = myForm.Pretty;
                string[] arrayOfStr = personDetails.Split(',');

                string[] rawName = arrayOfStr[1].Split(':');
                string fullName = rawName[1];

                //string[] rawEmailText = arrayOfStr[1].Split(':');
                //string email=rawEmailText[1];

                string[] rawMessage = arrayOfStr[2].Split(':');
                string contactInfo = rawMessage[1];
                string[] phoneEmail = contactInfo.Split(' ');
                string phone = phoneEmail[0];
                string email = phoneEmail[1];

                contactForm.FullName = fullName;
                contactForm.Email = new [] { new Emails() { Text=email,Type=""}/*, new Emails() { }*/};

                contactForm.Phone = new[] { new Phones() { Text = phone, Type = "" } };
                
                
                //contactForm.Message = message;
            }
            //Posting Data To the lessannoyingcrm API

            var serializedData = JsonConvert.SerializeObject(contactForm);
            var url = "https://api.lessannoyingcrm.com?UserCode=807AB&APIToken=H3RSW2TCRK0QN3KY71DMR1GF2RZ1BTSRP8T9W18KQJK7JPV4F8&Function=CreateContact";

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("Parameters", serializedData));

            using (var httpClient = new HttpClient())
            { using (var content = new FormUrlEncodedContent(postData))
                { 
                    content.Headers.Clear(); 
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");                     
                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                    //return await response.Content.ReadAsAsync<TResult>(); 
                    if (response.IsSuccessStatusCode)
                    {
                        var customerJsonString = await response.Content.ReadAsStringAsync();
                    }
                }
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    //public class Emails
    //{

    //    public string Text { get; set; }
    //    public string Type { get; set; }
    //}
    //public class Parameter
    //{
    //    public string FullName { get; set; }
    //}
}
