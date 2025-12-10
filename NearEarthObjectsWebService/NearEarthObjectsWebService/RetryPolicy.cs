namespace NearEarthObjectsWebService;

public static class RetryPolicy
{
    public static async Task ExecuteAsync(Func<Task> operation, int maxRetries = 5, int delayMilliseconds = 1000)
    {
        int attempt = 1;
        bool isSuccess = false;

        while (attempt <= maxRetries && !isSuccess)
        {
            try
            {
                await operation();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                if (attempt == maxRetries)
                {
                    throw;
                }

                Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                attempt++;
                await Task.Delay(delayMilliseconds * attempt);
            }
        }
    }
}
