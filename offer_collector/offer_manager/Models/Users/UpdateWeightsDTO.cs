namespace offer_manager.Models.Users
{
    public sealed class UpdateWeightsRequest
    {
        public string Jwt { get; init; } = string.Empty;

        public string[]? OrderByOption { get; init; }
        public string[]? MeanValueIds { get; init; }
        public float[]? Vector { get; init; }
        public float[]? MeanDist { get; init; }
        public float[]? MeansValueSum { get; init; }
        public double[]? MeansValueSsum { get; init; }
        public int[]? MeansValueCount { get; init; }
        public float[]? MeansWeightSum { get; init; }
        public double[]? MeansWeightSsum { get; init; }
        public int[]? MeansWeightCount { get; init; }
    }

    public sealed class UpdateWeightsResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;

        public bool OrderByOptionUpdated { get; init; }
        public bool MeanValueIdsUpdated { get; init; }
        public bool VectorUpdated { get; init; }
        public bool MeanDistUpdated { get; init; }
        public bool MeansValueSumUpdated { get; init; }
        public bool MeansValueSsumUpdated { get; init; }
        public bool MeansValueCountUpdated { get; init; }
        public bool MeansWeightSumUpdated { get; init; }
        public bool MeansWeightSsumUpdated { get; init; }
        public bool MeansWeightCountUpdated { get; init; }
    }
}
