using Microsoft.Extensions.Logging;
using SpeechBubble.Common.Requests;
using SpeechBubble.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Operations
{
    public class AuthorizationOperations : ApiOperationsBase
    {
        private string _loginUrl = _baseApiUrl + "Authentication/Login";

        public async Task<Tuple<bool, string>> LoginAsync(string username, string password)
        {
            var response = await PostAsync(_loginUrl, JsonSerializer.Serialize(new SigninRequest { Email = username, Password = password }));
        
            if(response.IsSuccessStatusCode)
            {
                var juttu = await response.Content.ReadAsStringAsync();

                var responseObject = JsonSerializer.Deserialize<AuthenticationResponse>(juttu);

                return Tuple.Create<bool, string>(true, responseObject.token);
            }

            return Tuple.Create<bool, string>(false, null);
        }
    }
}
