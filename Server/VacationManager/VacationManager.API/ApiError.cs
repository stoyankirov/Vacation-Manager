namespace VacationManager.API
{
    public class ApiError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        /// <param name="description"></param>
        public ApiError(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// The error description.
        /// </summary>
        public string Description { get; }
    }
}
