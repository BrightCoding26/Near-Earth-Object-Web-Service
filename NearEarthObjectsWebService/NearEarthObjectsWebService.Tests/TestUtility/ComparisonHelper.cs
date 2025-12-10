using System.Text;
using System.Text.Json.Serialization;
using KellermanSoftware.CompareNetObjects;

namespace NearEarthObjectsWebServiceTests.TestUtility;

public static class ComparisonHelper
{
    private const string FailureMessage = "Unexpected comparison difference(s).";

    public static void AssertComparisonResult<TObject>(
        TObject expectedObject,
        TObject actualObject,
        string? objectNamePrefix = null,
        string? additionalInfo = null)
    {
        AssertComparisonResult(GetDefaultComparisonConfig(), expectedObject, actualObject, objectNamePrefix, additionalInfo);
    }

    public static void AssertComparisonResultAndIgnoreObjectTypes<T>(
        T expectedObject,
        T actualObject,
        string? objectNamePrefix = null,
        string? additionalInfo = null)
    {
        var compareConfig = GetDefaultComparisonConfig();
        compareConfig.IgnoreObjectTypes = true;

        AssertComparisonResult(compareConfig, expectedObject, actualObject, objectNamePrefix, additionalInfo);
    }

    private static void AssertComparisonResult<T>(
        ComparisonConfig compareConfig,
        T expectedObject,
        T actualObject,
        string? objectNamePrefix,
        string? additionalInfo)
    {
        additionalInfo = string.IsNullOrWhiteSpace(additionalInfo) ? null : $" {additionalInfo}";

        if (Equals(actualObject, default(T)) && !Equals(expectedObject, default(T)))
        {
            Assert.Fail($"Actual {typeof(T).Name} was null.{additionalInfo}");
        }

        if (Equals(expectedObject, default(T)) && !Equals(actualObject, default(T)))
        {
            Assert.Fail($"Expected {typeof(T).Name} was null.{additionalInfo}");
        }

        var compareLogic = new CompareLogic(compareConfig);
        var comparisonResult = compareLogic.Compare(expectedObject, actualObject);

        if (comparisonResult.AreEqual)
        {
            return;
        }

        var message = $"{FailureMessage} Entity: {objectNamePrefix}{typeof(T).Name}.";
        message += additionalInfo;
        var differencesStringBuilder = new StringBuilder(message);
        differencesStringBuilder.AppendLine();
        differencesStringBuilder.AppendLine($"Total Differences: {comparisonResult.Differences.Count}");

        foreach (var difference in comparisonResult.Differences)
        {
            differencesStringBuilder.AppendLine(difference.ToString());
        }

        Assert.Fail(differencesStringBuilder.ToString());
    }

    private static ComparisonConfig GetDefaultComparisonConfig()
    {
        return new ComparisonConfig
        {
            AttributesToIgnore = new List<Type> { typeof(JsonIgnoreAttribute) },
            MaxMillisecondsDateDifference = 5000,
            MaxDifferences = 10
        };
    }
}
