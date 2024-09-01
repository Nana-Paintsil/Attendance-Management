using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace AttendanceManagement.Services
{
    public class MessageService {

            public bool sendCredentialsToUser(String name, String username, String password, String reciepientMail)
        {
            string fromMail = "app.bomsoma@gmail.com";
            string fromPassword = "mjjg ubgt bfps nhyt";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Credentials";
            message.To.Add(new MailAddress(reciepientMail));
            message.Body =  $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Transition to Digital Attendance System</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            background-color: #f4f4f4;
            padding: 20px;
        }}
        .container {{
            max-width: 600px;
            margin: auto;
            background: #fff;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }}
        h2 {{
            color: #007bff;
        }}
        p {{
            margin-bottom: 20px;
        }}
        .credentials {{
            border-top: 1px solid #ccc;
            padding-top: 20px;
        }}
        .credentials p {{
            margin-bottom: 10px;
        }}
        .footer {{
            text-align: center;
            margin-top: 30px;
        }}
        .footer p {{
            color: #777;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h2>Welcome to the Digital Attendance System</h2>
        <p>Dear {name},</p>
        <p>We are pleased to introduce you to our digital attendance system.</p>
        <p>The digital attendance system provides a convenient way for you to record your attendance online. You can log in using your provided credentials and easily mark your presence for each workday.</p>
        <div class=""credentials"">
            <p><strong>Your Digital Credentials:</strong></p>
            <p><strong>Username:</strong> {username}</p>
            <p><strong>Password:</strong> {password}</p>
        </div>
        
        <div class=""footer"">
            <p>Best Regards,<br>Administration</p>
        </div>
    </div>
</body>
</html>";

            message.IsBodyHtml = true;

            try
            {

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                smtpClient.Send(message);
            }
            catch (Exception ex) 
            { 
            }

    
            return true;
        }
    
    
 public async Task SendSms(string[] phoneNumbers, string message)
        {
            string endPoint = "https://api.mnotify.com/api/sms/quick";
            string apiKey = "b7JqEKIuqMns5BbRIKTKalfQC"; // Replace with your actual API key
            string url = endPoint + "?key=" + apiKey;

            
            var data = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("sender", "Bomso MA"),
            new KeyValuePair<string, string>("message", message),
            new KeyValuePair<string, string>("is_schedule", "false"),
            new KeyValuePair<string, string>("schedule_date", "")
        };

            foreach (var phoneNumber in phoneNumbers)
            {
                data.Add(new KeyValuePair<string, string>("recipient[]", phoneNumber));
            }

            using var client = new HttpClient();
            using var content = new FormUrlEncodedContent(data);

            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

                Console.WriteLine("Response:");
                foreach (var item in responseData)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
        }


    public static string ConvertToInternationalFormat(string localNumber)
    {
        // Ensure the phone number starts with '0'
        if (localNumber.StartsWith("0"))
        {
            // Remove the leading '0' and add '233' as the country code for Ghana
            string internationalFormat = "233" + localNumber.Substring(1);
            
            // Ensure the number is trimmed to 12 digits if it exceeds
            if (internationalFormat.Length > 12)
            {
                internationalFormat = internationalFormat.Substring(0, 12);
            }

            return internationalFormat;
        }
        else
        {
            throw new ArgumentException("Phone number must start with '0'.");
        }
    }
    }
    
}