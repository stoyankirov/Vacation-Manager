namespace VacationManager.API.Controllers
{
    using System;
    using VacationManager.Domain;

    public class BaseController
    {
        protected void ValidateRequest(Request request)
        {
            if (request == null)
                throw new ArgumentNullException("Invalid request!");
        }
    }
}
