using Microsoft.AspNetCore.Mvc;

namespace FandNCloud.Common.Responds
{
    public class CreateUserRespond : IRespond
    {
        public IActionResult Respond { get; set; }

        public CreateUserRespond(IActionResult respond)
        {
            Respond = respond;
        }
    }
}