#nullable enable
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace UnifiedOfferSchema
{
    /// <summary>
    /// Utility helpers for converting Unified Offer Schema (UOS) to/from JSON and
    /// performing non-destructive merges. All methods throw <see cref="UOSException"/>
    /// for validation and serialization errors.
    /// </summary>
    public static class UOSUtils
    {
        /// <summary>
        /// Shared JSON serializer options used across the utilities to ensure
        /// camelCase property names and compact output by default.
        /// </summary>
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        /// <summary>
        /// Builds a <see cref="UOS"/> instance by deserializing a JSON string.
        /// </summary>
        /// <param name="offer">JSON string representing a UOS offer.</param>
        /// <returns>Deserialized <see cref="UOS"/> instance.</returns>
        /// <exception cref="UOSException">
        /// Thrown when <paramref name="offer"/> is null/whitespace,
        /// deserialization yields null, or underlying JSON errors occur.
        /// The original exception is attached as <see cref="Exception.InnerException"/> when available.
        /// </exception>
        public static UOS BuildFromString(string offer)
        {
            if (string.IsNullOrWhiteSpace(offer))
                throw new UOSException("Input offer is empty.");

            try
            {
                var uos = JsonSerializer.Deserialize<UOS>(offer, _options);
                return uos is null ? throw new UOSException("Deserialized offer is null.") : uos;
            }
            catch (UOSException) { throw; }
            catch (Exception ex)
            {
                throw new UOSException("Failed to deserialize offer.", ex);
            }
        }

        /// <summary>
        /// Builds a list of <see cref="UOS"/> instances from a list of JSON strings in parallel,
        /// preserving input order. On per-item failure, the corresponding result slot is set to <c>null</c>
        /// and an error message is optionally appended to <paramref name="errorMessages"/>.
        /// </summary>
        /// <param name="jsonList">List of JSON strings to deserialize (order is preserved by index).</param>
        /// <param name="errorMessages">
        /// Optional sink for errors. When provided, messages like "Index i: reason" are appended
        /// from multiple threads under a lock.
        /// </param>
        /// <returns>List of <see cref="UOS"/> (nullable) in the same order as input; failed items are <c>null</c>.</returns>
        /// <exception cref="UOSException">Thrown when <paramref name="jsonList"/> is <c>null</c>.</exception>
        public static List<UOS?> BuildFromStringList(List<string> jsonList, List<string>? errorMessages = null)
        {
            if (jsonList is null) throw new UOSException("Input offers list is null.");

            var result = new UOS?[jsonList.Count];
            var errorLock = new object();

            Parallel.For(0, jsonList.Count, i =>
            {
                try
                {
                    var json = jsonList[i];
                    if (string.IsNullOrWhiteSpace(json))
                        throw new UOSException("Input JSON is empty.");

                    var uos = JsonSerializer.Deserialize<UOS>(json, _options) ?? throw new UOSException("Deserialized UOS is null.");
                    result[i] = uos;
                }
                catch (Exception ex)
                {
                    result[i] = null;
                    if (errorMessages is not null)
                    {
                        lock (errorLock)
                        {
                            errorMessages.Add($"Index {i}: {ex.Message}");
                        }
                    }
                }
            });

            return [.. result];
        }

        /// <summary>
        /// Serializes a <see cref="UOS"/> instance to a JSON string.
        /// Allows optional pretty printing and excluding specific top-level or nested fields by name.
        /// </summary>
        /// <param name="uos">The UOS instance to serialize.</param>
        /// <param name="pretty">When true, the JSON output is indented.</param>
        /// <param name="excludedFields">
        /// Optional list of property names to remove (case-sensitive, exact match) from the serialized tree.
        /// Unknown names are ignored. Works recursively across the entire object graph.
        /// </param>
        /// <returns>JSON string representation of the UOS instance.</returns>
        /// <exception cref="UOSException">
        /// Thrown when <paramref name="uos"/> is null or serialization fails.
        /// </exception>
        public static string AsString(this UOS uos, bool pretty = false, List<string>? excludedFields = null)
        {
            if (uos is null)
                throw new UOSException("UOS cannot be null.");

            JsonSerializerOptions localOptions = new(_options)
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = false
            };

            var node = JsonSerializer.SerializeToNode(uos, _options) as JsonObject
                ?? throw new UOSException("UOS Serialization failed.");

            if (excludedFields != null)
            {
                HashSet<string> names = new(StringComparer.Ordinal);
                foreach (string field in excludedFields)
                    if (!string.IsNullOrWhiteSpace(field))
                        names.Add(field.Trim());

                if (names.Count > 0)
                    RemovePropertiesByName(node, names);
            }

            localOptions.WriteIndented = pretty;
            return node.ToJsonString(localOptions);
        }

        /// <summary>
        /// Serializes a <see cref="UOS"/> instance to a <see cref="JsonObject"/>.
        /// Allows optional pretty-print configuration and field exclusions.
        /// </summary>
        /// <param name="uos">The UOS instance to serialize.</param>
        /// <param name="pretty">When true, the JSON object is formatted with indentation on conversion to string.</param>
        /// <param name="excludedFields">
        /// Optional list of property names to remove (case-sensitive, exact match) recursively from the object graph.
        /// Unknown names are ignored.
        /// </param>
        /// <returns>
        /// A mutable <see cref="JsonObject"/> representing the UOS instance (with requested fields removed).
        /// </returns>
        /// <exception cref="UOSException">
        /// Thrown when <paramref name="uos"/> is null or serialization fails.
        /// </exception>
        public static JsonObject AsJson(this UOS uos, bool pretty = false, List<string>? excludedFields = null)
        {
            if (uos is null)
                throw new UOSException("UOS cannot be null.");

            JsonSerializerOptions localOptions = new(_options)
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = pretty
            };

            // Serialize directly with desired indentation settings
            var node = JsonSerializer.SerializeToNode(uos, localOptions) as JsonObject
                ?? throw new UOSException("UOS serialization failed.");

            if (excludedFields != null)
            {
                HashSet<string> names = new(StringComparer.Ordinal);
                foreach (string field in excludedFields)
                    if (!string.IsNullOrWhiteSpace(field))
                        names.Add(field.Trim());

                if (names.Count > 0)
                    RemovePropertiesByName(node, names);
            }

            return node;
        }

        /// <summary>
        /// Merges the current <see cref="UOS"/> instance with a JSON "patch" string.
        /// The merge is non-destructive and applies only the values provided in <paramref name="offerToMerge"/>.
        /// Any keys not present on <see cref="UOS"/> are effectively ignored during re-deserialization back to <see cref="UOS"/>.
        /// </summary>
        /// <param name="uos">The target UOS instance to update (extension method 'this').</param>
        /// <param name="offerToMerge">A JSON object string with fields to override/augment.</param>
        /// <returns>The same <see cref="UOS"/> instance after being updated in place.</returns>
        /// <exception cref="UOSException">
        /// Thrown when <paramref name="uos"/> is null, <paramref name="offerToMerge"/> is null/whitespace,
        /// the patch is not a JSON object, parsing fails, or applying the merged JSON back to UOS fails.
        /// Unknown JSON members present in the patch are ignored by the UOS deserializer.
        /// The original exception is attached as <see cref="Exception.InnerException"/> when present.
        /// </exception>
        public static UOS MergeWith(this UOS uos, string offerToMerge)
        {
            if (uos is null)
                throw new UOSException("UOS is null.");

            if (string.IsNullOrWhiteSpace(offerToMerge))
                throw new UOSException("offerToMerge is empty.");

            var baseObj = JsonSerializer.SerializeToNode(uos, _options) as JsonObject
                ?? throw new UOSException("Base UOS Serialization failed.");

            JsonNode patchNode;
            try
            {
                patchNode = JsonNode.Parse(offerToMerge)!;
            }
            catch (Exception ex)
            {
                throw new UOSException("Parsing offerToMerge to JSON, failed.", ex);
            }

            if (patchNode is not JsonObject patchObj)
                throw new UOSException("offerToMerge root must be an object.");

            MergeObjects(baseObj, patchObj);

            try
            {
                var merged = baseObj.Deserialize<UOS>(_options)
                    ?? throw new UOSException("Merged UOS is null.");

                CopyInto(merged, uos);
                return uos;
            }
            catch (UOSException) { throw; }
            catch (Exception ex)
            {
                throw new UOSException("Failed to apply merged JSON to Base UOS.", ex);
            }
        }

        /// <summary>
        /// Parallel 1:1 merge of a base <see cref="UOS"/> list with JSON patch strings.
        /// Preserves input order and updates the original list in place.
        /// </summary>
        /// <param name="baseOffers">Base offers to modify. Offers count must match <paramref name="offersToMerge"/>.</param>
        /// <param name="offersToMerge">Offers used to overwrite base offers keys. Contained offers doesn't may be parts of UOS.</param>
        /// <param name="errorMessages">Optional; on failure logs "Index i: &lt;message&gt;".</param>
        /// <returns>The same <paramref name="baseOffers"/> instance; items that fail to merge remain unchanged.</returns>
        /// <exception cref="UOSException">Thrown if inputs are null/empty or counts differ.</exception>
        /// <remarks>Per-item errors are caught so processing continues for other indices.</remarks>
        public static List<UOS> MergeList(List<UOS?> baseOffers, List<string> offersToMerge, List<string>? errorMessages = null)
        {
            if (baseOffers is null || baseOffers.Count < 1)
                throw new UOSException("baseOffers is empty or null.");

            if (offersToMerge is null || offersToMerge.Count < 1)
                throw new UOSException("baseOffers is empty or null.");

            if (offersToMerge.Count != baseOffers.Count)
                throw new UOSException("baseOffers count is different from offersToMerge count.");

            int count = baseOffers.Count;
            var mergedBuffer = new UOS?[count];
            var errorLock = new object();

            Parallel.For(0, count, i =>
            {
                var baseOffer = baseOffers[i];
                try
                {
                    if (baseOffer is null)
                        throw new UOSException($"Base offer at index {i} is null.");

                    if (i < offersToMerge.Count)
                    {
                        var patchJson = offersToMerge[i];
                        baseOffer.MergeWith(patchJson);
                    }

                    mergedBuffer[i] = baseOffer;
                }
                catch (Exception ex)
                {
                    mergedBuffer[i] = baseOffer;
                    if (errorMessages is not null)
                    {
                        lock (errorLock)
                        {
                            errorMessages.Add($"Index {i}: {ex.Message}");
                        }
                    }
                }
            });

            for (int i = 0; i < count; i++)
                baseOffers[i] = mergedBuffer[i];

            return baseOffers;
        }

        /// <summary>
        /// Recursively merges <paramref name="patchJsonObject"/> into <paramref name="targetJsonObject"/>.
        /// Object properties are merged; arrays and scalar values are replaced wholesale.
        /// </summary>
        /// <param name="targetJsonObject">The base JSON object to be updated.</param>
        /// <param name="patchJsonObject">The JSON object providing overriding values.</param>
        private static void MergeObjects(JsonObject targetJsonObject, JsonObject patchJsonObject)
        {
            foreach (var patchProperty in patchJsonObject)
            {
                var propertyName = patchProperty.Key;
                var patchPropertyValue = patchProperty.Value;

                if (!targetJsonObject.TryGetPropertyValue(propertyName, out var targetPropertyValue) || targetPropertyValue is null)
                {
                    targetJsonObject[propertyName] = patchPropertyValue?.DeepClone();
                    continue;
                }

                if (targetPropertyValue is JsonObject targetChildObject && patchPropertyValue is JsonObject patchChildObject)
                {
                    MergeObjects(targetChildObject, patchChildObject);
                    continue;
                }

                targetJsonObject[propertyName] = patchPropertyValue?.DeepClone();
            }
        }

        /// <summary>
        /// Copies all fields from <paramref name="source"/> into <paramref name="destination"/>,
        /// overwriting existing values. Does not allocate a new instance.
        /// </summary>
        /// <param name="source">The UOS object providing values.</param>
        /// <param name="destination">The UOS object to receive values.</param>
        /// <remarks>
        /// This method performs a field-by-field assignment, ensuring the original reference
        /// (e.g., held by callers) is updated in place.
        /// </remarks>
        private static void CopyInto(UOS source, UOS destination)
        {
            destination.Id = source.Id;
            destination.Source = source.Source;
            destination.Url = source.Url;
            destination.JobTitle = source.JobTitle;
            destination.Company = source.Company;
            destination.Description = source.Description;
            destination.Salary = source.Salary;
            destination.Location = source.Location;
            destination.Category = source.Category;
            destination.Requirements = source.Requirements;
            destination.Employment = source.Employment;
            destination.Dates = source.Dates;
            destination.Benefits = source.Benefits;
            destination.IsUrgent = source.IsUrgent;
            destination.IsForUkrainians = source.IsForUkrainians;
        }

        /// <summary>
        /// Recursively removes properties with names contained in <paramref name="propertyNamesToRemove"/>
        /// from the provided <paramref name="jsonNode"/>. Operates in place.
        /// </summary>
        /// <param name="jsonNode">Root JSON node (object or array) to process.</param>
        /// <param name="propertyNamesToRemove">Set of property names to remove (case-sensitive).</param>
        private static void RemovePropertiesByName(JsonNode jsonNode, HashSet<string> propertyNamesToRemove)
        {
            if (jsonNode is JsonObject jsonObject)
            {
                var keysToRemove = new List<string>();
                foreach (var property in jsonObject)
                    if (propertyNamesToRemove.Contains(property.Key))
                        keysToRemove.Add(property.Key);

                foreach (var propertyName in keysToRemove)
                    jsonObject.Remove(propertyName);

                // Recurse into remaining children
                foreach (var property in jsonObject)
                    RemovePropertiesByName(property.Value!, propertyNamesToRemove);

                return;
            }

            if (jsonNode is JsonArray jsonArray)
            {
                // Recurse into array elements
                foreach (var elementNode in jsonArray)
                    RemovePropertiesByName(elementNode!, propertyNamesToRemove);
            }
        }
    }
}
