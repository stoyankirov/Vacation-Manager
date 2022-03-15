namespace VacationManager.Business
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using VacationManager.Domain;

    [ExcludeFromCodeCoverage]
    public abstract class Service
    {
        protected void ValidateRequest(Request request)
        {
            if (request == null)
                throw new ArgumentNullException($"{nameof(request)} should not be null.");
        }
    }
}
