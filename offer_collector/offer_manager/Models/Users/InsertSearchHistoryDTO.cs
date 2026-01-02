namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for storing a search snapshot in the user's search history.
    /// At least one search field (besides JWT) must be provided.
    /// </summary>
    public sealed class InsertSearchHistoryRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;

        /// <summary>
        /// Free-text search keywords; null means "not provided".
        /// </summary>
        public string? Keywords { get; init; }

        /// <summary>
        /// Search radius/distance; null means "not provided".
        /// </summary>
        public int? Distance { get; init; }

        /// <summary>
        /// Whether remote work is accepted; null means "not provided".
        /// </summary>
        public bool? IsRemote { get; init; }

        /// <summary>
        /// Whether hybrid work is accepted; null means "not provided".
        /// </summary>
        public bool? IsHybrid { get; init; }

        /// <summary>
        /// Leading category ID (offers dictionary ID); null means "not provided".
        /// </summary>
        public short? LeadingCategoryId { get; init; }

        /// <summary>
        /// City ID (offers dictionary ID); null means "not provided".
        /// </summary>
        public int? CityId { get; init; }

        /// <summary>
        /// Minimum salary; null means "not provided".
        /// </summary>
        public decimal? SalaryFrom { get; init; }

        /// <summary>
        /// Maximum salary; null means "not provided".
        /// </summary>
        public decimal? SalaryTo { get; init; }

        /// <summary>
        /// Salary period ID (offers dictionary ID); null means "not provided".
        /// </summary>
        public short? SalaryPeriodId { get; init; }

        /// <summary>
        /// Salary currency ID (offers dictionary ID); null means "not provided".
        /// </summary>
        public short? SalaryCurrencyId { get; init; }

        /// <summary>
        /// Employment schedule IDs (offers dictionary IDs); null means "not provided".
        /// </summary>
        public short[]? EmploymentScheduleIds { get; init; }

        /// <summary>
        /// Employment type IDs (offers dictionary IDs); null means "not provided".
        /// </summary>
        public short[]? EmploymentTypeIds { get; init; }
    }

    /// <summary>
    /// Response payload for inserting a search snapshot.
    /// </summary>
    public sealed class InsertSearchHistoryResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
