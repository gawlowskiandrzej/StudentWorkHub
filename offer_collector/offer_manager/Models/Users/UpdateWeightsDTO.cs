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

        public bool OrderByOptionUpdated { get; init; } = false;
        public bool MeanValueIdsUpdated { get; init; } = false;
        public bool VectorUpdated { get; init; } = false;
        public bool MeanDistUpdated { get; init; } = false;
        public bool MeansValueSumUpdated { get; init; } = false;
        public bool MeansValueSsumUpdated { get; init; } = false;
        public bool MeansValueCountUpdated { get; init; } = false;
        public bool MeansWeightSumUpdated { get; init; } = false;
        public bool MeansWeightSsumUpdated { get; init; } = false;
        public bool MeansWeightCountUpdated { get; init; } = false;
    }

}
