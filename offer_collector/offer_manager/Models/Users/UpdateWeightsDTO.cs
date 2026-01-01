namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for partially updating ranking weights.
    /// At least one non-empty array (besides JWT) must be provided to perform an update.
    /// </summary>
    public sealed class UpdateWeightsRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;

        /// <summary>
        /// Optional ordering options used by the ranking logic; null means "do not update".
        /// </summary>
        public string[]? OrderByOption { get; init; }

        /// <summary>
        /// Optional mean-value identifiers; null means "do not update".
        /// </summary>
        public string[]? MeanValueIds { get; init; }

        /// <summary>
        /// Optional ranking vector; null means "do not update".
        /// </summary>
        public float[]? Vector { get; init; }

        /// <summary>
        /// Optional mean distance values; null means "do not update".
        /// </summary>
        public float[]? MeanDist { get; init; }

        /// <summary>
        /// Optional mean value sums; null means "do not update".
        /// </summary>
        public float[]? MeansValueSum { get; init; }

        /// <summary>
        /// Optional mean value squared sums; null means "do not update".
        /// </summary>
        public double[]? MeansValueSsum { get; init; }

        /// <summary>
        /// Optional mean value counts; null means "do not update".
        /// </summary>
        public int[]? MeansValueCount { get; init; }

        /// <summary>
        /// Optional mean weight sums; null means "do not update".
        /// </summary>
        public float[]? MeansWeightSum { get; init; }

        /// <summary>
        /// Optional mean weight squared sums; null means "do not update".
        /// </summary>
        public double[]? MeansWeightSsum { get; init; }

        /// <summary>
        /// Optional mean weight counts; null means "do not update".
        /// </summary>
        public int[]? MeansWeightCount { get; init; }
    }

    /// <summary>
    /// Response payload for weights update.
    /// </summary>
    public sealed class UpdateWeightsResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Indicates whether <c>OrderByOption</c> was updated.
        /// </summary>
        public bool OrderByOptionUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>MeanValueIds</c> was updated.
        /// </summary>
        public bool MeanValueIdsUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>Vector</c> was updated.
        /// </summary>
        public bool VectorUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>MeanDist</c> was updated.
        /// </summary>
        public bool MeanDistUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>MeansValueSum</c> was updated.
        /// </summary>
        public bool MeansValueSumUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>MeansValueSsum</c> was updated.
        /// </summary>
        public bool MeansValueSsumUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>MeansValueCount</c> was updated.
        /// </summary>
        public bool MeansValueCountUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>MeansWeightSum</c> was updated.
        /// </summary>
        public bool MeansWeightSumUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>MeansWeightSsum</c> was updated.
        /// </summary>
        public bool MeansWeightSsumUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>MeansWeightCount</c> was updated.
        /// </summary>
        public bool MeansWeightCountUpdated { get; init; } = false;
    }
}
